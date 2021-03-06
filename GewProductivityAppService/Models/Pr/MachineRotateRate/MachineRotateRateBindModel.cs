﻿using System;

namespace GewProductivityAppService.Models.Pr.MachineRotateRate
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

        public decimal Begin { get; set; }

        public decimal End { get; set; }

        public string Operator { get; set; }

        public DateTime YieldDate { get; set; }

    }
}