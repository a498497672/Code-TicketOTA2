using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.TongCheng.Request
{
    /// <summary>
    /// 创建订单
    /// </summary>
    public class CreateOrderRequest
    {
        /// <summary>
        /// 平台订单流水号
        /// </summary>
        public string orderSerialId { get; set; }
        /// <summary>
        /// 合作方产品编号
        /// </summary>
        public string productNo { get; set; }
        /// <summary>
        /// 合作方景区编号
        /// </summary>
        public string sceneryNo { get; set; }
        /// <summary>
        /// 支付方式(0 景区到付 1 在线支付)
        /// </summary>
        public int payType { get; set; }
        /// <summary>
        /// 预订数量
        /// </summary>
        public int tickets { get; set; }
        /// <summary>
        /// 预订单价(分)
        /// </summary>
        public long price { get; set; }
        /// <summary>
        /// 预定协议单价(分)
        /// </summary>
        public long contractPrice { get; set; }
        /// <summary>
        /// 是否有场次,0：没有，1有
        /// </summary>
        public int? isEvent { get; set; }
        /// <summary>
        /// 场次编号（当isEvent为1时，该参数必传）
        /// </summary>
        public string eventId { get; set; }
        /// <summary>
        /// 预订人姓名
        /// </summary>
        public string bookName { get; set; }
        /// <summary>
        /// 预订人手机号
        /// </summary>
        public string bookMobile { get; set; }
        /// <summary>
        /// 身份证号(根据产品实名制信息传入)
        /// </summary>
        public string idCard { get; set; }
        /// <summary>
        /// 游玩日期(yyyy-MM-dd)
        /// </summary>
        public string travelDate { get; set; }
        /// <summary>
        /// 游玩人信息
        /// </summary>
        public List<VisitPersonRequest> visitPerson { get; set; }
    }

    /// <summary>
    /// 游玩人信息
    /// </summary>
    public class VisitPersonRequest
    {
        /// <summary>
        /// 游玩人姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 游玩人手机号
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 身份证号(根据产品实名制信息传入)
        /// </summary>
        public string idCard { get; set; }
    }
}
