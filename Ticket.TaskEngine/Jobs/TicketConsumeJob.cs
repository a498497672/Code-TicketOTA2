using Quartz;
using System;
using Ticket.TaskEngine.Application.Service;
using Ticket.Utility.Logger;

namespace Ticket.TaskEngine.Jobs
{
    public class TicketConsumeJob : IJob
    {
        private readonly SimpleLogger _logger = new SimpleLogger();
        private readonly TicketFacadeService _ticketFacadeService;

        public TicketConsumeJob(TicketFacadeService ticketFacadeService)
        {
            _ticketFacadeService = ticketFacadeService;
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                Processed();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void Processed()
        {
            _ticketFacadeService.TicketConsumeLine();
            Console.Write("\n开始门票入园核销");
        }
    }
}
