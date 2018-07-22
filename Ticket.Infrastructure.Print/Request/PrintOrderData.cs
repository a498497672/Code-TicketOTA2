namespace Ticket.Infrastructure.Print.Request
{
    public class PrintOrderData
    {
        /// <summary>
        /// 景区名称
        /// </summary>
        public string ScenicName { get; set; }
        /// <summary>
        /// 景区手机号
        /// </summary>
        public string ScenicPhone { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 凭证号
        /// </summary>
        public string CertificateNo { get; set; }
        /// <summary>
        /// 二维码内容
        /// </summary>
        public string QRcode { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 门票名称
        /// </summary>
        public string TicketName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Qunatity { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 打印次数
        /// </summary>
        public int? PrintCount { get; set; }

        public string UserName { get; set; }
        public string RealName { get; set; }
    }
}
