using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;

namespace Ticket.Core.Service
{
    /// <summary>
    /// 订单消费通知服务
    /// </summary>
    public class NoticeOrderConsumedService
    {
        private readonly NoticeOrderConsumedRepository _noticeOrderConsumedRepository;

        public NoticeOrderConsumedService(NoticeOrderConsumedRepository noticeOrderConsumedRepository)
        {
            _noticeOrderConsumedRepository = noticeOrderConsumedRepository;
        }

        public List<Tbl_NoticeOrderConsumed> GetList(int count = 20)
        {
            var nowDate = DateTime.Now.Date;
            return _noticeOrderConsumedRepository.GetAll()
                 .Where(a => a.IsTaken == true && a.UseDate >= nowDate && a.RunCount < 3)
                 .OrderBy(a => a.CreateTime, OrderByType.Desc)
                 .Take(count)
                 .ToList();
        }

        /// <summary>
        /// 添加订单消费通知
        /// </summary>
        /// <param name="tbl_Order"></param>
        /// <param name="tbl_OrderDetail"></param>
        /// <param name="business"></param>
        public void Add(Tbl_Order tbl_Order, Tbl_OrderDetail tbl_OrderDetail, Tbl_OTABusiness business)
        {
            var tbl_NoticeOrderConsumed = new Tbl_NoticeOrderConsumed
            {
                SequenceId = DateTime.Now.ToString("yyyyMMdd") + Guid.NewGuid().ToString("N"),
                ScenicId = tbl_Order.ScenicId,
                IdentityKey = business.IdentityKey,
                OrderNo = tbl_Order.OrderNo,
                OtaOrderId = tbl_Order.OTAOrderNo,
                OrderDetailNumber = tbl_OrderDetail.Number,
                OtaOrderDetailId = tbl_OrderDetail.OtaOrderDetailId,
                Count = tbl_OrderDetail.Quantity,
                IsTaken = false,
                RunCount = 0,
                CreateTime = DateTime.Now
            };
            _noticeOrderConsumedRepository.Add(tbl_NoticeOrderConsumed);
        }

        /// <summary>
        /// 添加订单消费通知
        /// </summary>
        /// <param name="tbl_Order"></param>
        /// <param name="tbl_OrderDetail"></param>
        /// <param name="business"></param>
        public void Add(Tbl_Order tbl_Order, List<Tbl_OrderDetail> tbl_OrderDetails, Tbl_OTABusiness business)
        {
            var list = new List<Tbl_NoticeOrderConsumed>();
            foreach (var row in tbl_OrderDetails)
            {
                list.Add(new Tbl_NoticeOrderConsumed
                {
                    SequenceId = DateTime.Now.ToString("yyyyMMdd") + Guid.NewGuid().ToString("N"),
                    ScenicId = tbl_Order.ScenicId,
                    IdentityKey = business.IdentityKey,
                    OrderNo = tbl_Order.OrderNo,
                    OtaOrderId = tbl_Order.OTAOrderNo,
                    OtaOrderDetailId = row.OtaOrderDetailId,
                    OrderDetailNumber = row.Number,
                    Count = row.Quantity,
                    IsTaken = false,
                    RunCount = 0,
                    CreateTime = DateTime.Now
                });
            }
            _noticeOrderConsumedRepository.Add(list);
        }

        public void Update(Tbl_OrderDetail tbl_OrderDetail)
        {
            if (tbl_OrderDetail == null)
            {
                return;
            }
            var model = _noticeOrderConsumedRepository.FirstOrDefault(a => a.OrderDetailNumber == tbl_OrderDetail.Number);
            if (model != null)
            {
                model.IsTaken = true;
                model.UseDate = DateTime.Now.Date;
                _noticeOrderConsumedRepository.Update(model);
            }
        }

        public void Update(string orderNo, int runCount)
        {
            var model = _noticeOrderConsumedRepository.FirstOrDefault(a => a.OrderNo == orderNo);
            if (model != null)
            {
                model.RunCount = runCount;
                model.UseDate = DateTime.Now.Date;
                _noticeOrderConsumedRepository.Update(model);
            }
        }
    }
}
