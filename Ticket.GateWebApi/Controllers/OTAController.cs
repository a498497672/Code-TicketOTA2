using DocomSDK.Ticket.Data;
using DocomSDK.Ticket.Enum;
using DocomSDK.Ticket.Interface;
using FengjingSDK461.Helpers;
using System;
using System.Web.Http;
using Ticket.Application;
using Ticket.Model.Enum;
using Ticket.Utility.Logger;

namespace YTS.WebApi.Controllers
{
    /// <summary>
    /// 票务闸机API业务接口定义
    /// </summary>
    public class OTAController : ApiController, ITicketAPI
    {
        private readonly SimpleLogger _log;
        private readonly OtaFacadeService _otaFacadeService;

        /// <summary>
        /// 票务闸机API
        /// </summary>
        public OTAController(OtaFacadeService otaFacadeService)
        {
            _log = new SimpleLogger();
            _otaFacadeService = otaFacadeService;
        }

        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public Result<Heartbeat_Object> Heartbeat(Heartbeat_Object token)
        {
            Result<Heartbeat_Object> result = new Result<Heartbeat_Object>();
            try
            {
                result.Data = new Heartbeat_Object()
                {
                    Time = DateTime.Now,
                    Token = token.Token
                };
                result.Description = "OK";
                result.State = Result_Code.OK;
                //_log.Debug(_otaFacadeService.ToJsonString(token));
            }
            catch (Exception ex)
            {
                result.Description = ex.Message;
                _log.Error(ex);
            }
            return result;
        }

        #region 检票业务接口

        /// <summary>
        /// 扫描进入，请求检票
        /// </summary>
        /// <param name="obj">请求参数</param>
        public Result<TicketResult> Ticket_CheckTicket(CheckTicket_Object obj)
        {
            _log.Info("进入【请求检票】事件: Ticket_CheckTicket");
            _log.Info("得到数据: " + JsonHelper.ObjectToJsonStr(obj));

            obj.Number = obj.Number.Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(" ", string.Empty);

            try
            {
                #region 判断设备是否存在
                string msg;
                bool isState = _otaFacadeService.CheckDoorGate(obj.Device.DeviceName, out msg);
                if (!isState)
                {
                    _log.Info("设备不存在：" + obj.Device.DeviceName);
                    return TicketDataResult.InvalidDeviceResult(msg);
                }
                #endregion
                //验票
                return _otaFacadeService.TicketValid(obj);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return TicketDataResult.FailResult("系统异常,请联系管理员！");
            }
        }

        /// <summary>
        /// 出口模式下的过闸记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Result<TicketResult> Ticket_DoorChannelExportPassed(DoorChannelExport_Object obj)
        {
            _log.Info("进入【出口模式下的过闸记录】事件: Ticket_DoorChannelExportPassed");
            return TicketDataResult.TaskTerminationResult();
        }

        /// <summary>
        /// 设备上报落杆模式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Result<TicketResult> Ticket_ShutdownChannel(ShutdownChannel_Object obj)
        {
            _log.Info("进入【设备上报落杆模式】事件: Ticket_ShutdownChannel");
            return TicketDataResult.TaskTerminationResult();
        }

        /// <summary>
        /// 提交人脸检测结果
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Result<TicketResult> Ticket_SubmitFaceDetect(FaceDetect obj)
        {
            _log.Info("进入【提交人脸检测结果】事件: Ticket_SubmitFaceDetect");
            return TicketDataResult.TaskTerminationResult();
        }

        /// <summary>
        /// 提交采集结果
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Result<TicketResult> Ticket_SubmitCollect(SubmitCollect_Object obj)
        {
            _log.Info("进入【提交采集结果】事件: Ticket_SubmitCollect");
            //_log.Info("得到数据: " + _otaFacadeService.ToJsonString(obj));
            return TicketDataResult.TaskTerminationResult();
        }

        /// <summary>
        /// 提交指纹比对结果
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Result<TicketResult> Ticket_SubmitFingerprints(SubmitFingerprints_Object obj)
        {
            _log.Info("进入【提交指纹比对结果】事件: Ticket_SubmitFingerprints");
            //_log.Info("得到数据: " + _otaFacadeService.ToJsonString(obj));
            try
            {
                obj.Session.Fingerprint_Pass = obj.Result;
                _log.Info("指纹验证状态：" + obj.Result);
                if (!obj.Result)
                {
                    return TicketDataResult.TaskTerminationResult("验证失败", "验证失败");
                }
                return TicketDataResult.VerifyPassResult(obj.Ticket, obj.Session, "验证成功");
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return TicketDataResult.FailResult("系统异常,请联系管理员！");
            }
        }

