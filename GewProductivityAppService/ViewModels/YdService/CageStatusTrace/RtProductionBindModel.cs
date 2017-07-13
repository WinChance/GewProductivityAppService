using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GewProductivityAppService.ViewModels.YdService.CageStatusTrace
{
    /// <summary>
    /// 产量录入对象
    /// </summary>
    public class RtProductionBindModel
    {
        public string BatchNo { get; set; }

        public string SarongNO { get; set; }

        public string InputClass { get; set; }

        public string WorkID { get; set; }

        public string InputerID { get; set; }

        public string YieldNum { get; set; }

        public string Department { get; set; }


    }

    
}