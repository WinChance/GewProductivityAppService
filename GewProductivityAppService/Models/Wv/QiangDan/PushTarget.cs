using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GewProductivityAppService.Models.Wv.QiangDan
{
    public class PushTarget
    {
        public IList<string> ConnectIds { get; set; }

        public string Msg { get; set; } 
    }
}