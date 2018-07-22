using FengjingSDK461.Core;
using FengjingSDK461.Enum;
using FengjingSDK461.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Infrastructure.Ctrip;
using Ticket.Infrastructure.Ctrip.Lib;
using Ticket.Infrastructure.Ctrip.Request;
using Ticket.Infrastructure.Ctrip.Response;

namespace Ticket.SaleWebApi.Application
{
    /// <summary>
    /// 携程
    /// </summary>
    public class CtripFacadeService
    {
        private readonly CtripGateway _ctripGateway;
        private readonly TicketGateway _ticketGateway;
        public CtripFacadeService(
            CtripGateway ctripGateway)
        {
            _ctripGateway = ctripGateway;
            _ticketGateway = new TicketGateway(OtaType.Ctrip);
        }

        public object Handler(string request)
        {
            var requestData = _ctripGateway.CheckData(request);
            if (!requestData.Status)
            {
                return requestData.Response;
            }

            switch (requestData.Data.Header.ServiceName)
            {
                case "VerifyOrder":
                    return VerifyOrder(requestData.Data.Body);
                case "CreateOrder":
                    return CreateOrder(requestData.Data.Body);
                case "CancelOrder":
                    return CancelOrder(requestData.Data.Body);
                case "QueryOrder":
                    return QueryOrder(requestData.Data.Body);
            }
            return null;
        }

