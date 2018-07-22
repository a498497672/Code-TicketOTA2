using Com.Alipay;
using Com.Alipay.Business;
using Com.Alipay.Domain;
using Com.Alipay.Model;
using System;
using System.Collections.Generic;
using Ticket.Infrastructure.Alipay.Response;

namespace Ticket.Infrastructure.Alipay
{
    /// <summary>
    /// 支付宝当面付-条形码
    /// </summary>
    public class F2FPayNotify
    {
        /// <summary>
        /// 刷卡支付完整业务流程逻辑(2018-04-19写)
        /// </summary>
        /// <param name="body">商品描述</param>
        /// <param name="total_fee">总金额(单位为元)</param>
        /// <param name="auth_code">支付授权码</param>
        /// <returns>刷卡支付结果</returns>
        public static AlipayPayResponse OrderPay(string body, string total_fee, string auth_code)
        {
            IAlipayTradeService serviceClient = F2FBiz.CreateClientInstance(
            F2FPayConfig.serverUrl,
            F2FPayConfig.appId,
            F2FPayConfig.merchant_private_key,
            F2FPayConfig.version,
            F2FPayConfig.sign_type,
            F2FPayConfig.alipay_public_key,
            F2FPayConfig.charset);

            var result = new AlipayPayResponse { Success = false, Message = "支付失败" };

            //线上联调时，请输入真实的外部订单号。
            result.OutTradeNo = DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString();

            //扫码枪扫描到的用户手机钱包中的付款条码
            AlipayTradePayContentBuilder builder = new AlipayTradePayContentBuilder();

            //收款账号
            builder.seller_id = F2FPayConfig.pid;
            //订单编号
            builder.out_trade_no = result.OutTradeNo;
            //支付场景，无需修改
            builder.scene = "bar_code";
            //支付授权码,付款码
            builder.auth_code = auth_code.Trim();
            //订单总金额
            builder.total_amount = total_fee.Trim();
            //参与优惠计算的金额
            //builder.discountable_amount = "";
            //不参与优惠计算的金额
            //builder.undiscountable_amount = "";
            //订单名称
            builder.subject = body.Trim();
            //自定义超时时间
            builder.timeout_express = "2m";
            //订单描述
            builder.body = body.Trim();
            //门店编号，很重要的参数，可以用作之后的营销
            builder.store_id = "";
            //操作员编号，很重要的参数，可以用作之后的营销
            builder.operator_id = "";


            //传入商品信息详情
            List<GoodsInfo> gList = new List<GoodsInfo>();

            GoodsInfo goods = new GoodsInfo();
            goods.goods_id = "304";
            goods.goods_name = "goods#name";
            goods.price = total_fee.Trim();
            goods.quantity = "1";
            gList.Add(goods);
            builder.goods_detail = gList;

            //系统商接入可以填此参数用作返佣
            //ExtendParams exParam = new ExtendParams();
            //exParam.sysServiceProviderId = "20880000000000";
            //builder.extendParams = exParam;

            AlipayF2FPayResult payResult = serviceClient.tradePay(builder);

            switch (payResult.Status)
            {
                case ResultEnum.SUCCESS:
                    result.Success = true;
                    result.Message = "支付成功";
                    break;
                case ResultEnum.FAILED:
                    var subMsg = payResult.response.SubMsg;
                    if (string.IsNullOrEmpty(subMsg))
                    {
                        subMsg = "支付失败";
                    }
                    result.Message = subMsg;
                    break;
                case ResultEnum.UNKNOWN:
                    result.Message = "支付失败，网络异常，请检查网络配置后，更换外部订单号重试";
                    break;
            }
            return result;
        }

        ///// <summary>
        ///// 构造支付请求数据
        ///// </summary>
        ///// <returns>请求数据集</returns>
        //private AlipayTradePayContentBuilder BuildPayContent()
        //{
        //    //线上联调时，请输入真实的外部订单号。
        //    string out_trade_no = System.DateTime.Now.ToString("yyyyMMddHHmmss") + "0000" + (new Random()).Next(1, 10000).ToString();

        //    //扫码枪扫描到的用户手机钱包中的付款条码
        //    AlipayTradePayContentBuilder builder = new AlipayTradePayContentBuilder();

        //    //收款账号
        //    builder.seller_id = Config.pid;
        //    //订单编号
        //    builder.out_trade_no = out_trade_no;
        //    //支付场景，无需修改
        //    builder.scene = "bar_code";
        //    //支付授权码,付款码
        //    builder.auth_code = WIDdynamic_id.Text.Trim();
        //    //订单总金额
        //    builder.total_amount = WIDtotal_fee.Text.Trim();
        //    //参与优惠计算的金额
        //    //builder.discountable_amount = "";
        //    //不参与优惠计算的金额
        //    //builder.undiscountable_amount = "";
        //    //订单名称
        //    builder.subject = WIDsubject.Text.Trim();
        //    //自定义超时时间
        //    builder.timeout_express = "2m";
        //    //订单描述
        //    builder.body = "";
        //    //门店编号，很重要的参数，可以用作之后的营销
        //    builder.store_id = "test store id";
        //    //操作员编号，很重要的参数，可以用作之后的营销
        //    builder.operator_id = "test";


        //    //传入商品信息详情
        //    List<GoodsInfo> gList = new List<GoodsInfo>();

        //    GoodsInfo goods = new GoodsInfo();
        //    goods.goods_id = "304";
        //    goods.goods_name = "goods#name";
        //    goods.price = "0.01";
        //    goods.quantity = "1";
        //    gList.Add(goods);
        //    builder.goods_detail = gList;

        //    //系统商接入可以填此参数用作返佣
        //    //ExtendParams exParam = new ExtendParams();
        //    //exParam.sysServiceProviderId = "20880000000000";
        //    //builder.extendParams = exParam;

        //    return builder;

        //}


        ///// <summary>
        ///// 请添加支付成功后的处理
        ///// </summary>
        //private void DoSuccessProcess(AlipayF2FPayResult payResult)
        //{

        //    //请添加支付成功后的处理
        //    System.Console.WriteLine("支付成功");
        //    result = payResult.response.Body;
        //}

        ///// <summary>
        ///// 请添加支付失败后的处理
        ///// </summary>
        //private void DoFailedProcess(AlipayF2FPayResult payResult)
        //{
        //    //请添加支付失败后的处理
        //    System.Console.WriteLine("支付失败");
        //    result = payResult.response.Body;
        //}
    }
}
