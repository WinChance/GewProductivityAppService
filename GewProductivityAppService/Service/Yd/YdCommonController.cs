using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using GewProductivityAppService.DAL.MIS01.YDMDB;
using GewProductivityAppService.Models;
using GewProductivityAppService.Utils;
using log4net;

namespace GewProductivityAppService.Service.Yd
{
    /// <summary>
    /// 染纱通用接口
    /// </summary>
    [RoutePrefix("api/YdCommom")]
    public class YdCommonController : ApiController
    {
        private YdmDbContext ydmDb = new YdmDbContext();
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 通用查询，返回任意数据集
        /// </summary>
        /// <param name="uspParams"></param>
        /// <returns></returns>
        [Route("GeneralQuery")]
        [HttpGet]
        public IHttpActionResult GeneralQuery([FromUri] GeneralQueryBm uspParams)
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
                var dynamicDbSet = DynamicSqlQueryClass.Instance.DynamicSqlQuery(ydmDb.Database, "EXEC [dbo].[usp_prdAppGeneralQuery]  @type,@param2,@param3,@param4,@param5", paramArray.ToArray());
                return Json(dynamicDbSet);
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
    }
}
