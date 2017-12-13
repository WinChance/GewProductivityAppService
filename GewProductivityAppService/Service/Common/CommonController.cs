using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Web.Http;
using GewProductivityAppService.DAL.MIS01.YDMDB;
using GewProductivityAppService.Models;
using GewProductivityAppService.Models.Common;

namespace GewProductivityAppService.Service.Common
{
    /// <summary>
    /// 公共
    /// </summary>
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
        public IHttpActionResult DropdownList([FromBody] UspParamBindModel dropDown)
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
        public HttpResponseMessage QuerySingleValue([FromUri]QuerySingleValueBindModel uspParams)
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
            string singleString = null;
            try
            {
                singleString = YdmDb.Database.SqlQuery<string>("EXEC dbo.usp_prdAppQuerySingleValue @type,@param2,@param3,@param4,@param5",
                   paramArray.ToArray()).FirstOrDefault();
                
                HttpResponseMessage responseMessage =
                    new HttpResponseMessage
                    {
                        Content =
                            new StringContent(singleString, Encoding.GetEncoding("UTF-8"),
                                "text/plain")
                    };
                return responseMessage;
            }
            catch (Exception e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(e.Message + singleString + uspParams.type + uspParams.param2),
                    ReasonPhrase = "存储过程异常"
                };
                throw new HttpResponseException(resp);
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
                var dynamicDbSet = DynamicSqlQuery(YdmDb.Database, "EXEC [dbo].[usp_prdAppGeneralQuery]  @type,@param2,@param3,@param4,@param5", paramArray.ToArray());
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
        /// 动态查询，参考StackOverflow
        /// https://stackoverflow.com/questions/26749429/anonymous-type-result-from-sql-query-execution-entity-framework
        /// </summary>
        /// <param name="database"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IEnumerable DynamicSqlQuery(Database database, string sql, params object[] parameters)
        {
            TypeBuilder builder = CreateTypeBuilder(
                "MyDynamicAssembly", "MyDynamicModule", "MyDynamicType");

            using (System.Data.IDbCommand command = database.Connection.CreateCommand())
            {
                try
                {
                    database.Connection.Open();
                    command.CommandText = sql;
                    command.CommandTimeout = command.Connection.ConnectionTimeout;
                    foreach (var param in parameters)
                    {
                        command.Parameters.Add(param);
                    }

                    using (System.Data.IDataReader reader = command.ExecuteReader())
                    {
                        var schema = reader.GetSchemaTable();

                        foreach (System.Data.DataRow row in schema.Rows)
                        {
                            string name = (string)row["ColumnName"];
                            Type type = (Type)row["DataType"];
                            if (type != typeof(string) && (bool)row.ItemArray[schema.Columns.IndexOf("AllowDbNull")])
                            {
                                type = typeof(Nullable<>).MakeGenericType(type);
                            }
                            CreateAutoImplementedProperty(builder, name, type);
                        }
                    }
                }
                finally
                {
                    database.Connection.Close();
                    command.Parameters.Clear();
                }
            }

            Type resultType = builder.CreateType();

            return database.SqlQuery(resultType, sql, parameters);
        }

        private static TypeBuilder CreateTypeBuilder(
            string assemblyName, string moduleName, string typeName)
        {
            TypeBuilder typeBuilder = AppDomain
                .CurrentDomain
                .DefineDynamicAssembly(new AssemblyName(assemblyName),
                    AssemblyBuilderAccess.Run)
                .DefineDynamicModule(moduleName)
                .DefineType(typeName, TypeAttributes.Public);
            typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            return typeBuilder;
        }

        private static void CreateAutoImplementedProperty(
            TypeBuilder builder, string propertyName, Type propertyType)
        {
            const string PrivateFieldPrefix = "m_";
            const string GetterPrefix = "get_";
            const string SetterPrefix = "set_";

            // Generate the field.
            FieldBuilder fieldBuilder = builder.DefineField(
                string.Concat(PrivateFieldPrefix, propertyName),
                propertyType, FieldAttributes.Private);

            // Generate the property
            PropertyBuilder propertyBuilder = builder.DefineProperty(
                propertyName, System.Reflection.PropertyAttributes.HasDefault, propertyType, null);

            // Property getter and setter attributes.
            MethodAttributes propertyMethodAttributes =
                MethodAttributes.Public | MethodAttributes.SpecialName |
                MethodAttributes.HideBySig;

            // Define the getter method.
            MethodBuilder getterMethod = builder.DefineMethod(
                string.Concat(GetterPrefix, propertyName),
                propertyMethodAttributes, propertyType, Type.EmptyTypes);

            // Emit the IL code.
            // ldarg.0
            // ldfld,_field
            // ret
            ILGenerator getterILCode = getterMethod.GetILGenerator();
            getterILCode.Emit(OpCodes.Ldarg_0);
            getterILCode.Emit(OpCodes.Ldfld, fieldBuilder);
            getterILCode.Emit(OpCodes.Ret);

            // Define the setter method.
            MethodBuilder setterMethod = builder.DefineMethod(
                string.Concat(SetterPrefix, propertyName),
                propertyMethodAttributes, null, new Type[] { propertyType });

            // Emit the IL code.
            // ldarg.0
            // ldarg.1
            // stfld,_field
            // ret
            ILGenerator setterILCode = setterMethod.GetILGenerator();
            setterILCode.Emit(OpCodes.Ldarg_0);
            setterILCode.Emit(OpCodes.Ldarg_1);
            setterILCode.Emit(OpCodes.Stfld, fieldBuilder);
            setterILCode.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getterMethod);
            propertyBuilder.SetSetMethod(setterMethod);
        }
        protected override void Dispose(bool disposing)
        {
            
            YdmDb.Dispose();
            base.Dispose(disposing);
        }
    }
}
