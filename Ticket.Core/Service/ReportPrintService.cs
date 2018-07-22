using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using Ticket.Core.Repository;
using Ticket.Infrastructure.Print;
using Ticket.Infrastructure.Print.Request;
using Ticket.Model.Enum;
using Ticket.Model.Model.Report;
using Ticket.Model.Result;
using Ticket.SqlSugar.Models;

namespace Ticket.Core.Service
{
    public class ReportPrintService
    {
        private readonly EnterpriseUserService _enterpriseUserService;
        private readonly PrintGateway _printGateway;
        private readonly OrderDetailRepository _orderDetailRepository;
        public ReportPrintService(EnterpriseUserService enterpriseUserService, OrderDetailRepository orderDetailRepository)
        {
            _enterpriseUserService = enterpriseUserService;
            _printGateway = new PrintGateway();
            _orderDetailRepository = orderDetailRepository;
        }

        /// <summary>
        /// 日结报表统计
        /// </summary>
        /// <returns></returns>
        public TResult<ReportStatisticsModel> ReportStatistics()
        {
            var result = new TResult<ReportStatisticsModel>();
            var reportList = new ReportStatisticsModel
            {
                List = new List<TicketSaleCount>()
            };
            var user = _enterpriseUserService.LoginForSession();
            var nowDate = DateTime.Now.Date;
            var endTime = nowDate.AddDays(1).Date;
            var list = _orderDetailRepository.db.Queryable<Tbl_OrderDetail, Tbl_Ticket, Tbl_Order>((a, b, c) => new object[] {
              JoinType.Left,a.TicketId==b.TicketId,
              JoinType.Inner,a.OrderNo==c.OrderNo
            })
            .Where((a, b, c) => a.ScenicId == user.ScenicId &&
            a.SellerId == user.UserId &&
            c.SellerId == user.UserId &&
            a.TicketSource == (int)TicketSourceStatus.ScenicSpot &&
            a.CreateTime >= nowDate &&
            a.CreateTime < endTime &&
            ((a.OrderStatus == (int)OrderDetailsDataStatus.IsTaken) ||
            (a.OrderStatus == (int)OrderDetailsDataStatus.Activate) ||
            (a.OrderStatus == (int)OrderDetailsDataStatus.Consume) ||
            (a.OrderStatus == (int)OrderDetailsDataStatus.Canncel) ||
            (a.OrderStatus == (int)OrderDetailsDataStatus.Refund)))
            .Select((a, b, c) => new
            {
                OrderDtlId = a.OrderDetailId,
                TicketId = a.TicketId,
                TicketName = a.TicketName,
                OrderNo = a.OrderNo,
                PayType = (PayStatus)c.PayType,
                OrderType = a.OrderType,
                Price = a.Price,
                Quantity = a.Quantity,
                ShelvesChannel = b.ShelvesChannel,
                OrderStatus = a.OrderStatus,
                CreateTime = a.CreateTime,
                SellerId = a.SellerId
            }).ToList();


            reportList.TotalAmount = list.Sum(p => (decimal)p.Quantity * p.Price);
            reportList.TotalCount = list.Sum(p => p.Quantity);
            reportList.AlipayAmount = list.Where(p => p.PayType == PayStatus.Alipay).Sum(p => (decimal)p.Quantity * p.Price);
            reportList.CashAmount = list.Where(p => p.PayType == PayStatus.ReadyMoney).Sum(p => (decimal)p.Quantity * p.Price);
            reportList.WxPayAmount = list.Where(p => p.PayType == PayStatus.Wechat).Sum(p => (decimal)p.Quantity * p.Price);


            var shelvesChannelType = ((int)ShelvesChannelEnum.TeamTicket).ToString();//上线渠道

            var refundList = list.Where(p => p.OrderStatus == (int)OrderDetailsDataStatus.Refund).ToList();
            reportList.TotalRefundAmount = refundList.Sum(p => (decimal)p.Quantity * p.Price);
            reportList.TotalRefundCount = refundList.Sum(p => p.Quantity);
            reportList.AlipayRefundAmount = refundList.Where(p => p.PayType == PayStatus.Alipay).Sum(p => (decimal)p.Quantity * p.Price);
            reportList.CashRefundAmount = refundList.Where(p => p.PayType == PayStatus.ReadyMoney).Sum(p => (decimal)p.Quantity * p.Price);
            reportList.WxPayRefundAmount = refundList.Where(p => p.PayType == PayStatus.Wechat).Sum(p => (decimal)p.Quantity * p.Price);



            foreach (var row in list)
            {
                reportList.List.Add(new TicketSaleCount
                {
                    OrderNo = row.OrderNo,
                    TicketName = row.TicketName,
                    TicketId = row.TicketId,
                    Price = row.Price,
                    Count = row.Quantity,
                    Amount = (decimal)row.Quantity * row.Price,
                    RefundAmount = 0,
                    RefundCount = 0
                });
            }
            foreach (var row in refundList)
            {
                reportList.List.Add(new TicketSaleCount
                {
                    OrderNo = row.OrderNo,
                    TicketName = row.TicketName,
                    TicketId = row.TicketId,
                    Price = row.Price,
                    Count = 0,
                    Amount = 0,
                    RefundAmount = (decimal)row.Quantity * row.Price,
                    RefundCount = row.Quantity
                });
            }
            reportList.List = reportList.List.GroupBy(a => new { a.TicketId, a.TicketName, a.Price }).Select(a => new TicketSaleCount
            {
                TicketName = a.Key.TicketName,
                TicketId = a.Key.TicketId,
                Price = a.Key.Price,
                Amount = a.Sum(b => b.Amount),
                Count = a.Sum(b => b.Count),
                RefundAmount = a.Sum(b => b.RefundAmount),
                RefundCount = a.Sum(b => b.RefundCount),
            }).ToList();
            return result.SuccessResult(reportList);
        }


