using System;
using Ticket.Infrastructure.Ctrip.Lib;
using Ticket.Infrastructure.Ctrip.Request;
using Ticket.Infrastructure.Ctrip.Response;
using Ticket.Utility.Helpers;

namespace Ticket.Infrastructure.Ctrip.Core
{
    /// <summary>
    /// 消费通知接口
    /// 业务说明 供应商在游客取件（票）、还件时候调用该接口。
    /// 注：该接口仅接受最终核单的结果。若因网络超时或其他原因导致没有收到OTA处理结果响应，
    /// 请用相同报文 body 体请求。
    /// </summary>
    public class OrderConsumed
    {
        /// <summary>
        /// 消费通知接口
        /// </summary>
        /// <param name="noticeOrderConsumedRequest"></param>
        /// <returns></returns>
        public static bool Run(NoticeOrderConsumedBodyRequest noticeOrderConsumedBodyRequest)
        {
            var request = new NoticeOrderConsumedRequest
            {
                header = new RequestHeader
                {
                    AccountId = CtripConfig.AccountId,
                    ServiceName = "NoticeOrderConsumed",
                    RequestTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Version = CtripConfig.Version
                }
            };
            var body = Api.BodyForAesEncrypt(noticeOrderConsumedBodyRequest);
            var sign = Helper.MakeSign(request.header, body);
            request.header.Sign = sign;
            request.body = body;
            var data = JsonSerializeHelper.ToJsonForlowercase(request);
            var contnt = HttpService.Post(data, CtripConfig.Website);
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
