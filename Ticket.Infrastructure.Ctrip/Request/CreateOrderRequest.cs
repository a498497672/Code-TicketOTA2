using System.Collections.Generic;
using System.Xml.Serialization;

namespace Ticket.Infrastructure.Ctrip.Request
{
    public class CreateOrderRequest
    {
        /// <summary>
        /// 携程订单号
        /// </summary>
        public string otaOrderId { get; set; }
        /// <summary>
        /// 确认类型 1 携程确认 2 供应商确认
        /// </summary>
        public int confirmType { get; set; }
        /// <summary>
        /// 携程处理批次流水号
        /// </summary>
        public string SequenceId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public List<CreateOrderContacts> Contacts { get; set; }
        public List<CreateOrderItems> Items { get; set; }
    }
    public class CreateOrderItems
    {
        /// <summary>
        /// 订单项编号
        /// </summary>
        public string itemId { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        public string PLU { get; set; }

        /// <summary>
        /// 结算价
        /// </summary>
        public decimal cost { get; set; }

        /// <summary>
        /// 订单数量
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 游玩（使用）结束时间
        /// </summary>
        public string useEndDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string priceCurrency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string costCurrency { get; set; }
        /// <summary>
        /// 游玩（使用）开始时间，格式：“yyyy-MM-dd”
        /// </summary>
        public string useStartDate { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal price { get; set; }

        public string lastConfirmTime { get; set; }
        /// <summary>
        /// 出行人
        /// </summary>
        public List<CreateOrderPassengers> passengers { get; set; }
        public CreateOrderDeposit deposit { get; set; }
    }

    /// <summary>
    /// 出行人
    /// </summary>
    public class CreateOrderPassengers
    {
        public string name { get; set; }
        public string mobile { get; set; }
        public string cardType { get; set; }
        public string cardNo { get; set; }


        public string intlCode { get; set; }
        public string ageType { get; set; }
        public string lastName { get; set; }
        public string weight { get; set; }
        public string myopiaDegreeR { get; set; }
        public string shoeSize { get; set; }
        public string lastConfirmTime { get; set; }
        public string cardIssueCountry { get; set; }
        public string cardIssueDate { get; set; }
        public string birthPlace { get; set; }
        public string cardIssuePlace { get; set; }
        public string cardValidDate { get; set; }
        public string height { get; set; }
        public string nationalityCode { get; set; }
        public string gender { get; set; }
        public string birthDate { get; set; }
        public string myopiaDegreeL { get; set; }
        public string nationalityName { get; set; }
        public string firstName { get; set; }
    }

    /// <summary>
    /// 联系人
    /// </summary>
    public class CreateOrderContacts
    {
        /// <summary>
        /// 出行人姓名，需要支持中/英文
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 出行人手机号
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 电话区号
        /// </summary>
        public string intlCode { get; set; }
        /// <summary>
        /// 备选手机号
        /// </summary>
        public string optionalMobile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
    }
    /// <summary>
    /// 押金、保证金
    /// </summary>
    public class CreateOrderDeposit
    {
        /// <summary>
        /// 类型 1 网站预付 2 景区现付
        /// </summary>
        public int type { get; set; }
        public string amount { get; set; }
    }
}
