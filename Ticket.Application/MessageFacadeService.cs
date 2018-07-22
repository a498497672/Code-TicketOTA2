using FengjingSDK461.Model.Request;
using FengjingSDK461.Model.Response;
using FengjingSDK461.Model.Result;
using System;
using System.Linq;
using Ticket.Core.Service;
using Ticket.SqlSugar.Models;

namespace Ticket.Application
{
    public class MessageFacadeService
    {
        private readonly OrderDetailService _orderDetailService;
        private readonly OrderService _orderService;
        private readonly SmsService _smsService;
        private readonly AuthorizationService _authorizationService;

        public MessageFacadeService(
            OrderService orderService,
            OrderDetailService orderDetailService,
            SmsService smsService,
            AuthorizationService authorizationService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _smsService = smsService;
            _authorizationService = authorizationService;
        }

        /// <summary>
        /// (重)发送入园凭证短信
        /// </summary>
        /// <param name="data"></param>
        /// <param name="sign"></param>
        public PageResult SendMessage(string data, string sign)
        {
            var request = _authorizationService.CheckFormatForMessageSendRequest(data);
            if (request == null)
            {
                return PageDataResult.JsonParsingFailure();
            }
            var business = _authorizationService.CheckData(request.Head, data, sign);
            if (business == null)
            {
                return PageDataResult.SignatureError();
            }
            return SendMessage(request, business);
        }

        private PageResult SendMessage(MessageSendRequest request, Tbl_OTABusiness business)
        {
            MessageSendOrderInfo orderInfo = request.Body.OrderInfo;
            MessageSendResponse result = new MessageSendResponse
            {
                Head = HeadResult.V1
            };
            var validResult = _orderService.ValidDataForMessageSendRequest(request);
            if (!validResult.Status)
            {
                result.Head.Code = validResult.Code;
                result.Head.Describe = validResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            var tbl_Order = _orderService.Get(orderInfo.OrderId);
            if (tbl_Order == null)
            {
                result.Head.Code = "117004";
                result.Head.Describe = "(重)发送入园凭证短信异常，订单不存在";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            var tbl_OrderDetails = _orderDetailService.GetList(tbl_Order.OrderNo);

            if (tbl_OrderDetails.FirstOrDefault(a => a.EticektSendQuantity >= 5) != null)
            {
                result.Head.Code = "117006";
                result.Head.Describe = "(重)发送入园凭证短信异常，发送次数不能超过5次";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            try
            {
                var sendResult = _smsService.Send(tbl_OrderDetails, tbl_Order.Mobile);
                if (sendResult.Status)
                {
                    result.Body = new MessageSendResponseBody
                    {
                        Message = "OK"
                    };
                    result.Head.Code = "000000";
                    result.Head.Describe = "成功";
                    return PageDataResult.Data(result, business.Saltcode.ToString());
                }
                result.Head.Code = "117005";
                result.Head.Describe = "(重)发送入园凭证短信异常，发送失败";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            catch (Exception ex)
            {
                result.Head.Code = "117005";
                result.Head.Describe = "(重)发送入园凭证短信异常，发送失败";
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
        }
    }
}
