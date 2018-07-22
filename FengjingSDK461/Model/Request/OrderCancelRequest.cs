using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengjingSDK461.Model.Request
{
    /// <summary>
    /// 取消订单
    /// </summary>
    public class OrderCancelRequest
    {
        public HeadRequest Head { get; set; }
        public OrderCancelBody Body { get; set; }
    }

    public class OrderCancelBody
    {
        public OrderCancelInfo OrderInfo { get; set; }
    }

    public class OrderCancelInfo
    {
        /// <summary>
        /// 携程订单号
        /// </summary>
        public string OtaOrderId { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 取消流水号，用于标记每一笔订单取消记录，由 OTA 定义
        /// </summary>
        public string Seq { get; set; }
        /// <summary>
        /// 原始订单总价
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// 原始订单总票数
        /// </summary>
        public int OrderQuantity { get; set; }
        /// <summary>
        /// 取消原因
        /// </summary>
        public string reason { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CancelOrderItemInfo> Items { get; set; }
    }

    public class CancelOrderItemInfo
    {
        /// <summary>
        /// ota订单详情id
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 门票ID(产品id)
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 取消数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 被取消的退款金额
        /// </summary>
        public string Amount { get; set; }
    }
}
