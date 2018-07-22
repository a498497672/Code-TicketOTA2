using System.Configuration;
using System.IO;

/// <summary>
/// 支付宝当面付-基础配置类
/// </summary>
namespace Ticket.Infrastructure.Alipay
{
    public class F2FPayConfig
    {
        ////支付宝公钥
        //public static string alipay_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEApdwJWoDxRtevkT169x8VE+LKH3qsq9kBWDjSti7TuTls3B0uhTKTvW0qe1maOvdb+7IbzMJAOJirDXc6u6kgsxaj6JOr+nQ6NueY4lCD2k4Djm+pJQZZZxw3ulU5+bOJDx/sQJmYvHQQg24I8vLYXGWcjdRQh9mrK0FiMDzzqFVJEX5416WHljRSD1OjDmRZEw1mrVDx1sKWD3ukwkR+blcKEZNaDrdo0wHh+BTRdLCKTY6E3r1FLQrKONViBtg+Zs/5LjNC2LSUKZsLKtlACIY9WbdEWwLehA1JNfA4IdzyzydEY5aJfi2x7HTqKUCv0JD2+hKg5BYP0tS2EGMqxQIDAQAB";
        ////这里要配置没有经过的原始私钥
        ////开发者私钥
        //public static string merchant_private_key = @"MIIEowIBAAKCAQEAw00QDW6c2TroZQCpZSOu2vQ6N0bq91d8Iy2jKmZTkhcZoj6ig548AM519djmD4ZJiU8hf6bGnq+JrUkyzgXCRwjE/sYgjJYKf/IgllpTIUQOAhtDn0yZ2Qp1eJrfNl3EFm/W9PT7/3SjnfL88ctIh8grLOP3j5Gp2YHcNHM6uvcdwhRJ6SG7VRas05mgnCw6VDijXITeKjYypggYre28KZ/l1ZfSAUL0uemduQyVQsK5qs8Pd/cnRbH5HvaowB8oSODhCHbGyubaEjldrIh/npvkya5f1o2j+CV/jBqoh/T9cTyxJM6AbU3BYfUhmCEcDPuwDjU1JDmpg29b4ljzOQIDAQABAoIBAG4YtlDsJ02qkLHHorbaW8sg3OSmfPT8g4WNR6syTjBQB2pYJuNNw4Os4AC8yky8Jh5a1oaQrO1uBY4DcrmDs5a50dvSMy0ttDOWelpoBu7832y+k+tskloxNDHOFMEgMNd5KtOie6Dxzv7OqVthfrzU0coXpUpxEjAEre9/ef9FLY1drerN5iePE2zA/n4NPpI6rL6Ewn+pbemWyAJiVoQCatWKDbO3dZiYCliqypBAgkmngSrbQo7WHmQBsc9jq5ApKrpSMnjWvF7aJ37LpPBU7gn1s/lBg6U/n7gfWAlIV1EH80mGKi+5CoFG39mFwVmKqjMOBvDKq3nI8jGZtTUCgYEA4KCuU0AwxNkw7GVbm4JWBI5j2pr9akq8EolzmDLIyAuFD24AP5Xa1Qvc3DGiQwb2Lz/6WyXmdfWg8VpUjP18cQCPobaZUvB7XihlyVBL2wTIk2EEZfQXitOe5Fo93swzhQrD4RzpybiEqomW9/cM52WVT7Jlj7Sa8PhqirzllfcCgYEA3pPW9NIBTV3EgygfYlcWAMghRqpcejzszM72ClRr5Uecf4bCrxLPAbJgTU6krXFOUHXO5iATKdpQSR3fwkvrwBAfjwXxLnTyak8gxorEAAvDdh0aRYDjOie2HPlQCmZEcTgScClrt7m+yK5vvb0P1Y/qq7yTetfmiI/dyOritE8CgYBAxdS+6NmnScHTyP57fz/ynGSq2tqMVfSZm80bbDl9heTeqCemLj4mBP+w9xcFIPLIeThmJC2qgiPgtN2Asm+53iGMziy0P+gzJI8FWAQnqd90sBsmkxZez9sXmfYRUuzgRlRpi37WzkBjNL21zyWHbEF788NZhwjOx7NhQkspzQKBgQCRRd88JcGR1IzJqitpzTtgQT9u1wzK/+7y8nYmYdmWabochkGVcWpJSjqO2rCjP9wEqg/jpbW8UHCseGIud80kq6FdgVxcnRoIByN2UuYAvazS78XQ7YHh2D+GA7eZQgyT9Swbpv1WilaatzfZmIe4NVnOw6Niv34JThVM6PoVAwKBgGqofE6zktvzElpdbQPZuTGI4fmzjlDbCn2udSTwwPeVciT22LLBhJyyz5HqDZ7l4gdWd3LRElbottG3BHMFQs+NGYAJSBs4XI+k3XQrpIC3IfHkiZs5cxTnkj7ugf+l/JdgI+N61ci9cWP+V2x08oJHuYVtFBeIsKRTzrVoQ9jQ";
        ////开发者公钥
        //public static string merchant_public_key = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAw00QDW6c2TroZQCpZSOu2vQ6N0bq91d8Iy2jKmZTkhcZoj6ig548AM519djmD4ZJiU8hf6bGnq+JrUkyzgXCRwjE/sYgjJYKf/IgllpTIUQOAhtDn0yZ2Qp1eJrfNl3EFm/W9PT7/3SjnfL88ctIh8grLOP3j5Gp2YHcNHM6uvcdwhRJ6SG7VRas05mgnCw6VDijXITeKjYypggYre28KZ/l1ZfSAUL0uemduQyVQsK5qs8Pd/cnRbH5HvaowB8oSODhCHbGyubaEjldrIh/npvkya5f1o2j+CV/jBqoh/T9cTyxJM6AbU3BYfUhmCEcDPuwDjU1JDmpg29b4ljzOQIDAQAB";
        ////应用ID
        //public static string appId = "2015122901049642";
        ////合作伙伴ID：partnerID
        //public static string pid = "2088811943126818";

