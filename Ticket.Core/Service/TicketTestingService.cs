using System;
using System.Collections.Generic;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;
using Ticket.Model.Enum;
using Ticket.Model.Model;
using Ticket.TaskEngine.Application.Model;
using Ticket.Utility.Extensions;
using Ticket.Utility.Helpers;
using Ticket.Utility.Key;
using Ticket.Utility.UnitOfWorks;

namespace Ticket.Core.Service
{
    /// <summary>
    /// 检票
    /// </summary>
    public class TicketTestingService
    {
        private readonly TicketTestingRepository _ticketTestingRepository;
        private readonly OrderDetailService _orderDetailService;
        private readonly DoorGateService _doorGateService;
        private readonly TicketService _ticketService;
        private readonly TicketConsumeService _ticketConsumeService;
        private readonly TravelAgencyOrderService _travelAgencyOrderService;

        public TicketTestingService(
            TicketTestingRepository ticketTestingRepository,
            OrderDetailService orderDetailService,
            DoorGateService doorGateService,
            TicketService ticketService,
            TicketConsumeService ticketConsumeService,
            TravelAgencyOrderService travelAgencyOrderService)
        {
            _ticketTestingRepository = ticketTestingRepository;
            _orderDetailService = orderDetailService;
            _doorGateService = doorGateService;
            _ticketService = ticketService;
            _ticketConsumeService = ticketConsumeService;
            _travelAgencyOrderService = travelAgencyOrderService;
        }

        /// <summary>
        /// 获取激活的票
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Tbl_Ticket_Testing GetActivateTicketByCode(string code)
        {
            return _ticketTestingRepository.FirstOrDefault(a => a.QRcode == code && a.DataStatus == (int)TicketTestingDataStatus.Activate);
        }

        /// <summary>
        /// 获取激活的票
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Tbl_Ticket_Testing GetActivateTicketByIdCard(string idCard)
        {
            return _ticketTestingRepository.FirstOrDefault(a => a.IDCard == idCard && a.DataStatus == (int)TicketTestingDataStatus.Activate);
        }

        /// <summary>
        /// 二维码支付，生成验票信息,自动激活
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderDetails"></param>
        /// <returns></returns>
        public List<Tbl_Ticket_Testing> addTicketTestings(Tbl_Order order, List<Tbl_OrderDetail> orderDetails)
        {
            List<Tbl_Ticket_Testing> ticketTestings = new List<Tbl_Ticket_Testing>();

            foreach (var orderDetail in orderDetails)
            {
                orderDetail.OrderStatus = (int)OrderDetailsDataStatus.Success;

                if (orderDetail.TicketCategory != 1)
                {
                    //生成凭证号
                    orderDetail.CertificateNO = OrderHelper.GetCertificateNo();

                    //二维码订单支付，生成二维码串，自动激活
                    orderDetail.OrderStatus = (int)OrderDetailsDataStatus.Activate;

                    //生成二维码串
                    orderDetail.QRcode = OrderHelper.GenerateQRCode();

                    //生成一张二维码图片，返回图片地址
                    string imgpath = string.Empty;
                    //OrderHelper.CreateCode_Simple(orderDetail.QRcode, out imgpath);
                    orderDetail.QRcodeUrl = imgpath;

                    //二维码支付，生成验票信息,自动激活
                    Tbl_Ticket_Testing tbl_Ticket_Testing = new Tbl_Ticket_Testing
                    {
                        OrderDetailNumber = orderDetail.Number,
                        OrderDetailId = orderDetail.OrderDetailId,
                        OrderNo = orderDetail.OrderNo,
                        EnterpriseId = orderDetail.EnterpriseId,
                        ScenicId = orderDetail.ScenicId,
                        TicketCategory = orderDetail.TicketCategory,
                        TicketId = orderDetail.TicketId,
                        TicketName = orderDetail.TicketName,
                        BarCode = orderDetail.BarCode,
                        CertificateNO = orderDetail.CertificateNO,
                        QRcode = orderDetail.QRcode,
                        DataStatus = 1,
                        Quantity = orderDetail.Quantity,
                        IDCard = orderDetail.IDCard
                    };
                    ticketTestings.Add(tbl_Ticket_Testing);
                }
            }
            return ticketTestings;
        }

