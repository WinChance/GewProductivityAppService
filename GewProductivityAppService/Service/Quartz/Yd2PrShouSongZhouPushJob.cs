using GewProductivityAppService.Service.SignalR;
using Quartz;

namespace GewProductivityAppService.Service.Quartz
{
    /// <summary>
    /// 
    /// </summary>
    public class Yd2PrShouSongZhouPushJob :IJob
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)//必须实现IJob接口下的Execute方法
        {
            // 定时推送消息
            PushHub.Instance.PushYdDaiSongZhouInfo();
        }
    }
}