        /// <summary>
        /// 提交过闸结果
        /// </summary>
        /// <param name="obj">过闸结果</param>
        /// <returns></returns>
        public Result<TicketResult> Ticket_SubmitWalkPast(SubmitWalkPast_Object obj)
        {
            _log.Info("进入【提交过闸结果】事件: Ticket_SubmitWalkPast");
            _log.Info("得到数据: " + JsonHelper.ObjectToJsonStr(obj));
            try
            {
                _log.Info("本次过闸人数 Count: " + obj.Count);
                _log.Info("可用人数 CapacitySize: " + obj.Ticket.CapacitySize);
                _log.Info("已用人数 UsageCount: " + obj.Ticket.UsageCount);
                if (obj.Count <= 0)
                {
                    _log.Info("通行超时，验票结束");
                    return TicketDataResult.TaskTerminationResult("通行超时，验票结束", "通行超时，验票结束");
                }
                ////掌静脉
                //if (obj.Ticket != null && obj.Ticket.TicketType == SourceType.PalmVein.ToString())
                //{
                //    return TicketDataResult.TaskTerminationResult("验证通过,感谢您的光临", "验证通过,感谢您的光临");
                //}
                //条形码和二维码和身份证，将验票结果写入数据库
                if (obj.Ticket != null && (obj.Ticket.TicketType == SourceType.BarCode.ToString() || obj.Ticket.TicketType == SourceType.QRcode.ToString() || obj.Ticket.TicketType == SourceType.IdCard.ToString()))
                {
                    _log.Info("条形码和二维码,更新状态");
                    var ticket = _otaFacadeService.UpdateTicket(obj.Ticket.UR_Text1, obj.Count, obj.Device.DeviceName);
                    if (ticket == null)
                    {
                        _log.Info("ticket等于null---------------------------------------");
                    }
                    else
                    {
                        obj.Ticket.UsageCount = ticket.UsedQuantity;
                        obj.Ticket.CapacitySize = ticket.Quantity;
                    }
                }
                //一票多人
                if (obj.Ticket != null && obj.Ticket.UsageCount < obj.Ticket.CapacitySize)
                {
                    var session = obj.Session;
                    session.IDCard_Pass = false;
                    session.Fingerprint_Pass = false;
                    session.AdminConfirm = false;
                    return Schedule_Ticket(obj.Ticket, session);
                }
                //年卡
                //if (obj.Ticket != null && obj.Ticket.TicketType == SourceType.YearTicket.ToString())
                //{
                //    //添加入园记录
                //    _otaFacadeService.AddYearTicketAdmissionRecord(obj.Ticket.Number);
                //}
                _log.Info("验证通过，感谢您的光临");
                _log.Info("结束【提交过闸结果】事件: Ticket_SubmitWalkPast");
                return TicketDataResult.TaskTerminationResult("验证通过,感谢您的光临", "验证通过,感谢您的光临");
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return TicketDataResult.FailResult("系统异常,请联系管理员！");
            }
        }

        /// <summary>
        /// 提交验证采集结果
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Result<TicketResult> Ticket_VerifyCollect(VerifyCollect_Object obj)
        {
            _log.Info("进入【提交验证采集结果】事件: Ticket_VerifyCollect");
            //_log.Info("得到数据: " + _otaFacadeService.ToJsonString(obj));
            return TicketDataResult.TaskTerminationResult();
        }

        /// <summary>
        /// 验票的流程及判断方法
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        private Result<TicketResult> Schedule_Ticket(DocomSDK.Ticket.Data.Ticket ticket, TicketSession session)
        {
            //先判断会话有否超时
            if (session == null)
            {
                return TicketDataResult.FailResult("会话超时", "操作超时");
            }

            //判断订单号及票号是否一致
            if (session.Ticket_No != ticket.Number)
            {
                return TicketDataResult.FailResult("订单号码及票号不一致", "订单号码及票号不一致");
            }

            //判断是否在有效期内
            DateTime now = DateTime.Now;
            //判断是否已超出有效日期
            if (ticket.ExpiryDate.Date < now.Date)
            {
                //提示已超出有效日期
                return TicketDataResult.TicketOverdueResult(ticket);
            }
            //判断是否票已用完
            if (ticket.CapacitySize <= ticket.UsageCount)
            {
                return TicketDataResult.TicketOffResult(ticket, session);
            }
            //判断是否未到生效日期
            if (ticket.BeginDate.Date > now.Date)
            {
                //提示未到生效日期
                return TicketDataResult.TicketNotEnableResult(ticket);
            }
            //票要求验指纹，并且还未验过
            if (!session.Fingerprint_Pass && ticket.NeedFinger)
            {
                //如果系统中已有指纹，则直接校验。
                //否则，先进行采集(只支持单人，多人必须是卖票的时候同时录好)
                //提示设备要求验指纹 
                return TicketDataResult.VerifyFingerprintsResult(ticket, session, "请验证指纹", "请验证指纹");
            }
            return TicketDataResult.VerifyPassResult(ticket, session, "欢迎光临", "欢迎光临");
        }
        #endregion

    }
}
