using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.WebPages;
using GewProductivityAppService.Models;
using GewProductivityAppService.Models.Common;
using GewProductivityAppService.Models.TechService;
using GewProductivityAppService.Models.TechService.HlChuanzongOutput;
using Microsoft.Ajax.Utilities;
using PDMDB;
using YDMDB;

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
                var firstOrDefault = PdmDb.hlBasicInfoes.Where(i=>i.HL_No.Equals(hlNo,StringComparison.CurrentCultureIgnoreCase)).Select(i=>i.grade_cent).FirstOrDefault();
                if (firstOrDefault != null)
                {
                    decimal gradeCent=(decimal) firstOrDefault;
                    decimal remainScore=gradeCent;
                    var sum = PdmDb.hlOutputs.Where(o => o.HL_No.Equals(hlNo,StringComparison.CurrentCultureIgnoreCase)).Sum(o => o.Dync_Score);
                    if (sum != null)
                    {
                        remainScore = gradeCent-sum.Value;

                    }
                  
                    return Json(new
                    {
                        SysScore=gradeCent,
                        RemainScore=remainScore
                    });
                }
                else
                {
                    return NotFound();
                }
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
            if (hlOutput==null)
            {
                return BadRequest();
            }
            var output=new hlOutput()
            {
                HL_No = hlOutput.HL_No,
                Sys_Score = hlOutput.Sys_Score.AsDecimal(),
                Dync_Score = hlOutput.Dync_Score.AsDecimal(),
                Class = hlOutput.Class,
                Post = "穿综",
                Name = hlOutput.Name,
                IsMore = hlOutput.IsMore.AsInt()

            };
            PdmDb.hlOutputs.Add(output);
            PdmDb.SaveChanges();
            return Ok();
        }

    }
}
