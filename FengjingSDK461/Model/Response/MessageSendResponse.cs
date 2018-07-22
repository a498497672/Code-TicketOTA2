using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengjingSDK461.Model.Response
{
    public class MessageSendResponse
    {
        public HeadResponse Head { get; set; }
        public MessageSendResponseBody Body { get; set; }
    }
    public class MessageSendResponseBody
    {
        public string Message { get; set; }
    }
}
