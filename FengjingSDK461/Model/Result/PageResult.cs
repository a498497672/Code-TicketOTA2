using FengjingSDK461.Helpers;
using FengjingSDK461.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengjingSDK461.Model.Result
{
    public class PageResult
    {
        public string Data { get; set; }
        public string Sign { get; set; }
        public string SecurityType { get; set; }
    }

    public class PageDataResult
    {
        public static PageResult Data<T>(T t, string saltCode)
        {
            var data = Base64Helper.ObjectToBase64Encode(t);
            string context = saltCode.ToUpper() + data;
            var sign = Md5Helper.Md5Encrypt32(context);
            return new PageResult
            {
                Data = data,
                Sign = sign,
                SecurityType = "MD5"
            };
        }

        /// <summary>
        /// JSON解析失败
        /// </summary>
        /// <returns></returns>
        public static PageResult JsonParsingFailure()
        {
            var publicResponse = new PublicResponse()
            {
                Head = HeadResult.V1
            };
            publicResponse.Head.Code = "900001";
            publicResponse.Head.Describe = "JSON解析失败";
            var data = Base64Helper.ObjectToBase64Encode(publicResponse);
            return new PageResult
            {
                Data = data,
                Sign = "",
                SecurityType = "MD5"
            };
        }

        /// <summary>
        /// 签名错误
        /// </summary>
        /// <returns></returns>
        public static PageResult SignatureError()
        {
            var publicResponse = new PublicResponse()
            {
                Head = HeadResult.V1
            };
            publicResponse.Head.Code = "900002";
            publicResponse.Head.Describe = "签名错误";
            var data = Base64Helper.ObjectToBase64Encode(publicResponse);
            return new PageResult
            {
                Data = data,
                Sign = "",
                SecurityType = "MD5"
            };
        }


        public static PageResult Fault()
        {
            return new PageResult
            {
                Data = "",
                Sign = "",
                SecurityType = ""
            };
        }
    }
}
