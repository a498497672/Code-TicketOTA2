using System;

namespace FengjingSDK461.Model.Response
{
    public class ProductInfos
    {
        /// <summary>
        /// 门票(产品)id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 景区id
        /// </summary>
        public int ScenicId { get; set; }
        /// <summary>
        /// 景区名
        /// </summary>
        public string ScenicName { get; set; }
        /// <summary>
        /// 目的地城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 景区具体地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 门票名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 售卖有效期开始
        /// </summary>
        public DateTime ExpiryDateStart { get; set; }
        /// <summary>
        /// 售卖有效期结束
        /// </summary>
        public DateTime ExpiryDateEnd { get; set; }
        /// <summary>
        /// 市场价格
        /// </summary>
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal SalePrice { get; set; }
        /// <summary>
        /// 日库存量(0:表示不限制)
        /// </summary>
        public int StockCount { get; set; }
        /// <summary>
        /// 延时验证(默认：0。即可马上验证) 分钟
        /// </summary>
        public int? DelayCheck { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 购票说明
        /// </summary>
        public string TicketNotice { get; set; }
        /// <summary>
        /// 入园说明
        /// </summary>
        public string EnterNotice { get; set; }
        /// <summary>
        /// 退订规则
        /// </summary>
        public string RefundNotice { get; set; }
        /// <summary>
        /// 是否支持退款(默认否)
        /// </summary>
        public bool CanRefund { get; set; }
        /// <summary>
        /// 有效期前(后)可退款天数(负数表示有效期前,正数表示有效期后;与RefundHour和RefundMinute共同计算可退款时间)
        /// </summary>
        public int? RefundDay { get; set; }
        /// <summary>
        /// 有效期前(后)可退款小时数
        /// </summary>
        public int? RefundHour { get; set; }
        /// <summary>
        /// 有效期前(后)可退款分钟数
        /// </summary>
        public int? RefundMinute { get; set; }
        /// <summary>
        /// 是否支持修改已支付订单（默认否）
        /// </summary>
        public bool CanModify { get; set; }
        /// <summary>
        /// 有效期前可修改已支付订单的天数
        /// </summary>
        public int? ModifyDay { get; set; }
        /// <summary>
        /// 有效期前可修改已支付订单的小时数
        /// </summary>
        public int? ModifyHour { get; set; }
        /// <summary>
        /// 有效期前可修改已支付订单的分钟数
        /// </summary>
        public int? ModifyMinute { get; set; }
    }
}
