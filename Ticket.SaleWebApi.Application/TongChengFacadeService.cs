using FengjingSDK461.Core;
using FengjingSDK461.Enum;
using FengjingSDK461.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using Ticket.Infrastructure.TongCheng;
using Ticket.Infrastructure.TongCheng.Lib;
using Ticket.Infrastructure.TongCheng.Request;
using Ticket.Infrastructure.TongCheng.Response;

namespace Ticket.SaleWebApi.Application
{
    public class TongChengFacadeService
    {
        private readonly TongChengGateway _tongChengGateway;
        private readonly TicketGateway _ticketGateway;
        public TongChengFacadeService(
            TongChengGateway tongChengGateway)
        {
            _tongChengGateway = tongChengGateway;
            _ticketGateway = new TicketGateway(OtaType.TongCheng);
        }

        public string Handler(string request)
        {
            var requestData = _tongChengGateway.Check(request);
            if (!requestData.Status)
            {
                return requestData.Response;
            }
            var response = string.Empty;
            switch (requestData.Data.RequestHead.Method)
            {
                case "GetProductInfo":
                    response = GetProductInfo(requestData.Data);
                    break;
                case "CreateOrder":
                    response = CreateOrder(requestData.Data);
                    break;
                case "CancelOrder":
                    response = CancelOrder(requestData.Data);
                    break;
                case "QueryOrder":
                    response = QueryOrder(requestData.Data);
                    break;
            }
            return response;
        }

