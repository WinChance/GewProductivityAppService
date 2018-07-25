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
        ///// 保存送布信息
        ///// </summary>
        ///// <param name="fnCardList"></param>
        ///// <param name="destination"></param>
        ///// <param name="currentDepartment"></param>
        ///// <param name="operators"></param>
        ///// <returns></returns>
        //[Route("FnSaveSendInfo")]
        //[HttpGet]
        //public HttpResponseMessage FnSaveSendInfo([FromUri]string fnCardList, string destination, string currentDepartment, string operators)
        //{

        //    List<SqlParameter> paramArray = new List<SqlParameter>();

        //    paramArray.Add(new SqlParameter("@FNCardList", fnCardList));
        //    paramArray.Add(new SqlParameter("@Destination", destination));
        //    paramArray.Add(new SqlParameter("@Current_Department", currentDepartment));
        //    paramArray.Add(new SqlParameter("@Operators", operators));
        //    SqlParameter rtnNote_NO = new SqlParameter("@Note_NO", SqlDbType.Char, 12);
        //    rtnNote_NO.Direction = ParameterDirection.Output;
        //    paramArray.Add(rtnNote_NO);
        //    try
        //    {
        //        fnmDb.Database.ExecuteSqlCommand(@"exec usp_SaveSendInfo @FNCardList,@Destination ,@Current_Department ,@Operators ,@Note_NO OUTPUT",
        //            paramArray.ToArray());

        //        var result = Convert.ToString(paramArray[4].Value);

        //        HttpResponseMessage responseMessage =
        //            new HttpResponseMessage
        //            {
        //                Content =
        //                    new StringContent(result, Encoding.GetEncoding("UTF-8"),
        //                        "text/plain")
        //            };
        //        return responseMessage;
        //    }
        //    catch (Exception e)
        //    {
        //        log.Error(e.Message);
        //        throw;
        //    }
        //}
        ///// <summary>
        ///// 保存收布信息
        ///// </summary>
        ///// <param name="p"></param>
        ///// <returns></returns>
        //[Route("FnSaveReceiveInfo")]
        //[HttpGet]
        //public IHttpActionResult FnSaveReceiveInfo([FromUri]SaveReceiveInfoBm p)
        //{
        //    foreach (var _uspParam in p.GetType().GetProperties())
        //    {
        //        if (_uspParam.GetValue(p) == null)
        //        {
        //            _uspParam.SetValue(p, "");
        //        }
        //    }
        //    List<SqlParameter> paramArray = new List<SqlParameter>();

        //    paramArray.Add(new SqlParameter("@Note_NO", p.Note_NO));
        //    paramArray.Add(new SqlParameter("@Destination", p.Destination));
        //    paramArray.Add(new SqlParameter("@Operator", p.Operator));
        //    paramArray.Add(new SqlParameter("@Type", p.Type));
        //    paramArray.Add(new SqlParameter("@sNewCarNo", p.sNewCarNo));
        //    paramArray.Add(new SqlParameter("@sNewLocationNo", p.sNewLocationNo));

        //    try
        //    {
        //        fnmDb.Database.ExecuteSqlCommand(@"exec [dbo].[usp_SaveReceiveInfo] @Note_NO,@Destination ,@Operator ,@Type ,@sNewCarNo,@sNewLocationNo",
        //            paramArray.ToArray());
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        log.Error(e.Message);
        //        return BadRequest();
        //        throw;
        //    }
        //}

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
