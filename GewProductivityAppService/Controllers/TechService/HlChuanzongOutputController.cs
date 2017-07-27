using System;
using System.Linq;
using System.Web.Http;
using System.Web.WebPages;
using GewProductivityAppService.DAL.MIS01.PDMDB;
using GewProductivityAppService.Models.TechService.HlChuanzongOutput;


namespace GewProductivityAppService.Controllers.TechService
{
    /// <summary>
    /// HL穿综产量APP
    /// </summary>
    [RoutePrefix("api/Tech")]
    public class HlChuanzongOutputController : ApiController
    {
        private PdmDbContext PdmDb = new PdmDbContext();

        /// <summary>
        /// 根据HL_NO，返回系统分数
        /// </summary>
        /// <param name="hlNo"></param>
        /// <returns></returns>
        [Route("hlBasicInfoes/GetScores")]
        [HttpGet]
        public IHttpActionResult GetSysScore([FromUri]string hlNo)
        {
            try
            {
                // 系统分=基础分+飞穿分
                var sqlText = @"SELECT ISNULL(C.InputScore,0)+ISNULL(b.HealdingScore,0) AS SysCalScore  From Pattern2HL A with(nolock) 
Inner Join hlBasicInfo B with(nolock) On A.strHLNo=B.HL_NO 
Inner Join hlUnHealdingScore C with(nolock) On A.strLBNo=C.LB_No 
                                          And B.Suggestion_Reed = C.Suggestion_Reed 
                                          And B.Drawing = C.Drawing 
Where A.strHLNo=@p0";
                // 系统分计算
                decimal sysCalScore = PdmDb.Database.SqlQuery<decimal>(sqlText, hlNo).FirstOrDefault();
                // 余下分数
                decimal remainScore = sysCalScore;
                // 计算该HL_NO已完成的分数和
                var sum = PdmDb.hlOutputs.Where(o => o.HL_No.Equals(hlNo, StringComparison.CurrentCultureIgnoreCase)).Sum(o => o.Dync_Score);
                if (sum != null)
                {
                    remainScore = (sysCalScore - sum.Value) <= 0 ? 0 : (sysCalScore - sum.Value);
                }
                return Json(new
                {
                    SysScore = sysCalScore,
                    RemainScore = remainScore
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 输入穿综产量
        /// </summary>
        /// <param name="hlOutput">绑定模型</param>
        /// <returns></returns>
        [Route("hlOutput/InputHlProduction")]
        [HttpPost]
        public IHttpActionResult InputHlProduction([FromBody]HlOutputBindModel hlOutput)
        {
            if (hlOutput == null)
            {
                return BadRequest();
            }
            var output = new hlOutput()
            {
                HL_No = hlOutput.HL_No.ToUpper(),
                Sys_Score = hlOutput.Sys_Score.AsDecimal(),
                Dync_Score = hlOutput.Dync_Score.AsDecimal(),
                Class = hlOutput.Class.ToUpper(),
                Post = "穿综",
                Name = hlOutput.Name,
                //IsMore = hlOutput.IsMore.AsInt(),
                InputTime = DateTime.Now
            };
            PdmDb.hlOutputs.Add(output);
            PdmDb.SaveChanges();
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            PdmDb.Dispose();
            base.Dispose(disposing);
        }
    }
}
