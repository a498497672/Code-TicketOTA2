using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.TongCheng.Request
{
    public class RequestHead
    {
        //[JsonProperty(PropertyName = "user_id")]
        public string user_id { get; set; }
        public string Method { get; set; }
        public string Timestamp { get; set; }
        public string Version { get; set; }
        public string Sign { get; set; }
    }
}
