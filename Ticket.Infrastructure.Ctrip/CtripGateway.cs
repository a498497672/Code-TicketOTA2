using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Infrastructure.Ctrip.Core;
using Ticket.Infrastructure.Ctrip.Lib;
using Ticket.Infrastructure.Ctrip.Request;
using Ticket.Infrastructure.Ctrip.Response;

namespace Ticket.Infrastructure.Ctrip
{
    /// <summary>
    /// 携程
    /// </summary>
    public class CtripGateway
    {
        public CheckDataResult CheckData(string request)
        {
            return Api.CheckData(request);
        }

        public PublicResponse ErrorResult(string code, string msg)
        {
            return Api.ErrorResult(code, msg);
        }

        /// <summary>
        /// 验证下单验证请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<VerifyOrderRequest> CheckVerifyOrder(string request)
        {
            return Api.CheckBodyData<VerifyOrderRequest>(request);
        }

        /// <summary>
        /// 验证创建订单请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<CreateOrderRequest> CheckCreateOrder(string request)
        {
            return Api.CheckBodyData<CreateOrderRequest>(request);
        }

        /// <summary>
        /// 验证取消订单请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<CancelOrderRequest> CheckCancelOrder(string request)
        {
            return Api.CheckBodyData<CancelOrderRequest>(request);
        }

        /// <summary>
        /// 验证查询订单请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<QueryOrderRequest> CheckQueryOrder(string request)
        {
            return Api.CheckBodyData<QueryOrderRequest>(request);
        }

        /// <summary>
        /// 下单验证接口
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public VerifyOrderResponse VerifyOrder(VerifyOrderBodyRespose response)
        {
            return Api.SuccessResult(response);
        }

        /// <summary>
        /// 创建订单接口
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public CreateOrderResponse CreateOrder(CreateOrderBodyResponse response)
        {
            return Api.SuccessResult(response);
        }

        /// <summary>
        /// 取消订单接口
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public CancelOrderResponse CancelOrder(CancelOrderBodyRespose response)
        {
            return Api.SuccessResult(response);
        }

        /// <summary>
        /// 查询订单接口
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public QueryOrderResponse QueryOrder(QueryOrderBodyResponse response)
        {
            return Api.SuccessResult(response);
        }

        /// <summary>
        /// 消费通知接口
        /// </summary>
        /// <param name="noticeOrderConsumedRequest"></param>
        /// <returns></returns>
        public bool NoticeOrderConsumed(NoticeOrderConsumedBodyRequest noticeOrderConsumedBodyRequest)
        {
            return OrderConsumed.Run(noticeOrderConsumedBodyRequest);
        }

        /// <summary>
        /// 订单确认接口
        /// </summary>
        /// <param name="noticeOrderConsumedRequest"></param>
        /// <returns></returns>
        public bool CreateOrderConfirm(CreateOrderConfirmBodyRequest createOrderConfirmBodyRequest)
        {
            return CreateOrderConfirmService.Run(createOrderConfirmBodyRequest);
        }

        /// <summary>
        /// 出行通知接口
        /// </summary>
        /// <param name="noticeOrderConsumedRequest"></param>
        /// <returns></returns>
        public bool OrderTravelNotice(OrderOrderTravelNoticeBodyRequest orderOrderTravelNoticeBodyRequest)
        {
            return OrderTravelNoticeService.Run(orderOrderTravelNoticeBodyRequest);
        }
    }
}
