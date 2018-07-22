using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Model.Enum;
using Ticket.Utility.Extensions;

namespace Ticket.Model.Model.Order
{
    public class OrderViewModel
    {
        public string OrderNo { get; set; }
        public int BookCount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Linkman { get; set; }
        public string Mobile { get; set; }
        public string IDCard { get; set; }
        public int PayType { get; set; }
        public string PayTypeName
        {
            get
            {
                return ((PayStatus)PayType).GetDescriptionByName();
            }
        }
        public bool SalyType { get; set; }
        public System.DateTime? CreateTime { get; set; }
        public System.DateTime? ValidityDateStart { get; set; }
        public System.DateTime? ValidityDateEnd { get; set; }

        public bool IsPrintT { get; set; }
        public bool IsTwo { get; set; }
        public bool IsYearTicket { get; set; }
        /// <summary>
        /// 更多
        /// </summary>
        public bool HasMore { get; set; }

        /// <summary>
        /// 标记是否允许退票（true：允许退票）
        /// </summary>
        public bool CanRefund { get; set; }
        public int? PrintCount { get; set; }

        /// <summary>
        /// 标记是否已经退票 如果已退票 CreateTime 显示退票时间
        /// </summary>
        public bool IsRefunded { get; set; }

        public string BusinessName { get; set; }
        public List<OdtDtl> ListDtl { get; set; }
    }

    public class OdtDtl
    {
        public int OrderDetailId { get; set; }
        public int ScenicId { get; set; }
        public int TicketCategory { get; set; }
        public string TickCateStr
        {
            get
            {
                if (TicketCategory == 1)
                    return "印刷票";
                else if (TicketCategory == 2)
                    return "二维码打印票";
                else if (TicketCategory == 3)
                    return "二维码电子票";
                else if (TicketCategory == 4)
                    return "印刷票";
                else
                    return "-";
            }
        }
        public int TicketId { get; set; }
        public string TicketName { get; set; }

        /// <summary>
        /// 截取名称 （修复门票名称显示bug）
        /// </summary>
        public string ShortTicketName
        {
            get
            {
                var str = TicketName;
                if (!string.IsNullOrWhiteSpace(str))
                {
                    var bytes = Encoding.Default.GetBytes(str);
                    if (bytes.Length > 12)
                    {
                        str = Encoding.Default.GetString(bytes, 0, 12);
                    }
                }
                return str;
            }
        }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public string CertificateNO { get; set; }
        public string BarCodeSection { get; set; }
        /// <summary>
        /// 数据收起的全部的条形码（用逗号隔开）
        /// </summary>
        public string Barcodes { get; set; }
        public string CertificateNOStr
        {
            get
            {
                if (string.IsNullOrEmpty(CertificateNO) || CertificateNO == " ")
                    return "-";
                else
                    return CertificateNO;
            }
        }
        public string Mobile { get; set; }
        public string MobileStr
        {
            get
            {
                if (string.IsNullOrEmpty(Mobile) || Mobile == " ")
                    return "-";
                else
                    return Mobile;
            }
        }
        public string IDCard { get; set; }
        public string IDCardStr
        {
            get
            {
                if (string.IsNullOrEmpty(IDCard))
                    return "-";
                else if (string.IsNullOrEmpty(IDCard.Trim()))
                    return "-";
                else
                    return IDCard;
            }
        }
        public int OrderStatus { get; set; }
        public string OrderStatusName
        {
            get
            {
                return ((OrderDetailsDataStatus)OrderStatus).GetDescriptionByName();
            }
        }

        public int? PrintCount { get; set; }

        /// <summary>
        /// 是否可退款
        /// </summary>
        public bool CanRefund { get; set; }
        public DateTime? RefundTime { get; set; }
    }
}
