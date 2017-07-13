using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEW_MIS01
{
    public static class ConnectionStrings
    {
        // 测试数据库账号
        // YDMDB数据库：账号密码
        public static string YdmDbConnectionString = @"Data Source=gew-mis01uat;database=YDMDB;uid=test;pwd=ittest;";


        public static string PbReadConnectionString = @"Data Source=gew-mis01;database=WVMDB;uid=pbread;pwd=limitereader01;";

        // 正式数据库账号

        //public static string GewPeAppConnectionString = @"Data Source=GETNT62;database=GEWPRDAPPDB;uid=gewapp;pwd=K6Jc*dqcpwt3;";
        //public static string PbReadConnectionString = @"Data Source=gew-mis01;database=WVMDB;uid=pbread;pwd=limitereader01;";
    }
}
