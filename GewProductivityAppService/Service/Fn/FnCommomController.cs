using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using GewProductivityAppService.DAL.MIS01.FNMDB;
using GewProductivityAppService.DAL.MIS01.PDMDB;
using GewProductivityAppService.Models;
using GewProductivityAppService.Utils;
using log4net;

namespace GewProductivityAppService.Service.Fn
{
    /// <summary>
    /// 后整通用接口
    /// </summary>
    [RoutePrefix("api/FnCommom")]
    public class FnCommomController : ApiController
    {
        private FnmDbContext fnmDb = new FnmDbContext();
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// FNMDB通用查询，返回任意数据集
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [Route("GeneralQuery")]
        [HttpGet]
        public IHttpActionResult GeneralQuery([FromUri] GeneralQueryBm p)
        {
            foreach (var _uspParam in p.GetType().GetProperties())
            {
                if (_uspParam.GetValue(p) == null)
                {
                    _uspParam.SetValue(p, "");
                }
            }

            List<SqlParameter> paramArray = new List<SqlParameter>();
            paramArray.Add(new SqlParameter("@type", p.type));
            paramArray.Add(new SqlParameter("@param2", p.param2));
            paramArray.Add(new SqlParameter("@param3", p.param3));
            paramArray.Add(new SqlParameter("@param4", p.param4));
            paramArray.Add(new SqlParameter("@param5", p.param5));

            try
            {
                var dynamicDbSet = DynamicSqlQueryClass.Instance.DynamicSqlQuery(fnmDb.Database, "EXEC [dbo].[usp_prdFnGeneralQuery]  @type,@param2,@param3,@param4,@param5", paramArray.ToArray());
                return Json(dynamicDbSet, JsonFormatSettings.Instance.GetSettings());
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(e.Message)
                };
                log.Error(e.Message);
                throw new HttpResponseException(resp);
            }
        }

        /// <summary>
        /// FNMDB公共存储过程
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [Route("CommonUsp")]
        [HttpGet]
        public IHttpActionResult CommonUsp([FromUri] CommonProcedureBm p)
        {
            foreach (var _uspParam in p.GetType().GetProperties())
            {
                if (_uspParam.GetValue(p) == null)
                {
                    _uspParam.SetValue(p, "");
                }
            }
            List<SqlParameter> paramArray = new List<SqlParameter>();
            paramArray.Add(new SqlParameter("@type", p.type));
            paramArray.Add(new SqlParameter("@param2", p.param2));
            paramArray.Add(new SqlParameter("@param3", p.param3));
            paramArray.Add(new SqlParameter("@param4", p.param4));
            paramArray.Add(new SqlParameter("@param5", p.param5));
            paramArray.Add(new SqlParameter("@param6", p.param6));

            try
            {
                fnmDb.Database.ExecuteSqlCommand(
                    "EXEC [dbo].[usp_prdFnCommonProcedure]  @type,@param2,@param3,@param4,@param5,@param6",
                    paramArray.ToArray());
                return Ok();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(e.Message)
                };
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

        /// <summary>
        /// 根据卡号，返回CAD图片
        /// </summary>
        /// <param name="fnCard"></param>
        /// <returns></returns>
        [Route("GetCadByFnCard")]
        [HttpGet]
        public HttpResponseMessage GetCadByFnCard(string fnCard)
        {
            try
            {
                SqlParameter _fnCard = new SqlParameter("@p0", fnCard);
                var imgByte = fnmDb.Database.SqlQuery<tdPatternPicture>(@"SELECT TOP 1 Picture FROM PDMDB..tdPatternPicture AS p INNER JOIN FNMDB..fnJobTraceHdr AS j ON j.GF_ID = p.GF_ID WHERE j.FN_Card=@p0", _fnCard).Select(p => p.Picture).FirstOrDefault();
                if (imgByte == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NoContent);
                }
                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(imgByte)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                return resp;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }
    }
}
