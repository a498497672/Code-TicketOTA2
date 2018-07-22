using System.Xml.Serialization;

namespace Ticket.Infrastructure.Ctrip.Request
{
    public class HeaderRequest
    {
        /// <summary>
        /// 供应商系统分配给OTA的账户
        /// </summary>
        public string accountId { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        public string serviceName { get; set; }
        /// <summary>
        /// 请求时间,格式：“yyyy-MM-dd HH:mm:ss”
        /// </summary>
        public string requestTime { get; set; }
        /// <summary>
        /// 版本号，2.0
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }
}
