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
    /// 订单消费通知
    /// </summary>
    public class NoticeOrderConsumedFacadeService
    {
        private readonly NoticeOrderConsumedService _noticeOrderConsumedService;
        private readonly CtripGateway _ctripGateway;
        private readonly TongChengGateway _tongChengGateway;

        public NoticeOrderConsumedFacadeService(
            NoticeOrderConsumedService noticeOrderConsumedService,
            CtripGateway ctripGateway,
            TongChengGateway tongChengGateway)
        {
            _noticeOrderConsumedService = noticeOrderConsumedService;
            _ctripGateway = ctripGateway;
            _tongChengGateway = tongChengGateway;
        }

        public void VerifyTicket()
        {
            var list = _noticeOrderConsumedService.GetList();
            foreach (var row in list)
            {
                if (row.IdentityKey.ToLower() == CtripConfig.MyAccountId.ToLower())
                {
                    var isSuccess = _ctripGateway.NoticeOrderConsumed(new NoticeOrderConsumedBodyRequest
                    {
                        OtaOrderId = row.OtaOrderId,
                        SupplierOrderId = row.OrderNo,
                        SequenceId = row.SequenceId,
                        items = new List<NoticeOrderConsumedItemRequest> {
                              new NoticeOrderConsumedItemRequest{
                                   itemId=row.OtaOrderDetailId,
                                   quantity=row.Count,
                                   useQuantity=row.Count
                              }
                         }
                    });
                    row.RunCount++;
                    if (isSuccess)
                    {
                        row.RunCount = 3;
                    }
                    _noticeOrderConsumedService.Update(row.OrderNo, row.RunCount);
                    Console.Write("订单消费通知,携程订单号：" + row.OrderNo + "  是否成功： " + isSuccess);
                }
                else if (row.IdentityKey.ToLower() == TongChengConfig.MyAccountId.ToLower())
                {
                    //var isSuccess = _tongChengGateway.NoticeOrderConsumed(new ConsumeNoticeRequest
                    //{
                    //    orderSerialId = row.OtaOrderId,
                    //    partnerOrderId = row.OrderNo,
                    //    tickets = row.Count,
                    //    consumeDate = row.UseDate.Value.ToString("yyyy-MM-dd HH:mm:ss"),
                    //});
                    //row.RunCount++;
                    //if (isSuccess)
                    //{
                    //    row.RunCount = 3;
                    //}
                    //_noticeOrderConsumedService.Update(row.OrderNo, row.RunCount);
                    //Console.Write("同城订单号：" + row.OrderNo + "  是否成功： " + isSuccess);
                }

            }


        }
    }
}
