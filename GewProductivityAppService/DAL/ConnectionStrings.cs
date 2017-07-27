using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GewProductivityAppService.DAL
{
    public class ConnectionStrings
    {
        static string conStr = ConfigurationManager.ConnectionStrings["MIS01"].ToString();
        public static string YdmDbConnectionString = conStr.Replace("master", "YDMDB");
        public static string PdmDbConnectionString = conStr.Replace("master","PDMDB");

        // TODO:请发布时，将以下连接字符串，改为正式数据库账号密码
        //public static string PdmDbConnectionString = @"Data Source=gew-mis01uat;database=PDMDB;uid=test;pwd=ittest;";

        #region 暂时注释的
        //public static string GewPeAppConnectionString = @"Data Source=GETNT62;database=GEWPRDAPPDB;uid=gewapp;pwd=K6Jc*dqcpwt3;";

        //public static string PbReadConnectionString = @"Data Source=gew-mis01;database=WVMDB;uid=pbread;pwd=limitereader01;";
        #endregion

        #region 测试数据库账号
        
        //public static string PdmDbConnectionString = @"Data Source=gew-mis01uat;database=PDMDB;uid=test;pwd=ittest;";
        // YDMDB数据库：账号密码
        //public static string YdmDbConnectionString = @"Data Source=gew-mis01uat;database=YDMDB;uid=test;pwd=ittest;";

        //public static string PbReadConnectionString = @"Data Source=gew-mis01;database=PDMDB;uid=pbread;pwd=limitereader01;";

        //public static string WinChanceConnectionString = @"Data Source=gew-mis01sit;database=WinChance;uid=test;pwd=ittest;";
        #endregion
    }
}