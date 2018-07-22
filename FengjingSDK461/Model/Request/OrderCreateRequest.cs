using System;
using System.Collections.Generic;

namespace FengjingSDK461.Model.Request
{
    /// <summary>
    /// 创建订单
    /// </summary>
    public class OrderCreateRequest
    {
        public HeadRequest Head { get; set; }
        public OrderCreateBody Body { get; set; }
    }

    public class OrderCreateBody
    {
        public OrderInfo OrderInfo { get; set; }
    }

    public class OrderInfo
    {
        /// <summary>
        /// OTA订单Id
        /// </summary>
        public string OrderOtaId { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// 订票数量
        /// </summary>
        public int OrderQuantity { get; set; }
        /// <summary>
        /// 游玩日期（有效期时间）
        /// </summary>
        public string VisitDate { get; set; }
        /// <summary>
        /// 是否收款 1 OTA用户已付款 0 OTA用户未支付
        /// </summary>
        public int OrderPayStatus { get; set; }

        /// <summary>
        /// 取票人的信息 
        /// </summary>
        public ContactPerson ContactPerson { get; set; }
        /// <summary>
        /// 门票
        /// </summary>
        public List<ProductItem> TicketList { get; set; }
    }



    public class ProductItem
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
        /// ota订单详情id
        /// </summary>
        public string ItemId { get; set; }
    }

    public class ContactPerson
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
