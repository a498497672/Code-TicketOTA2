using System;
using System.Collections.Generic;
using System.Linq;

namespace Ticket.Model.Model.Order
{
   public class OtaOrderViewModel
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// Ota订单号
        /// </summary>
        public string OtaOrderNo { get; set; }

        private int _bookCount;
        /// <summary>
        /// 门票总数
        /// </summary>
        public int BookCount
        {
            get
            {
                if (_bookCount <= 0)
                {
                    _bookCount = this.Details.Sum(p => p.Qunatity);
                }
                return _bookCount;
            }
            set { _bookCount = value; }
        }

        private decimal _totalAmount;
        /// <summary>
        /// 订单总额
        /// </summary>
        public decimal TotalAmount
        {
            get
            {
                if (_totalAmount <= 0)
                {
                    _totalAmount = this.Details.Sum(p => p.Price * p.Qunatity);
                }
                return _totalAmount;
            }
            set { _totalAmount = value; }
        }

        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 游玩日期（取有效期开始日期的日期部分）
        /// <remarks>目前系统的有效期是一个范围由订单表ValidityDateStart和ValidityDateEnd字段决定。但是在实现上目前只是同一天</remarks>
        /// </summary>
        public DateTime TravelDate { get; set; }

        /// <summary>
        /// 分销商Id
        /// </summary>
        public int? OTABusinessId { get; set; }
        /// <summary>
        /// 分销商简称（购票途径）
        /// </summary>
        public string OTABusinessName { get; set; }

        /// <summary>
        /// 购票人
        /// </summary>
        public string LinkName { get; set; }

        /// <summary>
        /// 取票人（游玩人）
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 身份证号（游玩人）
        /// </summary>
        public string IDCard { get; set; }

        private List<OtaOrderDetailViewModel> _details;
        /// <summary>
        /// 订单详情(已初始化)
        /// </summary>
        public List<OtaOrderDetailViewModel> Details
        {
            set { _details = value; }
            get
            {
                if (_details == null)
                {
                    _details = new List<OtaOrderDetailViewModel>();
                }
                return _details;
            }
        }
    }
}
