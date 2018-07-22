using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 门票状态
    /// </summary>
    public enum TicketDataStatus
    {
        /// <summary>
        /// 是否下架
        /// </summary>
        [Description("是否下架")]
        IsStop = DataStatus.BitOne
    }
}
