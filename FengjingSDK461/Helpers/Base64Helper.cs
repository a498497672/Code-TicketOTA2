using System;
using System.Text;

namespace FengjingSDK461.Helpers
{
    public class Base64Helper
    {
        /// <summary>
        /// 对象转Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string ObjectToBase64Encode(object request)
        {
            var str = JsonHelper.ObjectToJson(request);
            return Base64Encode(str);
        }

        /// <summary>
        /// Base64解密，采用utf8编码方式解密,同时转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static T Base64EncodeToObject<T>(string request)
        {
            var data = Base64Decode(request);
            return JsonHelper.JsonToObject<T>(data);
        }


        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string Base64Encode(string source)
        {
            return Base64Encode(Encoding.UTF8, source);
        }

        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decode(string result)
        {
            return Base64Decode(Encoding.UTF8, result);
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="encodeType">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        private static string Base64Encode(Encoding encodeType, string source)
        {
            string encode = string.Empty;
            byte[] bytes = encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="encodeType">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        private static string Base64Decode(Encoding encodeType, string result)
        {
            result = result.Replace(" ", "+");
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encodeType.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }
    }
}
