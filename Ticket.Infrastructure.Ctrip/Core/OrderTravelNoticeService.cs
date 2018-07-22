using System;
using Ticket.Infrastructure.Ctrip.Lib;
using Ticket.Infrastructure.Ctrip.Request;
using Ticket.Infrastructure.Ctrip.Response;
using Ticket.Utility.Helpers;

namespace Ticket.Infrastructure.Ctrip.Core
{
    public class OrderTravelNoticeService
    {
        /// <summary>
        /// 消费通知接口
        /// </summary>
        /// <param name="noticeOrderConsumedRequest"></param>
        /// <returns></returns>
        public static bool Run(OrderOrderTravelNoticeBodyRequest orderOrderTravelNoticeBodyRequest)
        {
            var request = new OrderTravelNoticeRequest
            {
                header = new RequestHeader
                {
                    AccountId = CtripConfig.AccountId,
                    ServiceName = "OrderTravelNotice",
                    RequestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Version = CtripConfig.Version
                }
            };
            var body = Api.BodyForAesEncrypt(orderOrderTravelNoticeBodyRequest);
            var sign = Helper.MakeSign(request.header, body);
            request.header.Sign = sign;
            request.body = body;
            var data = JsonSerializeHelper.ToJsonForlowercase(request);
            var contnt = HttpService.Post(data, CtripConfig.Website);
            Console.WriteLine("携程订单号：" + orderOrderTravelNoticeBodyRequest.OtaOrderId);
            Console.WriteLine("返回内容  ：" + contnt);
            if (!string.IsNullOrEmpty(contnt))
            {
                var requestBody = Api.CheckBodyData<PublicResponse>(contnt);
                if (requestBody == null)
                {
                    return false;
                }
                if (requestBody.Data.header.resultCode == ResultCode.Success)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
