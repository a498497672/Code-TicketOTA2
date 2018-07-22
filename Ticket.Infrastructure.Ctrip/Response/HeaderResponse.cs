using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ticket.Infrastructure.Ctrip.Response
{
    public class HeaderResponse
    {
        /// <summary>
        /// 返回状态 0000成功
        /// </summary>
        public string resultCode { get; set; }
        /// <summary>
        /// 返回状态信息：
        /// 验证成功，可以下单
        /// 验证失败+具体原因
        /// </summary>
        public string resultMessage { get; set; }
    }
}
