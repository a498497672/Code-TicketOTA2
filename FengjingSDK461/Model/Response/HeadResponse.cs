using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengjingSDK461.Model.Response
{
    /// <summary>
    /// 应答head
    /// </summary>
    public class HeadResponse
    {
        /// <summary>
        /// 协议版本 协议版本  如：“V1.0”
        /// </summary>
        public string ProtocolVersion { get; set; }
        /// <summary>
        /// 响应用户 固定 “Fengjing”
        /// </summary>
        public string InvokeUser { get; set; }
        /// <summary>
        /// 响应时间 本段报文的生成时间
        /// </summary>
        public string InvokeTime { get; set; }
        /// <summary>
        /// 验证结果 代码 参照验证结果部分错误描述
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 结果描述 结果文字描述
        /// </summary>
        public string Describe { get; set; }
    }
}
