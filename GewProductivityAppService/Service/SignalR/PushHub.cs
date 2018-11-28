using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GewProductivityAppService.DAL.GETNT103.MonitorWv2;
using GewProductivityAppService.DAL.GETNT62.GewPrdAppDB;
using GewProductivityAppService.DAL.MIS01.WVMDB;
using GewProductivityAppService.DAL.MIS01.YDMDB;
using GewProductivityAppService.Models.Wv.QiangDan;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Z.EntityFramework.Plus;

namespace GewProductivityAppService.Service.SignalR
{
    /// <summary>
    /// 
    /// </summary>
    [HubName("pushHub")]
    public class PushHub : Hub
    {
        /*
         GlobalHost.ConnectionManager.GetHubContext<PushHub>().Clients.Clients(pushTarget.ConnectIds)
                    .receivePushFromServer("任务", pushTarget.TaskCounts.ToString());
         */
        private YdmDbContext ydmDb = new YdmDbContext();

        /// <summary>
        /// 工厂方法
        /// </summary>
        public static readonly PushHub Instance = new PushHub();

        public override Task OnConnected()
        {
            var user = Context.ConnectionId;
            return base.OnConnected();
        }
        /// <summary>
        /// 断开连接时，更改登录状态
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            using (PrdAppDbContext prdAppDb = new PrdAppDbContext())
            {
                var user = prdAppDb.peAppWvUsers.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);
                if (user != null)
                {
                    prdAppDb.peAppWvUsers.Where(u => u.ConnectionId == Context.ConnectionId).Update(u => new peAppWvUser() { ConnectionId = "", IsLogin = false });
                    prdAppDb.SaveChanges();
                }
            }

