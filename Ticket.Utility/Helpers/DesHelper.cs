using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Utility.Helpers
{
    public class DesHelper
    {
        /// <summary>
        /// des加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>
        public static string Encrypt(string encryptString, string encryptKey)
        {
            string returnValue;
            try
            {
                byte[] temp = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;
                byte[] byteEncrypt = Encoding.UTF8.GetBytes(encryptString);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(Encoding.UTF8.GetBytes(encryptKey), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteEncrypt, 0, byteEncrypt.Length);
                cryptoStream.FlushFinalBlock();
                returnValue = Convert.ToBase64String(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }
        /// <summary>
        /// des解密
        /// </summary>
        /// <param name="decryptString"></param>
        /// <param name="decryptKey"></param>
        /// <returns></returns>
        public static string Decrypt(string decryptString, string decryptKey)
        {
            string returnValue;
            try
            {
                byte[] temp = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Mode = CipherMode.ECB;
                des.Padding = PaddingMode.PKCS7;
                byte[] byteDecryptString = Convert.FromBase64String(decryptString);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(Encoding.UTF8.GetBytes(decryptKey), temp), CryptoStreamMode.Write);
                cryptoStream.Write(byteDecryptString, 0, byteDecryptString.Length);
                cryptoStream.FlushFinalBlock();
                returnValue = Encoding.UTF8.GetString(memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return returnValue;
        }
    }
}
