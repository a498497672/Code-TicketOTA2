using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.TongCheng.Lib
{
    public class ResultCode
    {
        /// <summary>
        /// 成功（如果贵方系统中订单已经生成或者已经取消就按正常返回）
        /// </summary>
        public static string Success = "1000";

        /// <summary>
        /// 创建订单失败，预订限制
        /// </summary>
        public static string CreateOrderForReservationRestriction = "2001";
        /// <summary>
        /// 创建订单失败，库存不足
        /// </summary>
        public static string CreateOrderForLowStocks = "2003";
        /// <summary>
        /// 创建订单失败，产品下线
        /// </summary>
        public static string CreateOrderForProductDownline = "2004";
        /// <summary>
        /// 创建订单失败，价格不一致
        /// </summary>
        public static string CreateOrderForPriceDisagreement = "2005";

        /// <summary>
        /// 订单取消失败，游客已入园
        /// </summary>
        public static string CancelOrderForConsume = "4001";
        /// <summary>
        /// 订单取消失败，已过期
        /// </summary>
        public static string CancelOrderForExpired = "4002";
        /// <summary>
        /// 订单取消失败，不支持取消
        /// </summary>
        public static string CancelOrderForCancel = "4005";
        /// <summary>
        /// 订单号不存在或已作废
        /// </summary>
        public static string OrderNumberNotExist = "5001";
        /// <summary>
        /// 余额不足
        /// </summary>
        public static string VerifyIncorrectOrderState = "5002";
        /// <summary>
        /// 签名验证失败
        /// </summary>
        public static string SignatureError = "5003";
        /// <summary>
        /// 数据出错或为空
        /// </summary>
        public static string DataError = "5004";
        /// <summary>
        /// 合作方系统出错
        /// </summary>
        public static string Error = "5005";


    }
}
