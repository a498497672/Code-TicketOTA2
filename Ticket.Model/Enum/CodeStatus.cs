using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.Enum
{
    public enum CodeStatus
    {
        [Description("订单不存在")]
        NoOrder = 130001,
        [Description("订单已取消")]
        Cancel = 130002,
    }
}
