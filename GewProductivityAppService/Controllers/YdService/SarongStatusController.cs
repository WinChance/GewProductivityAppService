using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using System.Web.WebPages;
using GewProductivityAppService.Models.YdService.SarongStatus;
using Microsoft.Ajax.Utilities;
using YDMDB;
using Z.EntityFramework.Plus;

namespace GewProductivityAppService.Controllers.YdService
{
    /// <summary>
    /// 染纱染台笼子状态跟踪
    /// </summary>
    [RoutePrefix("api/Yd")]
    public class SarongStatusController : ApiController
    {
        private YdmDbContext YdmDb = new YdmDbContext();

        /// <summary>
        /// 查询纱笼状态
        /// </summary>
        /// <param name="batchType">缸型</param>
        /// <returns></returns>
        [Route("GetSarongStatusByBatchType")]
        [HttpGet]
        public IHttpActionResult GetSarongStatusByJarType([FromUri] string batchType)
        {
            if (batchType != null)
            {
                SqlParameter[] sqlParameter = new SqlParameter[1];
                sqlParameter[0] = new SqlParameter("@jarType", batchType);
                var result = YdmDb.Database.SqlQuery<SarongStatusViewModel>("EXEC dbo.usp_prdAppGetSarongStatusByJarType @jarType", sqlParameter).ToList();
                return Json(result);
            }
            return BadRequest();
        }

        /// <summary>
        /// 装纱产量录入
        /// </summary>
        /// <param name="rtProduction">产量录入Model</param>
        /// <returns></returns>
        [Route("InputRtProduction")]
        [HttpPost]
        public IHttpActionResult InputRtProduction([FromBody]RtProductionBindModel rtProduction)
        {
            // 将null替换为""
            foreach (var _rtProductionin in rtProduction.GetType().GetProperties())
            {
                if (_rtProductionin.GetValue(rtProduction) == null)
                {
                    _rtProductionin.SetValue(rtProduction, "");
                }
            }
            List<SqlParameter> paramArray = new List<SqlParameter>();
            paramArray.Add(new SqlParameter("@BatchNo", rtProduction.BatchNo));
            paramArray.Add(new SqlParameter("@SarongNO", rtProduction.SarongNO));
            paramArray.Add(new SqlParameter("@InputClass", rtProduction.InputClass));
            paramArray.Add(new SqlParameter("@Work_ID", rtProduction.WorkID));
            paramArray.Add(new SqlParameter("@Inputer_ID", rtProduction.InputerID));
            paramArray.Add(new SqlParameter("@YieldNum", rtProduction.YieldNum.AsDecimal()));
            paramArray.Add(new SqlParameter("@Department", rtProduction.Department));
            SqlParameter param = new SqlParameter("@RtnStatus", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            paramArray.Add(param);
            try
            {
                YdmDb.Database.ExecuteSqlCommand(@"EXEC dbo.usp_prdAppInputRtProduction @BatchNo ,@SarongNO ,@InputClass ,@Work_ID ,@Inputer_ID,@YieldNum,@Department,@RtnStatus out",
                    paramArray.ToArray());
            }
            catch (Exception)
            {

                throw;
            }
            int result = (int)paramArray[7].Value;
            if (result > 0)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// 传入缸号，改变纱笼使用状态，拆纱笼
        /// </summary>
        /// <param name="batchNo">缸号</param>
        /// <returns></returns>
        [Route("UnLoadSarong")]
        [HttpPut]
        public IHttpActionResult UnLoadSarong([FromUri]string batchNo)
        {
            string sarongNo = YdmDb.rtProductions.Where(p => p.Batch_NO.Equals(batchNo, StringComparison.CurrentCultureIgnoreCase) && p.Type.Equals("装笼")).Select(p => p.Sarong_No).FirstOrDefault();
            if (sarongNo.IsNullOrWhiteSpace())
            {
                return BadRequest();
            }
            YdmDb.prdAppSarongs.Where(s => s.SarongNo.Equals(sarongNo, StringComparison.CurrentCultureIgnoreCase)).Update(s => new prdAppSarong { IsUsed = "否" });
            YdmDb.SaveChanges();
            return Ok();
        }
    }
}
