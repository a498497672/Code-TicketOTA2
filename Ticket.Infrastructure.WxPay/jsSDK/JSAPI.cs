using System;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Security.Cryptography;
using System.Text;

namespace Ticket.Infrastructure.WxPay.jsSDK
{
    /// <summary>
    /// http://mp.weixin.qq.com/wiki/7/aaa137b55fb2e0456bf8dd9148dd613f.html
    /// 微信JS-SDK使用权限签名算法
    /// </summary>
    public class JSAPI
    {
        /// <summary>
        ///access_token expire time 
        /// </summary>
        public const int ACCESS_TOKEN_EXPIRE_SECONDS = 7000;

        /// <summary>
        /// cache 
        /// </summary>
        private static ObjectCache cache = MemoryCache.Default;

        class weixin_token
        {
            public string access_token { set; get; }
            public string jssdk_ticket { set; get; }
        }

        /// <summary>
        /// 获取jsapi_ticket
        /// jsapi_ticket是公众号用于调用微信JS接口的临时票据。
        /// 正常情况下，jsapi_ticket的有效期为7200秒，通过access_token来获取。
        /// 由于获取jsapi_ticket的api调用次数非常有限，频繁刷新jsapi_ticket会导致api调用受限，影响自身业务，开发者必须在自己的服务全局缓存jsapi_ticket 。
        /// </summary>
        /// <param name="access_token">BasicAPI获取的access_token,也可以通过TokenHelper获取</param>
        /// <returns></returns>
        public static dynamic GetTickect(string access_token)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi", access_token);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            if (!result.IsSuccessStatusCode) return string.Empty;
            var jsTicket = DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
            return jsTicket;
        }

        public static string GetJsSDKTicket(string appid, string appsecrect, bool force = false)
        {
            try
            {
                var access_token = "";
                var jssdk_ticket = "";
                if (force && cache.Contains(appid))
                {
                    cache.Remove(appid);
                }
                if (!cache.Contains(appid))
                {
                    access_token = GetAccessToken(appid, appsecrect).access_token;
                    jssdk_ticket = GetTickect(access_token).ticket;
                    var json = DynamicJson.Serialize(
                        new weixin_token
                        {
                            access_token = access_token,
                            jssdk_ticket = jssdk_ticket
                        });
                    var policy = new CacheItemPolicy()
                    {
                        AbsoluteExpiration = DateTime.Now.AddSeconds(ACCESS_TOKEN_EXPIRE_SECONDS)
                    };
                    cache.Set(appid, json, policy);
                }
                else
                {
                    var weixin_token = DynamicJson.Parse(cache.Get(appid).ToString());
                    access_token = weixin_token.access_token;
                    jssdk_ticket = weixin_token.jssdk_ticket;
                }
                //Logger.Info(string.Format("appid:{0};access_token:{1};jssdk_ticket:{2}", appid, access_token, jssdk_ticket));
                return jssdk_ticket;
            }
            catch (Exception ex)
            {
                Log.Debug("GetJsSDKTicket", "GetJsSDKTicket异常" + ex);
                //Logger.Debug("YTS_OrderAPI.Common.WxCommon.GetJsSDKTicket异常", ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 签名算法
        /// </summary>
        /// <param name="jsapi_ticket">jsapi_ticket</param>
        /// <param name="noncestr">随机字符串(必须与wx.config中的nonceStr相同)</param>
        /// <param name="timestamp">时间戳(必须与wx.config中的timestamp相同)</param>
        /// <param name="url">当前网页的URL，不包含#及其后面部分(必须是调用JS接口页面的完整URL)</param>
        /// <returns></returns>
        public static string GetSignature(string jsapi_ticket, string noncestr, string timestamp, string url, out string string1)
        {
            var string1Builder = new StringBuilder();
            string1Builder.Append("jsapi_ticket=").Append(jsapi_ticket).Append("&")
                          .Append("noncestr=").Append(noncestr).Append("&")
                          .Append("timestamp=").Append(timestamp).Append("&")
                          .Append("url=").Append(url.IndexOf("#") >= 0 ? url.Substring(0, url.IndexOf("#")) : url);
            string1 = string1Builder.ToString();
            return Sha1(string1);
        }

        /// <summary>
        /// Sha1
        /// </summary>
        /// <param name="orgStr"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Sha1(string orgStr, string encode = "UTF-8")
        {
            var sha1 = new SHA1Managed();
            var sha1bytes = System.Text.Encoding.GetEncoding(encode).GetBytes(orgStr);
            byte[] resultHash = sha1.ComputeHash(sha1bytes);
            string sha1String = BitConverter.ToString(resultHash).ToLower();
            sha1String = sha1String.Replace("-", "");
            return sha1String;
        }

        /// <summary>
        /// 检查签名是否正确:
        /// http://mp.weixin.qq.com/wiki/index.php?title=%E6%8E%A5%E5%85%A5%E6%8C%87%E5%8D%97
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="token">AccessToken</param>
        /// <returns>
        /// true: check signature success
        /// false: check failed, 非微信官方调用!
        /// </returns>
        public static bool CheckSignature(string signature, string timestamp, string nonce, string token, out string ent)
        {
            var arr = new[] { token, timestamp, nonce }.OrderBy(z => z).ToArray();
            var arrString = string.Join("", arr);
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(arrString));
            StringBuilder enText = new StringBuilder();
            foreach (var b in sha1Arr)
            {
                enText.AppendFormat("{0:x2}", b);
            }
            ent = enText.ToString();
            return signature == enText.ToString();
        }

        /// <summary>
        /// 获取AccessToken
        /// http://mp.weixin.qq.com/wiki/index.php?title=%E8%8E%B7%E5%8F%96access_token
        /// </summary>
        /// <param name="grant_type"></param>
        /// <param name="appid"></param>
        /// <param name="secrect"></param>
        /// <returns>access_toke</returns>
        public static dynamic GetAccessToken(string appid, string secrect)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type={0}&appid={1}&secret={2}", "client_credential", appid, secrect);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            if (!result.IsSuccessStatusCode) return string.Empty;
            var token = DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
            //Logging.Logger.Info(LitJson.JsonMapper.ToJson(token));
            return token;
        }
        /// <summary>
        /// 获取微信服务器IP地址
        ///http://mp.weixin.qq.com/wiki/0/2ad4b6bfd29f30f71d39616c2a0fcedc.html
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns>{"ip_list":["127.0.0.1","127.0.0.1"]}</returns>
        public static dynamic GetCallbackIP(string access_token)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token={0}", access_token);
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            if (!result.IsSuccessStatusCode) return string.Empty;
            return DynamicJson.Parse(result.Content.ReadAsStringAsync().Result);
        }

    }
}
