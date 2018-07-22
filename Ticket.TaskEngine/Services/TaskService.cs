using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.TaskEngine.Schedulers;

namespace Ticket.TaskEngine.Services
{
    public class TaskService
    {
        private readonly JobScheduler _jobScheduler;

        public TaskService(JobScheduler jobScheduler)
        {
            _jobScheduler = jobScheduler;
        }

        public void Start()
        {
            _jobScheduler.Start();
        }

        public void Stop()
        {
            _jobScheduler.Stop();
        }
    }
}
