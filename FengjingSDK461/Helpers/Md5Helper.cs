using System;
using System.Security.Cryptography;
using System.Text;

namespace FengjingSDK461.Helpers
{
    public class Md5Helper
    {
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="content">需要加密的内容</param>
        /// <param name="signKey">"盐值"key</param>
        /// <returns></returns>
        public static string Md5Encrypt32(string content, string signKey)
        {
            return Md5Encrypt32(signKey.ToUpper()+ content);
        }

        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string Md5Encrypt32(string content)
        {
            string cl = content;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X2");
            }
            return pwd;
        }

        /// <summary>
        /// 16位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static string MD5Encrypt16(string password)
        {
            var md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }
    }
}
