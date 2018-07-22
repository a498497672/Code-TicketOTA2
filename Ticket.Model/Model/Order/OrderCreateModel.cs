using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.Model.Order
{
    public class OrderCreateModel
    {
        /// <summary>
        /// 支付类型(1:支付宝;2:微信；3：现金)
        /// </summary>
        public int PayType { get; set; }

        /// <summary>
        /// 授权码，用于微信和支付宝
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 是否打印
        /// </summary>
        public bool IsPrint { get; set; }
        /// <summary>
        /// 打印key --指定打印机
        /// </summary>
        public string PrintKey { get; set; }

        /// <summary>
        /// 有效期时间
        /// </summary>
        public DateTime ValidityDate { get; set; }

        /// <summary>
        /// 门票
        /// </summary>
        public List<TicketItem> TicketItem { get; set; }
    }

    //本类调用
    public class TicketItem
    {
        /// <summary>
        /// 门票ID
        /// </summary>
        public int TicketId { get; set; }

        /// <summary>
        /// 预订数量
        /// </summary>
        public int BookCount { get; set; }
    }
}
