using DocomSDK.Ticket.Data;
using DocomSDK.Ticket.Enum;

namespace Ticket.Application
{
    /// <summary>
    /// 执行结果返回
    /// </summary>
    public static class TicketDataResult
    {
        /// <summary>
        /// 执行失败返回
        /// </summary>
        /// <param name="description"></param>
        /// <param name="tts"></param>
        /// <returns></returns>
        public static Result<TicketResult> FailResult(string description = "", string tts = "")
        {
            var result = new Result<TicketResult>
            {
                State = Result_Code.Fail,
                Description = description,
                TTS = tts,
                Data = null
            };
            return result;
        }

        /// <summary>
        /// 任务结束
        /// </summary>
        /// <param name="description">信息提示</param>
        /// <param name="tts">语音提示</param>
        /// <returns></returns>
        public static Result<TicketResult> TaskTerminationResult(string description = "", string tts = "")
        {
            var result = new Result<TicketResult>
            {
                State = Result_Code.OK,
                Description = description,
                TTS = tts,
                Data = new TicketResult
                {
                    Code = OperationCode.TaskTermination,
                    TicketData = null
                }
            };
            return result;
        }

        /// <summary>
        /// 验证通过，通知开闸
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="session"></param>
        /// <param name="description"></param>
        /// <param name="tts"></param>
        /// <returns></returns>
        public static Result<TicketResult> VerifyPassResult(DocomSDK.Ticket.Data.Ticket ticket, TicketSession session, string description = "", string tts = "")
        {
            var result = new Result<TicketResult>
            {
                State = Result_Code.OK,
                Description = description,
                TTS = tts,
                Data = new TicketResult
                {
                    Code = OperationCode.VerifyPass,
                    TicketData = ticket,
                    Session = session
                }
            };
            return result;
        }

        /// <summary>
        /// 请求验证指纹
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="session"></param>
        /// <param name="description"></param>
        /// <param name="tts"></param>
        /// <returns></returns>
        public static Result<TicketResult> VerifyFingerprintsResult(DocomSDK.Ticket.Data.Ticket ticket, TicketSession session, string description = "验证指纹", string tts = "")
        {
            var result = new Result<TicketResult>
            {
                State = Result_Code.OK,
                Description = description,
                TTS = tts,
                Data = new TicketResult
                {
                    Code = OperationCode.VerifyFingerprints,
                    //Code = OperationCode.CollectFingerprints,
                    TicketData = ticket,
                    Session = session
                }
            };
            return result;
        }

        /// <summary>
        /// 不存在的票
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static Result<TicketResult> TicketNotExitsResult(string description)
        {
            var result = new Result<TicketResult>
            {
                State = Result_Code.OK,
                Description = description,
                TTS = description,
                Data = new TicketResult
                {
                    Code = OperationCode.TicketNotExits
                }
            };
            return result;
        }

        /// <summary>
        /// 票已过期（该票已超出有效日期）
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static Result<TicketResult> TicketOverdueResult(DocomSDK.Ticket.Data.Ticket ticket)
        {
            var result = new Result<TicketResult>
            {
                State = Result_Code.OK,
                Description = "该票已超出有效日期",
                TTS = "该票已超出有效日期",
                Data = new TicketResult
                {
                    Code = OperationCode.TicketOverdue,
                    TicketData = ticket
                }
            };
            return result;
        }

        /// <summary>
        /// 该票已用完(人次)
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        public static Result<TicketResult> TicketOffResult(DocomSDK.Ticket.Data.Ticket ticket, TicketSession session)
        {
            var result = new Result<TicketResult>
            {
                State = Result_Code.OK,
                Description = "该票已用完",
                TTS = "该票已用完",
                Data = new TicketResult
                {
                    Code = OperationCode.TicketOff,
                    TicketData = ticket,
                    Session = session
                }
            };
            return result;
        }

        /// <summary>
        /// (票还未启用)该票未到生效日期
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static Result<TicketResult> TicketNotEnableResult(DocomSDK.Ticket.Data.Ticket ticket)
        {
            var result = new Result<TicketResult>
            {
                State = Result_Code.OK,
                Description = "该票未到生效日期",
                TTS = "该票未到生效日期",
                Data = new TicketResult
                {
                    Code = OperationCode.TicketNotEnable,
                    TicketData = ticket
                }
            };
            return result;
        }

        /// <summary>
        /// 该票未到入园时间
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static Result<TicketResult> TicketNotDelayCheckTimeResult(DocomSDK.Ticket.Data.Ticket ticket)
        {
            var result = new Result<TicketResult>
            {
                State = Result_Code.OK,
                Description = "该票未到入园时间",
                TTS = "该票未到入园时间",
                Data = new TicketResult
                {
                    Code = OperationCode.TaskTermination,
                    TicketData = ticket
                }
            };
            return result;
        }

        /// <summary>
        /// 该通道不可使用(设置了闸机不验此票时提示)
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public static Result<TicketResult> CheckTicketIsDoorGateResult(DocomSDK.Ticket.Data.Ticket ticket)
        {
            var result = new Result<TicketResult>
            {
                State = Result_Code.OK,
                Description = "门票在该通道不可使用",
                TTS = "门票在该通道不可使用",
                Data = new TicketResult
                {
                    Code = OperationCode.TaskTermination,
                    TicketData = ticket
                }
            };
            return result;
        }

        /// <summary>
        /// 非法设备(设备名称在数据库中找不到或未被激活)
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public static Result<TicketResult> InvalidDeviceResult(string description)
        {
            var result = new Result<TicketResult>
            {
                State = Result_Code.OK,
                Description = description,
                TTS = description,
                Data = new TicketResult
                {
                    Code = OperationCode.InvalidDevice
                }
            };
            return result;
        }
    }
}