using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using Ticket.Utility.Config;
using Ticket.Utility.Extensions;

namespace Ticket.Utility.Helpers
{
    /// <summary>
    /// 订单助手
    /// </summary>
    public sealed class OrderHelper
    {
        private static readonly object Locker = new object();
        private static int _sn;

        /// <summary>
        /// 生成订单编号（设置为20位，时间（17）+ 数字（3）），
        /// </summary>
        /// <returns></returns>
        public static string GenerateOrderNo()
        {
            //lock 关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。
            lock (Locker)
            {
                if (_sn == 99)
                {
                    _sn = 0;
                }
                else
                {
                    _sn++;
                }
                Thread.Sleep(50);
                string result = DateTime.Now.ToString("yyyyMMddHHmmssfff") + _sn.ToString().PadLeft(3, '0');
                return result.Substring(2, result.Length - 2);
            }
        }

        /// <summary>
        /// 生成凭证号 (16位)
        /// </summary>
        /// <returns></returns>
        public static string GenerateCertificateNO()
        {
            //lock 关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。
            lock (Locker)
            {
                Thread.Sleep(50);
                return GuidTo16String();
            }
        }

        /// <summary>
        /// 生成二维码串 23位数字 + 大小写字母 + 17位时间  40位
        /// </summary>
        /// <returns></returns>
        public static string GenerateQRCode()
        {
            string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            Random randrom = new Random((int)DateTime.Now.Ticks);

            string str = "";
            for (int i = 0; i < 23; i++)
            {
                str += chars[randrom.Next(chars.Length)];
            }

            lock (Locker)
            {
                Thread.Sleep(50);
                return str + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            }

        }

        //生成二维码图片
        public static void CreateCode_Simple(string nr, out string imgPath)
        {
            imgPath = "";

            //Des 加密
            string nrData = SecurityExtension.DesEncrypt(nr, AppSettingsConfig.QrCodeKey);

            //二维码内容 为： 验票接口?qrcode=二维码串
            //string strData = qrcodeAPI + "?qrcode=" + nrData;

            string strData = nrData;

            //定义二维码对象
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //三种尺寸：BYTE ，ALPHA_NUMERIC，NUMERIC
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //比例
            qrCodeEncoder.QRCodeScale = 4;
            //版本
            qrCodeEncoder.QRCodeVersion = 8;
            //大小
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //二维码串内容
            //System.Drawing.Image image = qrCodeEncoder.Encode("4408810820 深圳－广州 小江");
            System.Drawing.Image image = qrCodeEncoder.Encode(strData);
            string filename = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".jpg";
            //二维码图片物理路径
            string filepath = AppSettingsConfig.QrCodePath + "\\" + filename;
            System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

            fs.Close();
            image.Dispose();

            imgPath = AppSettingsConfig.ImgApiPath + filename;
            //二维码解码
            //var codeDecoder = CodeDecoder(filepath);
        }

        /// <summary>
        /// 生成二维码图片--小径平台不加密
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="imgPath"></param>
        public static void XJ_CreateCode_Simple(string strData, out string imgPath)
        {
            imgPath = "";

            //定义二维码对象
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //三种尺寸：BYTE ，ALPHA_NUMERIC，NUMERIC
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //比例
            qrCodeEncoder.QRCodeScale = 4;
            //版本
            qrCodeEncoder.QRCodeVersion = 8;
            //大小
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //二维码串内容
            //System.Drawing.Image image = qrCodeEncoder.Encode("4408810820 深圳－广州 小江");
            System.Drawing.Image image = qrCodeEncoder.Encode(strData);
            string filename = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".jpg";
            //二维码图片物理路径
            string filepath = AppSettingsConfig.QrCodePath + "\\" + filename;
            System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

            fs.Close();
            image.Dispose();

            imgPath = AppSettingsConfig.ImgApiPath + filename;
            //二维码解码
            //var codeDecoder = CodeDecoder(filepath);
        }

        //生成扫一扫接口二维码图片
        public static void CreateSYSCode(string nr, out string imgPath)
        {
            imgPath = "";

            string strData = nr;

            //定义二维码对象
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //三种尺寸：BYTE ，ALPHA_NUMERIC，NUMERIC
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //比例
            qrCodeEncoder.QRCodeScale = 4;
            //版本
            qrCodeEncoder.QRCodeVersion = 8;
            //大小
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //二维码串内容
            Image image = qrCodeEncoder.Encode(strData);
            string filename = DateTime.Now.ToString("yyyymmddhhmmssfff").ToString() + ".jpg";
            //二维码图片物理路径
            string filepath = AppSettingsConfig.QrCodePath + "\\" + filename;
            System.IO.FileStream fs = new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
            image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

            fs.Close();
            image.Dispose();

            imgPath = AppSettingsConfig.ImgApiPath + filename;
            //二维码解码
            //var codeDecoder = CodeDecoder(filepath);
        }

        /// <summary>
        /// 二维码解码
        /// </summary>
        /// <param name="filePath">图片路径</param>
        /// <returns></returns>
        public static string CodeDecoder(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return null;
            Bitmap myBitmap = new Bitmap(Image.FromFile(filePath));
            QRCodeDecoder decoder = new QRCodeDecoder();
            string decodedString = decoder.decode(new QRCodeBitmapImage(myBitmap));
            return decodedString;
        }

        /// <summary>
        /// 由连字符分隔的唯一32位数字
        /// </summary>
        /// <returns></returns>
        private static string GetGuid()
        {
            System.Guid guid = new Guid();
            guid = Guid.NewGuid();
            return guid.ToString();
        }
        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <param name=\"guid\"></param>  
        /// <returns></returns>  
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列  
        /// </summary>  
        /// <returns></returns>  
        public static long GuidToLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }

        /// <summary>
        /// 生成13位凭证码
        /// </summary>
        /// <returns></returns>
        public static string GetCertificateNo()
        {
            var certificateNo = string.Empty;
            while (true)
            {
                certificateNo = GenerateRandomCode(13);
                var number = Convert.ToInt64(certificateNo);
                if (number.ToString().Length == 13)
                {
                    break;
                }
            }
            return certificateNo;
        }

        /// <summary>
        ///生成制定位数的随机码（数字）
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string GenerateRandomCode(int length)
        {
            var result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var r = new Random(Guid.NewGuid().GetHashCode());
                result.Append(r.Next(0, 10));
            }
            return result.ToString();
        }
    }
}
