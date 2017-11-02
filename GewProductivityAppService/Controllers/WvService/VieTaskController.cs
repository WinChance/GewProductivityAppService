using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Messaging;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GewProductivityAppService.DAL.MIS01.Temp;
using GewProductivityAppService.Models.WvService.VieTask;
using Z.EntityFramework.Plus;

namespace GewProductivityAppService.Controllers.WvService
{
    [RoutePrefix("api/Wv")]
    public class VieTaskController : ApiController
    {
        
        private static PrdAppDbContext PrdAppDb=new PrdAppDbContext();
        private bool isExeced = false;
        private static int stockCounts;
        private static int _sum;
        /// <summary>
        /// 秒杀接口
        /// </summary>
        /// <param name="iden"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IHttpActionResult MiaoSha([FromUri] int iden,int count)
        {
            if (!isExeced)
            {
                stockCounts=PrdAppDb.Products.Where(p=>p.Iden.Equals(iden)).Select(p=>p.Count).FirstOrDefault();
                isExeced = true;
            }
            _sum=_sum+1;
            Createqueue(@".\private$\MiaoSha");
            SendMessage(new OrderInfo()
            {
                Iden = iden,
                Count = count
            });
            if (_sum>=stockCounts)
            {
                ReceiveMessage();
                return NotFound();
            }
            else
            {
                return Ok();
            }
            
            
        }

        /// <summary>
        /// 通过Create方法创建使用指定路径的新消息队列
        /// </summary>
        /// <param name="queuePath"></param>
        public static void Createqueue(string queuePath)
        {
            try
            {
                if (!MessageQueue.Exists(queuePath))
                {
                    MessageQueue.Create(@".\private$\MiaoSha");
                    //MessageQueue.Delete();
                }
            }
            catch (MessageQueueException e)
            {
                Debugger.Log(1, "error", "发生异常");
                Debugger.Break();
            }
        }
        /// <summary>
        /// 连接消息队列并发送消息到队列
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns></returns>
        public static bool SendMessage(OrderInfo orderInfo)
        {
            bool flag;
            try
            {
                MessageQueue myQueue = new MessageQueue(".\\private$\\MiaoSha");

                Message myMessage=new Message();
                myMessage.Body = orderInfo;
                myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(OrderInfo) });
                // 发送消息到队列中
                myQueue.Send(myMessage);
                flag = true;
            }
            catch (Exception e)
            {
                Debugger.Log(1, "error", "发生异常");
                Debugger.Break();
                throw;
            }
            return flag;
        }
        /// <summary>
        /// 连接消息队列并从队列中接收消息
        /// </summary>
        /// <returns></returns>
        public static void ReceiveMessage()
        {
            MessageQueue myQueue = new MessageQueue(".\\private$\\MiaoSha");
            XmlMessageFormatter formatter = new XmlMessageFormatter(new Type[] { typeof(OrderInfo) });
            try
            {
                Message[] messages=myQueue.GetAllMessages();
                for (int index = 0; index < messages.Length; index++)
                {
                    messages[index].Formatter = formatter;
                    OrderInfo orderInfo = messages[index].Body as OrderInfo;
                    if (PrdAppDb.Products.Where(p=>p.Iden.Equals(orderInfo.Iden)).Select(p=>p.Count).FirstOrDefault()<=0)
                    {
                        myQueue.Purge();
                        break;
                    }
                    int _oldCount=PrdAppDb.Products.Where(p=>p.Iden.Equals(orderInfo.Iden)).Select(p=>p.Count).FirstOrDefault();
                    PrdAppDb.Products.Where(p => p.Iden.Equals(orderInfo.Iden)).Update(p => new Product() { Count = (_oldCount - orderInfo.Count) });
                    PrdAppDb.SaveChanges();
                }
                
            }
            catch (InvalidCastException e)
            {
                Debugger.Log(1,"error","发生异常");
                Debugger.Break();
                throw;
            }
        }
    }
}
