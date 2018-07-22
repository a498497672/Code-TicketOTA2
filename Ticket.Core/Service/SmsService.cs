using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ticket.Core.Repository;
using Ticket.SqlSugar.Models;
using Ticket.Model.Result;
using Ticket.Utility.Config;
using Ticket.Utility.Helpers;

namespace Ticket.Core.Service
{
    /// <summary>
    /// 短信
    /// </summary>
    public class SmsService
    {
        private readonly SmsRepository _smsRepository;
        private readonly TemplateService _templateService;
        private readonly ScenicService _scenicService;
        private readonly OrderDetailService _orderDetailService;

        public SmsService(
            SmsRepository smsRepository,
            TemplateService templateService,
            ScenicService scenicService,
            OrderDetailService orderDetailService)
        {
            _smsRepository = smsRepository;
            _templateService = templateService;
            _scenicService = scenicService;
            _orderDetailService = orderDetailService;
        }

        /// <summary>
        /// 电子票发送短信
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <param name="mobile"></param>
        public StatusResult Send(List<Tbl_OrderDetail> orderDetails, string mobile)
        {
            var result = new StatusResult { Status = true };
            var sendOrderDetails = orderDetails.Where(a => a.TicketCategory == 3).ToList();
            if (sendOrderDetails.Count > 0)
            {
                //发送短信
                var scenicIds = orderDetails.Select(a => a.ScenicId).ToList();
                var tbl_Scenics = _scenicService.GetList(scenicIds);
                foreach (var detail in orderDetails)
                {
                    if (tbl_Scenics.FirstOrDefault(o => o.ScenicId == detail.ScenicId && (o.DataStatus & 1) == 0 && (o.DataStatus & 2) == 0 && o.SmsCount > 0) == null)
                    {
                        //发送退票短信失败,该景区未开启短信功能，或短信余额不足
                        result.Status = false;
                        continue;
                    }
                    var sendResult = Send(mobile, tbl_Scenics, detail);
                    if (!sendResult)
                    {
                        result.Status = false;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 电子票发送短信
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <param name="mobile"></param>
        public StatusResult Send(Tbl_OrderDetail orderDetail, string mobile)
        {
            //发送短信
            var tbl_Scenic = _scenicService.GetSurplusScenic(orderDetail.ScenicId);
            if (tbl_Scenic == null)
            {
                var message = "发送退票短信失败,该景区未开启短信功能，或短信余额不足";
                AddSms(orderDetail, mobile, message);
                return new StatusResult { Status = false, Message = message };
            }
            var sendContent = GetRefundSendContent(orderDetail, tbl_Scenic);
            var status = SesHelper.Send(mobile, sendContent);
            if (status)
            {
                //短信发送成功，更改景区短信额度
                _scenicService.UpdateSmsCount(orderDetail.ScenicId);
            }
            AddSms(orderDetail, mobile, sendContent, status);
            return new StatusResult { Status = status, Message = status ? "发送退票短信成功" : "发送退票短信失败" };
        }

        /// <summary>
        /// 电子票发送退票短信
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <param name="mobile"></param>
        public void RefundSend(List<Tbl_OrderDetail> orderDetails, string mobile)
        {
            foreach (var row in orderDetails)
            {
                //发送短信
                var tbl_Scenic = _scenicService.GetSurplusScenic(row.ScenicId);
                if (tbl_Scenic == null)
                {
                    AddSms(row, mobile, "发送短信失败,该景区未开启短信功能，或短信余额不足)");
                    continue;
                }
                var sendContent = GetRefundSendContent(row, tbl_Scenic);
                var status = SesHelper.Send(mobile, sendContent);
                if (status)
                {
                    //短信发送成功，更改景区短信额度
                    _scenicService.UpdateSmsCount(row.ScenicId);
                    _orderDetailService.UpdataForEticektSendQuantity(row.Number);
                }
                AddSms(row, mobile, sendContent, status);
            }
        }

        private bool Send(string mobile, List<Tbl_Scenic> tbl_Scenics, Tbl_OrderDetail detail)
        {
            var templateUrl = CreateTemplate(detail, tbl_Scenics);
            var sendContent = GetSendContent(detail, tbl_Scenics, templateUrl);
            var status = SesHelper.Send(mobile, sendContent);
            if (status)
            {
                //短信发送成功，更改景区短信额度
                _scenicService.UpdateSmsCount(detail.ScenicId);
                _orderDetailService.UpdataForEticektSendQuantity(detail.Number);
            }
            AddSms(detail, mobile, sendContent, status);
            return status;
        }

        /// <summary>
        /// 模板生成
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <param name="scenics"></param>
        /// <returns></returns>
        private string CreateTemplate(Tbl_OrderDetail orderDetail, List<Tbl_Scenic> scenics)
        {
            string ScenicName = string.Empty;
            //生成票模板
            var scenic = scenics.FirstOrDefault(o => o.ScenicId == orderDetail.ScenicId);
            if (scenic != null)
            {
                ScenicName = scenic.ScenicName;
            }
            var data = new
            {
                OrderNo = orderDetail.OrderNo,
                ScenicName = ScenicName,
                CertificateNO = orderDetail.CertificateNO,
                TicketName = orderDetail.TicketName,
                Quantity = orderDetail.Quantity,
                Price = orderDetail.Price,
                Amount = (orderDetail.Price * orderDetail.Quantity),
                Mobile = orderDetail.Mobile,
                ValidityDateStart = orderDetail.ValidityDateStart,
                QRcodeUrl = orderDetail.QRcodeUrl
            };
            string htmlStr = _templateService.GetJNTemplateData(@"printTicketNew.html", data);
            string modelname = OrderHelper.GenerateCertificateNO() + ".html";
            string fileName = AppSettingsConfig.QrCodeModelPath + "/" + modelname;
            System.IO.StreamWriter sw;
            sw = new System.IO.StreamWriter(fileName, false, Encoding.UTF8);
            sw.Write(htmlStr);
            sw.Close();
            string modelPath = AppSettingsConfig.PrintApiPath + modelname;
            return modelPath;
        }

        /// <summary>
        /// 将发送结果插入数据库
        /// </summary>
        /// <param name="orderDetail">订单详情</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="content">发生内容</param>
        /// <param name="SendStatus">短信发送是否成功状态</param>
        private void AddSms(Tbl_OrderDetail orderDetail, string mobile, string content, bool SendStatus)
        {
            var sms = new Tbl_Sms
            {
                OrderDtlID = orderDetail.OrderDetailId,
                EnterpriseId = orderDetail.EnterpriseId,
                ScenicId = orderDetail.ScenicId,
                OrderNo = orderDetail.OrderNo,
                SmsTmeplateId = 0,
                Mobile = mobile,
                SmsContent = content,
                SendResult = SendStatus ? "发送成功" : "发送失败",
                OrderId = 1,
                DataStatus = SendStatus ? 1 : 0,
                CreateTime = DateTime.Now
            };
            _smsRepository.Add(sms);
        }

        /// <summary>
        /// 将发送结果插入数据库
        /// </summary>
        /// <param name="orderDetail">订单详情</param>
        /// <param name="mobile">手机号码</param>
        /// <param name="sendResult">结果</param>
        private void AddSms(Tbl_OrderDetail orderDetail, string mobile, string sendResult)
        {
            var sms = new Tbl_Sms
            {
                OrderDtlID = orderDetail.OrderDetailId,
                EnterpriseId = orderDetail.EnterpriseId,
                ScenicId = orderDetail.ScenicId,
                OrderNo = orderDetail.OrderNo,
                SmsTmeplateId = 0,
                Mobile = mobile,
                SmsContent = "",
                SendResult = sendResult,
                OrderId = 1,
                DataStatus = 0,
                CreateTime = DateTime.Now
            };
            _smsRepository.Add(sms);
        }

        /// <summary>
        /// 获取短信发送内容
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <param name="scenics"></param>
        /// <param name="templateUrl"></param>
        /// <returns></returns>
        private string GetSendContent(Tbl_OrderDetail orderDetail, List<Tbl_Scenic> scenics, string templateUrl)
        {
            var scenicEnt = scenics.FirstOrDefault(o => o.ScenicId == orderDetail.ScenicId && (o.DataStatus & 1) == 0);
            string showScName = scenicEnt.SignName;
            if (string.IsNullOrEmpty(showScName))
            {
                showScName = scenicEnt.ScenicName;
            }
            if (showScName.Length > 7)
            {
                showScName = showScName.Substring(0, 7);
            }
            return string.Format(AppSettingsConfig.ProductOrderInfoPath, showScName, templateUrl);
        }

        /// <summary>
        /// 获取退票短信发送内容
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <param name="scenics"></param>
        /// <param name="templateUrl"></param>
        /// <returns></returns>
        private string GetRefundSendContent(Tbl_OrderDetail orderDetail, Tbl_Scenic scenics)
        {
            string showScName = scenics.SignName;
            if (string.IsNullOrEmpty(showScName))
            {
                showScName = scenics.ScenicName;
            }
            if (showScName.Length > 7)
            {
                showScName = showScName.Substring(0, 7);
            }
            return string.Format(AppSettingsConfig.RefundOrderInfoPath, showScName, orderDetail.TicketName, orderDetail.OrderNo, orderDetail.Quantity);
        }
    }
}
