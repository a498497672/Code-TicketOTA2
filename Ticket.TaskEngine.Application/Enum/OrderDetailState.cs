using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Utility.Extensions;

namespace Ticket.TaskEngine.Application.Enum
{
    /// <summary>
    /// 明细订单状态
    /// </summary>
    public enum OrderDetailState
    {
        /// <summary>
        /// F-已释放订单
        /// </summary>
        [Description("F")]
        ReleasedOrder,
        /// <summary>
        /// B-作废
        /// </summary>
        [Description("B")]
        Invalid,
        /// <summary>
        /// R-取消
        /// </summary>
        [Description("R")]
        Cancel,
        /// <summary>
        /// N-未付款
        /// </summary>
        [Description("N")]
        Unpaid,
        /// <summary>
        /// S-已付款
        /// </summary>
        [Description("S")]
        Paid,
        /// <summary>
        /// G-已改签
        /// </summary>
        [Description("G")]
        HasChange,
        /// <summary>
        /// H-已取纸质票
        /// </summary>
        [Description("H")]
        ReceivedTicket,
        /// <summary>
        /// O-线下已捡
        /// </summary>
        [Description("O")]
        CheckTicket,
        /// <summary>
        /// M-已退款
        /// </summary>
        [Description("M")]
        Refunded,
        /// <summary>
        /// E-退款审核中
        /// </summary>
        [Description("E")]
        RefundReview,
        /// <summary>
        /// P-部分退票
        /// </summary>
        [Description("P")]
        PartialRefund,
        /// <summary>
        /// A-全部退票
        /// </summary>
        [Description("A")]
        FullRefund,
    }

    public class StateAction
    {
        public static int GetState(string name)
        {
            int number = -1;
            switch (name.ToUpper())
            {
                case "F": number = (int)OrderDetailState.ReleasedOrder; break;
                case "B": number = (int)OrderDetailState.Invalid; break;
                case "R": number = (int)OrderDetailState.Cancel; break;
                case "N": number = (int)OrderDetailState.Unpaid; break;
                case "S": number = (int)OrderDetailState.Paid; break;
                case "G": number = (int)OrderDetailState.HasChange; break;
                case "H": number = (int)OrderDetailState.ReceivedTicket; break;
                case "O": number = (int)OrderDetailState.CheckTicket; break;
                case "M": number = (int)OrderDetailState.Refunded; break;
                case "E": number = (int)OrderDetailState.RefundReview; break;
                case "P": number = (int)OrderDetailState.PartialRefund; break;
                case "A": number = (int)OrderDetailState.FullRefund; break;
            }

            return number;
        }
    }
}
