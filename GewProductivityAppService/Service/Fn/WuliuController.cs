using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.WebPages;
using GewProductivityAppService.DAL.MIS01.FNMDB;
using GewProductivityAppService.Models;
using GewProductivityAppService.Models.Fn.Wuliu;
using GewProductivityAppService.Utils;
using log4net;

namespace GewProductivityAppService.Service.Fn
{
    /// <summary>
    /// 后整物流APP
    /// </summary>
    [RoutePrefix("api/Fn")]
    public class WuliuController : ApiController
    {
        private FnmDbContext fnmDb = new FnmDbContext();
        private static ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            command.CommandType=CommandType.StoredProcedure;

            try
            {
                fnmDb.Database.Connection.Open();
                var reader = command.ExecuteReader();
                List<UspGetSendInfo> _listOfGetSendInfos = ((IObjectContextAdapter) fnmDb).ObjectContext
                    .Translate<UspGetSendInfo>(reader).ToList();

                //reader.NextResult();
                //List<TestModel> _listOfTestModels = ((IObjectContextAdapter) fnmDb).ObjectContext
                //    .Translate<TestModel>(reader).ToList();

                return Json(_listOfGetSendInfos);
            }
            catch (Exception e)
            {
                return BadRequest();
                throw;
            }
            finally
            {
                fnmDb.Database.Connection.Close();
            }
            
        }
        /// <summary>
        /// 保存送布信息
        /// </summary>
        /// <param name="fnCardList"></param>
        /// <param name="destination"></param>
        /// <param name="currentDepartment"></param>
        /// <param name="operators"></param>
        /// <returns></returns>
        [Route("SaveSendInfo")]
        [HttpPost]
        public HttpResponseMessage SaveSendInfo([FromUri]string fnCardList, string destination, string currentDepartment, string operators)
        {
            
            List<SqlParameter> paramArray = new List<SqlParameter>();
            
            paramArray.Add(new SqlParameter("@FNCardList", fnCardList));
            paramArray.Add(new SqlParameter("@Destination", destination));
            paramArray.Add(new SqlParameter("@Current_Department", currentDepartment));
            paramArray.Add(new SqlParameter("@Operators", operators));
            SqlParameter rtnNote_NO = new SqlParameter("@Note_NO", SqlDbType.Char,12);
            rtnNote_NO.Direction = ParameterDirection.Output;
            paramArray.Add(rtnNote_NO);
            try
            {
                fnmDb.Database.ExecuteSqlCommand(@"exec usp_SaveSendInfo @FNCardList,@Destination ,@Current_Department ,@Operators ,@Note_NO OUTPUT",
                    paramArray.ToArray());

                var result = Convert.ToString(paramArray[4].Value);

                HttpResponseMessage responseMessage =
                    new HttpResponseMessage
                    {
                        Content =
                            new StringContent(result, Encoding.GetEncoding("UTF-8"),
                                "text/plain")
                    };
                return responseMessage;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存收布信息
        /// </summary>
        /// <param name="bm"></param>
        /// <returns></returns>
        [Route("SaveReceiveInfo")]
        [HttpPost]
        public IHttpActionResult SaveReceiveInfo([FromBody]SaveReceiveInfoBindModel bm)
        {
            foreach (var _bm in bm.GetType().GetProperties())
            {
                if (_bm.GetValue(bm) == null)
                {
                    _bm.SetValue(bm, "");
                }
            }
            List<SqlParameter> paramArray = new List<SqlParameter>();

            paramArray.Add(new SqlParameter("@Note_NO", bm.Note_NO));
            paramArray.Add(new SqlParameter("@Destination", bm.Destination));
            paramArray.Add(new SqlParameter("@Operator", bm.Operator));
            paramArray.Add(new SqlParameter("@Type", bm.Type));
            paramArray.Add(new SqlParameter("@sNewCarNo", bm.sNewCarNo));
            paramArray.Add(new SqlParameter("@sNewLocationNo", bm.sNewLocationNo));

            try
            {
                fnmDb.Database.ExecuteSqlCommand(@"exec [dbo].[usp_SaveReceiveInfo] @Note_NO,@Destination ,@Operator ,@Type ,@sNewCarNo,@sNewLocationNo",
                    paramArray.ToArray());
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        /// <summary>
        /// 通用查询，返回任意数据集
        /// </summary>
        /// <param name="uspParams"></param>
        /// <returns></returns>
        [Route("GeneralQuery")]
        [HttpGet]
        public IHttpActionResult GeneralQuery([FromUri] GeneralQueryBindModel uspParams)
        {
            foreach (var _uspParam in uspParams.GetType().GetProperties())
            {
                if (_uspParam.GetValue(uspParams) == null)
                {
                    _uspParam.SetValue(uspParams, "");
                }
            }
            // 传入下拉类别类型
            List<SqlParameter> paramArray = new List<SqlParameter>();
            paramArray.Add(new SqlParameter("@type", uspParams.type));
            paramArray.Add(new SqlParameter("@param2", uspParams.param2));
            paramArray.Add(new SqlParameter("@param3", uspParams.param3));
            paramArray.Add(new SqlParameter("@param4", uspParams.param4));
            paramArray.Add(new SqlParameter("@param5", uspParams.param5));

            try
            {
                var dynamicDbSet = DynamicSqlQueryClass.Instance.DynamicSqlQuery(fnmDb.Database, "EXEC [dbo].[usp_prdAppGeneralQuery]  @type,@param2,@param3,@param4,@param5", paramArray.ToArray());
                return Json(dynamicDbSet);
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "存储过程异常"
                };
                throw new HttpResponseException(resp);
            }
        }
        /// <summary>
        /// 公共存储过程
        /// </summary>
        /// <param name="uspParams"></param>
        /// <returns></returns>
        public IHttpActionResult CommonUsp([FromUri] CommonProcedureBindModel uspParams)
        {
            foreach (var _uspParam in uspParams.GetType().GetProperties())
            {
                if (_uspParam.GetValue(uspParams) == null)
                {
                    _uspParam.SetValue(uspParams, "");
                }
            }
            // 传入下拉类别类型
            List<SqlParameter> paramArray = new List<SqlParameter>();
            SqlParameter param = new SqlParameter("@rtnMsg", SqlDbType.VarChar,2000);
            param.Direction = ParameterDirection.Output;
            paramArray.Add(param);
            paramArray.Add(new SqlParameter("@type", uspParams.type));
            paramArray.Add(new SqlParameter("@param2", uspParams.param2));
            paramArray.Add(new SqlParameter("@param3", uspParams.param3));
            paramArray.Add(new SqlParameter("@param4", uspParams.param4));
            paramArray.Add(new SqlParameter("@param5", uspParams.param5));
            paramArray.Add(new SqlParameter("@param6", uspParams.param6));
            
            try
            {
                fnmDb.Database.ExecuteSqlCommand(
                    "EXEC [dbo].[usp_prdAppCommonProcedure]  @type,@param2,@param3,@param4,@param5,@param6,@rtnMsg out",
                    paramArray.ToArray());

                string rtnMsg = (string)paramArray[0].Value;
                if (rtnMsg == "success")
                {
                    return Ok();
                }
                else
                {
                    log.Error(rtnMsg);
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(e.Message),
                    ReasonPhrase = "存储过程异常"
                };
                log.Error(e.Message);
                throw new HttpResponseException(resp);
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
