using System.Collections.Generic;

namespace Ticket.Infrastructure.Ctrip.Request
{
    /// <summary>
    /// 请求参数
    /// </summary>
    public class NoticeOrderConsumedRequest
    {
        public RequestHeader header { get; set; }
        public string body { get; set; }
    }

    public class NoticeOrderConsumedBodyRequest
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
        public List<NoticeOrderConsumedItemRequest> items { get; set; }
    }
    public class NoticeOrderConsumedItemRequest
    {
        /// <summary>
        /// 订单项编号
        /// </summary>
        public string itemId { get; set; }
        /// <summary>
        /// 实际使用开始日期，格式：“yyyy-MM-dd”
        /// </summary>
        public int useStartDate { get; set; }
        /// <summary>
        /// 实际使用结束日期，格式：“yyyy-MM-dd”
        /// </summary>
        public string useEndDate { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 实际使用数量
        /// </summary>
        public int useQuantity { get; set; }
    }
}
