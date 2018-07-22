using Ticket.Infrastructure.Alipay.Response;

namespace Ticket.Infrastructure.Alipay
{
    /// <summary>
    /// 支付宝
    /// </summary>
    public class AlipayPayGateway
    {
        /// <summary>
        /// 刷卡支付
        /// </summary>
        /// <param name="body">商品描述</param>
        /// <param name="total_fee">总金额(单位为元)</param>
        /// <param name="auth_code">支付授权码</param>
        /// <returns>刷卡支付结果</returns>
        public AlipayPayResponse OrderPay(string body, string total_fee, string auth_code)
        {
            try
            {
                return F2FPayNotify.OrderPay(body, total_fee, auth_code);
            }
            catch
            {
                return new AlipayPayResponse { Success = false, Message = "支付失败，请稍后再说" };
            }
        }

        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="out_trade_no">商户订单号</param>
        /// <returns></returns>
        public bool Cancel(string out_trade_no, string total_fee)
        {
            return F2FPayRefund.Run(out_trade_no, total_fee);
        }
    }
}
