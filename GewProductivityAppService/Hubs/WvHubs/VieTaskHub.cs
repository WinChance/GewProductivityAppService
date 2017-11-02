using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace GewProductivityAppService.Hubs.WvHubs
{
    public class VieTaskHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}