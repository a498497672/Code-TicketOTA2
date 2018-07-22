using System;

namespace FengjingSDK461.Model.Request
{
    public class RequestBase
    {
        /// <summary>
        /// 分销商身份识别key
        /// </summary>
        public string IdentityKey { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
