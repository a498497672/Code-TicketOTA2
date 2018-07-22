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
    public class Api
    {
        /// <summary>
        /// 错误返回
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ResponseData ResultError(string code, string msg)
        {
            var responseData = new ResponseData
            {
                Header = new HeaderResponse
                {
                    resultCode = code,
                    resultMessage = msg
                }
            };
            return responseData;
        }

        /// <summary>
        /// 验证格式是否正确
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static RequestData CheckFormat(string data)
        {
            try
            {
                var request = JsonSerializeHelper.ToObject<RequestData>(data);
                if (request.Header == null || string.IsNullOrEmpty(request.Body))
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
        private static bool CheckSign(RequestData request)
        {
            var sign = Helper.MakeSign(request.Header, request.Body);
            if (sign == request.Header.Sign)
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
                body = Encoding.UTF8.GetString(Helper.AESDecrypt(Helper.DecodeBytes(requestBody), CtripConfig.AesKey, CtripConfig.AesIv));
            }
            catch
            {
                body = "";
            }
            return body;
        }
        /// <summary>
        /// body AES加密
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string BodyForAesEncrypt(object body)
        {
            var bodyData = JsonSerializeHelper.ToJsonForlowercase(body);
            return Helper.EncodeBytes(Helper.AESEncrypt(bodyData, CtripConfig.AesKey, CtripConfig.AesIv));
        }


        public static CheckDataResult CheckData(string request)
        {
            var requestData = CheckFormat(request);
            if (requestData == null)
            {
                return CheckDataResult.FailResult(ResultError(ResultCode.JsonParsingFailure, "报文解析失败"));
            }
            if (requestData.Header.AccountId != CtripConfig.AccountId)
            {
                return CheckDataResult.FailResult(ResultError(ResultCode.IncorrectAccountInformation, "携程账户信息不正确"));
            }
            var isSign = CheckSign(requestData);
            if (!isSign)
            {
                return CheckDataResult.FailResult(ResultError(ResultCode.SignatureError, "签名验证失败"));
            }
            var requestBody = CheckRequestBodyForDecrypt(requestData.Body);
            if (string.IsNullOrEmpty(requestBody))
            {
                return CheckDataResult.FailResult(ResultError(ResultCode.JsonParsingFailure, "报文解析失败"));
            }
            requestData.Body = requestBody;
            return CheckDataResult.SuccessResult(requestData);
        }




        ///// <summary>
        ///// 验证数据是否被篡改(签名等)
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //public static Result<HeaderRequest> CheckHeader(string request)
        //{
        //    var requestData = CheckHeaderFormat(request);
        //    if (requestData == null)
        //    {
        //        return Result<HeaderRequest>.FailResult(ErrorResult(ResultCode.XmlParsingFailure, "XML解析失败"));
        //    }
        //    if (requestData.accountId != CtripConfig.MyAccountId)
        //    {
        //        return Result<HeaderRequest>.FailResult(ErrorResult(ResultCode.IncorrectAccountInformation, "OTA账户信息不正确"));
        //    }
        //    var isSign = CheckSign(request, requestData);
        //    if (!isSign)
        //    {
        //        return Result<HeaderRequest>.FailResult(ErrorResult(ResultCode.SignatureError, "签名错误"));
        //    }

        //    return Result<HeaderRequest>.SuccessResult(requestData);
        //}

        /// <summary>
        /// 验证数据结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Result<T> CheckBodyData<T>(string request)
        {
            var requestBodyData = JsonSerializeHelper.ToObject<T>(request);
            if (requestBodyData == null)
            {
                return null;
            }
            return Result<T>.SuccessResult(requestBodyData);
        }

        /// <summary>
        /// 验证Header格式是否正确
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static HeaderRequest CheckHeaderFormat(string request)
        {
            try
            {
                var header = Helper.GetBodyStr(request, "<header>", "</header>");
                var headerRequest = (HeaderRequest)Helper.Deserialize(header, typeof(HeaderRequest));
                return headerRequest;
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
        public static bool CheckSign(string request, HeaderRequest header)
        {
            var body = Helper.GetBodyStr(request);
            var data = Helper.Base64Encode(body);
            var sign = Helper.MakeSign(header.accountId, header.serviceName, header.requestTime, data, header.version);
            if (sign == header.sign.ToLower())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 错误返回
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="resultMessage"></param>
        /// <returns></returns>
        public static PublicResponse ErrorResult(string resultCode, string resultMessage)
        {
            var data = new PublicResponse
            {
                header = new HeaderResponse
                {
                    resultCode = resultCode,
                    resultMessage = resultMessage
                }
            };
            return data;
        }

        ///// <summary>
        ///// 错误返回
        ///// </summary>
        ///// <param name="resultCode"></param>
        ///// <param name="resultMessage"></param>
        ///// <returns></returns>
        //public static string ErrorResult(string resultCode, string resultMessage, VerifyOrderBodyRespose verifyOrderBodyRespose)
        //{
        //    var publicRespose = new VerifyOrderResponse
        //    {
        //        header = new HeaderResponse
        //        {
        //            resultCode = resultCode,
        //            resultMessage = resultMessage
        //        },
        //        body = verifyOrderBodyRespose
        //    };
        //    return Helper.SerializeToXml(publicRespose);
        //}

        ///// <summary>
        ///// 错误返回
        ///// </summary>
        ///// <param name="resultCode"></param>
        ///// <param name="resultMessage"></param>
        ///// <returns></returns>
        //public static string ErrorResult(string resultCode, string resultMessage, CreateOrderBodyRespose verifyOrderBodyRespose)
        //{
        //    var publicRespose = new CreateOrderResponse
        //    {
        //        header = new HeaderResponse
        //        {
        //            resultCode = resultCode,
        //            resultMessage = resultMessage
        //        },
        //        body = verifyOrderBodyRespose
        //    };
        //    return Helper.SerializeToXml(publicRespose);
        //}

        /// <summary>
        /// 成功返回
        /// </summary>
        /// <param name="responseBody"></param>
        /// <returns></returns>
        public static CreateOrderResponse SuccessResult(CreateOrderBodyResponse data)
        {
            return new CreateOrderResponse
            {
                body = BodyForAesEncrypt(data),
                header = new HeaderResponse
                {
                    resultCode = ResultCode.Success,
                    resultMessage = "成功"
                }
            };
        }

        /// <summary>
        /// 成功返回
        /// </summary>
        /// <param name="responseBody"></param>
        /// <returns></returns>
        public static VerifyOrderResponse SuccessResult(VerifyOrderBodyRespose data)
        {
            return new VerifyOrderResponse
            {
                body = BodyForAesEncrypt(data),
                header = new HeaderResponse
                {
                    resultCode = ResultCode.Success,
                    resultMessage = "成功"
                }
            };
        }

        /// <summary>
        /// 成功返回
        /// </summary>
        /// <param name="responseBody"></param>
        /// <returns></returns>
        public static CancelOrderResponse SuccessResult(CancelOrderBodyRespose data)
        {
            return new CancelOrderResponse
            {
                body = BodyForAesEncrypt(data),
                header = new HeaderResponse
                {
                    resultCode = ResultCode.Success,
                    resultMessage = "成功"
                }
            };
        }

        /// <summary>
        /// 成功返回
        /// </summary>
        /// <param name="responseBody"></param>
        /// <returns></returns>
        public static QueryOrderResponse SuccessResult(QueryOrderBodyResponse data)
        {
            return new QueryOrderResponse
            {
                body = BodyForAesEncrypt(data),
                header = new HeaderResponse
                {
                    resultCode = ResultCode.Success,
                    resultMessage = "成功"
                }
            };
        }
    }
}
