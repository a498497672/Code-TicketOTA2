using Quartz;
using System;
using Ticket.TaskEngine.Application.Service;
using Ticket.Utility.Logger;

namespace Ticket.TaskEngine.Jobs
{
    public class CreateOrderJob : IJob
    {
        private readonly SimpleLogger _logger = new SimpleLogger();
        private readonly OrderFacadeService _orderFacadeService;

        public CreateOrderJob(OrderFacadeService orderFacadeService)
        {
            _orderFacadeService = orderFacadeService;
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Processed();
            }
            catch (Exception ex)
            {
                Console.Write("同步旅行社订单错误：" + ex.Message);
                _logger.Error(ex);
            }
        }

        private void Processed()
        {
            _orderFacadeService.SynchronizingOrder();
            Console.Write("\n开始同步旅行社订单");
        }
    }
}
