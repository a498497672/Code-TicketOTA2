using System.Configuration;

namespace Ticket.Utility.Config
{
    public class AppSettingsConfig
    {
        public static string voiceDemain = ConfigurationManager.AppSettings["voiceDemain"];
        public const string QrCodeKey = "fjqrcode";
        /// <summary>
        /// 二维码图片物理路径
        /// </summary>
        public static string QrCodePath = ConfigurationManager.AppSettings["QRCodePath"];

        /// <summary>
        /// 图片服务器 - IIS指向 物理路径
        /// </summary>
        public static string ImgApiPath = ConfigurationManager.AppSettings["ImgApi"];

        public static string PrintApiPath = ConfigurationManager.AppSettings["PrintApi"];
        public static string QrCodeModelPath = ConfigurationManager.AppSettings["QRCodeModel"];

        /// <summary>
        /// 入园凭证短信
        /// </summary>
        public static string ProductOrderInfoPath = ConfigurationManager.AppSettings["ProductOrderInfo"];
        /// <summary>
        /// 退票短信
        /// </summary>
        public static string RefundOrderInfoPath = ConfigurationManager.AppSettings["RefundOrderInfo"];
    }
}
