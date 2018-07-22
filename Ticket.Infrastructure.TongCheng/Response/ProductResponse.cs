using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.TongCheng.Response
{
    /// <summary>
    /// 产品
    /// </summary>
    public class ProductResponse
    {
        public long totalCount { get; set; }
        public List<ProductItemResponse> productList { get; set; }

    }

    public class ProductItemResponse
    {
        /// <summary>
        /// 合作方产品编号
        /// </summary>
        public string productNo { get; set; }
        /// <summary>
        /// 合作方产品名称
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 门市价(单位:分)
        /// </summary>
        public long retailPrice { get; set; }
        /// <summary>
        /// 网络价(单位:分)
        /// </summary>
        public long webPrice { get; set; }
        /// <summary>
        /// 协议价(单位:分)
        /// </summary>
        public long contractPrice { get; set; }
        /// <summary>
        /// 合作方景区编号
        /// </summary>
        public string sceneryNo { get; set; }
        /// <summary>
        /// 合作方景区名称
        /// </summary>
        public string sceneryName { get; set; }
        /// <summary>
        /// 支付方式(0 未区分，1 景区到付,2 在线支付)
        /// </summary>
        public int payType { get; set; }
        /// <summary>
        /// 开始售卖日期(格式:yyyy-MM-dd)
        /// </summary>
        public string beginSaleDate { get; set; }
        /// <summary>
        /// 结束售卖日期(格式:yyyy-MM-dd)
        /// </summary>
        public string endSaleDate { get; set; }
        /// <summary>
        /// 有效期开始时间(格式:yyyy-MM-dd)
        /// </summary>
        public string beginValidDate { get; set; }
        /// <summary>
        /// 有效期结束时间(格式:yyyy-MM-d
        /// </summary>
        public string endValidDate { get; set; }
        /// <summary>
        /// 屏蔽日期(yyyy-MM-dd,yyyy-MM-dd)
        /// </summary>
        public string shieldDate { get; set; }
        /// <summary>
        /// 是否支持实名制(0 不支持 1 支持)
        /// </summary>
        public int? isRealName { get; set; }
        /// <summary>
        /// 验证方式 0 身份证 1 手机号 2手机发送识别码
        /// </summary>
        public int? checkWay { get; set; }
        /// <summary>
        /// 取票方式
        /// </summary>
        public string getTicketWay { get; set; }
        /// <summary>
        /// 是否支持退款 0 不支持 1 支持
        /// </summary>
        public int? isCanRefund { get; set; }
        /// <summary>
        /// 是否支持过期退款 0 不支持 1 支持
        /// </summary>
        public int? isCanOverdueRefund { get; set; }
        /// <summary>
        /// 是否存在有效期（0 不存在 1 存在）
        /// </summary>
        public int? isExistExpiryDate { get; set; }
        /// <summary>
        /// 有效期天数(isExistExpiryDate=1)和isExistExpiryDate配合使用只有存在有效期时这个值才有作用
        /// </summary>
        public int? expiryDays { get; set; }
        /// <summary>
        /// 是否支持场次 0 不支持 1 支持
        /// </summary>
        public int? isEvent { get; set; }
        /// <summary>
        /// 是否存在库存 0 不存在 1 存在
        /// </summary>
        public int? isStock { get; set; }
        /// <summary>
        /// 预订限制信息
        /// </summary>
        public ProductlimitRuleResponse limitRule { get; set; }
    }

    /// <summary>
    /// 预订限制信息
    /// </summary>
    public class ProductlimitRuleResponse
    {
        /// <summary>
        /// 最大预订数量
        /// </summary>
        public int? maxNum { get; set; }
        /// <summary>
        /// 最小预订数量
        /// </summary>
        public int? minNum { get; set; }
        /// <summary>
        /// 开始年龄
        /// </summary>
        public int? beginAge { get; set; }
        /// <summary>
        /// 结束年龄
        /// </summary>
        public int? endAge { get; set; }
        /// <summary>
        /// 下单限制时间(HH:mm:ss)
        /// </summary>
        public string timeLimit { get; set; }
        /// <summary>
        /// 下单提前天数（0 无限制， 1 提前1天，2提前2天）
        /// </summary>
        public int? advanceLimit { get; set; }
        /// <summary>
        /// 限制购买天数（一定要比提前天数大，0表示无限制）
        /// </summary>
        public int? limitBuyDay { get; set; }
        /// <summary>
        /// 全天可预订最晚下单时间(HH:mm:ss)
        /// </summary>
        public string lastOrderTimeLimit { get; set; }
        /// <summary>
        /// 间隔类型0无；1一天；2一周；3一月；4一年
        /// </summary>
        public int? intervalType { get; set; }
        /// <summary>
        /// 间隔次数
        /// </summary>
        public int? intervalTime { get; set; }
        /// <summary>
        /// 间隔总票数默认0
        /// </summary>
        public int? ticketNumber { get; set; }
    }
}
