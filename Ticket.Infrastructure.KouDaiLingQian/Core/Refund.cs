using System;
using Ticket.Infrastructure.KouDaiLingQian.Lib;
using Ticket.Infrastructure.KouDaiLingQian.Response;
using Ticket.Utility.Helpers;

namespace Ticket.Infrastructure.KouDaiLingQian.Core
{
    public class Refund
    {
        public static OrderQueryResponse Run(string outTradeNo, string amount)
        {
            // 1固定参数
            PayData postmap = new PayData();    // 请求参数的map
            postmap.Put("version", PayConfig.Version);
            postmap.Put("reqtime", DateTime.Now.ToString("yyyyMMddHHmmss"));
            postmap.Put("rancode", Helper.GenerateRandom(5));
            postmap.Put("snNo", PayConfig.SnNo);

            postmap.Put("terminalType", "OTHER");
            postmap.Put("amount", amount);

            postmap.Put("refundNo", outTradeNo);//外部接入系统订单号
            postmap.Put("systemCode", PayConfig.SystemCode);

            // 2签名
            string sign = Helper.MakeSign(postmap.ToUrl());
            postmap.Put("sign", sign);

            // 3请求、响应
            string rspStr = HttpService.Post(postmap.ToJson(), PayConfig.WebSite + "/merchantpay/trade/refund?" + postmap.ToUrl());

            var response = JsonSerializeHelper.ToObject<OrderQueryResponse>(rspStr);

            if (response.ReturnCode == ResultCode.Success)
            {
                //签名验证
                Helper.CheckSign(rspStr, response.Sign);
                response.QueryData = JsonSerializeHelper.ToObject<OrderQueryDataResponse>(response.Data);

                //{ "bankOrderNo":"2018070517331227995415892","dateStr":"20180705173313","merchantNo":"0210a03fdd9c471d8682b584767bec4b","outChannelNo":"400012018070517331224222976","outTradeNo":"180705173311185002","returnCode":"userPaying","returnMessage":"需要用户输入支付密码","sign":"E43019AA329482DF381DD7C108A475F3","transTime":null}
            }
            return response;
        }
    }
}
