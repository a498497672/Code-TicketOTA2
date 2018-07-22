namespace Ticket.Infrastructure.TongCheng.Response
{
    public class QueryOrderResponse
    {
        /// <summary>
        /// 0.订单未找到 1.订单创建成功 2.订单退款审核中 
        /// 3.订单退款审核成功 4.订单退款审核失败 5.游客已入园
        /// </summary>
        public int orderStatus { get; set; }
        /// <summary>
        /// 操作时间(yyyy-MM-dd HH:mm:ss) 取生单、退款、入园等操作时间
        /// </summary>
        public string operateTime { get; set; }
        /// <summary>
        /// 票数（当状态为0：返回票数为0； 1：返回订单票数； 
        /// 2、3、4：返回退款票数；5：返回入园票数;）
        /// </summary>
        public int tickets { get; set; }
        /// <summary>
        /// 备注信息（当状态为4 退款审核失败时传入审核失败原因）
        /// </summary>
        public string remark { get; set; }
    }
}
