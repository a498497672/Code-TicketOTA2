using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ticket.Infrastructure.Ctrip.Response
{
    public class QueryOrderResponse
    {
        public HeaderResponse header { get; set; }
        public string body { get; set; }
    }

    public class QueryOrderBodyResponse
    {
        /// <summary>
        /// 携程订单号
        /// </summary>
        public string OtaOrderId { get; set; }
        /// <summary>
        /// 供应商订单号
        /// </summary>
        public string SupplierOrderId { get; set; }

        public List<QueryOrderitemRespose> items { get; set; }
    }

    public class QueryOrderitemRespose
    {
        /// <summary>
        /// 订单项编号
        /// </summary>
        public string itemId { get; set; }
        /// <summary>
        /// 实际使用开始日期，格式：“yyyy-MM-dd”
        /// </summary>
        public string useStartDate { get; set; }
        /// <summary>
        /// 实际使用结束日期，格式：“yyyy-MM-dd”
        /// </summary>
        public string useEndDate { get; set; }
        /// <summary>
        /// 订单状态  
        /// 1  新订待确认
        /// 2  新订已确认
        /// 3  取消待确认
        /// 4  部分取消
        /// 5  全部取消
        /// 6  已取物品（票券、物件）
        /// 7  部分使用
        /// 8  全部使用
        /// 9  已还物品（票券、物件）
        /// 10 已过期
        /// </summary>
        public int orderStatus { get; set; }
        /// <summary>
        /// 订单数量
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 实际使用数量
        /// </summary>
        public int useQuantity { get; set; }
        /// <summary>
        /// 实际取消数量
        /// </summary>
        public int cancelQuantity { get; set; }
    }
}
