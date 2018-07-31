using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using GewProductivityAppService.DAL.MIS01.FNMDB;
using GewProductivityAppService.DAL.MIS01.PDMDB;
using GewProductivityAppService.Utils;
using log4net;

namespace GewProductivityAppService.Service.Fn
{
    /// <summary>
    /// 找布及修改车卷号的需求
    /// </summary>
    [RoutePrefix("api/FnDingwei")]
    public class FnDingweiController : ApiController
    {
        private FnmDbContext fnmDb = new FnmDbContext();
        private static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 返回机台终端的信息
        /// </summary>
        /// <param name="machineId">机台</param>
        /// <param name="currentDepartment">部门</param>
        /// <returns></returns>
        [Route("GetMachineInfo")]
        [HttpGet]
        public IHttpActionResult GetMachineInfo([FromUri] string machineId, string currentDepartment)
        {
            List<SqlParameter> paramArray = new List<SqlParameter>();
            paramArray.Add(new SqlParameter("@p0", machineId));
            paramArray.Add(new SqlParameter("@p1", currentDepartment));
            try
            {
                var sqlText =
@"
declare  @tb_GetMachineTaskInfo table   --声明表变量
(
FN_Card CHAR(9),
Quantity NUMERIC(9,2),
GF_NO varchar(20),
LocationNO varchar(50),
RfidCarNo NVARCHAR(256),
Car_NO varchar(30),
Operation_CHN VARCHAR(20),    --字段 必须和插入表变量里的数量一一对应
Plan_Time DATETIME
)
INSERT INTO  @tb_GetMachineTaskInfo  EXEC dbo.usp_prdGetMachineTaskInfo @Machine_ID = @p0, -- varchar(10)
@Current_Department = @p1, -- varchar(10)
@Flag = 'Y' -- varchar(1)
SELECT FN_Card, Quantity, GF_NO, LocationNO, RfidCarNo, Car_NO, Operation_CHN,FNMDB.dbo.udf_GetCardHolders(FN_Card) as Holder,
       Plan_Time,FNMDB.dbo.udf_GetExOperationChn(FN_Card) AS ExOperation_CHN FROM @tb_GetMachineTaskInfo
";
                var dynamicDbSet = DynamicSqlQueryClass.Instance.DynamicSqlQuery(fnmDb.Database, sqlText, paramArray.ToArray());
                return Json(dynamicDbSet,JsonFormatSettings.Instance.GetSettings());
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// 修改车卷号
        /// </summary>
        /// <param name="fnCard"></param>
        /// <param name="carNo"></param>
        /// <param name="locationNo"></param>
        /// <param name="operator"></param>
        /// <returns></returns>
        [Route("SaveCarInfo")]
        [HttpGet]
        public IHttpActionResult SaveCarInfo([FromUri] string fnCard, string carNo, string locationNo, string @operator)
        {
            List<SqlParameter> paramArray = new List<SqlParameter>();
            paramArray.Add(new SqlParameter("@sFNCard", fnCard));
            paramArray.Add(new SqlParameter("@sCarNO", carNo));
            paramArray.Add(new SqlParameter("@sRoll", locationNo));
            paramArray.Add(new SqlParameter("@sOperator", @operator));
            try
            {
                fnmDb.Database.ExecuteSqlCommand(@"exec [dbo].[usp_fnSaveCarInfo] @sFNCard,@sCarNO,@sRoll,@sOperator,'',''",
                    paramArray.ToArray());
                return Ok();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return BadRequest();
            }
        }
    }
}
