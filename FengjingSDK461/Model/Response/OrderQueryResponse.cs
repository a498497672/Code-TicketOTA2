using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengjingSDK461.Model.Response
{
    /// <summary>
    /// 查询订单
    /// </summary>
    public class OrderQueryResponse
    {
        public HeadResponse Head { get; set; }
        public OrderQueryBody Body { get; set; }
    }

    public class OrderQueryBody
    {
        public OrderQueryInfo OrderInfo { get; set; }
    }

    public class OrderQueryInfo
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal OrderPrice { get; set; }
        /// <summary>
        /// 原始订单总票数
        /// </summary>
        public int OrderQuantity { get; set; }
        /// <summary>
        /// 游玩日期 “yyyy-MM-dd”
        /// </summary>
        public string VisitDate { get; set; }
        /// <summary>
        /// 取票人的信息 
        /// </summary>
        public OrderQueryContactPerson ContactPerson { get; set; }

        public List<OrderQueryTicketInfo> EticketInfo { get; set; }
    }

    public class OrderQueryTicketInfo
    {
        /// <summary>
        /// Desc:OTA 订单详情id
        /// </summary>           
        public string OtaOrderDetailId { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 票面价 票面价格 单价
        /// </summary>
        public decimal MarketPrice { get; set; }
        /// <summary>
        /// 销售价 销售产品单价
        /// </summary>
        public decimal SellPrice { get; set; }
        /// <summary>
        /// 票数
        /// </summary>
        public int EticketQuantity { get; set; }
        /// <summary>
        /// 电子凭证号
        /// </summary>
        public string EticektNo { get; set; }
        /// <summary>
        /// 电子票发送状态 1：电子票二维码已发送 0：电子票二维码未发送
        /// </summary>
        public int EticektSend { get; set; }
        /// <summary>
        /// 对应票种已消费的票数
        /// </summary>
        public int UseQuantity { get; set; }
        /// <summary>
        /// 状态 “OREDER_SUCCESS”已支付未消费; “OREDER_CANCEL” 已取消; 
        /// “OREDER_CONSUMED” 已消费 “OREDER_OVERDUE”  已过期
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 1 已支付 2 已取消 3 已消费（已入园） 4 已过期
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 订单创建时间（yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 订单取消时间（yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string CancelTime { get; set; }
        /// <summary>
        /// 入园时间 （yyyy-MM-dd HH:mm:ss）
        /// </summary>
        public string DelayCheckTime { get; set; }
        /// <summary>
        /// 实际使用开始日期，格式：“yyyy-MM-dd”
        /// </summary>
        public string UseStartDate { get; set; }
        /// <summary>
        /// 实际使用结束日期，格式：“yyyy-MM-dd”
        /// </summary>
        public string UseEndDate { get; set; }
    }

    public class OrderQueryContactPerson
    {
        public string Name { get; set; }

        public string Mobile { get; set; }

        public string CardType { get; set; }

        public string CardNo { get; set; }

    }
}
