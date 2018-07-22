using DocomSDK.Ticket.Data;
using DocomSDK.TVM;
using System.Collections.Generic;
using Ticket.Utility.Helpers;

namespace OtaWinFrom
{
    public class 闸机类
    {
        //private static string url = "http://120.76.195.73:50002/api/Ota/";
        //private static string urlTvm = "http://120.76.195.73:50002/api/Tvm/";
        //private static string url = "http://119.23.212.153/api/Ota/";
        //private static string urlTvm = "http://119.23.212.153/api/Tvm/";

        private static string url = "http://localhost:52615/api/Ota/";
        private static string urlTvm = "http://localhost:52615/api/Tvm/";

        public static Result<TicketResult> Ticket_CheckTicket(CheckTicket_Object checkTicketObject)
        {
            var data = JsonSerializeHelper.ToJson(checkTicketObject);
            var result = HttpHelper.Post(url + "Ticket_CheckTicket", data, 60);
            return JsonSerializeHelper.ToObject<Result<TicketResult>>(result); 
        }

        
        public static Result<TicketResult> Ticket_SubmitWalkPast(SubmitWalkPast_Object  submitWalkPastObject)
        {
            var data = JsonSerializeHelper.ToJson(submitWalkPastObject);
            var result = HttpHelper.Post(url + "Ticket_SubmitWalkPast", data, 60);
            return JsonSerializeHelper.ToObject<Result<TicketResult>>(result); 
        }

        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static TVMResult<GUITikcetConfig> DeviceIni(TVMDevice device)
        {
            var data = JsonSerializeHelper.ToJson(device);
            var result = HttpHelper.Post(urlTvm + "DeviceIni", data, 60);
            return JsonSerializeHelper.ToObject<TVMResult<GUITikcetConfig>>(result);
        }

        /// <summary>
        /// 快捷支付
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TVMResult<TVMPayInfo> QuickPayment(QuickPayment_Object obj)
        {
            var data = JsonSerializeHelper.ToJson(obj);
            var result = HttpHelper.Post(urlTvm + "QuickPayment", data, 60);
            return JsonSerializeHelper.ToObject<TVMResult<TVMPayInfo>>(result);
        }

        /// <summary>
        /// 查询支付
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TVMResult<TVMPayInfo> QueryPay(QuickPayment_Object obj)
        {
            var data = JsonSerializeHelper.ToJson(obj);
            var result = HttpHelper.Post(urlTvm + "QueryPay", data, 60);
            return JsonSerializeHelper.ToObject<TVMResult<TVMPayInfo>>(result);
        }
        /// <summary>
        /// 门票售卖
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TVMResult<List<SaleTicket_Result>> SaleTicket(QuickPayment_Object obj)
        {
            var data = JsonSerializeHelper.ToJson(obj);
            var result = HttpHelper.Post(urlTvm + "SaleTicket", data, 60);
            return JsonSerializeHelper.ToObject<TVMResult<List<SaleTicket_Result>>>(result);
        }
        /// <summary>
        /// 凭证码、身份证核销
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TVMResult<bool> VerifyTicket(VerifyTicket_Object obj)
        {
            var data = JsonSerializeHelper.ToJson(obj);
            var result = HttpHelper.Post(urlTvm + "VerifyTicket", data, 180);
            return JsonSerializeHelper.ToObject<TVMResult<bool>>(result);
        }

        /// <summary>
        /// 凭证码、身份证取票
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TVMResult<TVMTicketInfo[]> GetTicketOrder(TicketOrder_Object obj)
        {
            var data = JsonSerializeHelper.ToJson(obj);
            var result = HttpHelper.Post(urlTvm + "GetTicketOrder", data, 180);
            return JsonSerializeHelper.ToObject<TVMResult<TVMTicketInfo[]>>(result);
        }
    }
}
