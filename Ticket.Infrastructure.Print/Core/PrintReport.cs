using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Infrastructure.Print.Lib;
using Ticket.Infrastructure.Print.Request;
using Ticket.Infrastructure.Print.Response;

namespace Ticket.Infrastructure.Print.Core
{
    public class PrintReport
    {
        /// <summary>
        /// 打印报表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="configData"></param>
        /// <returns></returns>
        public static PrintResult Send(PrintReportData data, PrintConfigData configData)
        {
            var text = GetReportContext(data);
            return PrintHelper.Send(text, configData);
        }

        private static string GetReportContext(PrintReportData data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<center> 日结报表 </center>\r\n");
            sb.AppendFormat("\r\n开始时间: {0}\r\n", data.StartTime);
            sb.AppendFormat("\r\n结束时间: {0}\r\n", data.EndTime);

            AddBulkTicket(data, sb);

            AddTeamTicket(data, sb);
            sb.Append("................................\r\n\r\n");
            sb.AppendFormat("\r\n售票总数量: {0}\r\n", data.TotalCount);
            sb.AppendFormat("\r\n售票总金额: {0}\r\n", data.TotalAmount);
            if (data.RefundTotalCount > 0)
            {
                sb.AppendFormat("\r\n退款总数量: {0}\r\n", data.RefundTotalCount);
                sb.AppendFormat("\r\n退款总金额: {0}\r\n", data.RefundTotalAmount);
            }
            sb.AppendFormat("\r\n售票员: {0} \r\n", data.RealName);
            //sb.AppendFormat("\r\n总数量: {0}  总金额: {0}\r\n", data.OrderNo);
            return sb.ToString();
        }

        /// <summary>
        /// 添加团队票
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sb"></param>
        private static void AddTeamTicket(PrintReportData data, StringBuilder sb)
        {
            if (data.PrintTeamTicket.TotalCount <= 0)
            {
                return;
            }
            sb.Append("................................\r\n");
            sb.Append("\r\n团队票: \r\n");
            sb.Append("<table>");
            sb.Append("<tr><td>品名</td><td>数量</td><td>小计</td></tr>");
            List<PrintTicketData> TeamReadyMoney = data.PrintTeamTicket.ReadyMoney;
            if (TeamReadyMoney.Count > 0)
            {
                sb.AppendFormat("<tr><td>* 现金支付</td><td>{0}</td><td>{1}</td></tr>", TeamReadyMoney.Sum(p => p.TotalCount), TeamReadyMoney.Sum(p => p.TotalAmount));
                foreach (var row in TeamReadyMoney)
                {
                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}元</td></tr>", row.TicketName, row.TotalCount, row.TotalAmount);
                }
            }
            List<PrintTicketData> TeamWechat = data.PrintTeamTicket.Wechat;
            if (TeamWechat.Count > 0)
            {
                sb.AppendFormat("<tr><td>* 微信支付</td><td>{0}</td><td>{1}</td></tr>", TeamWechat.Sum(p => p.TotalCount), TeamWechat.Sum(p => p.TotalAmount));
                foreach (var row in TeamWechat)
                {
                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}元</td></tr>", row.TicketName, row.TotalCount, row.TotalAmount);
                }
            }
            List<PrintTicketData> TeamAlipay = data.PrintTeamTicket.Alipay;
            if (TeamAlipay.Count > 0)
            {
                sb.AppendFormat("<tr><td>* 支付宝支付</td><td>{0}</td><td>{1}</td></tr>", TeamAlipay.Sum(p => p.TotalCount), TeamAlipay.Sum(p => p.TotalAmount));
                foreach (var row in TeamAlipay)
                {
                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}元</td></tr>", row.TicketName, row.TotalCount, row.TotalAmount);
                }
            }
            sb.Append("</table>");
        }

        /// <summary>
        /// 添加散客票
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sb"></param>
        private static void AddBulkTicket(PrintReportData data, StringBuilder sb)
        {
            if (data.PrintBulkTicket.TotalCount <= 0)
            {
                return;
            }
            sb.Append("................................\r\n");
            sb.Append("\r\n散客票:\r\n");
            sb.Append("<table>");
            sb.Append("<tr><td>品名</td><td>数量</td><td>小计</td></tr>");
            List<PrintTicketData> bulkReadyMoney = data.PrintBulkTicket.ReadyMoney;
            if (bulkReadyMoney.Count > 0)
            {
                sb.AppendFormat("<tr><td>* 现金支付</td><td>{0}</td><td>{1}</td></tr>", bulkReadyMoney.Sum(p => p.TotalCount), bulkReadyMoney.Sum(p => p.TotalAmount));
                foreach (var row in bulkReadyMoney)
                {
                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}元</td></tr>", row.TicketName, row.TotalCount, row.TotalAmount);
                }
            }
            List<PrintTicketData> bulkWechat = data.PrintBulkTicket.Wechat;
            if (bulkWechat.Count > 0)
            {
                sb.AppendFormat("<tr><td>* 微信支付</td><td>{0}</td><td>{1}</td></tr>", bulkWechat.Sum(p => p.TotalCount), bulkWechat.Sum(p => p.TotalAmount));
                foreach (var row in bulkWechat)
                {
                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}元</td></tr>", row.TicketName, row.TotalCount, row.TotalAmount);
                }
            }
            List<PrintTicketData> bulkAlipay = data.PrintBulkTicket.Alipay;
            if (bulkAlipay.Count > 0)
            {
                sb.AppendFormat("<tr><td>* 支付宝支付</td><td>{0}</td><td>{1}</td></tr>", bulkAlipay.Sum(p => p.TotalCount), bulkAlipay.Sum(p => p.TotalAmount));
                foreach (var row in bulkAlipay)
                {
                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}元</td></tr>", row.TicketName, row.TotalCount, row.TotalAmount);
                }
            }
            sb.Append("</table>");
        }
    }
}