        /// <summary>
        /// 产品查询接口
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public string GetProductInfo(RequestData requestData)
        {
            var productQuery = _tongChengGateway.CheckProduct(requestData.RequestBody);
            var productRequest = productQuery.Data;
            var response = _ticketGateway.GetProduct(new ProductQueryRequest
            {
                Body = new Product
                {
                    Type = string.IsNullOrEmpty(productRequest.ProductNo) ? 1 : 2,
                    ProductId = string.IsNullOrEmpty(productRequest.ProductNo) ? 0 : Convert.ToInt32(productRequest.ProductNo),
                    CurrentPage = productRequest.PageIndex,
                    PageSize = productRequest.PageSize
                }
            });
            if (response.Head.Code == "000000")
            {
                var productResponse = new ProductResponse
                {
                    totalCount = response.Body.Count,
                    productList = new List<ProductItemResponse>()
                };
                foreach (var row in response.Body.ProductList)
                {
                    productResponse.productList.Add(new ProductItemResponse
                    {
                        productNo = row.ProductId.ToString(),
                        productName = row.ProductName,
                        retailPrice = Convert.ToInt64(row.PriceInfo.MarketPrice * 100),
                        webPrice = Convert.ToInt64(row.PriceInfo.SellPrice * 100),
                        contractPrice = Convert.ToInt64(row.PriceInfo.SellPrice * 100),
                        payType = 2,
                        beginSaleDate = row.BeginValidDate,
                        endSaleDate = row.EndValidDate,
                        beginValidDate = row.BeginValidDate,
                        endValidDate = row.EndValidDate,
                        checkWay = 2,
                        isCanRefund = row.CancelConfig.CanCancel,
                        isCanOverdueRefund = 0,
                        isStock = row.PriceInfo.SellStock == -1 ? 0 : 1
                    });
                }
                return _tongChengGateway.GetProductInfo(productResponse);
            }
            return _tongChengGateway.ErrorResult(ResultCode.Error, "系统出错");
        }
        /// <summary>
        /// 下单接口
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public string CreateOrder(RequestData requestData)
        {
            var request = _tongChengGateway.CheckCreateOrder(requestData.RequestBody);
            var data = request.Data;
            int productNo = 0;
            if (!int.TryParse(data.productNo, out productNo))
            {
                return _tongChengGateway.ErrorResult(ResultCode.DataError, "产品编号错误");
            }
            if (data.payType != 1)
            {
                return _tongChengGateway.ErrorResult(ResultCode.DataError, "支付方式必须在线支付");
            }
            var response = _ticketGateway.SingleCreateOrder(new OrderSingleCreateRequest
            {
                Body = new OrderSingleCreateBody
                {
                    OrderInfo = new OrderSingleInfo
                    {
                        OrderOtaId = data.orderSerialId,
                        OrderPayStatus = 1,
                        OrderPrice = (data.price / 100) * data.tickets,
                        OrderQuantity = data.tickets,
                        Ticket = new ProductItem
                        {
                            ProductId = productNo,
                            Quantity = data.tickets,
                            ProductName = "",
                            SellPrice = data.price / 100
                        },
                        VisitDate = data.travelDate,
                        ContactPerson = new ContactPerson
                        {
                            BuyName = data.bookName,
                            Name = data.bookName,
                            Mobile = data.bookMobile,
                            CardType = "ID_CARD",
                            CardNo = data.idCard
                        }
                    }
                }
            });
            if (response.Head.Code == "000000")
            {
                var tongChengResponse = new CreateOrderResponse
                {
                    partnerOrderId = response.Body.OrderId,
                    partnerCode = response.Body.Code
                };
                return _tongChengGateway.CreateOrder(tongChengResponse);
            }
            else if (response.Head.Code == "113019")
            {
                return _tongChengGateway.ErrorResult(ResultCode.CreateOrderForProductDownline, response.Head.Describe);
            }
            else if (response.Head.Code == "113026")
            {
                return _tongChengGateway.ErrorResult(ResultCode.CreateOrderForLowStocks, response.Head.Describe);
            }
            else if (response.Head.Code == "113020")
            {
                return _tongChengGateway.ErrorResult(ResultCode.CreateOrderForPriceDisagreement, response.Head.Describe);
            }
            else if (response.Head.Code == "113021")
            {
                return _tongChengGateway.ErrorResult(ResultCode.Error, "系统出错");
            }
            return _tongChengGateway.ErrorResult(ResultCode.CreateOrderForReservationRestriction, response.Head.Describe);
        }
        /// <summary>
        /// 取消接口
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public string CancelOrder(RequestData requestData)
        {
            var request = _tongChengGateway.CheckCancelOrder(requestData.RequestBody);
            var data = request.Data;
            var response = _ticketGateway.CancelOrder(new OrderCancelRequest
            {
                Body = new OrderCancelBody
                {
                    OrderInfo = new OrderCancelInfo
                    {
                        OrderId = data.partnerOrderId,
                        OrderPrice = 0,
                        OrderQuantity = data.tickets,
                        reason = data.reason,
                        Seq = data.orderSerialId
                    }
                }
            });
            if (response.Head.Code == "000000")
            {
                var tongChengResponse = new CancelOrderResponse
                {
                    refundStatus = 1,
                    remark = "成功"
                };
                return _tongChengGateway.CancelOrder(tongChengResponse);
            }
            else if (response.Head.Code == "114007")
            {
                var tongChengResponse = new CancelOrderResponse
                {
                    refundStatus = 3,
                    remark = "订单取消失败，不支持部分取消"
                };
                return _tongChengGateway.CancelOrder(tongChengResponse);
            }
            else if (response.Head.Code == "114004")
            {
                return _tongChengGateway.ErrorResult(ResultCode.OrderNumberNotExist, response.Head.Describe);
            }
            else if (response.Head.Code == "114009" || response.Head.Code == "114013")
            {
                return _tongChengGateway.ErrorResult(ResultCode.CancelOrderForConsume, response.Head.Describe);
            }
            else if (response.Head.Code == "114010")
            {
                return _tongChengGateway.ErrorResult(ResultCode.CancelOrderForCancel, response.Head.Describe);
            }
            else if (response.Head.Code == "114011")
            {
                return _tongChengGateway.ErrorResult(ResultCode.CancelOrderForExpired, response.Head.Describe);
            }
            else if (response.Head.Code == "114012")
            {
                return _tongChengGateway.ErrorResult(ResultCode.Error, response.Head.Describe);
            }
            return _tongChengGateway.ErrorResult(ResultCode.DataError, response.Head.Describe);
        }
        /// <summary>
        /// 查询接口
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public string QueryOrder(RequestData requestData)
        {
            var request = _tongChengGateway.CheckQueryOrder(requestData.RequestBody);
            var data = request.Data;
            var response = _ticketGateway.QueryOrder(new OrderQuery
            {
                OrderId = data.partnerOrderId
            });
            if (response.Head.Code == "000000")
            {
                var info = response.Body.OrderInfo.EticketInfo.FirstOrDefault();
                var tongChengResponse = new QueryOrderResponse
                {
                    orderStatus = 0,
                    tickets = 0
                };
                if (info.OrderStatus == (int)TicketOrderStatus.Success)
                {
                    tongChengResponse.orderStatus = 1;
                    tongChengResponse.operateTime = info.CreateTime;
                    tongChengResponse.tickets = info.EticketQuantity;
                }
                else if (info.OrderStatus == (int)TicketOrderStatus.Canncel)
                {
                    tongChengResponse.orderStatus = 3;
                    tongChengResponse.operateTime = info.CancelTime;
                    tongChengResponse.tickets = info.EticketQuantity;
                }
                else if (info.OrderStatus == (int)TicketOrderStatus.Consume)
                {
                    tongChengResponse.orderStatus = 5;
                    tongChengResponse.operateTime = info.DelayCheckTime;
                    tongChengResponse.tickets = info.UseQuantity;
                }
                return _tongChengGateway.QueryOrder(tongChengResponse);
            }
            else if (response.Head.Code == "115002")
            {
                return _tongChengGateway.ErrorResult(ResultCode.OrderNumberNotExist, response.Head.Describe);
            }
            return _tongChengGateway.ErrorResult(ResultCode.DataError, response.Head.Describe);
        }
    }
}
