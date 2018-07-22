using FengjingSDK461.Core;
using FengjingSDK461.Enum;
using FengjingSDK461.Model.Request;
using System;
using System.Timers;
using Ticket.Utility.Logger;

namespace Ticket.SaleWebApi.Application
{
    /// <summary>
    /// 解决 EF 启动慢问题 第一次访问慢,
    /// 解决刚部署之后，第一次启动很慢；程序放置一会儿，再次请求也会比较慢。
    /// 第一个问题，可以解释为初次请求某一个服务的时候，需要把程序集加载到内存中可能比较慢，
    /// 第二个问题, 有可能是IIS的线程回收机制导致放置若干长时间，空闲的进程被回收了，
    /// 再次请求的话可能比较慢
    /// </summary>
    public class WebSiteInitializationFacadeService
    {
        /// <summary>
        /// 定期访问一次服务器(9分钟=9*60*1000秒(s)=540000毫秒(ms))
        /// </summary>
        private static Timer sysTimer = new Timer(540000);
        private static TicketGateway _ticketGateway = new TicketGateway(OtaType.TongCheng);
        private static SimpleLogger _logger = new SimpleLogger();

        /// <summary>
        /// 初始化，解决刚部署之后，第一次启动很慢；程序放置一会儿，再次请求也会比较慢。
        /// </summary>
        public static void Init()
        {
            sysTimer.Enabled = true;
            sysTimer.Elapsed += sysTimer_Elapsed;
            sysTimer.Start();
        }

        private static void sysTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var response = _ticketGateway.GetProduct(new ProductQueryRequest
            {
                Body = new Product
                {
                    Type = 1,
                    ProductId = 0,
                    CurrentPage = 1,
                    PageSize = 1
                }
            });
            if (response.Head.Code == "000000")
            {

            }
            _logger.Info(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  : " + response.Head.Code);
        }
    }
}
