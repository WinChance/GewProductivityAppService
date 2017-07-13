using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using GewProductivityAppService.ViewModels;
using GewProductivityAppService.ViewModels.TechService;
using Microsoft.Ajax.Utilities;
using PDMDB;
using YDMDB;

namespace GewProductivityAppService.Controllers.TechService
{
    /// <summary>
    /// HL穿综产量APP
    /// </summary>
    public class HlChuanzongOutputController : ApiController
    {
        private PdmDbContext PdmDb = new PdmDbContext();
       
        /// <summary>
        /// 根据HL_NO，返回系统分数
        /// </summary>
        /// <param name="hlNo"></param>
        /// <returns></returns>
        [Route("GetGfNoByMachineName")]
        [HttpGet]
        public HttpResponseMessage GetSysScore([FromUri]string hlNo)
        {
            try
            {
                string gradeCent=PdmDb.hlBasicInfoes.Where(i=>i.HL_No.Equals(hlNo,StringComparison.CurrentCultureIgnoreCase)).Select(i=>i.grade_cent).FirstOrDefault().ToString();
                if (!gradeCent.IsNullOrWhiteSpace())
                {
                    HttpResponseMessage responseMessage =
                        new HttpResponseMessage
                        {
                            Content =
                                new StringContent(gradeCent, Encoding.GetEncoding("UTF-8"),
                                    "text/plain")
                        };
                    return responseMessage;

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hlNo"></param>
        /// <returns></returns>
        [Route("GetNameByClass")]
        [HttpGet]
        public IHttpActionResult GetNameByClass([FromUri] string staffClass)
        {
            List<DropDownListViewModel> rtnList =
                PdmDb.hlProductionStaffs.Where(
                        s => s.Class.Equals(staffClass, StringComparison.CurrentCultureIgnoreCase))
                    .Select(s => new DropDownListViewModel
                    {
                        text1 = s.Name
                    }).ToList();
            return Json(rtnList);
        }
        /// <summary>
        /// 输入穿综产量
        /// </summary>
        /// <param name="hlOutput">绑定模型</param>
        /// <returns></returns>
        [Route("InputHlProduction")]
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
                Sys_Score = hlOutput.Sys_Score,
                Dync_Score = hlOutput.Dync_Score,
                Class = hlOutput.Class,
                Name = hlOutput.Name

            };
            PdmDb.hlOutputs.Add(output);
            PdmDb.SaveChanges();
            return Ok();
        }

    }
}
