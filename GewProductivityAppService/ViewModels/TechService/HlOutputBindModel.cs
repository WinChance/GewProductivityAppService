using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GewProductivityAppService.ViewModels.TechService
{
    public class HlOutputBindModel
    {
        public string HL_No { get; set; }

        public string Name { get; set; }

        public string Class { get; set; }

        public string Post { get; set; }

        public decimal Sys_Score { get; set; }

        public decimal Dync_Score { get; set; }

        public decimal ProValue { get; set; }

        public DateTime InputTime { get; set; }

        public string ModifyName { get; set; }

        public DateTime ModifyTime { get; set; }

        public string Remark { get; set; }

        public int IsLargeType { get; set; }

        public int IsMore { get; set; }

        public int IsCalico { get; set; }
    }
}