using System;
using System.Text;
using Ticket.Infrastructure.KouDaiLingQian.Lib;
using Ticket.Infrastructure.KouDaiLingQian.Response;
using Ticket.Utility.Helpers;

namespace Ticket.Infrastructure.KouDaiLingQian.Core
{
    public class MicroOrder1
    {
        public static string Pay(string body, double totalFee, string authCode, string outTradeNo)
        {
            // 1固定参数
            PayData postmap = new PayData();    // 请求参数的map
            postmap.Put("version", PayConfig.Version);
            postmap.Put("rancode", Helper.GenerateRandom(5));
            postmap.Put("reqtime", DateTime.Now.ToString("yyyyMMddHHmmss"));
            postmap.Put("snNo", PayConfig.SnNo);
            //postmap.Put("merchantNo", PayConfig.MerchantNo);
            
            postmap.Put("terminalType", "OTHER");
            postmap.Put("outTradeNo", outTradeNo);//外部接入系统订单号
            postmap.Put("amount", totalFee);
            postmap.Put("authCode", authCode);
            postmap.Put("casherNo", "T001");//收银员编号
            postmap.Put("description", "OTHER");
            postmap.Put("orderTime", DateTime.Now.ToString("yyyyMMddHHmmss"));
            postmap.Put("systemCode", PayConfig.SystemCode);
            



            //byte[] byteArray = System.Text.Encoding.Default.GetBytes(PayConfig.PartnerKey);


            //byte[] bcdbyte = str2Bcd("B95EB858BAA4170731EDBB0D7661B39A34");
            //byte[] keybyte = str2Bcd("1111222233334444");
            //byte ssss = 0;
            //byte[] ggg = new byte[] { ssss };
            //byte borByte = 0;
            //for (int i = 0; i < bcdbyte.Length - 1; i++)
            //{
            //    if (i == 0)
            //    {
            //        borByte = bcdbyte[i];
            //    }
            //    borByte ^= bcdbyte[i + 1];
            //}
            //byte[] bor = new byte[] { borByte };
            //StringBuilder sb = new StringBuilder(bor.Length);
            //String sTemp;
            //for (int i = 0; i < bor.Length; i++)
            //{
            //    sTemp = String.Format("{0:X}", 0xFF & bor[i]);
            //    //sTemp = Integer.toHexString(0xFF & bor[i]);
            //    if (sTemp.Count() < 2)
            //        sb.Append(0);
            //    sb.Append(sTemp.ToUpper());
            //}
            //var str = sb.ToString();


            byte[] byteArray1 = Encoding.UTF8.GetBytes("F8F4221780C0BB9BD605DE12C5008A25C7");


            

            var sta = "";
            for (var i = 0; i < 32; i++)
            {
                sta += byteArray1[i];
            }

            var dddd= Hex_16To2(sta);



            //var key = SecretUtilTools.DecryptForDes(sta, PayConfig.DefaultKey);

            //var key = "7F7C2C4B5EE403F17D60C6BBACF6A808756060E3B923361F9177630F20164850";

            // 2签名
            string sign = Helper.MakeSign(postmap.ToUrl());
            postmap.Put("sign", sign);

            // 3请求、响应
            string rspStr = HttpService.Post(postmap.ToJson(), PayConfig.WebSite + "/merchantpay/trade/microorder?" + postmap.ToUrl());



            var response = JsonSerializeHelper.ToObject<ActivationResponse>(rspStr);
            if (response.ReturnCode == ResultCode.Success)
            {
                var data = JsonSerializeHelper.ToObject<ActivationDataResponse>(response.Data);
                //var key = DesHelper.Decrypt(data.PartnerKey, PayConfig.DefaultKey);

                //{ "bankOrderNo":"2018070517331227995415892","dateStr":"20180705173313","merchantNo":"0210a03fdd9c471d8682b584767bec4b","outChannelNo":"400012018070517331224222976","outTradeNo":"180705173311185002","returnCode":"userPaying","returnMessage":"需要用户输入支付密码","sign":"E43019AA329482DF381DD7C108A475F3","transTime":null}



            }
            return rspStr;
        }



        /// <summary>
        /// 16进制转2进制
        /// </summary>
        public static Byte[] Hex_16To2(String hexString)
        {
            if ((hexString.Length % 2) != 0)
            {
                hexString += " ";
            }
            Byte[] returnBytes = new Byte[hexString.Length / 2];
            for (Int32 i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return returnBytes;
        }

        /// <summary>
        /// BCD码转为10进制串(阿拉伯数据) 
        /// </summary>
        /// <param name="bytes">BCD码 </param>
        /// <returns>10进制串 </returns>
        public static String Bcd2Str(byte[] bytes)
        {
            StringBuilder temp = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
            {
                temp.Append((byte)((bytes[i] & 0xf0) >> 4));
                temp.Append((byte)(bytes[i] & 0x0f));
            }
            return temp.ToString().Substring(0, 1).Equals("0") ? temp.ToString().Substring(1) : temp.ToString();
        }

        /// <summary>
        /// 10进制串转为BCD码 
        /// </summary>
        /// <param name="asc">10进制串 </param>
        /// <returns>BCD码 </returns>
        public static byte[] Str2Bcd(String asc)
        {
            int len = asc.Length;
            int mod = len % 2;

            if (mod != 0)
            {
                asc = "0" + asc;
                len = asc.Length;
            }

            byte[] abt = new byte[len];
            if (len >= 2)
            {
                len = len / 2;
            }

            byte[] bbt = new byte[len];
            abt = System.Text.Encoding.ASCII.GetBytes(asc);
            int j, k;

            for (int p = 0; p < asc.Length / 2; p++)
            {
                if ((abt[2 * p] >= '0') && (abt[2 * p] <= '9'))
                {
                    j = abt[2 * p] - '0';
                }
                else if ((abt[2 * p] >= 'a') && (abt[2 * p] <= 'z'))
                {
                    j = abt[2 * p] - 'a' + 0x0a;
                }
                else
                {
                    j = abt[2 * p] - 'A' + 0x0a;
                }

                if ((abt[2 * p + 1] >= '0') && (abt[2 * p + 1] <= '9'))
                {
                    k = abt[2 * p + 1] - '0';
                }
                else if ((abt[2 * p + 1] >= 'a') && (abt[2 * p + 1] <= 'z'))
                {
                    k = abt[2 * p + 1] - 'a' + 0x0a;
                }
                else
                {
                    k = abt[2 * p + 1] - 'A' + 0x0a;
                }

                int a = (j << 4) + k;
                byte b = (byte)a;
                bbt[p] = b;
            }
            return bbt;
        }
    }
}
