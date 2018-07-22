using Ticket.Infrastructure.WxPay.Request;

namespace Ticket.Infrastructure.WxPay
{
    public class Refund
    {
        /***
        * 申请退款完整业务流程逻辑
        * @param transaction_id 微信订单号（优先使用）
        * @param out_trade_no 商户订单号
        * @param total_fee 订单总金额
        * @param refund_fee 退款金额
        * @return 退款结果（xml格式）
        */
        public static string Run(string transaction_id, string out_trade_no, string total_fee, string refund_fee)
        {
            Log.Info("Refund", "Refund is processing...");

            WxPayData data = new WxPayData();
            if (!string.IsNullOrEmpty(transaction_id))//微信订单号存在的条件下，则已微信订单号为准
            {
                data.SetValue("transaction_id", transaction_id);
            }
            else//微信订单号不存在，才根据商户订单号去退款
            {
                data.SetValue("out_trade_no", out_trade_no);
            }

            data.SetValue("total_fee", int.Parse(total_fee));//订单总金额
            data.SetValue("refund_fee", int.Parse(refund_fee));//退款金额
            data.SetValue("out_refund_no", WxPayApi.GenerateOutTradeNo());//随机生成商户退款单号
            data.SetValue("op_user_id", WxPayConfig.MCHID);//操作员，默认为商户号

            WxPayData result = WxPayApi.Refund(data);//提交退款申请给API，接收返回数据

            Log.Info("Refund", "Refund process complete, result : " + result.ToXml());
            return result.ToPrintStr();
        }

        /// <summary>
        /// 申请退款--改写
        /// </summary>
        /// <param name="transaction_id">微信订单号</param>
        /// <param name="total_fee">订单总金额</param>
        /// <param name="refund_fee">退款金额</param>
        /// <param name="outRefundNo">商户退款订单号</param>
        /// <returns></returns>
        public static bool Run(RefundRequest refundRequest)
        {
            Log.Info("Refund", "Refund is processing...");
            WxPayData data = new WxPayData();

            data.SetValue("transaction_id", refundRequest.TransactionId);//微信订单号存在的条件下，则已微信订单号为准
            data.SetValue("total_fee", int.Parse(refundRequest.TotalFee));//订单总金额
            data.SetValue("refund_fee", int.Parse(refundRequest.RefundFee));//退款金额
            data.SetValue("out_refund_no", refundRequest.OutRefundNo);//随机生成商户退款单号
            data.SetValue("op_user_id", WxPayConfig.MCHID);//操作员，默认为商户号

            WxPayData result = WxPayApi.Refund(data);//提交退款申请给API，接收返回数据

            Log.Info("Refund", "Refund process complete, result : " + result.ToXml());

            //退款直接成功
            if (result.GetValue("return_code").ToString() == "SUCCESS" &&
                result.GetValue("result_code").ToString() == "SUCCESS")
            {
                Log.Info("Refund", "Micropay business success, result : " + result.ToXml());
                return true;
            }
            return false;
        }
    }
}