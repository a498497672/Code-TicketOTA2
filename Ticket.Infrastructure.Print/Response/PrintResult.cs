namespace Ticket.Infrastructure.Print.Response
{
    public class PrintResult
    {
        /// <summary>
        ///  是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
    }
}