        /// <summary>
        /// 二维码支付，生成验票信息,自动激活
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderDetails"></param>
        /// <returns></returns>
        public Tbl_Ticket_Testing AddTicketTesting(Tbl_Order order, Tbl_OrderDetail orderDetail)
        {
            orderDetail.OrderStatus = (int)OrderDetailsDataStatus.Success;

            if (orderDetail.TicketCategory != 1)
            {
                //生成凭证号
                orderDetail.CertificateNO = OrderHelper.GetCertificateNo();

                //二维码订单支付，生成二维码串，自动激活
                orderDetail.OrderStatus = (int)OrderDetailsDataStatus.Activate;

                //生成二维码串
                orderDetail.QRcode = OrderHelper.GenerateQRCode();

                //生成一张二维码图片，返回图片地址
                string imgpath = string.Empty;
                //OrderHelper.CreateCode_Simple(orderDetail.QRcode, out imgpath);
                orderDetail.QRcodeUrl = imgpath;

                //二维码支付，生成验票信息,自动激活
                Tbl_Ticket_Testing tbl_Ticket_Testing = new Tbl_Ticket_Testing
                {
                    OrderDetailNumber = orderDetail.Number,
                    OrderDetailId = orderDetail.OrderDetailId,
                    OrderNo = orderDetail.OrderNo,
                    EnterpriseId = orderDetail.EnterpriseId,
                    ScenicId = orderDetail.ScenicId,
                    TicketCategory = orderDetail.TicketCategory,
                    TicketId = orderDetail.TicketId,
                    TicketName = orderDetail.TicketName,
                    BarCode = orderDetail.BarCode,
                    CertificateNO = orderDetail.CertificateNO,
                    QRcode = orderDetail.QRcode,
                    DataStatus = 1,
                    Quantity = orderDetail.Quantity,
                    IDCard = orderDetail.IDCard
                };
                return tbl_Ticket_Testing;
            }
            return null;
        }

        public void Add(Tbl_Ticket_Testing tbl_Ticket_Testing)
        {
            _ticketTestingRepository.Add(tbl_Ticket_Testing);
        }

        public void Add(List<Tbl_Ticket_Testing> tbl_Ticket_Testings)
        {
            _ticketTestingRepository.Add(tbl_Ticket_Testings);
        }

        /// <summary>
        /// 二维码支付，生成验票信息,自动激活--小径平台
        /// </summary>
        /// <param name="order"></param>
        /// <param name="orderDetails"></param>
        /// <returns></returns>
        public List<Tbl_Ticket_Testing> XJ_addTicketTestings(Tbl_Order order, List<Tbl_OrderDetail> orderDetails)
        {
            List<Tbl_Ticket_Testing> ticketTestings = new List<Tbl_Ticket_Testing>();

            foreach (var orderDetail in orderDetails)
            {
                orderDetail.OrderStatus = (int)OrderDetailsDataStatus.Success;

                if (orderDetail.TicketCategory != 1)
                {
                    //生成凭证号
                    orderDetail.CertificateNO = orderDetail.QRcode;

                    //二维码订单支付，生成二维码串，自动激活
                    orderDetail.OrderStatus = (int)OrderDetailsDataStatus.Activate;

                    //生成一张二维码图片，返回图片地址
                    string imgpath = string.Empty;
                    OrderHelper.CreateCode_Simple(orderDetail.QRcode, out imgpath);
                    orderDetail.QRcodeUrl = imgpath;

                    //二维码支付，生成验票信息,自动激活
                    Tbl_Ticket_Testing tbl_Ticket_Testing = new Tbl_Ticket_Testing
                    {
                        OrderDetailNumber = orderDetail.Number,
                        OrderDetailId = orderDetail.OrderDetailId,
                        OrderNo = orderDetail.OrderNo,
                        EnterpriseId = orderDetail.EnterpriseId,
                        ScenicId = orderDetail.ScenicId,
                        TicketCategory = orderDetail.TicketCategory,
                        TicketId = orderDetail.TicketId,
                        TicketName = orderDetail.TicketName,
                        BarCode = orderDetail.BarCode,
                        CertificateNO = orderDetail.CertificateNO,
                        QRcode = orderDetail.QRcode,
                        DataStatus = 1,
                        Quantity = orderDetail.Quantity,
                        IDCard = orderDetail.IDCard
                    };
                    ticketTestings.Add(tbl_Ticket_Testing);
                }
            }
            return ticketTestings;
        }

        /// <summary>
        /// 退激活票时，同步删除验票表存在的数据
        /// </summary>
        /// <param name="orderDetailId"></param>
        public void Delete(int orderDetailId)
        {
            var tbl_Ticket_Testing = _ticketTestingRepository.FirstOrDefault(a => a.OrderDetailId == orderDetailId);
            if (tbl_Ticket_Testing != null)
            {
                _ticketTestingRepository.Delete(tbl_Ticket_Testing);
            }
        }

