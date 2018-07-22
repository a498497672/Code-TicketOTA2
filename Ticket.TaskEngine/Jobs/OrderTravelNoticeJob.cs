using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.TaskEngine.Application.Service;
using Ticket.Utility.Logger;

namespace Ticket.TaskEngine.Jobs
{
    /// <summary>
    /// 携程出行通知服务
    /// </summary>
    public class OrderTravelNoticeJob : IJob
    {
        private readonly SimpleLogger _logger = new SimpleLogger();
        private readonly OrderTravelNoticeFacadeService _orderTravelNoticeFacadeService;

        public OrderTravelNoticeJob(OrderTravelNoticeFacadeService  orderTravelNoticeFacadeService)
        {
            _orderTravelNoticeFacadeService = orderTravelNoticeFacadeService;
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Processed();
            }
            catch (Exception ex)
            {
                Console.Write("携程出行通知服务：" + ex.Message);
                _logger.Error(ex);
            }
        }

        private void Processed()
        {
            Console.Write("\n开始携程出行通知服务");
            _orderTravelNoticeFacadeService.VerifyTicket();
        }
    }
}