        /// <summary>
        /// 打印日结报表
        /// </summary>
        /// <returns></returns>
        public TResult Daily(string printKey)
        {
            var result = new TResult();
            var printConfigData = _printGateway.Get(printKey);
            if (printConfigData == null)
            {
                return result.FailureResult("打印机配置不正确");
            }
            var reportList = new ReportStatisticsModel
            {
                List = new List<TicketSaleCount>()
            };
            var user = _enterpriseUserService.LoginForSession();
            var nowDate = DateTime.Now.Date;
            var endTime = nowDate.AddDays(1);

            var list = _orderDetailRepository.db.Queryable<Tbl_OrderDetail, Tbl_Ticket, Tbl_Order, Tbl_TicketType>((a, b, c, d) => new object[] {
              JoinType.Left,a.TicketId==b.TicketId,
              JoinType.Inner,a.OrderNo==c.OrderNo,
              JoinType.Left,b.TypeId==d.Id
            })
            .Where((a, b, c, d) => a.ScenicId == user.ScenicId &&
            a.SellerId == user.UserId &&
            c.SellerId == user.UserId &&
            a.TicketSource == (int)TicketSourceStatus.ScenicSpot &&
            a.CreateTime >= nowDate &&
            a.CreateTime < endTime &&
            ((a.OrderStatus == (int)OrderDetailsDataStatus.IsTaken) ||
            (a.OrderStatus == (int)OrderDetailsDataStatus.Activate) ||
            (a.OrderStatus == (int)OrderDetailsDataStatus.Consume) ||
            (a.OrderStatus == (int)OrderDetailsDataStatus.Canncel) ||
            (a.OrderStatus == (int)OrderDetailsDataStatus.Refund)))
            .Select((a, b, c, d) => new
            {
                OrderDtlId = a.OrderDetailId,
                TicketId = a.TicketId,
                TicketName = a.TicketName,
                TicketType = d.TicketType,
                OrderNo = a.OrderNo,
                PayType = (PayStatus)c.PayType,
                OrderType = a.OrderType,
                Price = a.Price,
                Quantity = a.Quantity,
                ShelvesChannel = b.ShelvesChannel,
                OrderStatus = a.OrderStatus,
                CreateTime = a.CreateTime
            }).ToList();


            if (list.Count <= 0)
            {
                return result.FailureResult("今天售卖门票数量为0，不能打印");
            }

            var refundList = list.Where(p => p.OrderStatus == (int)OrderDetailsDataStatus.Refund).ToList();

            var startData = list.OrderBy(a => a.CreateTime).FirstOrDefault();
            var endData = list.OrderByDescending(a => a.CreateTime).FirstOrDefault();
            PrintReportData printData = new PrintReportData
            {
                RealName = user.RealName,
                StartTime = startData.CreateTime.ToString("yyyy-MM-dd HH:dd:ss"),
                EndTime = endData.CreateTime.ToString("yyyy-MM-dd HH:dd:ss"),
                TotalCount = list.Sum(p => p.Quantity),
                TotalAmount = list.Sum(p => p.Quantity * p.Price),
                RefundTotalCount = refundList.Sum(p => p.Quantity),
                RefundTotalAmount = refundList.Sum(p => p.Quantity * p.Price),
                PrintBulkTicket = new PrintTicketList(),
                PrintTeamTicket = new PrintTicketList()
            };

            var totalList = list.GroupBy(a => new { a.TicketId, a.TicketName, a.PayType, a.TicketType }).Select(a => new PrintTicketData
            {
                TicketId = a.Key.TicketId,
                TicketName = a.Key.TicketName,
                TotalCount = a.Sum(b => b.Quantity),
                TotalAmount = a.Sum(b => b.Quantity * b.Price),
                TicketType = a.Key.TicketType,
                PayType = (int)a.Key.PayType
            }).ToList();

            //散客票
            printData.PrintBulkTicket.Alipay = totalList.Where(p => p.PayType == (int)PayStatus.Alipay && p.TicketType == (int)ShelvesChannelEnum.IndividualTicket).ToList();
            printData.PrintBulkTicket.ReadyMoney = totalList.Where(p => p.PayType == (int)PayStatus.ReadyMoney && p.TicketType == (int)ShelvesChannelEnum.IndividualTicket).ToList();
            printData.PrintBulkTicket.Wechat = totalList.Where(p => p.PayType == (int)PayStatus.Wechat && p.TicketType == (int)ShelvesChannelEnum.IndividualTicket).ToList();
            //团队票 
            printData.PrintTeamTicket.Alipay = totalList.Where(p => p.PayType == (int)PayStatus.Alipay && p.TicketType == (int)ShelvesChannelEnum.TeamTicket).ToList();
            printData.PrintTeamTicket.ReadyMoney = totalList.Where(p => p.PayType == (int)PayStatus.ReadyMoney && p.TicketType == (int)ShelvesChannelEnum.TeamTicket).ToList();
            printData.PrintTeamTicket.Wechat = totalList.Where(p => p.PayType == (int)PayStatus.Wechat && p.TicketType == (int)ShelvesChannelEnum.TeamTicket).ToList();

            _printGateway.Send(printData, printConfigData);

            return result.SuccessResult();
        }
    }
}
