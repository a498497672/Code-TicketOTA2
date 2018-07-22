using System.Collections.Generic;

namespace FengjingSDK461.Model.Response
{
    /// <summary>
    /// 订单创建结果返回
    /// </summary>
    public class OrderCreateResponse
    {
        public HeadResponse Head { get; set; }
        public OrderCreateInfo Body { get; set; }
    }

    public class OrderCreateInfo
    {
        public string OtaOrderId { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatus { get; set; }

        public List<OrderCreateItem> Item { get; set; }

    }

    public class OrderCreateItem
    {
        /// <summary>
        /// ota订单项id
        /// </summary>
        public string OtaOrderDetailId { get; set; }
        /// <summary>
        /// 产品id
        /// </summary>
        public string ProductId { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public string useDate { get; set; }
        /// <summary>
        /// 使用时间剩余库存数
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 凭证码
        /// </summary>
        public string CertificateNo { get; set; }
    }
}
