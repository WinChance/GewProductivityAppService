using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace GewProductivityAppService.DAL
{
    /// <summary>
    /// 拼接连接字符串
    /// </summary>
    public class ConnectionStrings
    {
        static string conStr = ConfigurationManager.ConnectionStrings["MIS01"].ToString();
        /// <summary>
        /// YdmDb连接字符串，供上下文使用
        /// </summary>
        public static string YdmDbConnectionString = conStr.Replace("master", "YDMDB");
        /// <summary>
        /// PDMDB连接字符串，供上下文使用
        /// </summary>
        public static string PdmDbConnectionString = conStr.Replace("master","PDMDB");

        /// <summary>
        /// WVMDB连接字符串，供上下文使用
        /// </summary>
        public static string WvmDbConnectionString = conStr.Replace("master", "WVMDB");

        /// <summary>
        /// WVMDB连接字符串，供上下文使用
        /// </summary>
        public static string PrdAppDbConnectionString = conStr.Replace("master", "GewPrdAppDB");
    }
}