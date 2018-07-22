using System;

namespace FengjingSDK461.Model.Response
{
    public class ResponseBase
    {
        /// <summary>
        /// 响应时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 验证结果代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 描述(验证结果说明)
        /// </summary>
        public string Message { get; set; }
    }
}
