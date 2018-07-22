namespace Ticket.Utility.Key
{
    public class MessageKey
    {
        /// <summary>
        /// 门票正在使用，请稍后再试
        /// </summary>
        public const string TicketBeingUsed = "门票正在使用，请稍后再试";
        /// <summary>
        /// 无效票
        /// </summary>
        public const string InvalidTicket = "无效票";

        /// <summary>
        /// 票过期
        /// </summary>
        public const string OverdueTicket = "票已过期";

        /// <summary>
        /// 验证通过
        /// </summary>
        public const string VerifyThroughTicket = "验证通过，{0} {1}人 ，可用{2}人.";

        /// <summary>
        /// 已使用
        /// </summary>
        public const string TicketEmploy = "已使用";

        /// <summary>
        /// 票已退订
        /// </summary>
        public const string TicketRefund = "票已退订";

        /// <summary>
        /// 该闸机不能验此票(设置了闸机不验此票时提示)
        /// </summary>
        public const string TicketNoValidate = "该闸机不能验此票";
    }
}
