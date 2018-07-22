using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Model.Enum.TraveAgency;
using Ticket.Utility.Helpers;

namespace Ticket.Model.Model.TravelAgency
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string OTABusinessName { get; set; }
        public string OrderNo { get; set; }
        public int BookCount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Linkman { get; set; }
        public string Mobile { get; set; }
        public string IdCard { get; set; }
        public int PlaceOrderType { get; set; }
        public string PlaceOrderTypeName
        {
            get
            {
                return ((TraveAgencyPlaceOrderType)PlaceOrderType).GetDescription();
            }
        }
        public int AuditStatus { get; set; }
        public string AuditStatusName
        {
            get
            {
                return ((TraveAgencyAuditStatus)AuditStatus).GetDescription();
            }
        }
        public int OrderStatus { get; set; }
        public string OrderStatusName
        {
            get
            {
                return ((TraveAgencyOrderStatus)OrderStatus).GetDescription();
            }
        }
        public DateTime CreateTime { get; set; }
        public string ValidityDate { get; set; }
        public string PlaceOrderName { get; set; }
    }
}
