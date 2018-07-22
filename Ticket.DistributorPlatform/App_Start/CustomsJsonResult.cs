using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Web.Mvc;

namespace Ticket.DistributorPlatform.App_Start
{
    public class CustomsJsonResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            var response = context.HttpContext.Response;
            response.ContentType = !string.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            var jsonSerizlizerSetting = new JsonSerializerSettings();
            //设置取消循环引用
            jsonSerizlizerSetting.MissingMemberHandling = MissingMemberHandling.Ignore;
            //设置首字母小写
            jsonSerizlizerSetting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //设置日期的格式为：yyyy-MM-dd HH:mm:ss
            jsonSerizlizerSetting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            var json = JsonConvert.SerializeObject(Data, Formatting.None, jsonSerizlizerSetting);
            response.Write(json);
        }
    }
}