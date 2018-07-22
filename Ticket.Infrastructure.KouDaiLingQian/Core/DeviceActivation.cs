using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Infrastructure.KouDaiLingQian.Lib;
using Ticket.Infrastructure.KouDaiLingQian.Response;
using Ticket.Utility.Helpers;

namespace Ticket.Infrastructure.KouDaiLingQian.Core
{
    public class DeviceActivation
    {
        public static void Run()
        {
            // 1固定参数
            PayData postmap = new PayData();    // 请求参数的map

            postmap.Put("rancode", Helper.GenerateRandom(5));
            postmap.Put("reqtime", DateTime.Now.ToString("yyyyMMddHHmmss"));
            postmap.Put("snNo", PayConfig.SnNo);
            postmap.Put("systemCode", PayConfig.SystemCode);
            postmap.Put("vender", "123456");
            postmap.Put("version", PayConfig.Version);

            // 2签名
            string sign = Helper.MakeSign(postmap.ToUrl(), PayConfig.DefaultKey);
            postmap.Put("sign", sign);
            //return;
            // 3请求、响应
            string rspStr = HttpService.Post(postmap.ToJson(), PayConfig.WebSite + "/merchantpay/trade/deviceActivation?" + postmap.ToUrl());

            rspStr = rspStr.Replace("/", "");

            var response = JsonSerializeHelper.ToObject<ActivationResponse>(rspStr);
            if (response.ReturnCode == ResultCode.Success)
            {
                var data = JsonSerializeHelper.ToObject<ActivationDataResponse>(response.Data);


                //var key = DesHelper.Decrypt(data.PartnerKey, PayConfig.DefaultKey);
            }
        }
    }
}
