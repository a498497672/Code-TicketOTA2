using Quartz;
using System;
using Ticket.TaskEngine.Application.Service;
using Ticket.Utility.Logger;

namespace Ticket.TaskEngine.Jobs
{
    public class CreateOTAOrderJob : IJob
    {
        private readonly SimpleLogger _logger = new SimpleLogger();
        private readonly OrderFacadeService _orderFacadeService;

        public CreateOTAOrderJob(OrderFacadeService orderFacadeService)
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
                Console.Write("同步OTA订单：" + ex.Message);
                _logger.Error(ex);
            }
        }

        private void Processed()
        {
            _orderFacadeService.SynchronizingOtaOrder();
            Console.Write("\n开始同步OTA订单");
            //var lateFeeList = _lateFeeProcessFacadeService.GetNoLateFeeProcessedFiveHundred().ToList();
            //while (lateFeeList.Count > 0)
            //{
            //    foreach (var lateFee in lateFeeList)
            //    {
            //        _lateFeeProcessFacadeService.LateFeeProcess(lateFee);
            //    }
            //    lateFeeList = _lateFeeProcessFacadeService.GetNoLateFeeProcessedFiveHundred().ToList();
            //}
        }
    }
}