            return base.OnDisconnected(false);
        }

        /// <summary>
        /// 登陆成功后/WiFi重新连接时调用
        /// </summary>
        /// <param name="empoNo">员工号</param>
        /// <param name="subDept">分厂</param>
        public void OnLogined(string empoNo, string subDept)
        {
            using (PrdAppDbContext prdAppDb = new PrdAppDbContext())
            {
                var deptGroup = prdAppDb.peAppWvUsers.FirstOrDefault(u => u.SubDept.Equals(subDept));
                if (deptGroup != null)
                {
                    var isuser = prdAppDb.peAppWvUsers.FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);
                    if (isuser == null)
                    {
                        // 更新数据库的登录状态
                        prdAppDb.peAppWvUsers.Where(u => u.code == empoNo).Update(u => new peAppWvUser() { ConnectionId = Context.ConnectionId, IsLogin = true });
                        prdAppDb.SaveChanges();
                    }
                }
            }
        }

        /// <summary>
        /// 推送染纱待拉轴信息到准备车间的拉轴工
        /// </summary>
        public void PushYdDaiSongZhouInfo()
        {
            using (PrdAppDbContext prdAppDb = new PrdAppDbContext())
            {
                int daiSongZhouCounts = ydmDb.prdSongZhouinfoes.Count(s => s.properattime == null);
                // 调用客户端的方法，在消息栏显示消息
                if (daiSongZhouCounts>0)
                {
                    List<string> conIds = prdAppDb.peAppWvUsers.Where(u => u.SubDept.Contains("PrSz") && u.IsLogin == true)
                        .Select(u => u.ConnectionId).ToList();
                    GlobalHost.ConnectionManager.GetHubContext<PushHub>().Clients.Clients(conIds)
                        .receivePushFromServer("您有 " + daiSongZhouCounts + " 条轴待送！", "");
                }
            }
        }
        /// <summary>
        /// 推送织造待做单消息到客户端
        /// </summary>
        public void PushWvNewTaskInfo()
        {
            List<PushTarget> pushTargets = new List<PushTarget>();

            pushTargets.Add(QiangDanPushTarget("WV1", "上轴", 0, 0));
            pushTargets.Add(QiangDanPushTarget("WV1", "组长", 10, 0));
            pushTargets.Add(QiangDanPushTarget("WV1", "机修", 20, 0));

            pushTargets.Add(QiangDanPushTarget("WV2", "上轴", 0, 0));
            pushTargets.Add(QiangDanPushTarget("WV2", "组长", 10, 1));
            pushTargets.Add(QiangDanPushTarget("WV2", "组长", 10, 2));
            pushTargets.Add(QiangDanPushTarget("WV2", "机修", 20, 0));

            pushTargets.Add(QiangDanPushTarget("WV3", "上轴", 0, 0));
            pushTargets.Add(QiangDanPushTarget("WV3", "组长", 10, 0));
            pushTargets.Add(QiangDanPushTarget("WV3", "机修", 20, 0));


            foreach (var pushTarget in pushTargets)
            {
                // 调用客户端的方法，在消息栏显示消息
                GlobalHost.ConnectionManager.GetHubContext<PushHub>().Clients.Clients(pushTarget.ConnectIds)
                    .receivePushFromServer("您有 "+pushTarget.Msg + " 个单可抢！","");
            }
        }

        /// <summary>
        /// 返回抢单APP指定部门和工种信息
        /// </summary>
        /// <param name="subDept"></param>
        /// <param name="remark"></param>
        /// <param name="taskStatus"></param>
        /// <param name="deptId"></param>
        /// <returns></returns>
         public PushTarget QiangDanPushTarget(string subDept, string remark, int taskStatus, int deptId)
        {
            IList<string> conId = new List<string>();
            int undoTasks = 0;
            using (MonitorWv2DbContext monitorWv2Db = new MonitorWv2DbContext())
            using (PrdAppDbContext prdAppDb = new PrdAppDbContext())
            using (WvmDbContext wvmDb = new WvmDbContext())
            {
                if (deptId < 1)
                {
                    // 增加Department筛选 18-11-28
                    undoTasks =
                        monitorWv2Db.QiangDanTasks.Count(
                            t => (t.TaskStatus == taskStatus) && t.IsActive == true && t.Department.Equals(subDept));
                    //var connectedUsers = prdAppDb.peAppWvUsers
                    //    .Join(wvmDb.peAppWvWorkers, u => u.code, w => w.cardno, (u, w) => new { u, w })
                    //    .Where(t => t.u.SubDept.Equals(subDept) && t.u.IsLogin == true && t.w.Remark.Equals(remark))
                    //    .Select(t => t.u.ConnectionId).ToList();
                    // 方式2
                    var userList = prdAppDb.peAppWvUsers.Where(u => u.SubDept.Equals(subDept) && u.IsLogin == true).ToList();
                    var workerList = wvmDb.peAppWvWorkers.Where(w => w.Remark.Equals(remark)).ToList();
                    var connectedUsers = userList.GroupJoin(workerList, u => u.code, w => w.cardno, (u, w) => new { u, w })
                        .Select(t => t.u.ConnectionId);

                    if (connectedUsers.Any()&&undoTasks>0)
                    {
                        foreach (var u in connectedUsers)
                        {
                            conId.Add(u);
                        }
                    }
                }
                else
                {
                    var sqlText = @"SELECT COUNT(*) FROM dbo.QiangDanTask AS q INNER JOIN dbo.machine AS m ON m.MachineName = q.MachineName WHERE m.DeptID=@p0 AND q.Department=@p1 AND q.TaskStatus=@p2 AND q.IsActive=1";
                    undoTasks = monitorWv2Db.Database.SqlQuery<int>(sqlText, deptId, subDept, taskStatus).Single();
                    if (deptId == 1)
                    {
                        //var connectedUsers = prdAppDb.peAppWvUsers
                        //    .Join(wvmDb.peAppWvWorkers, u => u.code, w => w.cardno, (u, w) => new { u, w })
                        //    .Where(t => t.u.SubDept.Equals(subDept) && t.u.IsLogin == true && t.w.Remark.Equals(remark) && t.w.GroupName.Contains("西"))
                        //    .Select(t => t.u.ConnectionId).ToList();

                        var userList = prdAppDb.peAppWvUsers.Where(u => u.SubDept.Equals(subDept) && u.IsLogin == true).ToList();
                        var workerList = wvmDb.peAppWvWorkers.Where(w => w.Remark.Equals(remark) && w.GroupName.Contains("西")).ToList();
                        var connectedUsers = userList.GroupJoin(workerList, u => u.code, w => w.cardno, (u, w) => new { u, w })
                            .Select(t => t.u.ConnectionId);
                        if (connectedUsers.Any() &&undoTasks>0)
                        {
                            foreach (var u in connectedUsers)
                            {
                                conId.Add(u);
                            }
                        }
                    }
                    else
                    {
                        //var connectedUsers = prdAppDb.peAppWvUsers
                        //    .Join(wvmDb.peAppWvWorkers, u => u.code, w => w.cardno, (u, w) => new { u, w })
                        //    .Where(t => t.u.SubDept.Equals(subDept) && t.u.IsLogin == true && t.w.Remark.Equals(remark) && t.w.GroupName.Contains("东"))
                        //    .Select(t => t.u.ConnectionId).ToList();
                        var userList = prdAppDb.peAppWvUsers.Where(u => u.SubDept.Equals(subDept) && u.IsLogin == true).ToList();
                        var workerList = wvmDb.peAppWvWorkers.Where(w => w.Remark.Equals(remark) && w.GroupName.Contains("东")).ToList();
                        var connectedUsers = userList.GroupJoin(workerList, u => u.code, w => w.cardno, (u, w) => new { u, w })
                            .Select(t => t.u.ConnectionId);
                        if (connectedUsers.Any() && undoTasks > 0)
                        {
                            foreach (var u in connectedUsers)
                            {
                                conId.Add(u);
                            }
                        }
                    }
                }

                return new PushTarget { ConnectIds = conId, Msg = undoTasks.ToString() };
            }

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ydmDb.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}