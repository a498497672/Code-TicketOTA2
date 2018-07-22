using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;
using Ticket.Model.Enum;

namespace Ticket.Core.Service
{
    public class TicketConsumeService
    {
        private readonly TicketConsumeRepository _ticketConsumeRepository;
        private readonly OrderService _orderService;
        public TicketConsumeService(TicketConsumeRepository ticketConsumeRepository, OrderService orderService)
        {
            _ticketConsumeRepository = ticketConsumeRepository;
            _orderService = orderService;
        }

        public void Add(Tbl_TicketConsume ticketConsume)
        {
            _ticketConsumeRepository.Add(ticketConsume);
        }

        public void Add(Tbl_Ticket_Testing ticketTesting, Tbl_OrderDetail orderDetail)
        {
            //if (ticketTesting.DataStatus != (int)TicketTestingDataStatus.Employ)
            //{
            //    return;
            //}
            if (orderDetail.OrderSource == (int)OrderSource.OTA || orderDetail.OrderSource == (int)OrderSource.XiaoJing)
            {
                var order = _orderService.Get(orderDetail.OrderNo);
                if (order != null)
                {
                    _ticketConsumeRepository.Add(new Tbl_TicketConsume
                    {
                        OrderNo = order.OrderNo,
                        OtaOrderNo = order.OTAOrderNo,
                        TicketTestingId = ticketTesting.TicketTestingId,
                        TicketCategory = ticketTesting.TicketCategory,
                        BarCode = ticketTesting.BarCode,
                        QRcode = ticketTesting.QRcode,
                        OrderDetailNumber = orderDetail.Number,
                        OrderSource = orderDetail.OrderSource,
                        SendStatus = false,
                        CreateTime = DateTime.Now,
                        SendCount = 0,
                        TicketId = ticketTesting.TicketId
                    });
                }
            }
        }

        /// <summary>
        /// 获取入园核销
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<Tbl_TicketConsume> GetList(int count = 200)
        {
            var date = DateTime.Now.Date;
            var tomorrowDate = date.AddDays(1);
            return _ticketConsumeRepository.GetAll().Take(count).Where(a => a.OrderSource == (int)OrderSource.XiaoJing && a.SendStatus == false && a.SendCount <= 5 && a.CreateTime >= date && a.CreateTime < tomorrowDate).OrderBy(a => a.CreateTime).ToList();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="list"></param>
        public void Update(List<Tbl_TicketConsume> list)
        {
            foreach (var row in list)
            {
                row.SendStatus = true;
                row.SendCount += 1;
                _ticketConsumeRepository.Update(row);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ticketConsume"></param>
        public void Update(Tbl_TicketConsume ticketConsume)
        {
            ticketConsume.SendStatus = true;
            _ticketConsumeRepository.Update(ticketConsume);
        }
    }
}
