namespace Ticket.Infrastructure.WxPay.Response
{
    public class PayNotifyResponse
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 微信用户唯一标识
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 微信订单号（用于退款优先使用）
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// 附加参数
        /// </summary>
        public string Attach { get; set; }
    }
}
