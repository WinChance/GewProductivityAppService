using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GewProductivityAppService.Models.Common;
using YDMDB;

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
        public IHttpActionResult DropdownList([FromBody] DropDownListBindingModel dropDown)
        {
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
                List<ItemViewModel> rtnList = YdmDb.Database.SqlQuery<ItemViewModel>("EXEC dbo.usp_proAppDropDownListGet @param1,@param2,@param3,@param4,@param5,@rtn out",
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
    }
}