        /// <summary>
        /// 下单验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object VerifyOrder(string request)
        {
            var requestBody = _ctripGateway.CheckVerifyOrder(request);
            if (requestBody == null)
            {
                return _ctripGateway.ErrorResult(ResultCode.JsonParsingFailure, "报文解析失败");
            }

            var data = requestBody.Data;
            bool isProduct = true;
            bool isDate = true;//判断游玩时间是否是同一天
            bool isCard = true;
            string useStartDate = data.Items[0].useStartDate;//游玩时间
            var productItems = new List<ProductItem>();
            foreach (var row in data.Items)
            {
                foreach (var item in row.passengers)
                {
                    if (item.cardType == "0")
                    {
                        isCard = false;
                    }
                }
                if (useStartDate != row.useStartDate)
                {
                    isDate = false;
                }
                int productId = 0;
                if (!int.TryParse(row.PLU, out productId))
                {
                    isProduct = false;
                }
                productItems.Add(new ProductItem
                {
                    ProductId = productId,
                    Quantity = row.quantity,
                    ProductName = "",
                    SellPrice = row.price
                });
            }
            if (!isCard)
            {
                return _ctripGateway.ErrorResult(ResultCode.CreateOrderForParameterEmpty, "缺失证件");
            }
            if (!isProduct)
            {
                return _ctripGateway.ErrorResult(ResultCode.CreateOrderForProductNotExist, "产品Id不存在/错误");
            }
            if (!isDate)
            {
                return _ctripGateway.ErrorResult(ResultCode.CreateOrderForDate, "不同的产品，游玩时间不一致");
            }

            var response = _ticketGateway.VerifyOrder(new OrderCreateRequest
            {
                Body = new OrderCreateBody
                {
                    OrderInfo = new OrderInfo
                    {
                        OrderOtaId = "",
                        OrderPayStatus = 1,
                        OrderPrice = productItems.Sum(a => a.SellPrice * a.Quantity),
                        OrderQuantity = productItems.Sum(a => a.Quantity),
                        TicketList = productItems,
                        VisitDate = useStartDate,
                        ContactPerson = new ContactPerson
                        {
                            BuyName = data.Contacts[0].name,
                            Name = data.Contacts[0].name,
                            Mobile = data.Contacts[0].mobile,
                            CardType = data.Items[0].passengers[0].cardType == "1" ? "ID_CARD" : "",
                            CardNo = data.Items[0].passengers[0].cardType == "1" ? data.Items[0].passengers[0].cardNo : ""
                        }
                    }
                }
            });
            if (response.Head.Code == "000000")
            {
                var responseBody = new VerifyOrderBodyRespose
                {
                    Items = new List<VerifyOrderItemRespose>()
                };
                foreach (var row in response.Body.Item)
                {
                    responseBody.Items.Add(new VerifyOrderItemRespose
                    {
                        PLU = row.ProductId,
                        inventorys = new VerifyOrderInventoryRespose
                        {
                            quantity = row.quantity,
                            useDate = row.useDate
                        }

                    });
                }
                return _ctripGateway.VerifyOrder(responseBody);
            }
            else if (response.Head.Code == "113019")
            {
                return _ctripGateway.ErrorResult(ResultCode.CreateOrderForProductDownline, response.Head.Describe);
            }
            else if (response.Head.Code == "113026")
            {
                var responseBody = new VerifyOrderBodyRespose
                {
                    Items = new List<VerifyOrderItemRespose>()
                };
                foreach (var row in response.Body.Item)
                {
                    responseBody.Items.Add(new VerifyOrderItemRespose
                    {
                        PLU = row.ProductId,
                        inventorys = new VerifyOrderInventoryRespose
                        {
                            quantity = row.quantity,
                            useDate = row.useDate
                        }

                    });
                }
                return _ctripGateway.ErrorResult(ResultCode.CreateOrderForLowStocks, response.Head.Describe);
            }
            else if (response.Head.Code == "113021")
            {
                return _ctripGateway.ErrorResult(ResultCode.SystemError, "系统出错");
            }
            return _ctripGateway.ErrorResult(ResultCode.CreateOrderForParameterIllegality, response.Head.Describe);
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public object CreateOrder(string request)
        {
            var requestBody = _ctripGateway.CheckCreateOrder(request);
            if (requestBody == null)
            {
                return _ctripGateway.ErrorResult(ResultCode.JsonParsingFailure, "报文解析失败");
            }
            var data = requestBody.Data;
            bool isProduct = true;
            bool isDate = true;//判断游玩时间是否是同一天
            bool isCard = true;
            string useStartDate = data.Items[0].useStartDate;//游玩时间
            var productItems = new List<ProductItem>();
            foreach (var row in data.Items)
            {
                foreach (var item in row.passengers)
                {
                    if (item.cardType == "0")
                    {
                        isCard = false;
                    }
                }
                if (useStartDate != row.useStartDate)
                {
                    isDate = false;
                }
                int productId = 0;
                if (!int.TryParse(row.PLU, out productId))
                {
                    isProduct = false;
                }
                productItems.Add(new ProductItem
                {
                    ProductId = productId,
                    Quantity = row.quantity,
                    ProductName = "",
                    SellPrice = row.price,
                    ItemId = row.itemId
                });
            }
            if (!isCard)
            {
                return _ctripGateway.ErrorResult(ResultCode.CreateOrderForParameterEmpty, "缺失证件");
            }
            if (!isProduct)
            {
                return _ctripGateway.ErrorResult(ResultCode.CreateOrderForProductNotExist, "产品Id不存在/错误");
            }
            if (!isDate)
            {
                return _ctripGateway.ErrorResult(ResultCode.CreateOrderForDate, "不同的产品，游玩时间不一致");
            }
            //if (data.payMode != "1")
            //{
            //    return _ctripGateway.ErrorResult(ResultCode.CreateOrderForParameterIllegality, "付款方式必须网站预付");
            //}
            var response = _ticketGateway.CreateOrder(new OrderCreateRequest
            {
                Body = new OrderCreateBody
                {
                    OrderInfo = new OrderInfo
                    {
                        OrderOtaId = data.otaOrderId,
                        OrderPayStatus = 1,
                        OrderPrice = productItems.Sum(a => a.SellPrice * a.Quantity),
                        OrderQuantity = productItems.Sum(a => a.Quantity),
                        TicketList = productItems,
                        VisitDate = useStartDate,
                        ContactPerson = new ContactPerson
                        {
                            BuyName = data.Contacts[0].name,
                            Name = data.Contacts[0].name,
                            Mobile = data.Contacts[0].mobile,
                            CardType = data.Items[0].passengers[0].cardType == "1" ? "ID_CARD" : "",
                            CardNo = data.Items[0].passengers[0].cardType == "1" ? data.Items[0].passengers[0].cardNo : ""
                        }
                    }
                }
            });
            if (response.Head.Code == "000000")
            {
                var responseBody = new CreateOrderBodyResponse
                {
                    otaOrderId = response.Body.OtaOrderId,
                    supplierOrderId = response.Body.OrderId,
                    supplierConfirmType = 1,
                    voucherSender = 1,
                    items = new List<CreateOrderitemRespose>(),
                    vouchers = new List<CreateOrderVouchers>()
                };
                foreach (var row in response.Body.Item)
                {
                    responseBody.items.Add(new CreateOrderitemRespose
                    {
                        itemId = row.OtaOrderDetailId,
                        inventorys = new CreateOrderInventoryRespose
                        {
                            quantity = row.quantity,
                            useDate = row.useDate
                        }
                    });
                    responseBody.vouchers.Add(new CreateOrderVouchers
                    {
                        itemId = row.OtaOrderDetailId,
                        voucherType = 2,
                        voucherCode = row.CertificateNo,
                        voucherData = ""
                    });
                }
                return _ctripGateway.CreateOrder(responseBody);
            }
            else if (response.Head.Code == "113019")
            {
                return _ctripGateway.ErrorResult(ResultCode.CreateOrderForProductDownline, response.Head.Describe);
            }
            else if (response.Head.Code == "113026")
            {
                //var responseBody = new CreateOrderBodyRespose
                //{
                //    inventory = response.Body.Inventory
                //};
                return _ctripGateway.ErrorResult(ResultCode.CreateOrderForLowStocks, response.Head.Describe);
            }
            else if (response.Head.Code == "113021")
            {
                return _ctripGateway.ErrorResult(ResultCode.SystemError, "系统出错");
            }
            return _ctripGateway.ErrorResult(ResultCode.CreateOrderForParameterIllegality, response.Head.Describe);
        }
        /// <summary>
        /// 取消接口
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public object CancelOrder(string request)
        {
            var requestBody = _ctripGateway.CheckCancelOrder(request);
            if (requestBody == null)
            {
                return _ctripGateway.ErrorResult(ResultCode.JsonParsingFailure, "报文解析失败");
            }
            var data = requestBody.Data;
            var CancelOrderRequest = new OrderCancelRequest
            {
                Body = new OrderCancelBody
                {
                    OrderInfo = new OrderCancelInfo
                    {
                        OrderId = data.SupplierOrderId,
                        OtaOrderId = data.OtaOrderId,
                        OrderPrice = 0,
                        OrderQuantity = data.Items.Sum(a => a.Quantity),
                        reason = "",
                        Seq = data.SequenceId,
                        Items = new List<CancelOrderItemInfo>()
                    }
                }
            };
            foreach (var row in data.Items)
            {
                CancelOrderRequest.Body.OrderInfo.Items.Add(new CancelOrderItemInfo
                {
                    ItemId = row.ItemId,
                    ProductId = row.PLU,
                    Quantity = row.Quantity,
                    Amount = row.Amount
                });
            }
            var response = _ticketGateway.CancelOrderDetail(CancelOrderRequest);
            if (response.Head.Code == "000000")
            {
                var responseBody = new CancelOrderBodyRespose
                {
                    supplierConfirmType = 1,
                    items = new List<CancelOrderitemRespose>()
                };
                foreach (var row in data.Items)
                {
                    responseBody.items.Add(new CancelOrderitemRespose
                    {
                        itemId = row.ItemId
                    });
                }
                return _ctripGateway.CancelOrder(responseBody);
            }
            else if (response.Head.Code == "114003" || response.Head.Code == "114014")
            {
                return _ctripGateway.ErrorResult(ResultCode.CancelOrderForNotCount, response.Head.Describe);
            }
            else if (response.Head.Code == "114004")
            {
                return _ctripGateway.ErrorResult(ResultCode.CancelOrderNumberNotExist, response.Head.Describe);
            }
            else if (response.Head.Code == "114009")
            {
                return _ctripGateway.ErrorResult(ResultCode.CancelOrderForConsume, response.Head.Describe);
            }
            else if (response.Head.Code == "114010" || response.Head.Code == "114013")
            {
                return _ctripGateway.ErrorResult(ResultCode.CancelOrderForCancel, response.Head.Describe);
            }
            else if (response.Head.Code == "114011")
            {
                return _ctripGateway.ErrorResult(ResultCode.CancelOrderForExpired, response.Head.Describe);
            }
            else if (response.Head.Code == "114012")
            {
                return _ctripGateway.ErrorResult(ResultCode.SystemError, response.Head.Describe);
            }
            return _ctripGateway.ErrorResult(ResultCode.CancelOrderForError, response.Head.Describe);
        }
        /// <summary>
        /// 查询接口
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public object QueryOrder(string request)
        {
            var requestBody = _ctripGateway.CheckQueryOrder(request);
            if (requestBody == null)
            {
                return _ctripGateway.ErrorResult(ResultCode.JsonParsingFailure, "报文解析失败");
            }
            var data = requestBody.Data;
            var response = _ticketGateway.QueryOrder(new OrderQuery
            {
                OrderId = data.SupplierOrderId,
                OtaOrderId = data.OtaOrderId
            });
            if (response.Head.Code == "000000")
            {
                var info = response.Body.OrderInfo.EticketInfo.FirstOrDefault();
                var responseBody = new QueryOrderBodyResponse
                {
                    SupplierOrderId = data.SupplierOrderId,
                    OtaOrderId = data.OtaOrderId,
                    items = new List<QueryOrderitemRespose>()
                };
                foreach (var row in response.Body.OrderInfo.EticketInfo)
                {
                    var item = new QueryOrderitemRespose
                    {
                        cancelQuantity = 0,
                        useQuantity = row.UseQuantity,
                        itemId = row.OtaOrderDetailId,
                        quantity = row.EticketQuantity,
                        useStartDate = row.UseStartDate,
                        useEndDate = row.UseEndDate
                    };
                    if (row.OrderStatus == (int)TicketOrderStatus.Success)
                    {
                        item.orderStatus = 2;
                    }
                    else if (row.OrderStatus == (int)TicketOrderStatus.Canncel)
                    {
                        item.cancelQuantity = row.EticketQuantity;
                        item.orderStatus = 5;
                    }
                    else if (row.OrderStatus == (int)TicketOrderStatus.Consume)
                    {
                        item.orderStatus = 8;
                        if (info.UseQuantity != info.EticketQuantity)
                        {
                            item.orderStatus = 7;
                        }
                    }
                    responseBody.items.Add(item);
                }
                return _ctripGateway.QueryOrder(responseBody);
            }
            else if (response.Head.Code == "115002")
            {
                return _ctripGateway.ErrorResult(ResultCode.QueryOrderNumberNotExist, response.Head.Describe);
            }
            return _ctripGateway.ErrorResult(ResultCode.SystemError, response.Head.Describe);
        }
    }
}
