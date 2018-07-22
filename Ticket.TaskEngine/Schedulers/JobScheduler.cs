using Quartz;
using System.Configuration;
using Ticket.TaskEngine.Jobs;

namespace Ticket.TaskEngine.Schedulers
{
    public class JobScheduler
    {
        private readonly IScheduler _scheduler;
        private readonly string _createOrderTimeInterval = ConfigurationManager.AppSettings["taskEngine:CreateOrderTimeInterval"];
        private readonly string _createOTAOrderTimeInterval = ConfigurationManager.AppSettings["taskEngine:CreateOTAOrderTimeInterval"];
        private readonly string _ticketConsumeTimeInterval = ConfigurationManager.AppSettings["taskEngine:TicketConsumeTimeInterval"];


        public JobScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public void Start()
        {
            _scheduler.Start();
            var noticeOrderConsumedJob = JobBuilder.Create<NoticeOrderConsumedJob>().Build();
            var noticeOrderConsumedTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(int.Parse(_ticketConsumeTimeInterval))
                    .RepeatForever())
                .Build();
            _scheduler.ScheduleJob(noticeOrderConsumedJob, noticeOrderConsumedTrigger);

            var orderTravelNoticeJob = JobBuilder.Create<OrderTravelNoticeJob>().Build();
            var orderTravelNoticeTrigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(int.Parse(_ticketConsumeTimeInterval))
                    .RepeatForever())
                .Build();
            _scheduler.ScheduleJob(orderTravelNoticeJob, orderTravelNoticeTrigger);
        }

        public void Stop()
        {
            _scheduler.Shutdown();
        }
    }
}
