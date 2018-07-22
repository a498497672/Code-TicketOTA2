using FengjingSDK461.Helpers;
using FengjingSDK461.Model.Request;
using System;
using System.Collections.Generic;
using Ticket.SqlSugar.Models;

namespace Ticket.Core.Service
{
    /// <summary>
    /// 权限验证
    /// </summary>
    public class AuthorizationService
    {
        private readonly OtaBusinessService _otaBusinessService;
        private readonly OtaTicketRelationService _otaTicketRelationService;

        public AuthorizationService(OtaBusinessService otaBusinessService, OtaTicketRelationService otaTicketRelationService)
        {
            _otaBusinessService = otaBusinessService;
            _otaTicketRelationService = otaTicketRelationService;
        }

        /// <summary>
        /// 验证数据是否被篡改，进行认证
        /// </summary>
        /// <param name="request"></param>
        /// <param name="data"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public Tbl_OTABusiness CheckData(HeadRequest request, string data, string sign)
        {
            var business = _otaBusinessService.Get(request.InvokeUser);
            if (business == null)
            {
                return null;
            }
            data = data.Replace(" ", "+");
            var context = business.Saltcode.ToString().ToUpper() + data;
            string mySign = Md5Helper.Md5Encrypt32(context);
            if (sign.ToUpper() != mySign.ToUpper())
            {
                return null;
            }
            return business;
        }

        /// <summary>
        /// 验证产品格式是否正确
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ProductQueryRequest CheckFormatForProductQueryRequest(string data)
        {
            try
            {
                var request = Base64Helper.Base64EncodeToObject<ProductQueryRequest>(data);
                if (request.Head == null || request.Body == null)
                {
                    return null;
                }
                return request;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 验证创建订单格式是否正确
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public OrderCreateRequest CheckFormatForOrderCreateRequest(string data)
        {
            try
            {
                var request = Base64Helper.Base64EncodeToObject<OrderCreateRequest>(data);
                if (request.Head == null || request.Body == null || request.Body.OrderInfo == null)
                {
                    return null;
                }
                return request;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 验证创建单个产品订单格式是否正确
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public OrderSingleCreateRequest CheckFormatForOrderSingleCreateRequest(string data)
        {
            try
            {
                var request = Base64Helper.Base64EncodeToObject<OrderSingleCreateRequest>(data);
                if (request.Head == null || request.Body == null || request.Body.OrderInfo == null)
                {
                    return null;
                }
                return request;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 取消订单 格式验证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public OrderCancelRequest CheckFormatForOrderCancelRequest(string data)
        {
            try
            {
                var request = Base64Helper.Base64EncodeToObject<OrderCancelRequest>(data);
                if (request.Head == null || request.Body == null || request.Body.OrderInfo == null)
                {
                    return null;
                }
                return request;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 查询订单 格式验证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public OrderQueryRequest CheckFormatForOrderQueryRequest(string data)
        {
            try
            {
                var request = Base64Helper.Base64EncodeToObject<OrderQueryRequest>(data);
                if (request.Head == null || request.Body == null)
                {
                    return null;
                }
                return request;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 修改订单 格式验证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public OrderUpdateRequest CheckFormatForOrderUpdateRequest(string data)
        {
            try
            {
                var request = Base64Helper.Base64EncodeToObject<OrderUpdateRequest>(data);
                if (request.Head == null || request.Body == null|| request.Body.OrderInfo == null)
                {
                    return null;
                }
                return request;
            }
            catch
            {
                return null;
            }
        }
        

        /// <summary>
        /// 发送入园凭证短信 格式验证
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public MessageSendRequest CheckFormatForMessageSendRequest(string data)
        {
            try
            {
                var request = Base64Helper.Base64EncodeToObject<MessageSendRequest>(data);
                if (request.Head == null || request.Body == null || request.Body.OrderInfo == null)
                {
                    return null;
                }
                return request;
            }
            catch
            {
                return null;
            }
        }
        public bool Check(string identityKey, out List<int> ticketIds)
        {
            var business = _otaBusinessService.Get(identityKey);
            if (business == null)
            {
                ticketIds = null;
                return false;
            }
            ticketIds = _otaTicketRelationService.GetTicketIds(business.Id);
            return true;
        }

        /// <summary>
        /// 验证数据是否被篡改，进行认证
        /// </summary>
        /// <param name="request"></param>
        /// <param name="data"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public Tbl_OTABusiness CheckData(RequestBase request, string data, string sign)
        {
            var business = _otaBusinessService.Get(request.IdentityKey);
            if (business == null)
            {
                return null;
            }
            string mySign = Md5Helper.Md5Encrypt32(data, business.Saltcode.ToString());
            if (sign != mySign)
            {
                return null;
            }
            return business;
        }
    }
}
