using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using GewProductivityAppService.DAL.MIS01.YDMDB;
using GewProductivityAppService.Utils;
using log4net;
using Z.EntityFramework.Plus;

namespace GewProductivityAppService.Service.Yd
{
    /// <summary>
    /// 染纱与准备收送轴APP
    /// </summary>
    [RoutePrefix("api/Yd")]
    public class SongZhouController : ApiController
    {
        private YdmDbContext ydmDb = new YdmDbContext();
        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 取染台待染轴信息
        /// </summary>
        /// <param name="machinetype">缸型</param>
        /// <returns></returns>
        [Route("GetDaiRanZhouInfo"),HttpGet]
        public IHttpActionResult GetDaiRanZhouInfo(string machinetype="")
        {
            try
            {
                //SqlParameter param=new SqlParameter("@param",machinetype);
                //List<SqlParameter> paramArray = new List<SqlParameter>();
                //paramArray.Add(new SqlParameter("@param", machinetype));
                var rtn = DynamicSqlQueryClass.Instance.DynamicSqlQuery(ydmDb.Database, "EXEC usp_getSongZhouinfo @param", new SqlParameter("@param", machinetype));

                return Json(rtn);
            }
            catch (Exception e)
            {
                return NotFound();
            }
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
        [Route("SongZhouByYd"), HttpPost]
        public IHttpActionResult SongZhouByYd([FromUri] string machinetype, string batchno, string nums,
            string plantime, string ydoperator)
        {
            try
            {
                ydmDb.SongZhouinfoes.Add(new SongZhouinfo()
                {
                    machinetype = machinetype,
                    batchno = batchno,
                    nums = nums.AsInt(),
                    plantime = plantime.AsDateTime(),
                    ydoperator = ydoperator,
                    ydoperattime = DateTime.Now
                });
                ydmDb.ydBatchTraces.Where(t => t.Batch_NO.Equals(batchno, StringComparison.CurrentCultureIgnoreCase))
                    .Update(z => new ydBatchTrace()
                    {
                        IsSongZhou = "Y"
                    });
                ydmDb.SaveChanges();
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
        [Route("GetDaiSongZhou"), HttpGet]
        public IHttpActionResult GetDaiSongZhou()
        {
            var rtn=ydmDb.SongZhouinfoes.Where(z => z.properattime != null).Select(z=>new {z.machinetype,z.batchno,z.nums,z.plantime}).OrderBy(z=>z.plantime).ToList();
            return Json(rtn);
        }

        /// <summary>
        /// 准备拉轴工确认拉轴
        /// </summary>
        /// <param name="batchno"></param>
        /// <param name="properator"></param>
        /// <returns></returns>
        [Route("ShouZhouByPr"), HttpPost]
        public IHttpActionResult ShouZhouByPr([FromUri]  string batchno, string properator)
        {
            try
            {
                ydmDb.SongZhouinfoes.Where(z => z.batchno.Equals(batchno, StringComparison.CurrentCultureIgnoreCase))
                    .Update(z=>new SongZhouinfo()
                    {
                        properator = z.properator,
                        properattime = DateTime.Now
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
