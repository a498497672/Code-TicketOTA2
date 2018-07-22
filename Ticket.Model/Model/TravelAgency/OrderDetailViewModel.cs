using System;
using System.Collections.Generic;

namespace Ticket.Model.Model.TravelAgency
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Linkman { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 有效期时间
        /// </summary>
        public DateTime ValidityDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 下单方式
        /// </summary>
        public string PlaceOrderTypeName { get; set; }
        /// <summary>
        /// 审核状态
        /// </summary>
        public int AuditStatus { get; set; }
        /// <summary>
        /// 驳回时间
        /// </summary>
        public DateTime? RejectTime { get; set; }
        /// <summary>
        /// 驳回原因
        /// </summary>
        public string RejectReason { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// Desc:下单人，起到记录的作用
        /// </summary>           
        public string PlaceOrderName { get; set; }
        /// <summary>
        /// 门票集合
        /// </summary>
        public List<TicketItemModel> TicketItem { get; set; }
        public string TicketItemJson { get; set; }
    }
}
