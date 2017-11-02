using System;
using System.Linq;
using System.Web.Http;
using System.Web.WebPages;
using GewProductivityAppService.DAL.MIS01.PDMDB;
using GewProductivityAppService.DAL.MIS01.WVMDB;
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
                string sqlText;
                if (!hlNo.ToUpper().Contains("HL"))
                {
                    int _nAutoID = Convert.ToInt32(hlNo);
                    hlNo = PdmDb.Pattern2HL.Where(p => p.nAutoID.Equals(_nAutoID)).Select(p => p.strHLNo).FirstOrDefault();
                }
                // 系统分=基础分+飞穿分
                sqlText = @"
DECLARE @HL_NO VARCHAR(30)=@p0
SELECT SUM(c.Score) AS 'SysScore' FROM
(Select ISNULL(C.InputScore,0) AS 'Score' From Pattern2HL A with(nolock) 
                   Inner Join hlBasicInfo B with(nolock) On A.strHLNo=B.HL_NO 
                   Inner Join hlUnHealdingScore C with(nolock) On B.Drawing = C.Drawing 
                   Where A.strHLNo=@HL_NO 
				   AND C.TotalEnds=(SELECT a.TotalEnds FROM dbo.hlBasicInfo  AS a  with(nolock)WHERE a.HL_No=@HL_NO)
UNION ALL
SELECT    ISNULL(b.HealdingScore,0)
          FROM      hlBasicInfo AS b with(nolock)
          WHERE     b.HL_No = @HL_NO)AS c ;";
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
            string hlNo = hlOutput.HL_No.ToUpper();
            // 当提交的HL_NO是条码扫描值，转换为HL_NO
            if (!hlNo.Contains("HL"))
            {
                int _nAutoID = Convert.ToInt32(hlNo);
                hlNo = PdmDb.Pattern2HL.Where(p => p.nAutoID.Equals(_nAutoID)).Select(p => p.strHLNo).FirstOrDefault();
            }
            var output = new hlOutput()
            {
                HL_No = hlNo,
                Sys_Score = hlOutput.Sys_Score.AsDecimal(),
                Dync_Score = hlOutput.Dync_Score.AsDecimal(),
                Class = hlOutput.Class.ToUpper(),
                Post = "穿综",
                Name = hlOutput.Name,
                Remark = "输入人：" + hlOutput.Remark,// 记录APP登录人
                InputTime = DateTime.Now
            };
            PdmDb.hlOutputs.Add(output);
            PdmDb.SaveChanges();

            return Ok();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                PdmDb.Dispose();

            }
            base.Dispose(disposing);
        }
    }
}
