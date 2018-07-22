using System.Collections.Generic;

namespace Ticket.Infrastructure.Ctrip.Response
{
    public class CreateOrderResponse
    {
        public HeaderResponse header { get; set; }
        public string body { get; set; }
    }

    public class CreateOrderBodyResponse
    {
        /// <summary>
        /// 携程订单号
        /// </summary>
        public string otaOrderId { get; set; }
        /// <summary>
        /// 供应商订单号
        /// </summary>
        public string supplierOrderId { get; set; }
        /// <summary>
        /// 供应商确认类型：
        ///1.新订已确认（当 confirmType = 1 / 2 时可同步 返回确认结果）
        ///2.新订待确认（当 confirmType = 2 时需异步返 回确认结果的）
        /// </summary>
        public int supplierConfirmType { get; set; }
        /// <summary>
        /// 凭证发送方：1.携程发送凭证 2.供应商发送凭证
        /// </summary>
        public int voucherSender { get; set; }
        /// <summary>
        /// 凭证集合节点 当 supplierConfirmType = 1 时可返回值； 当 supplierConfirmType = 2 时不返回值；
        /// </summary>
        public List<CreateOrderVouchers> vouchers { get; set; }
        public List<CreateOrderitemRespose> items { get; set; }
    }

    public class CreateOrderVouchers
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

    public class CreateOrderitemRespose
    {
        /// <summary>
        /// 订单项编号
        /// </summary>
        public string itemId { get; set; }
        public CreateOrderInventoryRespose inventorys { get; set; }
    }

    public class CreateOrderInventoryRespose
    {
        /// <summary>
        /// 使用时间
        /// </summary>
        public string useDate { get; set; }
        /// <summary>
        /// 使用时间剩余库存数
        /// </summary>
        public int quantity { get; set; }
    }
}
