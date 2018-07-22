using FengjingSDK461.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Ticket.Core.Service;
using Ticket.SqlSugar.Models;
using Ticket.TaskEngine.Application.MobileTicketService;
using Ticket.TaskEngine.Application.Model;
using Ticket.Utility.Extensions;
using Ticket.Utility.Helpers;

namespace Ticket.TaskEngine.Application.Service
{
    public class TicketFacadeService
    {
        private readonly string _merCode = ConfigurationManager.AppSettings["service:MerCode"];
        private readonly string _key = ConfigurationManager.AppSettings["service:Key"];
        private readonly string _terminalNo = ConfigurationManager.AppSettings["service:TerminalNo"];
        private readonly TicketConsumeService _ticketConsumeService;
        private readonly TicketService _ticketService;
        private readonly MobileTicketSoapClient _client;

        public TicketFacadeService(TicketConsumeService ticketConsumeService, TicketService ticketService)
        {
            _ticketConsumeService = ticketConsumeService;
            _ticketService = ticketService;
            _client = new MobileTicketSoapClient();
        }

        ///// <summary>
        ///// 门票入园核销
        ///// </summary>
        //public void TicketConsumeLine()
        //{
        //    var _terminalNo = "L20170713031920";
        //    var ticketConsumes = _ticketConsumeService.GetList();
        //    foreach (var row in ticketConsumes)
        //    {
        //        string timeStamp = DateTime.Now.GetTimeStamp();
        //        var sign = Md5HashHelper.HashPassword(_merCode + _key + timeStamp);

        //        var ticket = _ticketService.Get(row.TicketId);
        //        var ticketJson = new List<TicketJson>();


        //        if (ticket == null)
        //        {
        //            continue;
        //        }
        //        ticketJson.Add(new TicketJson
        //        {
        //            ProductCode = ticket.Code,
        //            CodeStr = row.BarCode,
        //            TicketCount = "1"
        //        });

        //        var ticketJsonStr = JsonHelper.ObjectToJson(ticketJson);
        //        var result = _client.TicketConsumeLine(_merCode, row.OtaOrderNo, ticketJsonStr, timeStamp, sign);
        //        var resultData = JsonHelper.JsonToObject<ResultData>(result);
        //        if (resultData.IsTrue && resultData.ResultCode == "200")
        //        {
        //            //成功
        //            _ticketConsumeService.Update(row);
        //        }
        //    }
        //}


        public void TicketConsumeLine()
        {
            var ticketConsumes = _ticketConsumeService.GetList();
            List<Tbl_TicketConsume> list = new List<Tbl_TicketConsume>();
            int count = 0;
            int sum = 0;
            foreach (var row in ticketConsumes)
            {
                sum++;
                count++;
                list.Add(row);
                if (count == 1 || sum == ticketConsumes.Count)
                {
                    string timeStamp = DateTime.Now.GetTimeStamp();
                    var sign = Md5HashHelper.HashPassword(_merCode + _key + timeStamp);

                    var ticketIds = list.Select(a => a.TicketId).Distinct().ToList();
                    var tickets = _ticketService.GetList(ticketIds);
                    var ticketJson = new List<TicketJson>();
                    foreach (var item in list)
                    {
                        var ticket = tickets.FirstOrDefault(a => a.TicketId == item.TicketId);
                        if (ticket == null)
                        {
                            continue;
                        }
                        ticketJson.Add(new TicketJson
                        {
                            ProductCode = ticket.Code,
                            CodeStr = item.BarCode,
                            TicketCount = "1"
                        });
                    }

                    var ticketJsonStr = JsonHelper.ObjectToJson(ticketJson);
                    var result = _client.TicketConsumeLine(_merCode, _terminalNo, ticketJsonStr, timeStamp, sign);
                    var resultData = JsonHelper.JsonToObject<ResultData>(result);
                    if (resultData.IsTrue && resultData.ResultCode == "200")
                    {
                        //成功
                        _ticketConsumeService.Update(list);
                    }
                    Console.Write("\n门票入园核销：" + (resultData.IsTrue == true ? "成功" : "失败") + "  订单号：" + row.OrderNo);
                    count = 0;
                    list = new List<Tbl_TicketConsume>();
                }
            }
        }
    }
}
