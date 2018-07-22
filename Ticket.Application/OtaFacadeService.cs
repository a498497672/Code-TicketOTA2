using System;
using System.Collections.Generic;
using DocomSDK.Ticket.Data;
using DocomSDK.Ticket.Enum;
using Ticket.Utility.Logger;
using Ticket.Core.Service;
using Ticket.Model.Enum;
using Ticket.Model.Model;
using Ticket.Utility.Key;
using Ticket.Utility.Extensions;
using Ticket.Utility.Validation;
using Ticket.Utility.Helpers;

namespace Ticket.Application
{
    /// <summary>
    /// 闸机
    /// </summary>
    public class OtaFacadeService
    {
        private readonly SimpleLogger _log;
        private readonly TicketTestingService _ticketTestingService;
        private readonly YearTicketUserService _yearTicketUserService;
        private readonly YearTicketFingerPrintService _ticketFingerPrintService;
        private readonly DoorGateService _doorGateService;
        private readonly YearTicketAdmissionRecordService _yearTicketAdmissionRecordService;
        private readonly TicketService _ticketService;
        private readonly VenaService _venaService;

        public OtaFacadeService(
            TicketTestingService ticketTestingService,
            YearTicketUserService yearTicketUserService,
            YearTicketFingerPrintService yearTicketFingerPrintService,
            DoorGateService doorGateService,
            YearTicketAdmissionRecordService yearTicketAdmissionRecordService,
            TicketService ticketService,
            VenaService venaService)
        {
            _log = new SimpleLogger();
            _ticketTestingService = ticketTestingService;
            _yearTicketUserService = yearTicketUserService;
            _ticketFingerPrintService = yearTicketFingerPrintService;
            _doorGateService = doorGateService;
            _yearTicketAdmissionRecordService = yearTicketAdmissionRecordService;
            _ticketService = ticketService;
            _venaService = venaService;
        }

        /// <summary>
        /// 检票
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Result<TicketResult> TicketValid(CheckTicket_Object obj)
        {
            return ValidQRcode(obj);
            //var sourceType = GetSourceType(obj);
            //switch (sourceType)
            //{
            //    //条形码
            //    case SourceType.BarCode:
            //        return ValidBarCode(obj);
            //    //二维码
            //    case SourceType.QRcode:
            //        return ValidQRcode(obj);
            //    //年卡
            //    case SourceType.YearTicket:
            //        return ValidYearTicket(obj);
            //    //身份证
            //    case SourceType.IdCard:
            //        return ValidIdCard(obj);
            //    //掌静脉
            //    case SourceType.PalmVein:
            //        return ValidPalmVein(obj);
            //}
            //return TicketDataResult.FailResult("系统异常,请联系管理员！");
        }

        /// <summary>
        /// 获取传感器的来源
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public SourceType GetSourceType(CheckTicket_Object obj)
        {
            //P10C1-91
            if (obj.SensorSource.Length >= 8)
            {
                var sensorSub = obj.SensorSource.Substring(6, 2);
                if (sensorSub == "30")
                {
                    //印刷纸质票,条形码为11位数字，编码规则 1位年份+2位价格+8位发票号
                    if (obj.Number.Length > 13)
                    {
                        _log.Info("扫描方式:二维码");
                        return SourceType.QRcode;
                    }
                    else
                    {
                        _log.Info("扫描方式:条形码");
                        return SourceType.BarCode;
                    }
                }
                if (sensorSub == "80" || sensorSub == "20")
                {
                    //验证通过即为身份证
                    if (IdCardValidation.CheckIdCard(obj.Number))
                    {
                        _log.Info("扫描方式:二维码");
                        return SourceType.IdCard;
                    }
                    //年卡
                    _log.Info("扫描方式:年卡");
                    return SourceType.YearTicket;
                }
                if (sensorSub == "91")
                {
                    _log.Info("扫描方式:二维码");
                    return SourceType.PalmVein;
                }
            }
            else if (obj.SensorSource == "P10")
            {
                //验证通过即为身份证
                if (IdCardValidation.CheckIdCard(obj.Number))
                {
                    _log.Info("扫描方式:二维码");
                    return SourceType.IdCard;
                }
                //年卡
                _log.Info("扫描方式:年卡");
                return SourceType.YearTicket;
            }
            _log.Info("扫描数据类型不正确:" + obj.SensorSource);
            return SourceType.UnFound;
        }

        /// <summary>
        /// 验证条形码
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Result<TicketResult> ValidBarCode(CheckTicket_Object obj)
        {
            _log.Info("开始验证条形码");
            string msg;
            TicketTestingModel ticketTestingModel;
            var isCheck = _ticketTestingService.CheckTicketBarCode(obj.Number, out msg, out ticketTestingModel);
            _log.Info("条形码验证结果：" + msg);
            if (!isCheck)
            {
                return TicketDataResult.TaskTerminationResult(msg, msg);
            }
            return TicketResult(obj, SourceType.BarCode, ticketTestingModel, msg);
        }

