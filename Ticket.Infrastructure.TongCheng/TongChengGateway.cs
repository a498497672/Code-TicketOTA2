using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Infrastructure.TongCheng.Core;
using Ticket.Infrastructure.TongCheng.Lib;
using Ticket.Infrastructure.TongCheng.Request;
using Ticket.Infrastructure.TongCheng.Response;

namespace Ticket.Infrastructure.TongCheng
{
    /// <summary>
    /// 同程
    /// </summary>
    public class TongChengGateway
    {
        /// <summary>
        /// 验证请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<RequestData> Check(string request)
        {
            return Api.Check(request);
        }

        public string ErrorResult(string code, string msg)
        {
            return Api.ErrorResult(code, msg);
        }

        /// <summary>
        /// 验证产品请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<ProductRequest> CheckProduct(string requestBody)
        {
            return Api.CheckData<ProductRequest>(requestBody);
        }

        /// <summary>
        /// 验证下单请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<CreateOrderRequest> CheckCreateOrder(string requestBody)
        {
            return Api.CheckData<CreateOrderRequest>(requestBody);
        }

        /// <summary>
        /// 验证退款接口请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<CancelOrderRequest> CheckCancelOrder(string requestBody)
        {
            return Api.CheckData<CancelOrderRequest>(requestBody);
        }

        /// <summary>
        /// 验证查询接口请求数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<QueryOrderRequest> CheckQueryOrder(string requestBody)
        {
            return Api.CheckData<QueryOrderRequest>(requestBody);
        }

        /// <summary>
        /// 产品查询接口
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public string GetProductInfo(ProductResponse response)
        {
            return Api.SuccessResult(response);
        }

        /// <summary>
        /// 下单接口
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public string CreateOrder(CreateOrderResponse response)
        {
            return Api.SuccessResult(response);
        }

        /// <summary>
        /// 取消接口
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public string CancelOrder(CancelOrderResponse response)
        {
            return Api.SuccessResult(response);
        }

        /// <summary>
        /// 查询接口
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public string QueryOrder(QueryOrderResponse response)
        {
            return Api.SuccessResult(response);
        }

        /// <summary>
        /// 消费通知接口
        /// </summary>
        /// <param name="noticeOrderConsumedRequest"></param>
        /// <returns></returns>
        public bool NoticeOrderConsumed(ConsumeNoticeRequest consumeNoticeRequest)
        {
            return ConsumeNotice.Run(consumeNoticeRequest);
        }
    }
}