        //支付宝公钥
        public static string alipay_public_key = ConfigurationManager.AppSettings["alipay_public_key"];
        //开发者私钥
        public static string merchant_private_key = ConfigurationManager.AppSettings["alipay_merchant_private_key"];
        //开发者公钥
        public static string merchant_public_key = ConfigurationManager.AppSettings["alipay_merchant_public_key"];
        //应用ID
        public static string appId = ConfigurationManager.AppSettings["alipay_appId"];
        //合作伙伴ID：partnerID
        public static string pid = ConfigurationManager.AppSettings["alipay_pid"];

        //支付宝网关
        public static string serverUrl = "https://openapi.alipay.com/gateway.do";
        public static string mapiUrl = "https://mapi.alipay.com/gateway.do";
        public static string monitorUrl = "http://mcloudmonitor.com/gateway.do";

        //编码，无需修改
        public static string charset = "utf-8";
        //签名类型，支持RSA2（推荐！）、RSA
        //public static string sign_type = "RSA2";
        public static string sign_type = "RSA2";
        //版本号，无需修改
        public static string version = "1.0";


        /// <summary>
        /// 公钥文件类型转换成纯文本类型
        /// </summary>
        /// <returns>过滤后的字符串类型公钥</returns>
        public static string getMerchantPublicKeyStr()
        {
            StreamReader sr = new StreamReader(merchant_public_key);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
                pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("\r", "");
                pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }

        /// <summary>
        /// 私钥文件类型转换成纯文本类型
        /// </summary>
        /// <returns>过滤后的字符串类型私钥</returns>
        public static string getMerchantPriveteKeyStr()
        {
            StreamReader sr = new StreamReader(merchant_private_key);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
                pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("\r", "");
                pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }



    }
}