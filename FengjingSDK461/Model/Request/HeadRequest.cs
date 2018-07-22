using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengjingSDK461.Model.Request
{
    /// <summary>
    /// 请求head
    /// </summary>
    public class HeadRequest
    {
        /// <summary>
        /// 协议版本 协议版本  如：“V1.0”
        /// </summary>
        public string ProtocolVersion { get; set; }
        /// <summary>
        /// 请求用户 分销商ID
        /// </summary>
        //[JsonProperty("invokeUser")]
        public string InvokeUser { get; set; }
        /// <summary>
        /// 创建时间 本段报文的生成时间
        /// </summary>
        //[JsonProperty("invokeTime")]
        public string InvokeTime { get; set; }
        /// <summary>
        /// 保留,不使用 填""
        /// </summary>
        //[JsonProperty("reserved")]
        public string Reserved { get; set; }
    }
}
