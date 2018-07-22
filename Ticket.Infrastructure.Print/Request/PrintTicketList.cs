using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.Print.Request
{
    /// <summary>
    /// 门票列表
    /// </summary>
    public class PrintTicketList
    {
        /// <summary>
        /// 现金
        /// </summary>
        public List<PrintTicketData> ReadyMoney { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        public List<PrintTicketData> Wechat { get; set; }
        /// <summary>
        /// 支付宝
        /// </summary>
        public List<PrintTicketData> Alipay { get; set; }

        public int TotalCount
        {
            get
            {
                return ReadyMoney.Count + Wechat.Count + Alipay.Count;
            }
        }
    }
}
