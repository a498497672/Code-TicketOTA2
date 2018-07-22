namespace Ticket.Infrastructure.Ctrip.Lib
{
    public class ResultCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        public static string Success = "0000";
        /// <summary>
        /// JSON解析失败
        /// </summary>
        public static string JsonParsingFailure = "0001";
        /// <summary>
        /// XML解析失败
        /// </summary>
        public static string XmlParsingFailure = "0002";
        /// <summary>
        /// 签名错误
        /// </summary>
        public static string SignatureError = "0002";
        /// <summary>
        /// 供应商或OTA账户信息不正确
        /// </summary>
        public static string IncorrectAccountInformation = "0003";
        /// <summary>
        /// 系统出错
        /// </summary>
        public static string SystemError = "0005";


        /// <summary>
        /// 下单失败，产品Id不存在/错误
        /// </summary>
        public static string CreateOrderForProductNotExist = "1001";
        /// <summary>
        /// 下单失败，产品下线
        /// </summary>
        public static string CreateOrderForProductDownline = "1002";
        /// <summary>
        /// 下单失败，库存不足
        /// </summary>
        public static string CreateOrderForLowStocks = "1003";
        /// <summary>
        /// 下单失败，预订限购 如：您已经预订过了，一个手机号1天只能预订1张
        /// </summary>
        public static string CreateOrderForReservationRestriction = "1004";
        /// <summary>
        /// 下单失败，参数为空
        /// </summary>
        public static string CreateOrderForParameterEmpty = "1005";
        /// <summary>
        /// 下单失败，参数不合法
        /// </summary>
        public static string CreateOrderForParameterIllegality = "1006";
        /// <summary>
        /// 下单失败，1007  产品价格不存在
        /// </summary>
        public static string CreateOrderForProductPriceNotExist = "1007";
        /// <summary>
        /// 下单失败，1008  账户余额不足
        /// </summary>
        public static string CreateOrderForBalance = "1008";
        /// <summary>
        /// 下单失败，1009  日期错误+具体错误类型日期
        /// </summary>
        public static string CreateOrderForDate = "1009";
        /// <summary>
        /// 下单失败，1010  不成团
        /// </summary>
        public static string CreateOrderForNotGroup = "1010";





        /// <summary>
        /// 取消失败 该订单号不存在
        /// </summary>
        public static string CancelOrderNumberNotExist = "2001";
        /// <summary>
        /// 取消失败 该订单已经使用
        /// </summary>
        public static string CancelOrderForConsume = "2002";
        /// <summary>
        /// 取消失败 该订单已过期，不可退
        /// </summary>
        public static string CancelOrderForExpired = "2003";
        /// <summary>
        /// 取消失败 取消数量不正确
        /// </summary>
        public static string CancelOrderForNotCount = "2004";
        /// <summary>
        /// 取消失败 该订单不允许退订
        /// </summary>
        public static string CancelOrderForCancel = "2005";
        /// <summary>
        /// 取消失败 供应商不支持退订 该返回码必须和携程方面确认后才能使用
        /// </summary>
        public static string CancelOrderForNotExistCancel = "2006";
        /// <summary>
        /// 取消失败 通用错误
        /// </summary>
        public static string CancelOrderForError = "2100";

        /// <summary>
        /// 查询失败 该订单号不存在
        /// </summary>
        public static string QueryOrderNumberNotExist = "4001";

        /// <summary>
        /// 核单回调 OTA订单号不存在
        /// </summary>
        public static string VerifyOrderNumberNotExist = "2001";
        /// <summary>
        /// 核单回调 订单状态不正确
        /// </summary>
        public static string VerifyIncorrectOrderState = "2002";
    }
}
