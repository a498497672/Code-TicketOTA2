using System;
using System.Security.Cryptography;
using System.Text;

namespace Ticket.Utility.Extensions
{
    public static class SecurityExtension
    {
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DesEncrypt(string text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            var inputByteArray = Encoding.Default.GetBytes(text);
            des.Key = Encoding.ASCII.GetBytes(sKey);
            des.IV = Encoding.ASCII.GetBytes(sKey);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            des.Mode = CipherMode.CBC;
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:x2}", b);
            }
            return ret.ToString();
        }

        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string DesDecrypt(string text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            var len = text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x;
            for (x = 0; x < len; x++)
            {
                var i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = Encoding.ASCII.GetBytes(sKey);
            des.IV = Encoding.ASCII.GetBytes(sKey);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
    }
}
