using DocomSDK.TVM;
using FengjingSDK461.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Ticket.Application;
using Ticket.Model.Docom;
using Ticket.Utility.Logger;

namespace Ticket.GateWebApi.Controllers
{
    /// <summary>
    /// 售取票机
    /// </summary>
    public class TVMController : ApiController, ITVMInterface
    {
        private readonly SimpleLogger _log;
        private readonly TVMFacadeService _TVMFacadeService;

        /// <summary>
        /// 
        /// </summary>
        public TVMController(TVMFacadeService TVMFacadeService)
        {
            _log = new SimpleLogger();
            _TVMFacadeService = TVMFacadeService;
        }

        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [HttpPost]
        public TVMResult<GUITikcetConfig> DeviceIni(TVMDevice device)
        {
            _log.Info("获取产品信息DeviceIni：" + JsonHelper.ObjectToJsonStr(device));
            return _TVMFacadeService.GetTicketList(device);
        }

        /// <summary>
        /// 获取设备配置
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        [HttpPost]
        public TVMResult<TVMConfig> GetConfig(TVMDevice device)
        {
            _log.Info("获取设备配置GetConfig：" + JsonHelper.ObjectToJsonStr(device));
            TVMResult<TVMConfig> result = new TVMResult<TVMConfig>();
            result.ResultCode = 0;
            result.SysDate = DateTime.Now;
            result.Data = new TVMConfig()
            {
                AdminIDCard = "00000",
                CSPhoneNumber = "13012345678",
                FunctionMode = "FunctionMode",
                HostIP = "192.168.1.156",
                HostName = "HostName",
                MobileTerminalID = "safsadfasdfsaf",
                PaymentMode = "safasfasf"
            };
            return result;
        }

        /// <summary>
        /// 凭证码、身份证取票
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public TVMResult<TVMTicketInfo[]> GetTicketOrder(TicketOrder_Object obj)
        {
            _log.Info("凭证码、身份证取票GetTicketOrder：" + JsonHelper.ObjectToJsonStr(obj));
            var data = _TVMFacadeService.GetTicketOrder(obj.Msg, obj.MsgType);
            _log.Info("凭证码、身份证取票GetTicketOrder结果：" + JsonHelper.ObjectToJsonStr(data));
            return data;
        }

        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public TVMResult<TVM_Heartbeat_Object> Heartbeat(TVM_Heartbeat_Object obj)
        {
            //_log.Info("心跳Heartbeat：" + JsonHelper.ObjectToJsonStr(obj));
            TVMResult<TVM_Heartbeat_Object> result = new TVMResult<TVM_Heartbeat_Object>();
            result.ResultCode = 0;
            result.SysDate = DateTime.Now;
            result.Data = new TVM_Heartbeat_Object()
            {
                Device = obj.Device,
                SysTime = DateTime.Now
            };
            //_log.Debug(obj);
            return result;
        }

        /// <summary>
        /// 查询微信、支付宝支付情况
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public TVMResult<TVMPayInfo> QueryPay(QuickPayment_Object obj)
        {
            _log.Info("查询微信、支付宝支付情况QueryPay：" + JsonHelper.ObjectToJsonStr(obj));
            var data = _TVMFacadeService.QueryPay(obj);
            _log.Info("查询微信、支付宝支付情况QueryPay结果：" + JsonHelper.ObjectToJsonStr(data));
            return data;
        }

        /// <summary>
        /// 快捷支付
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public TVMResult<TVMPayInfo> QuickPayment(QuickPayment_Object obj)
        {
            _log.Info("快捷支付QuickPayment：" + JsonHelper.ObjectToJsonStr(obj));

            var data = _TVMFacadeService.QuickPayment(obj);
            _log.Info("快捷支付QuickPayment结果：" + JsonHelper.ObjectToJsonStr(data));
            return data;
        }

        /// <summary>
        /// 门票售卖
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public TVMResult<List<SaleTicket_Result>> SaleTicket(QuickPayment_Object obj)
        {
            _log.Info("门票售卖SaleTicket：" + JsonHelper.ObjectToJsonStr(obj));

            var data = _TVMFacadeService.SaleTicket(obj);
            _log.Info("门票售卖SaleTicket结果：" + JsonHelper.ObjectToJsonStr(data));
            return data;
        }

        /// <summary>
        /// 凭证码、身份证核销
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public TVMResult<bool> VerifyTicket(VerifyTicket_Object obj)
        {
            _log.Info("凭证码、身份证核销VerifyTicket：" + JsonHelper.ObjectToJsonStr(obj));
            foreach (var row in obj.TicketList)
            {
                _log.Info("作废订单号" + row.OrderID);
            }

            var data = _TVMFacadeService.VerifyTicket(obj.TicketList);
            _log.Info("凭证码、身份证取票GetTicketOrder结果：" + JsonHelper.ObjectToJsonStr(data));
            return data;
        }

        /// <summary>
        /// 透传接口
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public TVMResult<TVMExtendData> TransparentTVM(TVMExtendData obj)
        {
            _log.Info("透传接口TransparentTVM：" + JsonHelper.ObjectToJsonStr(obj));
            TVMResult<TVMExtendData> result = new TVMResult<TVMExtendData>();
            result.ResultCode = 0;
            result.SysDate = DateTime.Now;
            result.Data = _TVMFacadeService.GetScanTicketList(obj);
            return result;
        }
    }
}
