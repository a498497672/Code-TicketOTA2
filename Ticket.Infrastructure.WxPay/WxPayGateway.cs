using System;
using System.Collections.Generic;
using System.Web;
using Ticket.Infrastructure.WxPay.business;
using Ticket.Infrastructure.WxPay.jsSDK;
using Ticket.Infrastructure.WxPay.Request;
using Ticket.Infrastructure.WxPay.Response;

namespace Ticket.Infrastructure.WxPay
{
    /// <summary>
    /// 微信支付
    /// </summary>
    public class WxPayGateway
    {
        private readonly UserInfoQuery _userInfoQuery;
        public WxPayGateway()
        {
            _userInfoQuery = new UserInfoQuery();
        }

        public string GetAppId()
        {
            return WxPayConfig.APPID;
        }

        /// <summary>
        /// 刷卡支付
        /// </summary>
        /// <param name="body">商品描述</param>
        /// <param name="total_fee">总金额(元)</param>
        /// <param name="auth_code">支付授权码</param>
        /// <returns>刷卡支付结果</returns>
        public PayResultResponse OrderPay(string body, decimal total_fee, string auth_code)
        {
            try
            {
                return MicroPay.OrderPay(body, total_fee, auth_code);
            }
            catch (Exception e)
            {

                return new PayResultResponse { Success = false, Message = "支付失败，请稍后再说" };
            }
        }

        /// <summary>
        /// 撤销订单，如果失败会重复调用10次
        /// </summary>
        /// <param name="out_trade_no">商户订单号</param>
        /// <returns></returns>
        public bool Cancel(string out_trade_no)
        {
            return MicroPay.Cancel(out_trade_no);
        }

        /// <summary>
        /// 从统一下单成功返回的数据中获取微信浏览器调起jsapi支付所需的参数
        /// </summary>
        /// <returns>返回微信浏览器调起jsapi支付所需的参数</returns>
        public string PayOrder(PayRequest payRequest)
        {
            var wxPayApi = new JsApiPay(HttpContext.Current);
            wxPayApi.body = payRequest.Body;
            wxPayApi.attach = payRequest.Attach;
            wxPayApi.openid = payRequest.OpenId;
            wxPayApi.out_trade_no = payRequest.OutTradeNo;
            wxPayApi.total_fee = (int)(payRequest.TotalFee * 100);//元转化为分
            wxPayApi.GetUnifiedOrderResult();
            return wxPayApi.GetJsApiParameters();
        }

        /// <summary>
        /// 支付结果通知回调处理类
        /// 负责接收微信支付后台发送的支付结果并对订单有效性进行验证，将验证结果反馈给微信支付后台
        /// </summary>
        /// <returns>返回订单号</returns>
        public PayNotifyResponse PayNotify()
        {
            var notifyResult = new ResultNotify(HttpContext.Current);
            var notifyData = notifyResult.ProcessNotifyExtend();
            var out_trade_no = notifyData.GetValue("out_trade_no").ToString();
            var attach = notifyData.GetValue("attach").ToString();
            var openId = notifyData.GetValue("openid").ToString();
            var transaction_id = notifyData.GetValue("transaction_id").ToString();
            return new PayNotifyResponse
            {
                OutTradeNo = out_trade_no,
                OpenId = openId,
                TransactionId = transaction_id,
                Attach = attach
            };
        }

        /// <summary>
        /// 订单退款
        /// </summary>
        /// <param name="refundRequest"></param>
        /// <returns></returns>
        public bool OrderRefund(RefundRequest refundRequest)
        {
            try
            {
                return Refund.Run(refundRequest);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 通过code换取网页授权access_token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public AauthResponse GetAccessToken(string code)
        {
            return _userInfoQuery.GetAccessToken(code);
        }

        /// <summary>
        /// 微信公众号推送消息 根据OpenID列表群发【订阅号不可用，服务号认证后可用】
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="OpenIds">OpenID列表</param>
        public bool ReplayOpenids(string content, List<string> OpenIds)
        {
            try
            {
                var accessToken = JSAPI.GetAccessToken(WxPayConfig.APPID, WxPayConfig.APPSECRET);
                //微信公众号推送消息
                AdvanceMassReplayMessageAPI.ReplayOpenids(accessToken.access_token, content, WeixinArtcleType.Text, OpenIds, "", "");
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 拉取用户信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="openId"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public UserInfoResponse GetUserInfo(string accessToken, string openId, string lang = "zh_CN")
        {
            return _userInfoQuery.GetUserInfo(accessToken, openId, lang);
        }

        /// <summary>
        /// 分享到朋友圈参数自定义
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public JsSDKResponse GetJsSDKTicket(string url)
        {
            var jssdk_ticket = JSAPI.GetJsSDKTicket(WxPayConfig.APPID, WxPayConfig.APPSECRET);
            var nonceStr = WxPayApi.GenerateNonceStr();
            var timestamp = WxPayApi.GenerateTimeStamp();
            //var domain = weChat.BackDomain;
            //url = domain + url;
            var string1 = "";
            var signature = JSAPI.GetSignature(jssdk_ticket, nonceStr, timestamp, url, out string1);
            var model = new JsSDKResponse
            {
                Debug = true,
                AppId = WxPayConfig.APPID,
                Timestamp = WxPayApi.GenerateTimeStamp(),
                NonceStr = WxPayApi.GenerateNonceStr(),
                Signature = signature,
                ShareUrl = url,
                JsapiTicket = jssdk_ticket,
                //ShareImg = domain + Url.Content("/images/noimg.jpg"),
                String1 = string1,
                JsApiList = System.Configuration.ConfigurationManager.AppSettings["jsApiList"].Split(','),
            };
            return model;
        }
    }
}
