using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.WebPages;
using GewProductivityAppService.DAL.MIS01.FNMDB;
using GewProductivityAppService.Models.Fn.Wuliu;
using GewProductivityAppService.Utils;
using log4net;

namespace GewProductivityAppService.Service.Fn
{
    /// <summary>
    /// 后整物流APP
    /// </summary>
    [RoutePrefix("api/FnWuliu")]
    public class FnWuliuController : ApiController
    {
        private FnmDbContext fnmDb = new FnmDbContext();
        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 取待送布信息
        /// </summary>
        /// <param name="sCode"></param>
        /// <param name="currentDepartment"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        [Route("GetSendInfo")]
        [HttpGet]
        public IHttpActionResult GetSendInfo([FromUri] string sCode, string currentDepartment, string iType)
        {
            if (sCode == null && currentDepartment == null && iType == null)
                return BadRequest();

            List<SqlParameter> paramArray = new List<SqlParameter>();
            paramArray.Add(new SqlParameter("@sCode", sCode));
            paramArray.Add(new SqlParameter("@Current_Department", currentDepartment));
            paramArray.Add(new SqlParameter("@iType", iType.AsInt()));

            var command = fnmDb.Database.Connection.CreateCommand();
            command.CommandText = "dbo.usp_GetSendInfo";
            command.Parameters.AddRange(paramArray.ToArray());
            command.CommandType = CommandType.StoredProcedure;

            try
            {
                fnmDb.Database.Connection.Open();
                var reader = command.ExecuteReader();
                List<UspGetSendInfo> _listOfGetSendInfos = ((IObjectContextAdapter)fnmDb).ObjectContext
                    .Translate<UspGetSendInfo>(reader).ToList();

                return Json(_listOfGetSendInfos, JsonFormatSettings.Instance.GetSettings());
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return BadRequest();
                throw;
            }
            finally
            {
                fnmDb.Database.Connection.Close();
            }

        }

        /// <summary>
        /// 发送邮件通知相关人员
        /// </summary>
        /// <param name="fnCard"></param>
        /// <returns></returns>
        [Route("SendEmailCannotFindFabric")]
        [HttpGet]
        public IHttpActionResult SendEmailCannotFindFabric([FromUri] string fnCard)
        {
            List<SqlParameter> paramArray = new List<SqlParameter>();
            paramArray.Add(new SqlParameter("@sFNCard", fnCard));
            try
            {
                fnmDb.Database.ExecuteSqlCommand(@"exec dbo.usp_prdSendEmailCannotFindFabric @sFNCard",
                    paramArray.ToArray());
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
                fnmDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
