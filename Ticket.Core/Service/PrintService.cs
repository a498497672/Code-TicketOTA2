using System;
using Ticket.Infrastructure.Print;
using Ticket.Infrastructure.Print.Request;
using Ticket.Infrastructure.Print.Response;
using Ticket.Model.Enum;
using Ticket.Model.Result;
using Ticket.SqlSugar.Models;
using Ticket.Utility.Extensions;
using Ticket.Utility.Helpers;
using Ticket.Utility.Key;

namespace Ticket.Core.Service
{
    public class PrintService
    {
        private readonly OrderService _orderService;
        private readonly TicketService _ticketService;
        private readonly OrderDetailService _orderDetailService;
        private readonly PrintGateway _printGateway;
        private readonly ScenicService _scenicService;
        private readonly EnterpriseUserService _enterpriseUserService;

        public PrintService(
            OrderService orderService,
            TicketService ticketService,
            OrderDetailService orderDetailService,
            ScenicService scenicService,
            EnterpriseUserService enterpriseUserService)
        {
            _orderService = orderService;
            _ticketService = ticketService;
            _orderDetailService = orderDetailService;
            _printGateway = new PrintGateway();
            _scenicService = scenicService;
            _enterpriseUserService = enterpriseUserService;
        }

        public TResult Print(string orderNo, string printKey)
        {
            var result = new TResult();
            try
            {
                var printConfigData = _printGateway.Get(printKey);
                if (printConfigData == null)
                {
                    return result.FailureResult("打印机编号不存在,请进行补打");
                }
                var tbl_Order = _orderService.Get(orderNo);
                if (tbl_Order == null)
                {
                    return result.FailureResult("订单不存在");
                }
                var tbl_Scenic = _scenicService.Get(tbl_Order.ScenicId);
                var tbl_OrderDetails = _orderDetailService.GetList(orderNo);

                foreach (var row in tbl_OrderDetails)
                {
                    Print(printConfigData, tbl_Scenic, row);
                }
                return result.SuccessResult();
            }
            catch (Exception e)
            {
                return result.FailureResult("打印系统繁忙，请稍后进行补打.");
            }
        }

        public TResult Print(int orderDetailId, string printKey)
        {
            var result = new TResult();
            var printConfigData = _printGateway.Get(printKey);
            if (printConfigData == null)
            {
                return result.FailureResult("打印机配置不正确");
            }
            var tbl_OrderDetail = _orderDetailService.Get(orderDetailId);
            if (tbl_OrderDetail == null)
            {
                return result.FailureResult("订单不存在");
            }
            if ((tbl_OrderDetail.OrderStatus == (int)OrderDetailsDataStatus.Activate || tbl_OrderDetail.OrderStatus == (int)OrderDetailsDataStatus.IsTaken) && tbl_OrderDetail.ValidityDateEnd.Date >= DateTime.Now.Date)
            {
                var tbl_Scenic = _scenicService.Get(tbl_OrderDetail.ScenicId);
                var printResult = Print(printConfigData, tbl_Scenic, tbl_OrderDetail);
                if (!printResult.Success)
                {
                    return result.FailureResult(printResult.Message);
                }
                return result.SuccessResult();
            }
            else
            {
                return result.FailureResult("该订单，不能进行打印");
            }
        }
        private PrintResult Print(PrintConfigData printConfigData, Tbl_Scenic tbl_Scenic, Tbl_OrderDetail tbl_OrderDetail)
        {
            tbl_OrderDetail.UsedQuantity = tbl_OrderDetail.Quantity;
            tbl_OrderDetail.PrintCount++;
            if (tbl_OrderDetail.OrderStatus == (int)OrderDetailsDataStatus.Activate)
            {
                tbl_OrderDetail.OrderStatus = (int)OrderDetailsDataStatus.IsTaken;
            }

            _orderDetailService.Update(tbl_OrderDetail);
            var userInfo = _enterpriseUserService.LoginForSession();
            var printOrderData = new PrintOrderData
            {
                OrderNo = tbl_OrderDetail.OrderNo,
                TicketName = tbl_OrderDetail.TicketName,
                Qunatity = tbl_OrderDetail.Quantity,
                Price = tbl_OrderDetail.Price,
                TotalAmount = tbl_OrderDetail.Quantity * tbl_OrderDetail.Price,
                CertificateNo = tbl_OrderDetail.CertificateNO,
                QRcode = SecurityExtension.DesEncrypt(tbl_OrderDetail.QRcode, DesKey.QrCodeKey),
                CreateTime = tbl_OrderDetail.ValidityDateStart.ToString("yyyy-MM-dd"),
                PrintCount = tbl_OrderDetail.PrintCount,
                RealName = userInfo.RealName,
                UserName = userInfo.UserName
            };
            if (tbl_Scenic != null)
            {
                printOrderData.ScenicName = tbl_Scenic.ScenicName;
                printOrderData.ScenicPhone = tbl_Scenic.Tel;
            }
            return _printGateway.Send(printOrderData, printConfigData);
        }
    }
}
