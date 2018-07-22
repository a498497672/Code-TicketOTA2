using System;
using System.Collections.Generic;

namespace Ticket.TaskEngine.Application.Model
{
    public class XJ_Order
    {
        /// <summary>
        /// OTA订单Id
        /// </summary>
        public string OrderOtaId { get; set; }
        /// <summary>
        /// 分销商id
        /// </summary>
        public int OTABusinessId { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public double OrderPrice { get; set; }
        /// <summary>
        /// 订票数量
        /// </summary>
        public int OrderQuantity { get; set; }
        /// <summary>
        /// 游玩日期（有效期时间）
        /// </summary>
        public DateTime VisitDate { get; set; }
        /// <summary>
        /// 取票人的信息 
        /// </summary>
        public XJ_ContactPerson ContactPerson { get; set; }
        /// <summary>
        /// 门票
        /// </summary>
        public List<XJ_ProductItem> TicketList { get; set; }
    }



    public class XJ_ProductItem
    {
        /// <summary>
        /// 门票ID(产品id)
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 本产品实际售卖价格
        /// </summary>
        public decimal SellPrice { get; set; }
        /// <summary>
        /// 预订数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 凭证码
        /// </summary>
        public string CodeStr { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// OTA订单详情id
        /// </summary>
        public string OrderDetailId { get; set; }
        /// <summary>
        /// 游玩开始日期(日期格式：yyyy-MM-dd)
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 游玩结束日期(日期格式：yyyy-MM-dd)
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 订单详情状态
        /// </summary>
        public int State { get; set; }
    }

    public class XJ_ContactPerson
    {
        /// <summary>
        /// 购买人姓名
        /// </summary>
        public string BuyName { get; set; }
        /// <summary>
        /// 游玩人姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 游玩人手机号
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 取票人证件类型 身份证 : ID_CARD,    护照 : HUZHAO,    台胞证 : TAIBAO ,港澳通行证: GANGAO 其它：OTHER
        /// </summary>
        public string CardType { get; set; }
        /// <summary>
        /// 取票人证件号
        /// </summary>
        public string CardNo { get; set; }
    }
}
