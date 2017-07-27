using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using GewProductivityAppService.DAL.MIS01.YDMDB;
using GewProductivityAppService.Models.Common;


namespace GewProductivityAppService.Controllers.Manage
{
    [RoutePrefix("api/Common")]
    public class CommonController : ApiController
    {
        private YdmDbContext YdmDb = new YdmDbContext();

        /// <summary>
        /// 根据不同的参数获取下拉列表的值。
        /// 1.返回穿综姓名：fun(GetHlProductionStaffs_Name,班别)；
        /// </summary>
        /// <param name="dropDown">1.param1:"GetHlProductionStaffs_Name",param2:"A"</param>
        /// <returns></returns>
        [Route("DropdownList")]
        [HttpPost]
        public IHttpActionResult DropdownList([FromBody] UspParamBindingModel dropDown)
        {
            // 将null替换为""
            foreach (var _dropDown in dropDown.GetType().GetProperties())
            {
                if (_dropDown.GetValue(dropDown) == null)
                {
                    _dropDown.SetValue(dropDown, "");
                }
            }
            // 传入下拉类别类型
            List<SqlParameter> paramArray = new List<SqlParameter>();
            paramArray.Add(new SqlParameter("@param1", dropDown.param1));
            paramArray.Add(new SqlParameter("@param2", dropDown.param2));
            paramArray.Add(new SqlParameter("@param3", dropDown.param3));
            paramArray.Add(new SqlParameter("@param4", dropDown.param4));
            paramArray.Add(new SqlParameter("@param5", dropDown.param5));
            SqlParameter param = new SqlParameter("@rtn", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            paramArray.Add(param);
            try
            {
                List<ItemViewModel> rtnList = YdmDb.Database.SqlQuery<ItemViewModel>("EXEC dbo.usp_prdAppDropDownListGet @param1,@param2,@param3,@param4,@param5,@rtn out",
                       paramArray.ToArray()).ToList();
                int i = 1;
                if ((int)paramArray[5].Value > 0)
                {

                    return Json(rtnList);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {

                throw;
            }


            return NotFound();
        }
        /// <summary>
        /// 传入多参数查询，返回单个值
        /// </summary>
        /// <param name="uspParams">存储过程参数Model</param>
        /// <returns>类型：text/plain</returns>
        [Route("QuerySingleValue")]
        [HttpGet]
        public HttpResponseMessage QuerySingleValue([FromUri]UspParamBindingModel uspParams)
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
            paramArray.Add(new SqlParameter("@param1", uspParams.param1));
            paramArray.Add(new SqlParameter("@param2", uspParams.param2));
            paramArray.Add(new SqlParameter("@param3", uspParams.param3));
            paramArray.Add(new SqlParameter("@param4", uspParams.param4));
            paramArray.Add(new SqlParameter("@param5", uspParams.param5));
            SqlParameter param = new SqlParameter("@rtn", SqlDbType.Int);
            param.Direction = ParameterDirection.Output;
            paramArray.Add(param);
            try
            {
                string rtnSingleString = YdmDb.Database.SqlQuery<string>("EXEC dbo.usp_prdAppQuerySingleValue @param1,@param2,@param3,@param4,@param5,@rtn out",
                       paramArray.ToArray()).FirstOrDefault();
                HttpResponseMessage responseMessage =
                        new HttpResponseMessage
                        {
                            Content =
                                new StringContent(rtnSingleString, Encoding.GetEncoding("UTF-8"),
                                    "text/plain")
                        };
                if ((int)paramArray[5].Value > 0)
                {

                    return responseMessage;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
                throw;
            }
        }
        protected override void Dispose(bool disposing)
        {
            YdmDb.Dispose();
            base.Dispose(disposing);
        }
    }
}
