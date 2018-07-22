using System;

namespace Ticket.Model.Model.Order
{
    public class OrderDetailsValidateModel
    {
        public string OrderNo { get; set; }
        public int TicketId { get; set; }
        public decimal Price { get; set; }
        public int OrderStatus { get; set; }
        /// <summary>
        /// 票的有效期,开始时间
        /// </summary>
        public DateTime ValidityDateStart { get; set; }
        /// <summary>
        /// 票的有效期,结束时间
        /// </summary>
        public DateTime ValidityDateEnd { get; set; }
        /// <summary>
        /// 延时验票时间
        /// </summary>
        public DateTime? DelayCheckTime { get; set; }
        /// <summary>
        /// Desc:1：默认全部通过，2：全不通过，3：指定闸机（此时和闸机关联表联合）
        /// </summary>           
        public int CheckWay { get; set; }
    }
}
