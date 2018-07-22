using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengjingSDK461.Model.Request
{
    public class MessageSendRequest
    {
        public HeadRequest Head { get; set; }
        public MessageSendBody Body { get; set; }
    }

    public class MessageSendBody
    {
        public MessageSendOrderInfo OrderInfo { get; set; }
    }

    public class MessageSendOrderInfo
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 重发手机号
        /// </summary>
        public string phoneNumber { get; set; }
    }
}
