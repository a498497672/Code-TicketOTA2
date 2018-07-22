using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Ticket.Infrastructure.Ctrip.Request;

namespace Ticket.Infrastructure.Ctrip.Lib
{
    public class Helper
    {
        /// <summary>
        /// 反序列化XML字符串为指定类型
        /// </summary>
        public static object Deserialize(string Xml, Type ThisType)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ThisType);
            object result;
            try
            {
                using (StringReader stringReader = new StringReader(Xml))
                {
                    result = xmlSerializer.Deserialize(stringReader);
                }
            }
            catch (Exception innerException)
            {
                bool flag = false;
                if (Xml != null)
                {
                    if (Xml.StartsWith(Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble())))
                    {
                        flag = true;
                    }
                }
                throw new ApplicationException(string.Format("Couldn't parse XML: '{0}'; Contains BOM: {1}; Type: {2}.",
                Xml, flag, ThisType.FullName), innerException);
            }
            return result;
        }

        /// <summary>
        /// 将自定义对象序列化为XML字符串
        /// </summary>
        /// <param name="myObject">自定义对象实体</param>
        /// <returns>序列化后的XML字符串</returns>
        public static string SerializeToXml<T>(T myObject)
        {
            if (myObject != null)
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));

                MemoryStream stream = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
                writer.Formatting = Formatting.None;//缩进
                xs.Serialize(writer, myObject);

                stream.Position = 0;
                StringBuilder sb = new StringBuilder();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        sb.Append(line);
                    }
                    reader.Close();
                }
                writer.Close();
                return sb.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 生成签名，详见签名生成算法
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="serviceName"></param>
        /// <param name="requestTime"></param>
        /// <param name="data"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static string MakeSign(string accountId, string serviceName, string requestTime, string data, string version)
        {
            string str = accountId + serviceName + requestTime + data + version + CtripConfig.Key;
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToLower();
        }

        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="requestHeader"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string MakeSign(RequestHeader requestHeader, string body)
        {
            string str = requestHeader.AccountId + requestHeader.ServiceName + requestHeader.RequestTime + body + requestHeader.Version + CtripConfig.Key;
            //MD5加密
            //所有字符转为小写
            return Md5(str).ToLower();
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
        /// 截取字符串中指定标签内的内容  
        /// </summary>  
        /// <param name="Content">需要截取的字符串</param>  
        /// <param name="start">开始标签</param>  
        /// <param name="end">结束标签</param>  
        /// <returns></returns>  
        public static string GetBodyStr(string Content, string start = "<body>", string end = "</body>")
        {
            var posStart = Content.IndexOf(start);
            var posEnd = Content.IndexOf(end);
            return Content.Substring(posStart, (posEnd - posStart + end.Length));
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="encodeType">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string Base64Encode(Encoding encodeType, string source)
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

        public static byte[] AESEncrypt(string str, string key, string iv)
        {
            if (string.IsNullOrEmpty(str)) return null;
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                Byte[] toEncryptArray = Encoding.UTF8.GetBytes(str);
                provider.Key = Encoding.UTF8.GetBytes(key);
                provider.IV = Encoding.UTF8.GetBytes(iv);
                provider.Mode = CipherMode.CBC;
                provider.Padding = PaddingMode.PKCS7;
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, provider.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(toEncryptArray, 0, toEncryptArray.Length);
                        cs.FlushFinalBlock();
                        byte[] cipherBytes = ms.ToArray();
                        cs.Close();
                        ms.Close();
                        return cipherBytes;
                    }
                }
            }
        }

        public static byte[] AESDecrypt(byte[] content, string key, string iv)
        {
            if (content == null || content.Length == 0) return null;
            using (AesCryptoServiceProvider provider = new AesCryptoServiceProvider())
            {
                provider.Key = Encoding.UTF8.GetBytes(key);
                provider.IV = Encoding.UTF8.GetBytes(iv);
                provider.Mode = CipherMode.CBC;
                provider.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = provider.CreateDecryptor())
                {
                    byte[] results = cryptoTransform.TransformFinalBlock(content, 0, content.Length);
                    provider.Clear();
                    return results;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncodeBytes(byte[] source)
        {
            StringBuilder strBuf = new StringBuilder();
            foreach (byte t in source)
            {
                strBuf.Append((char)(((t >> 4) & 0xF) + ((int)'a')));
                strBuf.Append((char)(((t) & 0xF) + ((int)'a')));
            }
            return strBuf.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] DecodeBytes(string data)
        {
            byte[] bytes = new byte[data.Length / 2];
            for (int i = 0; i < data.Length; i += 2)
            {
                char c = data[i];
                bytes[i / 2] = (byte)((c - 'a') << 4);
                c = data[i + 1];
                bytes[i / 2] += (byte)(c - 'a');
            }
            return bytes;
        }

        public static string Md5(string input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = md5.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(input));
            StringBuilder builder = new StringBuilder(32);
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
