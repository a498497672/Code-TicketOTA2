using Ticket.Infrastructure.KouDaiLingQian.Core;
using Ticket.Infrastructure.KouDaiLingQian.Response;

namespace Ticket.Infrastructure.KouDaiLingQian
{
    public class KouDaiLingQianGateway
    {
        /// <summary>
        /// 微信（支付宝）被扫下单接口
        /// </summary>
        /// <param name="totalFee">支付金额，单位：元，保留小数点后两位</param>
        /// <param name="authCode">支付码</param>
        /// <param name="outTradeNo">订单号</param>
        /// <returns></returns>
        public static PayResult Pay(string totalFee, string authCode, string outTradeNo)
        {
            return MicroOrder.Pay(totalFee, authCode, outTradeNo);
        }
    }
}
