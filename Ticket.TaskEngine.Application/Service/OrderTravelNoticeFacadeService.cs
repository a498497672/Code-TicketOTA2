using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Ticket.Core.Service;
using Ticket.Infrastructure.Ctrip;
using Ticket.Infrastructure.Ctrip.Lib;
using Ticket.Infrastructure.Ctrip.Request;
using Ticket.Infrastructure.TongCheng;
using Ticket.Infrastructure.TongCheng.Lib;
using Ticket.Infrastructure.TongCheng.Request;
using Ticket.Model.Model;
using Ticket.Utility.Helpers;

namespace Ticket.TaskEngine.Application.Service
{
    /// <summary>
    /// 订单出行通知
    /// </summary>
    public class OrderTravelNoticeFacadeService
    {
        private readonly OrderTravelNoticeService _orderTravelNoticeService;
        private readonly OrderDetailService _orderDetailService;
        private readonly CtripGateway _ctripGateway;

        public OrderTravelNoticeFacadeService(
            OrderTravelNoticeService orderTravelNoticeService,
            OrderDetailService orderDetailService,
            CtripGateway ctripGateway)
        {
            _orderTravelNoticeService = orderTravelNoticeService;
            _orderDetailService = orderDetailService;
            _ctripGateway = ctripGateway;
        }

        public void VerifyTicket()
        {
            var list = _orderTravelNoticeService.GetList();
            foreach (var row in list)
            {
                var orderDetails = _orderDetailService.GetList(row.OrderNo);



                var confirmBodyRequest = new CreateOrderConfirmBodyRequest
                {
                    OtaOrderId = row.OtaOrderId,
                    SupplierOrderId = row.OrderNo,
                    SequenceId = row.SequenceId,
                    confirmResultCode = "0000",
                    confirmResultMessage = "确认成功",
                    voucherSender = 1,
                    vouchers = new List<CreateOrderConfirmVouchersRequest>(),
                    items = new List<CreateOrderConfirmItemRequest>()
                };
                foreach (var item in orderDetails)
                {
                    confirmBodyRequest.vouchers.Add(new CreateOrderConfirmVouchersRequest
                    {
                        itemId = item.OtaOrderDetailId,
                        voucherType = 2,
                        voucherCode = item.CertificateNO,
                        voucherData = ""
                    });
                    confirmBodyRequest.items.Add(new CreateOrderConfirmItemRequest
                    {
                        itemId = item.OtaOrderDetailId,
                        inventorys = new List<CreateOrderConfirmInventoryRespose> {
                             new  CreateOrderConfirmInventoryRespose{
                                  quantity=50000,
                                   useDate=item.ValidityDateStart.ToString("yyyy-MM-dd")
                             }
                         }
                    });
                }

                var ddd = _ctripGateway.CreateOrderConfirm(confirmBodyRequest);
                Console.WriteLine("订单确认接口,携程订单号：" + row.OrderNo + "  是否成功： " + ddd);












                var bodyRequest = new OrderOrderTravelNoticeBodyRequest
                {
                    OtaOrderId = row.OtaOrderId,
                    SupplierOrderId = row.OrderNo,
                    SequenceId = row.SequenceId,
                    vouchers = new List<OrderOrderTravelNoticeVouchersRequest>(),
                    items = new List<OrderTravelNoticeItemRequest>()
                };
                foreach (var item in orderDetails)
                {
                    bodyRequest.vouchers.Add(new OrderOrderTravelNoticeVouchersRequest
                    {
                        itemId = item.OtaOrderDetailId,
                        voucherType = 2,
                        voucherCode = item.CertificateNO,
                        voucherData = ""
                    });
                    bodyRequest.items.Add(new OrderTravelNoticeItemRequest
                    {
                        itemId = item.OtaOrderDetailId,
                        travelInformations = new List<OrderTravelNoticeTravelInformationsRequest> {
                             new OrderTravelNoticeTravelInformationsRequest{
                                 name = "游玩时间",
                                 content = item.ValidityDateStart.ToString("yyyy-MM-dd")
                             }
                         }
                    });
                }
                var isSuccess = _ctripGateway.OrderTravelNotice(bodyRequest);
                row.RunCount++;
                if (isSuccess)
                {
                    row.RunCount = 3;
                }
                _orderTravelNoticeService.Update(row.OrderNo, row.RunCount);
                Console.WriteLine("订单出行通知,携程订单号：" + row.OrderNo + "  是否成功： " + isSuccess);
            }


        }
    }
}
