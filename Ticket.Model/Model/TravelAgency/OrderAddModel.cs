using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.Model.TravelAgency
{
    public class OrderAddModel
    {
        public int OtaBusinessId { get; set; }
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
        /// Desc:下单人，起到记录的作用
        /// </summary>           
        public string PlaceOrderName { get; set; }
        /// <summary>
        /// 门票集合
        /// </summary>
        public List<TicketItemModel> TicketItem { get; set; }
    }

    public class TicketItemModel
    {
        /// <summary>
        /// 门票Id 
        /// </summary>
        public int TicketId { get; set; }
        /// <summary>
        /// 门票名称
        /// </summary>
        public string TicketName { get; set; }
        /// <summary>
        /// 预订数量
        /// </summary>
        public int BookCount { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 总额
        /// </summary>
        public decimal TotalAmount { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
    }
}
