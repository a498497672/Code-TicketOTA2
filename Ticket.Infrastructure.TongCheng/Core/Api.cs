using Ticket.Infrastructure.TongCheng.Lib;
using Ticket.Infrastructure.TongCheng.Request;
using Ticket.Infrastructure.TongCheng.Response;
using Ticket.Utility.Helpers;

namespace Ticket.Infrastructure.TongCheng.Core
{
    /// <summary>
    /// 验证
    /// </summary>
    public class Api
    {
        /// <summary>
        /// 验证数据是否被篡改(签名等)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Result<RequestData> Check(string request)
        {
            var requestData = CheckFormat(request);
            if (requestData == null)
            {
                return Result<RequestData>.FailResult(ErrorResult(ResultCode.DataError, "数据出错或为空"));
            }
            var isSign = CheckSign(requestData);
            if (!isSign)
            {
                return Result<RequestData>.FailResult(ErrorResult(ResultCode.SignatureError, "签名验证失败"));
            }
            var requestBody = CheckRequestBodyForDecrypt(requestData.RequestBody);
            if (string.IsNullOrEmpty(requestBody))
            {
                return Result<RequestData>.FailResult(ErrorResult(ResultCode.DataError, "数据出错或为空"));
            }
            requestData.RequestBody = requestBody;
            return Result<RequestData>.SuccessResult(requestData);
        }

        /// <summary>
        /// 验证数据结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public static Result<T> CheckData<T>(string requestBody)
        {
            var requestBodyData = CheckRequestBody<T>(requestBody);
            if (requestBodyData == null)
            {
                return Result<T>.FailResult(ErrorResult(ResultCode.DataError, "数据出错或为空"));
            }
            return Result<T>.SuccessResult(requestBodyData);
        }

        /// <summary>
        /// 验证格式是否正确
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RequestData CheckFormat(string data)
        {
            try
            {
                data = Helper.Base64Decode(data);
                var request = JsonSerializeHelper.ToObject<RequestData>(data);
                if (request.RequestHead == null || string.IsNullOrEmpty(request.RequestBody))
                {
                    return null;
                }
                return request;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool CheckSign(RequestData request)
        {
            var requestHead = request.RequestHead;
            var sign = Helper.MakeSign(request.RequestHead, request.RequestBody);
            if (sign == request.RequestHead.Sign)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 验证请求内容是否可以解密(des)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string CheckRequestBodyForDecrypt(string requestBody)
        {
            var body = string.Empty;
            try
            {
                body = DesHelper.Decrypt(requestBody, TongChengConfig.UserKey);
            }
            catch
            {
                body = "";
            }
            return body;
        }

        /// <summary>
        /// 验证请求内容(json数据是否正确)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T CheckRequestBody<T>(string data)
        {
            try
            {
                var request = JsonSerializeHelper.ToObject<T>(data);
                if (request == null)
                {
                    return default(T);
                }
                return request;
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 错误返回
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string ErrorResult(string code, string msg)
        {
            var responseData = new ResponseData
            {
                responseHead = new ResponseHead
                {
                    res_code = code,
                    res_msg = msg,
                    timestamp = Helper.GenerateTimeStamp()
                }
            };
            var json = JsonSerializeHelper.ToJson(responseData);
            return Helper.Base64Encode(json);
        }

        /// <summary>
        /// 成功返回
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string SuccessResult<T>(T responseBody)
        {
            var body = JsonSerializeHelper.ToJson(responseBody);
            body = DesHelper.Encrypt(body, TongChengConfig.UserKey);
            var responseData = new ResponseData
            {
                responseHead = new ResponseHead
                {
                    res_code = ResultCode.Success,
                    res_msg = "成功",
                    timestamp = Helper.GenerateTimeStamp()
                },
                responseBody = body
            };
            var json = JsonSerializeHelper.ToJson(responseData);
            return Helper.Base64Encode(json);
        }
    }
}
