using System.Collections.Generic;

namespace Ticket.Infrastructure.Ctrip.Response
{
    public class VerifyOrderResponse 
    {
        public HeaderResponse header { get; set; }
        public string body { get; set; }
    }

    public class VerifyOrderBodyRespose
    {
        public List<VerifyOrderItemRespose> Items { get; set; }
    }

    public class VerifyOrderItemRespose
    {
        public string PLU { get; set; }
        public VerifyOrderInventoryRespose inventorys { get; set; }
    }

    public class VerifyOrderInventoryRespose
    {
        /// <summary>
        /// 使用时间
        /// </summary>
        public string useDate { get; set; }
        /// <summary>
        /// 使用时间剩余库存数
        /// </summary>
        public int quantity { get; set; }
    }
}
