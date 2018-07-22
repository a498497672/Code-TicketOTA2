namespace FengjingSDK461.Model.Request
{
    /// <summary>
    /// 订单单独创建
    /// </summary>
    public class OrderSingleCreateRequest
    {
        public HeadRequest Head { get; set; }
        public OrderSingleCreateBody Body { get; set; }
    }

    public class OrderSingleCreateBody
    {
        public OrderSingleInfo OrderInfo { get; set; }
    }

    public class OrderSingleInfo
    {
        /// <summary>
        /// OTA订单Id
        /// </summary>
        public string OrderOtaId { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// 订票数量
        /// </summary>
        public int OrderQuantity { get; set; }
        /// <summary>
        /// 游玩日期(yyyy-MM-dd)
        /// </summary>
        public string VisitDate { get; set; }
        /// <summary>
        /// 是否收款 1 OTA用户已付款 0 OTA用户未支付
        /// </summary>
        public int OrderPayStatus { get; set; }
        /// <summary>
        /// 取票人的信息 
        /// </summary>
        public ContactPerson ContactPerson { get; set; }
        /// <summary>
        /// 门票
        /// </summary>
        public ProductItem Ticket { get; set; }
    }
}
