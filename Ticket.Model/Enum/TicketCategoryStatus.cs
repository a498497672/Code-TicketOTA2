using System.ComponentModel;

namespace Ticket.Model.Enum
{
    /// <summary>
    /// 票形式
    /// </summary>
    public enum TicketCategoryStatus
    {
        /// <summary>
        /// 印刷票
        /// </summary>
        [Description("印刷票")]
        PrintedTicket = 1,

        /// <summary>
        /// 二维码打印票
        /// </summary>
        [Description("二维码打印票")]
        QrCodePrintTicket = 2,

        /// <summary>
        /// 二维码电子票
        /// </summary>
        [Description("二维码电子票")]
        QrCodeElectronTicket = 3,
    }
}
