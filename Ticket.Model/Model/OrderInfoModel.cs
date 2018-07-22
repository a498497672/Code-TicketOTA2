using System;
using System.Collections.Generic;

namespace Ticket.Model.Model
{
    public class OrderInfoModel
    {
        /// <summary>
        /// 票的来源 1：景区自己的;2：OTA
        /// </summary>
        public int TicketSource { get; set; }

        /// <summary>
        /// 票的种类 1：印刷票，2：二维码打印票，3：二维码电子票
        /// </summary>
        public int TicketCategory { get; set; }

        /// <summary>
        /// 支付类型(1:支付宝;2:微信；3：现金)
        /// </summary>
        public int PayType { get; set; }

        /// <summary>
        /// 授权码，用于微信和支付宝
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Linkman { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 有效期时间
        /// </summary>
        public DateTime ValidityDate { get; set; }

        /// <summary>
        /// 门票
        /// </summary>
        public List<TicketItemModel> TicketItem { get; set; }
    }


}