        public void Delete(Guid orderDetailNumber)
        {
            var tbl_Ticket_Testing = _ticketTestingRepository.FirstOrDefault(a => a.OrderDetailNumber == orderDetailNumber);
            if (tbl_Ticket_Testing != null)
            {
                _ticketTestingRepository.Delete(tbl_Ticket_Testing);
            }
        }

        /// <summary>
        /// 退激活票时，同步删除验票表存在的数据
        /// </summary>
        /// <param name="orderNo"></param>
        public void Delete(string orderNo)
        {
            var list = _ticketTestingRepository.GetAllList(a => a.OrderNo == orderNo);
            foreach (var row in list)
            {
                _ticketTestingRepository.Delete(row);
            }

        }

        /// <summary>
        /// 验证条形码是否存在
        /// </summary>
        /// <param name="barCode">条形码</param>
        /// <param name="msg">返回的消息</param>
        /// <param name="model">返回的票的信息</param>
        /// <returns>存在true，不存在false</returns>
        public bool CheckTicketBarCode(string barCode, out string msg, out TicketTestingModel model)
        {
            model = new TicketTestingModel();
            var orderDetail = _orderDetailService.GetByBarCode(barCode);
            if (orderDetail != null)
            {
                if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Canncel || orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Refund)
                {
                    msg = MessageKey.TicketRefund;
                    return false;
                }
                model.ValidityDateStart = orderDetail.ValidityDateStart;
                model.ValidityDateEnd = orderDetail.ValidityDateEnd;
                model.DelayCheckTime = orderDetail.DelayCheckTime;
                model.Price = orderDetail.Price;
            }
            var entity = _ticketTestingRepository.FirstOrDefault(a => a.BarCode == barCode);
            var isValid = CheckTicket(entity, out msg);
            if (isValid)
            {
                model.TicketTestingId = entity.TicketTestingId;
                model.Quantity = entity.Quantity.Value;
                model.UsedQuantity = entity.UsedQuantity;
            }
            return isValid;
        }

