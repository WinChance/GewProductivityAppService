using System.Web.Http;
using GewProductivityAppService.DAL.MIS01.WVMDB;
using GewProductivityAppService.Models.Pr.AbandonYarn;

namespace GewProductivityAppService.Service.Pr
{
    /// <summary>
    /// PR PY废纱项目
    /// </summary>
    [RoutePrefix("api/Pr")]
    public class AbandonYarnController : ApiController
    {
        private WvmDbContext WvmDb=new WvmDbContext();

        /// <summary>
        /// 新增废纱信息
        /// </summary>
        /// <param name="abandonYarn">绑定模型</param>
        /// <returns></returns>
        [Route("AbandonYarn/AddAbandonYarn")]
        [HttpPost]
        public IHttpActionResult AddAbandonYarn([FromBody]AbandonYarnBindModel abandonYarn)
        {
            if (abandonYarn == null)
            {
                return BadRequest();
            }

            var model = new PrdAbandonYarn()
            {
               Department = abandonYarn.Department,
               Process = abandonYarn.Process,
               WorkerClass = abandonYarn.WorkerClass,
               Type = abandonYarn.Type,
               Weight = abandonYarn.Weight,
               Operator = abandonYarn.Operator,
               YieldDate = abandonYarn.YieldDate 
            };
            WvmDb.PrdAbandonYarns.Add(model);
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
