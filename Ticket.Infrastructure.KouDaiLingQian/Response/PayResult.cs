using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.KouDaiLingQian.Response
{
    public class PayResult
    {
        /// <summary>
        ///  是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 商户订单号,可以查单和退单
        /// </summary>
        public string OutTradeNo { get; set; }
    }
}
