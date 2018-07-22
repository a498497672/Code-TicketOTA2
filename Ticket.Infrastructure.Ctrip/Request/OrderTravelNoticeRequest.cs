using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Infrastructure.Ctrip.Request
{
    /// <summary>
    /// 出行通知
    /// </summary>
    public class OrderTravelNoticeRequest
    {
        public RequestHeader header { get; set; }
        public string body { get; set; }
    }

    public class OrderOrderTravelNoticeBodyRequest
    {
        /// <summary>
        /// 携程处理批次流水号
        /// </summary>
        public string SequenceId { get; set; }
        /// <summary>
        /// 携程订单号
        /// </summary>
        public string OtaOrderId { get; set; }
        /// <summary>
        /// 供应商订单号
        /// </summary>
        public string SupplierOrderId { get; set; }
        public List<OrderOrderTravelNoticeVouchersRequest> vouchers { get; set; }
        public List<OrderTravelNoticeItemRequest> items { get; set; }
    }
    public class OrderOrderTravelNoticeVouchersRequest
    {
        /// <summary>
        /// 订单项编号
        /// </summary>
        public string itemId { get; set; }
        /// <summary>
        /// 凭证形式：
        ///1.普通凭证（凭身份证/手机号/订单单号等使 用）
        ///2.数字码
        ///3.二维码图片（根据供应商返回的原始字符串 voucherData 生成二维码图片）
        ///4.二维码图片（根据供应商返回的图片数据流 voucherData 生成二维码图片）
        ///5.PDF 确认单（根据供应商返回的文件数据流 voucherData 生成 PDF 确认单）
        ///6.凭证链接（根据供应商返回调原始凭证链接 voucherData 生成短链接）
        /// </summary>
        public int voucherType { get; set; }
        /// <summary>
        /// 凭证数字码 当 voucherType=2 时必返回值； 当 voucherType = 3 / 4 / 5 / 6 时可返回值，作为辅 助码；
        /// </summary>
        public string voucherCode { get; set; }
        /// <summary>
        /// 凭证数据源 
        /// 当 voucherType=3 时返回可生成二维码的原 始字符串；
        /// 当 voucherType=4 时返回 base64 后的二维码 图片数据流；
        /// 当 voucherType=5 时返回base64 后的PDF 文 件数据流；
        /// 当 voucherType=6 时返回原始凭证链接；
        /// </summary>
        public string voucherData { get; set; }
    }

    public class OrderTravelNoticeItemRequest
    {
        /// <summary>
        /// 订单项编号
        /// </summary>
        public string itemId { get; set; }

        public List<OrderTravelNoticeTravelInformationsRequest> travelInformations { get; set; }
    }

    public class OrderTravelNoticeTravelInformationsRequest
    {
        /// <summary>
        /// 订单项编号
        /// </summary>
        public string name { get; set; }
        public string content { get; set; }

    }
}
