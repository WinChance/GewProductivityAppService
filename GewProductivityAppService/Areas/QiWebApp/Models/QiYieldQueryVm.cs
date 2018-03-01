using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GewProductivityAppService.Areas.QiWebApp.Models
{
    public class QiYieldQueryVm
    {
        public string HL_No { get; set; }

        public string Class { get; set; }

        public decimal? Sys_Score { get; set; }

        public decimal? Dync_Score { get; set; }


        public DateTime? InputTime { get; set; }
    }
}