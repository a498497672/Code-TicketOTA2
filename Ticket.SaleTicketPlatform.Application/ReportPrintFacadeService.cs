using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Service;
using Ticket.Model.Model.Report;
using Ticket.Model.Result;

namespace Ticket.SaleTicketPlatform.Application
{
    public class ReportPrintFacadeService
    {
        private readonly ReportPrintService _reportPrintService;
        public ReportPrintFacadeService(ReportPrintService reportPrintService)
        {
            _reportPrintService = reportPrintService;
        }

        /// <summary>
        /// 日结报表统计
        /// </summary>
        /// <returns></returns>
        public TResult<ReportStatisticsModel> ReportStatistics()
        {
            return _reportPrintService.ReportStatistics();
        }

        public TResult Daily(string printKey)
        {
            return _reportPrintService.Daily(printKey);
        }
    }
}
