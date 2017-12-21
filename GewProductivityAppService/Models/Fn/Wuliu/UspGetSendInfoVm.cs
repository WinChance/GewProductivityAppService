using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GewProductivityAppService.DAL.MIS01.FNMDB
{
    /// <summary>
    /// 存储过程返回实体
    /// </summary>
    public class UspGetSendInfo
    {
        public string FN_Card { get; set; }

        public int GF_ID { get; set; }

        public decimal Quantity { get; set; }

        public string GF_NO { get; set; }


        public string car_no { get; set; }

        public string sCode { get; set; }

        public string Current_Department { get; set; }

    }
}