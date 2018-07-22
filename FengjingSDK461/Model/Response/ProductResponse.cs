using System.Collections.Generic;

namespace FengjingSDK461.Model.Response
{
    /// <summary>
    /// 产品
    /// </summary>
    public class ProductResponse
    {
        public HeadResponse Head { get; set; }
        public ProductPage Body { get; set; }
    }

    public class ProductPage
    {
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 产品列表
        /// </summary>
        public List<ProductInfo> ProductList { get; set; }
    }

    /// <summary>
    /// 产品基本信息
    /// </summary>
    public class ProductInfo
    {
        /// <summary>
        /// 供应商产品ID 供应商产品ID（唯一）
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 有效期开始时间(格式:yyyy-MM-dd)
        /// </summary>
        public string BeginValidDate { get; set; }
        /// <summary>
        /// 有效期结束时间(格式:yyyy-MM-dd)
        /// </summary>
        public string EndValidDate { get; set; }
        /// <summary>
        /// 景区名称 罗浮山风景名胜区
        /// </summary>
        public string SightName { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 产品价格信息
        /// </summary>
        public PriceInfo PriceInfo { get; set; }
        /// <summary>
        /// 产品预定规则
        /// </summary>
        public BookInfo BookInfo { get; set; }
        /// <summary>
        /// 产品取消规则
        /// </summary>
        public CancelConfig CancelConfig { get; set; }
        /// <summary>
        /// 其他配置信息
        /// </summary>
        public OtherConfig OtherConfig { get; set; }
        /// <summary>
        /// 使用说明 格式：文本 300 字以内 含费用说明、使用说明、退款说明
        /// </summary>
        public string Remind { get; set; }
    }

    /// <summary>
    /// 产品价格信息
    /// </summary>
    public class PriceInfo
    {
        /// <summary>
        /// 日期	“yyyy-MM-dd”
        /// </summary>
        public string UseDate { get; set; }
        /// <summary>
        /// 票面价 票面价格单价
        /// </summary>
        public decimal MarketPrice { get; set; }
        /// <summary>
        /// 销售价 销售产品单价
        /// </summary>
        public decimal SellPrice { get; set; }
        /// <summary>
        /// 当日库存	-1，表示没有库存概念
        /// </summary>
        public int SellStock { get; set; }
    }

    /// <summary>
    /// 产品预定规则
    /// </summary>
    public class BookInfo
    {
        /// <summary>
        /// 支付方式 ：	“PREPAY”：在线支付，OTA直接收费 “CASHPAY”: 景区现付,目前只支持“PREPAY”模式。
        /// </summary>
        public string PaymentType { get; set; }
        /// <summary>
        /// 预订限制：提前预定天数 例如 “0”，即当天
        /// </summary>
        public int BookAdvanceDay { get; set; }
        /// <summary>
        /// 预订限制: 提前 预定时间   hh:mm 例如“14:00”，今日的14:00分之前可以预 订 ，同 bookadvanceDay  共同生效
        /// </summary>
        public string BookAdvanceTime { get; set; }
        /// <summary>
        /// 使用限制: 预定后几小时才能入园 例如：该值为 “2”. 用户 8 点订票 出票，最早需 10 点才能进行入园。（单位：小时）
        /// </summary>
        public int UseAdvanceHour { get; set; }
        /// <summary>
        /// 使用限制：不支付自动取消 下单后多少分钟不支付自动取消订单；例如：120 分钟，填“120” 未使用，目前填“”
        /// </summary>
        public int AutoCancelTime { get; set; }
        /// <summary>
        /// 使用限制: 是否需要游客信息	 “CONTACT_PERSON”：只需要取票 人信息 ；                       “CONTACT_PERSON_AND_VISIT_P ERSON”：需要游客和取票人信息 目前仅支持CONTACT_PERSON
        /// </summary>
        public string BookPersonType { get; set; }
        /// <summary>
        /// 每几个游客共享 仅bookPersonType 为CONTACT_PERSON_AND_VISIT_P ERSON有效 目前保留未使用，填“”
        /// </summary>
        public string VisitPersonRequi { get; set; }
        /// <summary>
        /// 是否游玩日有效期设置	0无设置；1设置 目前固定是 1
        /// </summary>
        public int ValidType { get; set; }
        /// <summary>
        /// 游客选定的游玩日期起 x天内有效    validType =1时生效。注：1=当日
        /// </summary>
        public int DaysAfterUseDate { get; set; }
    }

    /// <summary>
    /// 产品取消规则
    /// </summary>
    public class CancelConfig
    {
        /// <summary>
        /// 是否支持取消	0，不支持；1支持
        /// </summary>
        public int CanCancel { get; set; }
        /// <summary>
        /// 最晚有效期前几天几点可取消   格式：x_hh:mm 如：2_22:00, 表示最晚有效期前 2 天22 点之 前可退款
        /// </summary>
        public string CancelApplyTimeBeforeValidEndDay { get; set; }

    }

    /// <summary>
    /// 其他配置信息
    /// </summary>
    public class OtherConfig
    {
        /// <summary>
        /// 短信模板
        /// </summary>
        public string SmsTemplet { get; set; }
        /// <summary>
        /// 电子票类型	”C_CODE”: 只提供的二维码作为电子票 ；
        /// </summary>
        public string ETicketType { get; set; }
        /// <summary>
        /// 电子票发送方	0：由风景网发 1： 只OTA发 只支持方式0
        /// </summary>
        public int TicketWhoSent { get; set; }
    }
}
