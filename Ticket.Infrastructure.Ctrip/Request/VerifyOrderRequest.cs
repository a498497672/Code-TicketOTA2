using System.Collections.Generic;

namespace Ticket.Infrastructure.Ctrip.Request
{
    public class VerifyOrderRequest
    {
        /// <summary>
        /// 携程处理批次流水号
        /// </summary>
        public string SequenceId { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public List<VerifyOrderContacts> Contacts { get; set; }
        public List<VerifyOrderItems> Items { get; set; }
    }

    public class VerifyOrderItems
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public string PLU { get; set; }
        /// <summary>
        /// 结算价
        /// </summary>
        public decimal cost { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public int adjunctions { get; set; }
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
        /// <summary>
        /// 出行人
        /// </summary>
        public List<VerifyOrderPassengers> passengers { get; set; }
    }

    /// <summary>
    /// 出行人
    /// </summary>
    public class VerifyOrderPassengers
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
    public class VerifyOrderContacts
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
}
