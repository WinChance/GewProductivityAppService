using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using Quartz.Impl;

namespace GewProductivityAppService.Service.Quartz
{
    public class Yd2PrShouSongZhouPushJobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler(); //从工厂中获取一个调度器实例化
            scheduler.Start();   //开始调度器
            IJobDetail job = JobBuilder.Create<Yd2PrShouSongZhouPushJob>().Build();//创建一个作业
            ITrigger trigger = TriggerBuilder.Create()
                .WithSimpleSchedule(t =>
                    t.WithIntervalInSeconds(1800) //触发执行，半小时一次
                        .RepeatForever())          //重复执行
                .Build();
            scheduler.ScheduleJob(job, trigger);       //把作业，触发器加入调度器。 
        }
    }
}