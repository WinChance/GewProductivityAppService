using System.Web.Http;
using GewProductivityAppService.DAL.MIS01.WVMDB;
using GewProductivityAppService.Models.Pr.MachineRotateRate;

namespace GewProductivityAppService.Service.Pr
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
            if (bm == null || bm.End.Equals(0))
            {
                return BadRequest();
            }

            var model = new PrdMachineRotateRate()
            {
                Department = bm.Department,
                Process = bm.Process,
                WorkerClass = bm.WorkerClass,
                Machine = bm.Machine,
                BeginTime = bm.Begin,
                EndTime = bm.End,
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
