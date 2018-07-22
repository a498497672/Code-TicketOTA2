namespace Ticket.Infrastructure.WxPay.Response
{
    /// <summary>
    /// 支付结果返回(刷卡支付)
    /// </summary>
    public class PayResultResponse
    {
        /// <summary>
        ///  是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 商户订单号,可以查单和退单
        /// </summary>
        public string OutTradeNo { get; set; }
    }
}