        /// <summary>
        /// 验证二维码是否存在
        /// </summary>
        /// <param name="qRcode">二维码</param>
        /// <param name="deviceName">设备名，唯一标识符</param>
        /// <param name="msg">返回的消息</param>
        /// <param name="model">票的信息</param>
        /// <returns>存在true，不存在false</returns>
        public bool CheckTicketQrCode(string qRcode, string deviceName, out string msg, out TicketTestingModel model)
        {
            model = new TicketTestingModel();
            var orderDetail = _orderDetailService.GetByQRcode(qRcode);
            if (orderDetail == null)
            {
                msg = MessageKey.InvalidTicket;
                return false;
            }
            if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Canncel || orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Refund)
            {
                msg = MessageKey.TicketRefund;
                return false;
            }
            if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Consume)
            {
                msg = MessageKey.TicketEmploy;
                return false;
            }
            var isCheckTicket = _ticketService.CheckTicketIsDoorGate(orderDetail, deviceName);
            if (!isCheckTicket)
            {
                msg = MessageKey.TicketNoValidate;
                return false;
            }

            model.ValidityDateStart = orderDetail.ValidityDateStart;
            model.ValidityDateEnd = orderDetail.ValidityDateEnd;
            model.DelayCheckTime = orderDetail.DelayCheckTime;
            model.Price = orderDetail.Price;
            model.TicketId = orderDetail.TicketId;

            var entity = Get(orderDetail.OrderNo, qRcode);
            var isValid = CheckTicket(entity, out msg);
            if (isValid)
            {
                model.TicketTestingId = entity.TicketTestingId;
                model.Quantity = entity.Quantity.Value;
                model.UsedQuantity = entity.UsedQuantity;
                UpdateForCheckDate(entity);
            }
            return isValid;
        }

        public Tbl_Ticket_Testing Get(string orderNo, string qrCode)
        {
            DateTime nowTime = DateTime.Now.Date;
            return _ticketTestingRepository.FirstOrDefault(a =>
             a.QRcode == qrCode &&
             a.OrderNo == orderNo);
        }


        /// <summary>
        /// 验证身份证是否存在
        /// </summary>
        /// <param name="idCard">身份证</param>
        /// <param name="msg">返回的消息</param>
        /// <param name="model">票的信息</param>
        /// <returns>存在true，不存在false</returns>
        public bool CheckTicketIdCard(string idCard, out string msg, out TicketTestingModel model)
        {
            model = new TicketTestingModel();
            var orderDetail = _orderDetailService.GetByIDCard(idCard);
            if (orderDetail != null)
            {
                if (orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Canncel || orderDetail.OrderStatus == (int)OrderDetailsDataStatus.Refund)
                {
                    msg = MessageKey.TicketRefund;
                    return false;
                }
                model.ValidityDateStart = orderDetail.ValidityDateStart;
                model.ValidityDateEnd = orderDetail.ValidityDateEnd;
                model.DelayCheckTime = orderDetail.DelayCheckTime;
                model.Price = orderDetail.Price;
            }
            var entity = _ticketTestingRepository.FirstOrDefault(a => a.IDCard == idCard);
            var isValid = CheckTicket(entity, out msg);
            if (isValid)
            {
                model.TicketTestingId = entity.TicketTestingId;
                model.Quantity = entity.Quantity.Value;
                model.UsedQuantity = entity.UsedQuantity;
            }
            return isValid;
        }

        /// <summary>
        /// 解密二维码
        /// </summary>
        /// <param name="qRcode"></param>
        /// <returns></returns>
        public string QrCodeDecrypt(string qRcode)
        {
            string nowQRcode;
            //开始解密验证二维码
            try
            {
                nowQRcode = SecurityExtension.DesDecrypt(qRcode, DesKey.QrCodeKey);
            }
            catch (Exception)
            {
                nowQRcode = string.Empty;
            }
            return nowQRcode;
        }

        private bool CheckTicket(Tbl_Ticket_Testing entity, out string msg)
        {
            if (entity == null)
            {
                //无效票，请您重新购买！
                msg = MessageKey.InvalidTicket;
                return false;
            }
            if (entity.DataStatus == (int)TicketTestingDataStatus.Employ)
            {
                //票已使用
                msg = MessageKey.TicketEmploy;
                return false;
            }
            if (entity.CheckDate != null && (DateTime.Now - entity.CheckDate.Value).TotalSeconds <= 10)
            {
                //门票正在使用，请稍后再试
                msg = MessageKey.TicketBeingUsed;
                return false;
            }
            int count = entity.Quantity.Value - entity.UsedQuantity;
            msg = string.Format(MessageKey.VerifyThroughTicket, entity.TicketName, entity.Quantity, count);
            return true;
        }

        /// <summary>
        /// 把已激活的票变成已使用的状态
        /// </summary>
        /// <param name="ticketTestingId"></param>
        /// <param name="count"></param>
        /// <param name="doorGateNo">闸机号</param>
        /// <returns></returns>
        public TicketTestingModel UpdateDataStatus(int ticketTestingId, int count, string doorGateNo)
        {
            //开始更改票的状态
            var entity = _ticketTestingRepository.FirstOrDefault(a =>
            a.TicketTestingId == ticketTestingId &&
            (a.DataStatus == (int)TicketTestingDataStatus.Activate || a.DataStatus == (int)TicketTestingDataStatus.PartEmploy));
            if (entity == null)
            {
                return null;
            }
            entity.UsedQuantity += count;
            entity.DataStatus = (int)TicketTestingDataStatus.PartEmploy;
            if (entity.UsedQuantity >= entity.Quantity.Value)
            {
                entity.DataStatus = (int)TicketTestingDataStatus.Employ;//已使用
                entity.CheckTicketUserId = (int)ActivateType.System;//0为系统激活
                entity.CheckDate = DateTime.Now;
                entity.DoorGateNO = doorGateNo;
                var scenicGateId = _doorGateService.GetScenicGateId(doorGateNo);
                entity.ScenicGateId = scenicGateId;
                entity.CheckTicketWay = entity.ScenicGateId > 0 ? (int)CheckTicketWayType.ScenicGate : (int)CheckTicketWayType.NoScenicGate;//景区有园门闸机验票
            }
            try
            {
                _ticketTestingRepository.BeginTran();
                _ticketTestingRepository.Update(entity);
                //开始更改订单详情的状态
                var orderDetail = _orderDetailService.Update(entity.OrderDetailNumber, entity.UsedQuantity);
                _travelAgencyOrderService.UpdateForConsume(orderDetail, entity);
                _ticketTestingRepository.CommitTran();
            }
            catch
            {
                _ticketTestingRepository.RollbackTran();
            }
            return new TicketTestingModel
            {
                UsedQuantity = entity.UsedQuantity,
                Quantity = entity.Quantity.Value
            };
        }

        /// <summary>
        /// 更新检票时间，以判断是否有2台以上的闸机同时在验票
        /// </summary>
        public void UpdateForCheckDate(Tbl_Ticket_Testing entity)
        {
            entity.CheckDate = DateTime.Now;
            _ticketTestingRepository.Update(entity);
        }
    }
}
