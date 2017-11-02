using System;
using System.Data;

namespace GewProductivityAppService.Models.PrService.AbandonYarn
{
    public class AbandonYarnBindModel
    {

        public string Department { get; set; }

        public string Process { get; set; }

        public string WorkerClass { get; set; }

        public string Type { get; set; }

        public decimal Weight { get; set; }

        public string Operator { get; set; }

        public DateTime YieldDate { get; set; }

    }
}