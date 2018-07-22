using System;
using System.Threading;
using Ticket.Infrastructure.KouDaiLingQian.Lib;
using Ticket.Infrastructure.KouDaiLingQian.Response;
using Ticket.Utility.Helpers;

namespace Ticket.Infrastructure.KouDaiLingQian.Core
{
    public class MicroOrder
    {
        /// <summary>
        /// 微信（支付宝）被扫下单接口
        /// </summary>
        /// <param name="totalFee">支付金额，单位：元，保留小数点后两位</param>
        /// <param name="authCode">支付码</param>
        /// <param name="outTradeNo">订单号</param>
        /// <returns></returns>
        public static PayResult Pay(string totalFee, string authCode, string outTradeNo)
        {
            var result = new PayResult
            {
                Success = false
            };
            if (string.IsNullOrEmpty(totalFee))
            {
                result.Message = "支付金额,不能为空";
                return result;
            }
            if (string.IsNullOrEmpty(authCode))
            {
                result.Message = "支付码,不能为空";
                return result;
            }
            if (string.IsNullOrEmpty(outTradeNo))
            {
                result.Message = "订单号,不能为空";
                return result;
            }

            // 1固定参数
            PayData postmap = new PayData();
            postmap.Put("version", PayConfig.Version);
            postmap.Put("rancode", Helper.GenerateRandom(5));
            postmap.Put("reqtime", DateTime.Now.ToString("yyyyMMddHHmmss"));
            postmap.Put("snNo", PayConfig.SnNo);
            postmap.Put("terminalType", "OTHER");
            postmap.Put("outTradeNo", outTradeNo);//外部接入系统订单号
            postmap.Put("amount", totalFee);//支付金额，单位：元，保留小数点后两位
            postmap.Put("authCode", authCode);//支付码
            postmap.Put("casherNo", "T001");//收银员编号
            postmap.Put("description", "OTHER");
            postmap.Put("orderTime", DateTime.Now.ToString("yyyyMMddHHmmss"));
            postmap.Put("systemCode", PayConfig.SystemCode);

            // 2签名
            string sign = Helper.MakeSign(postmap.ToUrl());
            postmap.Put("sign", sign);

            // 3请求、响应
            string rspStr = HttpService.Post(postmap.ToJson(), PayConfig.WebSite + "/merchantpay/trade/microorder?" + postmap.ToUrl());

            var response = JsonSerializeHelper.ToObject<MicroOrderResponse>(rspStr);
            result.Message = response.ReturnMessage;

            if (response.ReturnCode == ResultCode.Success)
            {
                //支付成功
                //签名验证
                Helper.CheckSign(rspStr, response.Sign);
                var queryResult = OrderQuery.Query(response.OutTradeNo);//用商户订单号去查单
                result.Success = true;
                result.Message = "支付成功";
                result.OutTradeNo = queryResult.QueryData.CustomerNo;
                return result;
            }
            if (response.ReturnCode == ResultCode.UserPaying)
            {
                //签名验证
                Helper.CheckSign(rspStr, response.Sign);
                //等待用户支付，需查单
                //用商户订单号去查单

                //确认支付是否成功,每隔一段时间查询一次订单，共查询30次--订单有效时间1分钟
                int queryTimes = 30;//查询次数计数器
                while (queryTimes-- > 0)
                {
                    var queryResult = OrderQuery.Query(response.OutTradeNo);//用商户订单号去查单
                    //如果需要继续查询，则等待2s后继续
                    if (queryResult.ReturnCode == ResultCode.Success && (queryResult.QueryData.PayStatus == "I" || queryResult.QueryData.PayStatus == "O"))
                    {
                        Thread.Sleep(2000);
                        continue;
                    }
                    //查询成功,返回订单查询接口返回的数据,支付成功!
                    if (queryResult.ReturnCode == ResultCode.Success && queryResult.QueryData.PayStatus == "P")
                    {
                        result.Success = true;
                        result.Message = "支付成功";
                        result.OutTradeNo = queryResult.QueryData.CustomerNo;
                        return result;
                    }
                    //订单交易失败，直接返回刷卡支付接口返回的结果，失败原因会在err_code中描述
                    result.Message = "支付失败";
                    return result;
                }
            }

            //Refund.Run(response.BankOrderNo, totalFee);
            //支付失败
            return result;
        }
    }
}
