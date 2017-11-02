using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GewProductivityAppService.DAL.MIS01.WVMDB;
using GewProductivityAppService.Models.PrService.MachineRotateRate;

namespace GewProductivityAppService.Controllers.PrService
{
    /// <summary>
    /// 开动率
    /// </summary>
    [RoutePrefix("api/Pr")]
    public class MachineRotateRateController : ApiController
    {

        private WvmDbContext WvmDb=new WvmDbContext();

        /// <summary>
        /// 插数MachineRotateRate
        /// </summary>
        /// <param name="bm"></param>
        /// <returns></returns>
        [Route("MachineRotateRate/AddMachineRotateRate")]
        [HttpPost]
        public IHttpActionResult AddMachineRotateRate([FromBody] MachineRotateRateBindModel bm)
        {
            if (bm == null)
            {
                return BadRequest();
            }

            var model = new PrdMachineRotateRate()
            {
                Department = bm.Department,
                Process = bm.Process,
                WorkerClass = bm.WorkerClass,
                Machine = bm.Machine,
                Begin = bm.Begin,
                End = bm.End,
                RotateDuration = bm.End - bm.Begin,
                Operator = bm.Operator,
                YieldDate = bm.YieldDate

            };
            WvmDb.PrdMachineRotateRates.Add(model);
            WvmDb.SaveChanges();
            
            return Ok();
            
        }
        protected override void Dispose(bool disposing)
        {
            WvmDb.Dispose();
            base.Dispose(disposing);
        }
    }
}
