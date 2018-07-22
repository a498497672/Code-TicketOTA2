namespace FengjingSDK461.Model.Response
{
    /// <summary>
    /// 单独创建订单返回
    /// </summary>
    public class OrderSingleCreateResponse
    {
        public HeadResponse Head { get; set; }
        public OrderSingleCreateInfo Body { get; set; }
    }

    public class OrderSingleCreateInfo
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// OTA订单号
        /// </summary>
        public string OtaOrderId { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatus { get; set; }
        /// <summary>
        /// 凭证码
        /// </summary>
        public string CertificateNo { get; set; }
        /// <summary>
        /// 二维码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 剩余库存
        /// </summary>
        public int Inventory { get; set; }
    }
}
