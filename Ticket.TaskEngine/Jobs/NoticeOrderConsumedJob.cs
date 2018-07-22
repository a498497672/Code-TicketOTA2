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
    public class NoticeOrderConsumedJob : IJob
    {
        private readonly SimpleLogger _logger = new SimpleLogger();
        private readonly NoticeOrderConsumedFacadeService _noticeOrderConsumedFacadeService;

        public NoticeOrderConsumedJob(NoticeOrderConsumedFacadeService noticeOrderConsumedFacadeService)
        {
            _noticeOrderConsumedFacadeService = noticeOrderConsumedFacadeService;
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Processed();
            }
            catch (Exception ex)
            {
                Console.Write("订单消费通知：" + ex.Message);
                _logger.Error(ex);
            }
        }

        private void Processed()
        {
            Console.Write("\n开始订单消费通知");
            _noticeOrderConsumedFacadeService.VerifyTicket();
        }
    }
}
