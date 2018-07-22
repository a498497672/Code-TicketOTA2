using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.Ctrip.Request
{
    public class RequestData
    {
        public RequestHeader Header { get; set; }
        public string Body { get; set; }
    }

    public class RequestHeader
    {
        /// <summary>
        /// 供应商系统分配给OTA的账户
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 请求时间,格式：“yyyy-MM-dd HH:mm:ss”
        /// </summary>
        public string RequestTime { get; set; }
        /// <summary>
        /// 版本号，1.0
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Sign { get; set; }
    }
}
