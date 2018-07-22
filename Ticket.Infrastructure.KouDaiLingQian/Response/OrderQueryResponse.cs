using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.KouDaiLingQian.Response
{
    public class OrderQueryResponse
    {
        /// <summary>
        /// 系统时间
        /// </summary>
        public string DateStr { get; set; }
        /// <summary>
        /// 结果码
        /// </summary>
        public string ReturnCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string ReturnMessage { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public string Data { get; set; }
        public OrderQueryDataResponse QueryData { get; set; }
        /// <summary>
        /// 签名，详见本文档签名说明
        /// </summary>
        public string Sign { get; set; }
    }

    public class OrderQueryDataResponse
    {
        /// <summary>
        /// 平台分配的商户编号
        /// </summary>
        public string MerchantNo { get; set; }
        /// <summary>
        /// 商户系统订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 支付渠道订单号
        /// </summary>
        public string OutChannelNo { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public string Amount { get; set; }
        /// <summary>
        /// 商户系统下单时间，格式为yyyyMMddHHmmss
        /// </summary>
        public string OrderTime { get; set; }
        /// <summary>
        /// 交易成功时间，格式为yyyyMMddHHmmss
        /// </summary>
        public string TransTime { get; set; }
        /// <summary>
        /// 请求来源系统编号
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// 订单状态 O：未支付 P：已支付 C：已取消 R：已退款 I：支付中 N：订单不存在 F：支付失败 T：订单超时
        /// </summary>
        public string PayStatus { get; set; }
        /// <summary>
        /// 支付方式 微信：weixin 支付宝：alipay
        /// </summary>
        public string PayWayCode { get; set; }
        /// <summary>
        /// 用户在支付渠道内的编号
        /// </summary>
        public string CustomerNo { get; set; }
    }
}
