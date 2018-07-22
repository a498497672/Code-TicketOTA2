namespace Ticket.Infrastructure.Print.Request
{
    public class PrintConfigData
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string Partner { get; set; }
        /// <summary>
        /// apikey API 密钥
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// 机器码 终端编号
        /// </summary>
        public string MachineCode { get; set; }
        /// <summary>
        /// 终端密钥
        /// </summary>
        public string MachineKey { get; set; }
    }
}
