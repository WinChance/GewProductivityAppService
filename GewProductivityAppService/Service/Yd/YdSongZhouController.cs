﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.WebPages;
using GewProductivityAppService.DAL.MIS01.YDMDB;
using GewProductivityAppService.Service.SignalR;
using GewProductivityAppService.Utils;
using log4net;
using Z.EntityFramework.Plus;

namespace GewProductivityAppService.Service.Yd
{
    /// <summary>
    /// 染纱与准备收送轴APP
    /// </summary>
    [RoutePrefix("api/YdSongZhou")]
    public class YdSongZhouController : ApiController
    {

        private YdmDbContext ydmDb = new YdmDbContext();
        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public class DaiRanZhouInfoBm
        {
            public string machinetype { get; set; }
        }

        /// <summary>
        /// 取染台待染轴信息
        /// </summary>
        /// <param name="machinetype">缸型</param>
        /// <returns></returns>
        [Route("GetDaiRanZhouInfo"), HttpGet]
        public IHttpActionResult GetDaiRanZhouInfo([FromUri] DaiRanZhouInfoBm bm)
        {
            try
            {
                foreach (var _bm in bm.GetType().GetProperties())
                {
                    if (_bm.GetValue(bm) == null)
                    {
                        _bm.SetValue(bm, "");
                    }
                }
                var rtn = DynamicSqlQueryClass.Instance.DynamicSqlQuery(ydmDb.Database, "EXEC usp_prdGetSongZhouinfo @param", new SqlParameter("@param", bm.machinetype));

                return Json(rtn);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return BadRequest();
            }
        }


        /// <summary>
        /// 绑定类型
        /// </summary>
        public class SendTaskBm
        {
            public string machinetype { get; set; }
            public string batchno { get; set; }
            public string nums { get; set; }
            public string plantime { get; set; }
            public string ydoperator { get; set; }
            public string predictInBatchTime { get; set; }

        }
        /// <summary>
        /// 染纱向准备发出拉轴任务
        /// </summary>
        /// <param name="machinetype"></param>
        /// <param name="batchno"></param>
        /// <param name="nums"></param>
        /// <param name="plantime"></param>
        /// <param name="ydoperator"></param>
        /// <returns></returns>
        [Route("SendTaskByYd"), HttpPost]
        public IHttpActionResult SendTaskByYd([FromBody]SendTaskBm bm)
        {

            try
            {
                DateTime predictInBatchTime;
                int tianIndex = bm.predictInBatchTime.IndexOf("天", StringComparison.Ordinal);
                int dianIndex = bm.predictInBatchTime.IndexOf("点", StringComparison.Ordinal);
                int fenIndex = bm.predictInBatchTime.IndexOf("分", StringComparison.Ordinal);
                if (bm.predictInBatchTime.Contains("今天"))
                {
                    predictInBatchTime = (DateTime.Today.ToShortDateString() + " " + bm.predictInBatchTime.Substring(tianIndex + 1, dianIndex - tianIndex - 1) + ":" + bm.predictInBatchTime.Substring(dianIndex + 1, fenIndex - dianIndex - 1)).AsDateTime();
                }
                else if (bm.predictInBatchTime.Contains("明天"))
                {
                    predictInBatchTime = (DateTime.Today.AddDays(1).ToShortDateString() + " " + bm.predictInBatchTime.Substring(tianIndex + 1, dianIndex - tianIndex - 1) + ":" + bm.predictInBatchTime.Substring(dianIndex + 1, fenIndex - dianIndex - 1)).AsDateTime();
                }
                else if (bm.predictInBatchTime.Contains("后天"))
                {
                    predictInBatchTime = (DateTime.Today.AddDays(2).ToShortDateString() + " " + bm.predictInBatchTime.Substring(tianIndex + 1, dianIndex - tianIndex - 1) + ":" + bm.predictInBatchTime.Substring(dianIndex + 1, fenIndex - dianIndex - 1)).AsDateTime();
                }
                else
                {
                    return BadRequest();
                }

                ydmDb.prdSongZhouinfoes.Add(new prdSongZhouInfo()
                {
                    machinetype = bm.machinetype,
                    batchno = bm.batchno,
                    nums = bm.nums.AsInt(),
                    plantime = bm.plantime.AsDateTime(),
                    ydoperator = bm.ydoperator,
                    ydoperattime = DateTime.Now,
                    PredictInBatchTime = predictInBatchTime
                });
                ydmDb.ydBatchTraces
                    .Where(t => t.Batch_NO.Equals(bm.batchno, StringComparison.CurrentCultureIgnoreCase))
                    .Update(z => new ydBatchTrace()
                    {
                        IsSongZhou = "Y"
                    });


                ydmDb.SaveChanges();
                PushHub.Instance.PushYdDaiSongZhouInfo();
                return Ok();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return BadRequest();
                throw;
            }
        }