        /// <summary>
        /// 验证二维码
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Result<TicketResult> ValidQRcode(CheckTicket_Object obj)
        {
            //_log.Info("开始验证二维码");
            var qrCode = _ticketTestingService.QrCodeDecrypt(obj.Number);
            if (string.IsNullOrEmpty(qrCode))
            {
                //_log.Info("解密二维码失败");
                return TicketDataResult.TaskTerminationResult(MessageKey.InvalidTicket, MessageKey.InvalidTicket);
            }
            //_log.Info("解密二维码成功");
            string msg;
            TicketTestingModel ticketTestingModel;
            var isCheck = _ticketTestingService.CheckTicketQrCode(qrCode, obj.Device.DeviceName, out msg, out ticketTestingModel);
            //_log.Info("二维码验证结果：" + msg);
            if (!isCheck)
            {
                return TicketDataResult.TaskTerminationResult(msg, msg);
            }
            return TicketResult(obj, SourceType.QRcode, ticketTestingModel, msg);
        }

        /// <summary>
        /// 验证身份证
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Result<TicketResult> ValidIdCard(CheckTicket_Object obj)
        {
            string msg;
            TicketTestingModel ticketTestingModel;
            var isCheck = _ticketTestingService.CheckTicketIdCard(obj.Number, out msg, out ticketTestingModel);
            if (!isCheck)
            {
                return TicketDataResult.TaskTerminationResult(msg, msg);
            }
            return TicketResult(obj, SourceType.IdCard, ticketTestingModel, msg);
        }

        /// <summary>
        /// 验证年卡
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Result<TicketResult> ValidYearTicket(CheckTicket_Object obj)
        {
            _log.Info("开始验证年卡");
            string msg;
            int yearTicketUserId;
            var isCheck = _yearTicketUserService.CheckYearTicketUser(obj.Number, out msg, out yearTicketUserId);
            _log.Info("年卡验证结果：" + msg);
            if (!isCheck)
            {
                //年卡无效
                return TicketDataResult.TaskTerminationResult(msg, msg);
            }
            var ticketSession = new TicketSession
            {
                OrderID = Guid.NewGuid().ToString("N"),
                Fingerprint_Pass = false,
                Fingerprint_Token = string.Empty,
                IDCard_Number = string.Empty,
                IDCard_Pass = false,
                TicketNo_Pass = false,
                Ticket_No = obj.Number,
                CallCount = 0,
                ParkID = "罗湖山5A",//可以用于景区id
                DeviceID = obj.Device.DeviceName,//可以用于设备id
                Temp_Fingerprint = new List<Fingerprint>()
            };
            _log.Info("验证指纹状态：");
            var list = _ticketFingerPrintService.GetFingers(yearTicketUserId);
            foreach (var item in list)
            {
                var finger = new Fingerprint
                {
                    Data = item.FingerFeature,
                    Token = item.FingerFeature
                };
                ticketSession.Temp_Fingerprint.Add(finger);
            }
            _log.Info("指纹数量" + list.Count);

            DocomSDK.Ticket.Data.Ticket ticket = new DocomSDK.Ticket.Data.Ticket
            {
                BeginDate = DateTime.Now,  //结束日期
                ExpiryDate = DateTime.Now.AddDays(1),  //开始日期
                CapacitySize = 1,        // 可用人数   
                NeedIDCard = false,  //是否验证身份证
                NeedFinger = true,
                UsageCount = 1,  //已用人数
                Number = obj.Number, //门票条码
                Price = 0,//价格
                TicketSource = obj.SensorSource,  //门票来源
                TicketType = SourceType.YearTicket.ToString(),  //门票类型
                UR_Text1 = yearTicketUserId.ToString(),  //使用预留字段
            };
            _log.Info("发送指纹验证请求:" + OperationCode.VerifyFingerprints);
            return TicketDataResult.VerifyFingerprintsResult(ticket, ticketSession, "验证指纹", "验证指纹");
        }


