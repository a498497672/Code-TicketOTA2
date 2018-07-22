namespace Ticket.Infrastructure.WxPay.Request
{
    public class PayRequest
    {
        /// <summary>
        /// 微信用户唯一标识
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 下单金额(单位：元)
        /// </summary>
        public decimal TotalFee { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 附加参数，可用于订单类别
        /// </summary>
        public string Attach { get; set; }
    }
}
