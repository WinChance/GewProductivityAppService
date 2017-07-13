using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GewProductivityAppService.ViewModels.YdService.CageStatusTrace
{
    public class SarongStatusViewModel
    {
        public int ID { get; set; }

        public string SarongNo { get; set; }


        public string CageType { get; set; }

      
        public string Department { get; set; }

     
        public string JarType { get; set; }

        public int? CountOfCategory { get; set; }

        public string Remark { get; set; }

        public string IsUsed { get; set; }

        public string FixedColor { get; set; }

        public string ColorCode { get; set; }


    }
}