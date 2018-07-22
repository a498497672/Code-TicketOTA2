using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.TongCheng.Response
{
    public class CreateOrderResponse
    {
        /// <summary>
        /// 合作方订单号
        /// </summary>
        public string partnerOrderId { get; set; }
        /// <summary>
        /// 合作方入园辅助码
        /// </summary>
        public string partnerCode { get; set; }
        /// <summary>
        /// 合作二维码链接地址
        /// </summary>
        public string partnerQRCodeAddress { get; set; }
        /// <summary>
        /// 场次信息
        /// </summary>
        public string partnerAreaInfo { get; set; }
    }
}
