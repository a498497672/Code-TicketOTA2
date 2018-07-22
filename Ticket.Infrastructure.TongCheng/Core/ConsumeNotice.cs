using Ticket.Infrastructure.TongCheng.Lib;
using Ticket.Infrastructure.TongCheng.Request;
using Ticket.Infrastructure.TongCheng.Response;
using Ticket.Utility.Helpers;

namespace Ticket.Infrastructure.TongCheng.Core
{
    public class ConsumeNotice
    {
        /// <summary>
        /// 消费通知接口
        /// </summary>
        /// <param name="noticeOrderConsumedRequest"></param>
        /// <returns></returns>
        public static bool Run(ConsumeNoticeRequest consumeNoticeRequest)
        {
            string requestBody = JsonSerializeHelper.ToJson(consumeNoticeRequest);
            requestBody = DesHelper.Encrypt(requestBody, TongChengConfig.UserKey);
            var sign = Helper.MakeSign("ConsumeNotice", requestBody);
            RequestData request = new RequestData
            {
                RequestHead = new RequestHead
                {
                    user_id = TongChengConfig.UserId,
                    Method = "ConsumeNotice",
                    Timestamp = Helper.GenerateTimeStamp(),
                    Version = TongChengConfig.Version,
                    Sign = sign
                },
                RequestBody = requestBody
            };
            string body = JsonSerializeHelper.ToJson(request);
            body = Helper.Base64Encode(body);
            var contnt = HttpService.Post(body, TongChengConfig.Website);
            if (!string.IsNullOrEmpty(contnt))
            {
                contnt = Helper.Base64Decode(contnt);
                var response = JsonSerializeHelper.ToObject<ResponseData>(contnt);
                if (response != null && response.responseHead.res_code == ResultCode.Success)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
