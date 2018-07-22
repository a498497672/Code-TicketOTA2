using FengjingSDK461.Model.Response;
using FengjingSDK461.Model.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using Ticket.Core.Repository;
using Ticket.Core.Service;
using Ticket.SqlSugar.Models;
using Ticket.Model.Enum;
using Ticket.Utility.Config;

namespace Ticket.Application
{
    public class TicketFacadeService
    {
        private readonly OtaBusinessService _otaBusinessService;
        private readonly OtaTicketRelationService _otaTicketRelationService;
        private readonly TicketService _ticketService;
        private readonly AuthorizationService _AuthorizationService;
        private readonly ScenicService _scenicService;
        private readonly TicketRuleService _ticketRuleService;
        private readonly TicketRelationRepository _ticketRelationRepository;

        public TicketFacadeService(
            OtaBusinessService otaBusinessService,
            OtaTicketRelationService otaTicketRelationService,
            TicketService ticketService,
            AuthorizationService AuthorizationService,
            ScenicService scenicService,
            TicketRuleService ticketRuleService,
            TicketRelationRepository ticketRelationRepository
            )
        {
            _otaBusinessService = otaBusinessService;
            _otaTicketRelationService = otaTicketRelationService;
            _ticketService = ticketService;
            _AuthorizationService = AuthorizationService;
            _scenicService = scenicService;
            _ticketRuleService = ticketRuleService;
            _ticketRelationRepository = ticketRelationRepository;
        }

        /// <summary>
        /// 获取所有产品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PageResult GetAll(string data, string sign)
        {
            ProductResponse result = new ProductResponse
            {
                Head = HeadResult.V1
            };
            var request = _AuthorizationService.CheckFormatForProductQueryRequest(data);
            if (request == null)
            {
                return PageDataResult.JsonParsingFailure();
            }
            var business = _AuthorizationService.CheckData(request.Head, data, sign);
            if (business == null)
            {
                return PageDataResult.SignatureError();
            }
            var validResult = _ticketService.ValidDataForProductQueryRequest(request);
            if (!validResult.Status)
            {
                result.Head.Code = validResult.Code;
                result.Head.Describe = validResult.Message;
                return PageDataResult.Data(result, business.Saltcode.ToString());
            }
            var ticketIds = _otaTicketRelationService.GetTicketIds(business.Id);

            List<Tbl_Ticket> list = new List<Tbl_Ticket>();
            int total = 0;
            if (request.Body.Type == (int)ProductQureyType.OneProduct)
            {
                var count = ticketIds.Count(a => a.Equals(request.Body.ProductId));
                if (count > 0)
                {
                    //获取单个产品
                    var ticke = _ticketService.Get(request.Body.ProductId);
                    if (ticke != null)
                    {
                        list.Add(ticke);
                        total = 1;
                    }
                }
                if (list.Count <= 0)
                {
                    result.Head.Code = "111001";
                    result.Head.Describe = "获取产品异常，产品不存在";
                    return PageDataResult.Data(result, business.Saltcode.ToString());
                }
            }
            else if (request.Body.Type == (int)ProductQureyType.NoPage)
            {
                //不分页
                list = _ticketService.GetTickets(ticketIds, business.ScenicId);
                total = list.Count;
            }
            else
            {
                //分页
                list = _ticketService.GetPageList(ticketIds, business.ScenicId, request.Body.PageSize, request.Body.CurrentPage, out total);
            }
            result.Body = new ProductPage
            {
                Count = total,
                ProductList = Mapper(list)
            };
            result.Head.Code = "000000";
            result.Head.Describe = "成功";
            return PageDataResult.Data(result, business.Saltcode.ToString());
        }

        public List<ProductInfo> Mapper(List<Tbl_Ticket> list)
        {
            var scenics = _scenicService.GetList(list.Select(a => a.ScenicId).Distinct().ToList());
            var ticketRules = _ticketRuleService.GetList(list.Select(a => a.RuleId).ToList());
            var productList = new List<ProductInfo>();

            DateTime validityDate = DateTime.Now.Date;

            foreach (var row in list)
            {
                var product = new ProductInfo();
                product.ProductId = row.TicketId;
                product.ProductName = row.TicketName;
                product.BeginValidDate = row.ExpiryDateStart.ToString("yyyy-MM-dd");
                product.EndValidDate = row.ExpiryDateEnd.ToString("yyyy-MM-dd");

                var scenic = scenics.FirstOrDefault(a => a.ScenicId == row.ScenicId);
                var ticketRule = ticketRules.FirstOrDefault(a => a.Id == row.RuleId);

                //根据游玩日期，变动价格
                //根据orderId 顺序排列 取第一条
                var ticketRelation = _ticketRelationRepository.FirstOrDefault(p =>
                p.ExpiryDateStart <= validityDate &&
                p.ExpiryDateEnd >= validityDate &&
                p.ScenicId == row.ScenicId &&
                p.TicketId == row.TicketId, c => c.OrderId);
                if (ticketRelation != null && ticketRelation.SalePrice > 0)
                {
                    row.SalePrice = ticketRelation.SalePrice;
                    row.MarkPrice = ticketRelation.MarkPrice;
                }

                if (scenic != null)
                {
                    product.SightName = scenic.ScenicName;
                    product.City = scenic.CityName;
                    product.Address = scenic.FullAddress;
                }
                product.PriceInfo = new PriceInfo
                {
                    UseDate = row.ExpiryDateStart.ToString("yyyy-MM-dd"),
                    MarketPrice = row.MarkPrice == null ? 0 : row.MarkPrice.Value,
                    SellPrice = row.SalePrice,
                    SellStock = row.StockCount == null ? -1 : row.StockCount.Value
                };
                product.BookInfo = new BookInfo
                {
                    PaymentType = "PREPAY",
                    BookAdvanceDay = 0,
                    BookAdvanceTime = "",
                    UseAdvanceHour = row.DelayCheck == null ? 0 : row.DelayCheck.Value,
                    AutoCancelTime = 0,
                    BookPersonType = "CONTACT_PERSON",
                    VisitPersonRequi = "",
                    ValidType = 1,
                    DaysAfterUseDate = 1
                };
                var refund = ticketRule.RefundDay + "_" + ticketRule.RefundHour + ":" + ticketRule.RefundMinute;
                product.CancelConfig = new CancelConfig
                {
                    CanCancel = ticketRule.CanRefund ? 1 : 0,
                    CancelApplyTimeBeforeValidEndDay = ticketRule.CanRefund ? refund : ""
                };
                product.OtherConfig = new OtherConfig
                {
                    SmsTemplet = AppSettingsConfig.ProductOrderInfoPath,
                    ETicketType = "C_CODE",
                    TicketWhoSent = 0
                };
                product.Remind = "购票说明 :" + ticketRule.TicketNotice + "  入园说明 :" + ticketRule.EnterNotice + "   退订规则 :" + ticketRule.RefundNotice;
                productList.Add(product);
            }
            return productList;
        }
    }
}
