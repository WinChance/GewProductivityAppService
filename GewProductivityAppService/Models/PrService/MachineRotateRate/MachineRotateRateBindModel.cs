using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GewProductivityAppService.Models.PrService.MachineRotateRate
{
    /// <summary>
    /// 模型
    /// </summary>
    public class MachineRotateRateBindModel
    {
        public string Department { get; set; }

        public string Process { get; set; }

        public string WorkerClass { get; set; }

        public string Machine { get; set; }

        public int Begin { get; set; }

        public int End { get; set; }

        //public int RotateDuration { get; set; }

        public string Operator { get; set; }

        public DateTime YieldDate { get; set; }

    }
}