        /// <summary>
        /// 验证掌静脉
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Result<TicketResult> ValidPalmVein(CheckTicket_Object obj)
        {
            var ticketSession = new TicketSession
            {
                OrderID = Guid.NewGuid().ToString("N"),
                Fingerprint_Pass = false,
                Fingerprint_Token = string.Empty,
                IDCard_Number = string.Empty,
                IDCard_Pass = false,
                TicketNo_Pass = false,
                Ticket_No = obj.Number,
                CallCount = 0,
                DeviceID = obj.Device.DeviceName//可以用于设备id
            };

            DocomSDK.Ticket.Data.Ticket ticket = new DocomSDK.Ticket.Data.Ticket()
            {
                BeginDate = DateTime.Now,  //开始日期
                ExpiryDate = DateTime.Now,  //结束日期
                CapacitySize = 1,          //总人数
                UsageCount = 1,  // 已入园人数 
                NeedIDCard = false,  //是否验证身份证
                NeedFinger = false,
                Number = obj.Number, //门票条码
                Price = 0,//价格
                TicketSource = obj.SensorSource,  //门票来源
                TicketType = SourceType.PalmVein.ToString()  //门票类型
            };


            if (obj.Device.DeviceName == "2f0fae9db03e9b73")
            {
                TestHelper.IsAction = false;
                //添加掌静脉特征
                var isAdd = _venaService.CreateFeature(obj.Number);
                if (!isAdd)
                {
                    return TicketDataResult.TaskTerminationResult("采集失败", "采集失败");
                }
                return TicketDataResult.VerifyPassResult(ticket, ticketSession, "采集成功", "采集成功");
            }
            TestHelper.IsAction = true;
            //比对掌静脉特征
            var result = _venaService.SearchFeature(obj.Number);
            if (!result)
            {
                return TicketDataResult.TaskTerminationResult("验证失败", "验证失败");
            }
            return TicketDataResult.VerifyPassResult(ticket, ticketSession, "验证成功,欢迎光临", "验证成功,欢迎光临");
        }

        /// <summary>
        /// 更新 : 条形码和二维码的数据。
        /// </summary>
        /// <param name="yearTicketUserId"></param>
        /// <param name="count"></param>
        /// <param name="doorGateNo">闸机号</param>
        public TicketTestingModel UpdateTicket(string yearTicketUserId, int count, string doorGateNo)
        {
            int userId = yearTicketUserId.ToInt();
            if (userId <= 0)
            {
                return null;
            }
            return _ticketTestingService.UpdateDataStatus(userId, count, doorGateNo);
        }

        /// <summary>
        /// 检测闸机是否可用
        /// </summary>
        /// <param name="doorGateNo">闸机号</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool CheckDoorGate(string doorGateNo, out string msg)
        {
            return _doorGateService.CheckDoorGate(doorGateNo, out msg);
        }

        /// <summary>
        /// 添加年票入园记录
        /// </summary>
        /// <param name="cradNo">年卡号</param>
        public void AddYearTicketAdmissionRecord(string cradNo)
        {
            _yearTicketAdmissionRecordService.AddYearTicketAdmissionRecord(cradNo);
        }

        /// <summary>
        /// 验票的判断方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private Result<TicketResult> TicketResult(CheckTicket_Object obj, SourceType type, TicketTestingModel model, string msg)
        {
            var ticketSession = new TicketSession
            {
                OrderID = Guid.NewGuid().ToString("N"),
                Fingerprint_Pass = false,
                Fingerprint_Token = string.Empty,
                IDCard_Number = string.Empty,
                IDCard_Pass = false,
                TicketNo_Pass = false,
                Ticket_No = obj.Number,
                CallCount = 0,
                //ParkID = "罗湖山5A",//可以用于景区id
                DeviceID = obj.Device.DeviceName//可以用于设备id
            };

            DocomSDK.Ticket.Data.Ticket ticket = new DocomSDK.Ticket.Data.Ticket()
            {
                BeginDate = model.ValidityDateStart,  //开始日期
                ExpiryDate = model.ValidityDateEnd,  //结束日期
                CapacitySize = model.Quantity,          //总人数
                UsageCount = model.UsedQuantity,  // 已入园人数 
                NeedIDCard = false,  //是否验证身份证
                NeedFinger = false,
                Number = obj.Number, //门票条码
                Price = 0,//价格
                TicketSource = obj.SensorSource,  //门票来源
                TicketType = type.ToString(),  //门票类型
                UR_Text1 = model.TicketTestingId.ToString(),  //使用预留字段
            };

            DateTime now = DateTime.Now;
            if (ticket.ExpiryDate.Date < now.Date)
            {
                //提示已超出有效日期
                return TicketDataResult.TicketOverdueResult(ticket);
            }
            if (ticket.CapacitySize <= ticket.UsageCount)
            {
                //提示该票已用完
                return TicketDataResult.TicketOffResult(ticket, ticketSession);
            }
            if (ticket.BeginDate.Date > now.Date)
            {
                //提示未到生效日期
                return TicketDataResult.TicketNotEnableResult(ticket);
            }
            if (model.DelayCheckTime > now)
            {
                //该票未到入园时间
                return TicketDataResult.TicketNotDelayCheckTimeResult(ticket);
            }
            //验证通过，通知开闸
            return TicketDataResult.VerifyPassResult(ticket, ticketSession, msg, msg);
        }
    }
}