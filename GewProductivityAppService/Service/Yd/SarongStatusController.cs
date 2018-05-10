using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using System.Web.WebPages;
using GewProductivityAppService.DAL.MIS01.YDMDB;
using GewProductivityAppService.Models.Yd.SarongStatus;
using Microsoft.Ajax.Utilities;
using Z.EntityFramework.Plus;

namespace GewProductivityAppService.Service.Yd
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
        /// <param name="sarongType">笼的类型：纱笼/轴笼</param>
        /// <returns></returns>
        [Route("GetSarongStatusByBatchType")]
        [HttpGet]
        public IHttpActionResult GetSarongStatusByBatchType([FromUri] string batchType, string sarongType)
        {
            if (batchType != null && sarongType!=null)
            {
                SqlParameter[] sqlParameter = new SqlParameter[2];
                sqlParameter[0] = new SqlParameter("@batchType", batchType);
                sqlParameter[1] = new SqlParameter("@sarongType", sarongType);

                var result = YdmDb.Database.SqlQuery<SarongStatusViewModel>("EXEC dbo.usp_prdAppGetSarongStatusByBatchType @batchType,@sarongType", sqlParameter).ToList();
                if (result.Count>0)
                {
                    return Json(result);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
        /*
         * map.put("Department",inputfactory.getText().toString());
map.put("yieldtype",inputyiedtype.getText().toString()); 
         */
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
            SqlParameter rtnStatus = new SqlParameter("@RtnStatus", SqlDbType.Int);
            rtnStatus.Direction = ParameterDirection.Output;
            paramArray.Add(rtnStatus);
            paramArray.Add(new SqlParameter("@Type", rtProduction.Type));
            paramArray.Add(new SqlParameter("@BatchNo", rtProduction.BatchNo));
            paramArray.Add(new SqlParameter("@SarongNO", rtProduction.SarongNO));
            paramArray.Add(new SqlParameter("@InputClass", rtProduction.InputClass));
            paramArray.Add(new SqlParameter("@Work_ID", rtProduction.WorkID));
            paramArray.Add(new SqlParameter("@Inputer_ID", rtProduction.InputerID));
            paramArray.Add(new SqlParameter("@YieldNum", rtProduction.YieldNum.AsDecimal()));
            paramArray.Add(new SqlParameter("@Department", rtProduction.Department));
            paramArray.Add(new SqlParameter("@PayType", rtProduction.PayType));
            paramArray.Add(new SqlParameter("@Way", rtProduction.Way));
            try
            {
                YdmDb.Database.ExecuteSqlCommand(@"EXEC dbo.usp_prdAppInputRtProduction @Type,@BatchNo ,@SarongNO ,@InputClass ,@Work_ID ,@Inputer_ID,@YieldNum,@Department,@PayType,@Way,@RtnStatus out",
                    paramArray.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
            int result = (int)paramArray[0].Value;
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
            string sarongNo = YdmDb.rtProductions
                .Where(p => p.Batch_NO.Equals(batchNo, StringComparison.CurrentCultureIgnoreCase) && p.Type.Equals("装笼"))
                .OrderByDescending(p=>p.Input_Time)
                .Select(p => p.Sarong_No)
                .FirstOrDefault();
            if (sarongNo.IsNullOrWhiteSpace())
            {
                return BadRequest();
            }
            YdmDb.prdAppSarongs.Where(s => s.SarongNo.Equals(sarongNo, StringComparison.CurrentCultureIgnoreCase)).Update(s => new prdAppSarong { IsUsed = "否" });
            YdmDb.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// 回收
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            
            YdmDb.Dispose();
            base.Dispose(disposing);
        }
    }
}