        /// <summary>
        /// 准备工人查询待送的轴
        /// </summary>
        /// <returns></returns>
        [Route("GetDaiSongZhouInfo"), HttpGet]
        public IHttpActionResult GetDaiSongZhouInfo()
        {
            var rtn = ydmDb.prdSongZhouinfoes.Where(z => z.properattime == null).OrderBy(z => z.PredictInBatchTime).ToList();
            return Json(rtn);
        }

        public class ReceiveTaskBm
        {
            public string batchno { get; set; }
            public string properator { get; set; }
            public string location { get; set; }
        }
        /// <summary>
        /// 准备拉轴工确认拉轴
        /// </summary>
        /// <param name="batchno"></param>
        /// <param name="properator"></param>
        /// <returns></returns>
        [Route("ReceiveTaskByPr"), HttpPost]
        public IHttpActionResult ReceiveTaskByPr([FromBody]ReceiveTaskBm bm)
        {
            try
            {
                ydmDb.prdSongZhouinfoes
                    .Where(z => z.batchno.Equals(bm.batchno, StringComparison.CurrentCultureIgnoreCase))
                    .Update(z => new prdSongZhouInfo()
                    {
                        properator = bm.properator,
                        properattime = DateTime.Now,
                        Location = bm.location
                    });
                ydmDb.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return BadRequest();
            }
        }

        public class ZhuangzhouYieldInputBm
        {
            /// <summary>
            /// 纱笼号
            /// </summary>
            public string sarongId { get; set; }
            /// <summary>
            /// 缸号
            /// </summary>
            public string batchNo { get; set; }
            /// <summary>
            /// 卡号
            /// </summary>
            public string cardNo { get; set; }

        }

        /// <summary>
        /// 装轴产量录入
        /// </summary>
        /// <param name="bm"></param>
        /// <returns></returns>
        [Route("ZhuangzhouYieldInput"), HttpPost]
        public IHttpActionResult ZhuangzhouYieldInput([FromBody] ZhuangzhouYieldInputBm bm)
        {
            List<SqlParameter> paramArray = new List<SqlParameter>();
            paramArray.Add(new SqlParameter("@SarongId", bm.sarongId));
            paramArray.Add(new SqlParameter("@BatchNo", bm.batchNo));
            paramArray.Add(new SqlParameter("@CardNo", bm.cardNo));
            try
            {
                ydmDb.Database.ExecuteSqlCommand(@"EXEC [dbo].[usp_prdZhuangzhouYieldInput] @SarongId,@BatchNo,@CardNo",
                    paramArray.ToArray());
                return Ok();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return BadRequest();
            }
        }

        public class LazhouYieldInputBm
        {
            /// <summary>
            /// 缸号
            /// </summary>
            public string batchNo { get; set; }
            /// <summary>
            /// 卡号
            /// </summary>
            public string cardNo { get; set; }
            /// <summary>
            /// 地位号
            /// </summary>
            public string location { get; set; }

        }

        /// <summary>
        /// 拉轴产量录入
        /// </summary>
        /// <param name="bm"></param>
        /// <returns></returns>
        [Route("LazhouYieldInput"), HttpPost]
        public IHttpActionResult LazhouYieldInput([FromBody] LazhouYieldInputBm bm)
        {
            List<SqlParameter> paramArray = new List<SqlParameter>();
            paramArray.Add(new SqlParameter("@BatchNo", bm.batchNo));
            paramArray.Add(new SqlParameter("@CardNo", bm.cardNo));
            paramArray.Add(new SqlParameter("@Location", bm.location));
            try
            {
                ydmDb.Database.ExecuteSqlCommand(@"EXEC [dbo].[usp_prdLazhouYieldInput] @BatchNo,@CardNo,@Location",
                    paramArray.ToArray());
                return Ok();
            }
            catch (SqlException ex)
            {
                log.Error(ex.Message);
                //在webapi中要想抛出异常必须这样抛出，否则之抛出一个默认500的异常

                var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(
                        ex.Message.Replace(@"EXECUTE 后的事务计数指示 BEGIN 和 COMMIT 语句的数目不匹配。上一计数 = 1，当前计数 = 0。", "")),
                    ReasonPhrase = "error"
                };
                throw new HttpResponseException(resp);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// 回收资源
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                ydmDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
