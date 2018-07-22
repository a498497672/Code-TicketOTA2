
using System;

namespace Ticket.Model.Model
{
    public class TicketTestingModel
    {
        public int TicketTestingId { get; set; }
        public int OrderDetailId { get; set; }
        public string OrderNo { get; set; }
        public int EnterpriseId { get; set; }
        public int ScenicId { get; set; }
        public int TicketCategory { get; set; }
        public int TicketId { get; set; }
        public string TicketName { get; set; }
        public string BarCode { get; set; }
        public string CertificateNO { get; set; }
        public string QRcode { get; set; }
        public int DataStatus { get; set; }
        public int? CardType { get; set; }
        public string IDCard { get; set; }
        public int Quantity { get; set; }

        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 已入园人数
        /// </summary>
        public int UsedQuantity { get; set; }

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
    }
}
