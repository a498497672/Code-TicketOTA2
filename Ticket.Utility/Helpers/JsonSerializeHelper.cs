using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Ticket.Utility.Helpers
{
    public class JsonSerializeHelper
    {
        public static string ToJson(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public static string ToJsonForlowercase(object Data)
        {
            var jsonSerizlizerSetting = new JsonSerializerSettings();
            //设置取消循环引用
            jsonSerizlizerSetting.MissingMemberHandling = MissingMemberHandling.Ignore;
            //设置首字母小写
            jsonSerizlizerSetting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //设置日期的格式为：yyyy-MM-dd HH:mm:ss
            jsonSerizlizerSetting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            return JsonConvert.SerializeObject(Data, Formatting.None, jsonSerizlizerSetting);
        }

        /// <summary>
        /// 把json字符串转成对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="data">json字符串</param> 
        public static T ToObject<T>(string data)
        {

            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}
