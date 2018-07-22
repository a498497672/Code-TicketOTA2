using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ticket.Infrastructure.Ctrip.Request
{
    public class CancelOrderRequest
    {
        /// <summary>
        /// 携程处理批次流水号
        /// </summary>
        public string SequenceId { get; set; }
        /// <summary>
        /// 携程订单号
        /// </summary>
        public string OtaOrderId { get; set; }
        /// <summary>
        /// 供应商订单号
        /// </summary>
        public string SupplierOrderId { get; set; }
        /// <summary>
        /// 确认类型：
        ///1.携程自动确认，仅通知供应商订单操作
        ///2.供应商(人工/自动)确认，根据供应商返回结果进行处理
        /// </summary>
        public int ConfirmType { get; set; }

        public List<CancelOrderItemRespose> Items { get; set; }
    }

    public class CancelOrderItemRespose
    {
        /// <summary>
        /// 订单项编号
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// 供应商产品的资源编号
        /// </summary>
        public string PLU { get; set; }
        /// <summary>
        /// 供应商最晚确认时间，格式：“yyyy-MM-ddHH:mm:ss”。该时间为北京时区的时间，当不为空
        /// 且 confirmType=2 时如超过该时间后携程将会进行结果的确认处理（订单会被自动取消）
        /// </summary>
        public string LastConfirmTime { get; set; }
        /// <summary>
        /// 取消数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 被取消的退款金额
        /// </summary>
        public string Amount { get; set; }
        public string AmountCurrency { get; set; }
    }
}
