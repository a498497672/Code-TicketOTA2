namespace Ticket.Infrastructure.KouDaiLingQian.Response
{
    public class MicroOrderResponse
    {
        /// <summary>
        /// 系统时间
        /// </summary>
        public string DateStr { get; set; }
        /// <summary>
        /// 结果码
        /// </summary>
        public string ReturnCode { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string ReturnMessage { get; set; }
        /// <summary>
        /// 商户系统内部的订单号,32 个字符内、可包含字母, 确保在商户系统唯一
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 口袋零钱系统订单号
        /// </summary>
        public string OutChannelNo { get; set; }
        public string BankOrderNo { get; set; }
        /// <summary>
        /// 签名，详见本文档签名说明
        /// </summary>
        public string Sign { get; set; }
    }
}
