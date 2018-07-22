using LitJson;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Ticket.Infrastructure.Print.Request;
using Ticket.Infrastructure.Print.Response;

namespace Ticket.Infrastructure.Print.Lib
{
    /// <summary>
    /// 易联云打印机--第三方
    /// </summary>
    public class PrintHelper
    {
        public static void Send(PrintOrderData data, List<PrintConfigData> list)
        {
            var text = GetOrderContext(data);
            foreach (var row in list)
            {
                Send(text, row);
            }
        }

        public static PrintResult Send(PrintOrderData data, PrintConfigData configData)
        {
            var text = GetOrderContext(data);
            return Send(text, configData);
        }



        private static string GetOrderContext(PrintOrderData data)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<center>{0}{1}</center>", data.ScenicName, data.PrintCount > 1 ? "(补打)" : "");
            sb.Append("................................\r\n");
            sb.AppendFormat("\r\n收银员: {0}  牌号:{1}\r\n", data.RealName, data.UserName);
            sb.AppendFormat("\r\n下单时间: {0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.AppendFormat("\r\n门票名称: {0}\r\n", data.TicketName);
            sb.AppendFormat("\r\n入园时间: {0}\r\n", data.CreateTime);
            //sb.AppendFormat("\r\n数量: {0}\r\n", data.Qunatity);
            //sb.AppendFormat("\r\n单价: {0}\r\n", data.Price);
            //sb.AppendFormat("\r\n总价: {0}\r\n", data.TotalAmount);
            sb.Append("................................\r\n");
            sb.Append("<table>");
            sb.Append("<tr><td>数量</td><td>单价</td><td>总价</td></tr>");
            sb.AppendFormat("<tr><td>{0}人</td><td>{1}元</td><td>{2}元</td></tr>", data.Qunatity, data.Price, data.TotalAmount);
            sb.Append("</table>");
            sb.Append("................................\r\n\r\n");
            sb.AppendFormat("<QR>{0}</QR>", data.QRcode);
            sb.AppendFormat("\r\n凭证号: {0}\r\n", data.CertificateNo);
            sb.AppendFormat("\r\n订单号: {0}\r\n", data.OrderNo);
            sb.AppendFormat("\r\n注意事项: \r\n\r\n{0}\r\n", PrintManager.Considerations);
            sb.Append("\r\n<FB>请持票到景区入园处扫码入园</FB>\r\n");
            sb.AppendFormat("\r\n如需协助请拨打: {0}\r\n", data.ScenicPhone);
            return sb.ToString();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="text"></param>
        /// <param name="timeOut"></param>
        public static PrintResult Send(string text, PrintConfigData printConfigData, int timeOut = 10)
        {
            //用户id
            string partner = printConfigData.Partner;//"21108";
            //apikey API 密钥
            string apikey = printConfigData.ApiKey; //"2f12779e593499b981beb7fe9644bb934b9dada7";
            //机器码 终端编号
            string machine_code = printConfigData.MachineCode;//"4004547827";
            //终端密钥
            string machine_key = printConfigData.MachineKey;//"6a7r3enumfbu";
            //接口地址  
            string url = "http://open.10ss.net:8888";

            //时间戳
            string time = "";
            //签名
            string sign = "";

            #region 开始生成时间戳
            time = GetTimeSpan();
            #endregion

            #region 参数列表
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("machine_code", machine_code);
            parameters.Add("partner", partner);
            parameters.Add("time", time);
            #endregion

            //对apikey+(machine_code+partner+time)+mkey 进行签名加密
            sign = getSign(parameters, apikey, machine_key);

            PrintData data = new PrintData();
            data.Put("partner", partner);//用户id
            data.Put("machine_code", machine_code);//机器码 终端编号
            data.Put("time", time);//时间戳
            data.Put("sign", sign);//签名
            data.Put("content", text);//产生随机的商户订单号

            string response = HttpService.Post(url, data.ToUrl(), timeOut);

            JsonData jsonData = JsonMapper.ToObject(response);
            //服务器返回状态
            var state = jsonData["state"].ToString();
            var result = new PrintResult { Success = false, Message = "系统繁忙，稍后再试" };
            if (state == "1")
            {
                result.Success = true;
                result.Message = "打印成功";
            }
            else if (state == "2")
            {
                result.Message = "打印失败，提交时间超时";
            }
            else if (state == "3")
            {
                result.Message = "打印失败，参数有误";
            }
            else if (state == "4")
            {
                result.Message = "打印失败，sign加密验证失败";
            }
            return result;
            //Logger.Error("服务器错误:", actionExecutedContext.Exception);

            //state:"1","id":"1234"   数据提交成功，1234代表单号，打印完成上报时用、详情请查看：打印完成自动上报接口文档
            //state:"2"   提交时间超时。验证你所提交的时间戳超过3分钟后拒绝接受
            //state:"3"   参数有误
            //state:"4"   sign加密验证失败
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        private static string GetTimeSpan()
        {
            string time;
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            time = Convert.ToInt64(ts.TotalSeconds).ToString();     //获取时间戳
            return time;
        }

        /// <summary>
        /// 创建本次调用的签名
        /// </summary>       
        /// <param name="parameters">parameters，参数列表</param>
        /// <param name="preKey">preKey，apikey的值</param>
        /// <param name="secKey">secKey，终端密钥的值</param>
        /// <returns>String，签名</returns>
        private static String getSign(IDictionary<string, string> parameters, string preKey, string secKey)
        {
            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();
            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder("");

            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append(value);
                }
            }
            string source = query.ToString();
            source = preKey + source + secKey;
            return GetMD5Hash(source);
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns>MD5 32位大写</returns>
        private static string GetMD5Hash(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            string md = BitConverter.ToString(res).Replace("-", "");
            return md;
        }
    }
}
