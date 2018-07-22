using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.KouDaiLingQian.Lib
{
    public class SecretUtilTools
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncryptForDes(string message, string key)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(message);
                des.Key = UTF8Encoding.UTF8.GetBytes(key);
                des.IV = UTF8Encoding.UTF8.GetBytes(key);
                des.Mode = System.Security.Cryptography.CipherMode.ECB;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptForDes(string message, string key)
        {
            byte[] inputByteArray = Convert.FromBase64String(message);
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = UTF8Encoding.UTF8.GetBytes(key);
                des.IV = UTF8Encoding.UTF8.GetBytes(key);
                des.Mode = System.Security.Cryptography.CipherMode.ECB;
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }

    }
}
