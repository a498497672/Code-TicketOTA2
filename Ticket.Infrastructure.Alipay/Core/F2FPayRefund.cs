using Com.Alipay;
using Com.Alipay.Business;
using Com.Alipay.Domain;
using Com.Alipay.Model;
using System;

namespace Ticket.Infrastructure.Alipay
{
    /// <summary>
    /// 支付宝退款
    /// </summary>
    public class F2FPayRefund
    {
        /// <summary>
        /// 申请退款
        /// </summary>
        /// <param name="out_trade_no">订单编号</param>
        /// <param name="total_fee">订单总金额(单位：元)</param>
        /// <param name="refund_fee">退款金额(单位：元)</param>
        /// <returns></returns>
        public static bool Run(string out_trade_no, string total_fee)
        {
            IAlipayTradeService serviceClient = F2FBiz.CreateClientInstance(
                F2FPayConfig.serverUrl,
                F2FPayConfig.appId,
                F2FPayConfig.merchant_private_key,
                F2FPayConfig.version,
                F2FPayConfig.sign_type,
                F2FPayConfig.alipay_public_key,
                F2FPayConfig.charset);
            var out_request_no = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString();
            AlipayTradeRefundContentBuilder builder = new AlipayTradeRefundContentBuilder();

            //支付宝交易号与商户网站订单号不能同时为空
            builder.out_trade_no = out_trade_no.Trim();
            //退款请求单号保持唯一性。
            builder.out_request_no = out_request_no;
            //退款金额
            builder.refund_amount = total_fee.Trim();
            builder.refund_reason = "refund reason";
            AlipayF2FRefundResult refundResult = serviceClient.tradeRefund(builder);
            bool isRefund = false;
            string result = "";

            //请在这里加上商户的业务逻辑程序代码
            //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
            switch (refundResult.Status)
            {
                case ResultEnum.SUCCESS:
                    isRefund = true;
                    result = "退款成功";
                    break;
                case ResultEnum.FAILED:
                    result = "退款失败，" + refundResult.response.SubMsg;
                    break;
                case ResultEnum.UNKNOWN:
                    if (refundResult.response == null)
                    {
                        result = "退款失败，配置或网络异常，请检查";
                    }
                    else
                    {
                        result = "退款失败，系统异常，请走人工退款流程";
                    }
                    break;
            }
            return isRefund;
        }
    }
}
