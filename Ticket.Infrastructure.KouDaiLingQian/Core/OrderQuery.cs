using System;
using Ticket.Infrastructure.KouDaiLingQian.Lib;
using Ticket.Infrastructure.KouDaiLingQian.Response;
using Ticket.Utility.Helpers;

namespace Ticket.Infrastructure.KouDaiLingQian.Core
{
    public class OrderQuery
    {
        public static OrderQueryResponse Query(string outTradeNo)
        {
            // 1固定参数
            PayData postmap = new PayData();    // 请求参数的map
            postmap.Put("version", PayConfig.Version);
            postmap.Put("reqtime", DateTime.Now.ToString("yyyyMMddHHmmss"));
            postmap.Put("rancode", Helper.GenerateRandom(5));
            postmap.Put("snNo", PayConfig.SnNo);
            postmap.Put("outTradeNo", outTradeNo);//外部接入系统订单号
            postmap.Put("systemCode", PayConfig.SystemCode);

            // 2签名
            string sign = Helper.MakeSign(postmap.ToUrl());
            postmap.Put("sign", sign);

            // 3请求、响应
            string rspStr = HttpService.Post(postmap.ToJson(), PayConfig.WebSite + "/merchantpay/trade/orderquery?" + postmap.ToUrl());

            var response = JsonSerializeHelper.ToObject<OrderQueryResponse>(rspStr);

            if (response.ReturnCode == ResultCode.Success)
            {
                //签名验证
                Helper.CheckSign(rspStr, response.Sign);
                response.QueryData = JsonSerializeHelper.ToObject<OrderQueryDataResponse>(response.Data);
            }
            return response;
        }


    }
}
