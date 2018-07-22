using System.Configuration;

namespace Ticket.Infrastructure.Ctrip.Lib
{
    /// <summary>
    /// 携程信息配置
    /// </summary>
    public class CtripConfig
    {
        //测试环境
        //接口帐号(供所有接口使用)  61d68a580c9350bf
        //接口密钥    e4d6d94a8891f319f69fc6328d28e29f
        //AES加密密钥 d2f9c80bdbaa4f9d
        //AES加密初始向量	701e042f8402cf8c
        //接口请求地址
        //通知接口地址 https://ttdstp.ctrip.com/api/order/notice.do

        //生产环境
        //接口帐号(供所有接口使用)  a130ca5c65d8d1fc
        //接口密钥	400becb43650b1a35364631e0a9b2d61
        //AES加密密钥	2d4bed284e727a13
        //AES加密初始向量	6911470e0939d44c
        //接口请求地址
        //通知接口地址 https://ttdstp.ctrip.com/api/order/notice.do

        //=======【基本信息设置】=====================================
        /* 携程信息配置
        * AccountId：OTA分配给供应商的账户（必须配置）
        * Version：版本号（必须配置）
        * Key：密钥（必须配置）
        * Website：请求地址（必须配置）
        * AesKey：AES加密密钥（必须配置）
        * AesIv：AES加密初始向量（必须配置）
        */
        /// <summary>
        /// 携程用户标识
        /// </summary>
        public static readonly string AccountId = ConfigurationManager.AppSettings["Ctrip:AccountId"];
        public static readonly string Version = ConfigurationManager.AppSettings["Ctrip:Version"];
        public static readonly string Key = ConfigurationManager.AppSettings["Ctrip:Key"];
        public static readonly string Website = ConfigurationManager.AppSettings["Ctrip:Website"];
        public static readonly string AesKey = ConfigurationManager.AppSettings["Ctrip:AesKey"];
        public static readonly string AesIv = ConfigurationManager.AppSettings["Ctrip:AesIv"];

        /// <summary>
        /// 供应商分配给携程用户标识
        /// </summary>
        public static readonly string MyAccountId = ConfigurationManager.AppSettings["TicketCtrip:UserId"];
    }
}
