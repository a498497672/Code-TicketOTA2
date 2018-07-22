using SqlSugar;
using System;
using System.Collections.Generic;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;

namespace Ticket.Core.Service
{
    /// <summary>
    /// 订单出行通知服务（携程）
    /// </summary>
    public class OrderTravelNoticeService
    {
        private readonly OrderTravelNoticeRepository _orderTravelNoticeRepository;

        public OrderTravelNoticeService(OrderTravelNoticeRepository orderTravelNoticeRepository)
        {
            _orderTravelNoticeRepository = orderTravelNoticeRepository;
        }

        public List<Tbl_OrderTravelNotice> GetList(int count = 100)
        {
            var nowDate = DateTime.Now.Date;
            return _orderTravelNoticeRepository.GetAll()
                 .Where(a => a.RunCount < 3)
                 .OrderBy(a => a.CreateTime, OrderByType.Desc)
                 .Take(count)
                 .ToList();
        }

        /// <summary>
        /// 添加订单出行通知
        /// </summary>
        /// <param name="tbl_Order"></param>
        /// <param name="tbl_OrderDetail"></param>
        /// <param name="business"></param>
        public void Add(Tbl_Order tbl_Order, Tbl_OTABusiness business)
        {
            var tbl_OrderTravelNotice = new Tbl_OrderTravelNotice
            {
                SequenceId = DateTime.Now.ToString("yyyyMMdd") + Guid.NewGuid().ToString("N"),
                ScenicId = tbl_Order.ScenicId,
                IdentityKey = business.IdentityKey,
                OrderNo = tbl_Order.OrderNo,
                OtaOrderId = tbl_Order.OTAOrderNo,
                RunCount = 0,
                CreateTime = DateTime.Now
            };
            _orderTravelNoticeRepository.Add(tbl_OrderTravelNotice);
        }

        public void Update(string orderNo, int runCount)
        {
            var model = _orderTravelNoticeRepository.FirstOrDefault(a => a.OrderNo == orderNo);
            if (model != null)
            {
                model.RunCount = runCount;
                _orderTravelNoticeRepository.Update(model);
            }
        }
    }
}
