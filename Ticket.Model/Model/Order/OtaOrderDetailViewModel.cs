using Ticket.Model.Enum;
using Ticket.Utility.Extensions;

namespace Ticket.Model.Model.Order
{
   public class OtaOrderDetailViewModel
    {
        /// <summary>
        /// 订单详情id
        /// </summary>
        public int OrderDetailId { get; set; }
        /// <summary>
        /// 门票产品Id
        /// </summary>
        public int TicketId { get; set; }

        /// <summary>
        /// 门票名称
        /// </summary>
        public string TicketName { get; set; }

        /// <summary>
        /// 门票类型
        /// </summary>
        public int TicketCategory { get; set; }

        /// <summary>
        /// 出票类型
        /// </summary>
        public string TicketCategoryDes
        {
            get { return ((TicketCategoryStatus)TicketCategory).GetDescriptionByName(); }
        }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Qunatity { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatusDes
        {
            get { return ((OtaOrderStatus)OrderStatus).GetDescriptionByName(); }
        }
        public int sentTimes { get; set; }
        /// <summary>
        /// 凭证号
        /// </summary>
        public string CertificateNo { get; set; }

        public int? PrintCount { get; set; }
    }
}
