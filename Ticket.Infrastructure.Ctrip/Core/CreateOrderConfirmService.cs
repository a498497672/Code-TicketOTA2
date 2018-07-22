using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Infrastructure.Ctrip.Lib;
using Ticket.Infrastructure.Ctrip.Request;
using Ticket.Infrastructure.Ctrip.Response;
using Ticket.Utility.Helpers;

namespace Ticket.Infrastructure.Ctrip.Core
{
    public class CreateOrderConfirmService
    {
        /// <summary>
        /// 订单确认接口
        /// </summary>
        /// <param name="noticeOrderConsumedRequest"></param>
        /// <returns></returns>
        public static bool Run(CreateOrderConfirmBodyRequest createOrderConfirmBodyRequest)
        {
            var request = new CreateOrderConfirmRequest
            {
                header = new RequestHeader
                {
                    AccountId = CtripConfig.AccountId,
                    ServiceName = "OrderTravelNotice",
                    RequestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Version = CtripConfig.Version
                }
            };
            var body = Api.BodyForAesEncrypt(createOrderConfirmBodyRequest);
            var sign = Helper.MakeSign(request.header, body);
            request.header.Sign = sign;
            request.body = body;
            var data = JsonSerializeHelper.ToJsonForlowercase(request);
            var contnt = HttpService.Post(data, CtripConfig.Website);
            Console.WriteLine("订单确认接口携程订单号：" + createOrderConfirmBodyRequest.OtaOrderId);
            Console.WriteLine("订单确认接口返回内容  ：" + contnt);
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