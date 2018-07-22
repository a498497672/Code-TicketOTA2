using Ticket.Infrastructure.Print.Core;
using Ticket.Infrastructure.Print.Lib;
using Ticket.Infrastructure.Print.Request;
using Ticket.Infrastructure.Print.Response;

namespace Ticket.Infrastructure.Print
{
    /// <summary>
    /// 易联云打印机--第三方
    /// </summary>
    public class PrintGateway
    {
        public PrintResult Send(PrintOrderData data, PrintConfigData configData)
        {
            return PrintHelper.Send(data, configData);
        }

        public PrintResult Send(PrintReportData data, PrintConfigData configData)
        {
            return PrintReport.Send(data, configData);
        }

        public PrintConfigData Get(string printKey)
        {
            return PrintManager.Get(printKey);
        }

    }
}
