using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace FengjingSDK461.Helpers
{
    public static class JsonHelper
    {
        /// <summary>
        /// 对象转化成json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {


            //JavaScriptSerializer json = new JavaScriptSerializer();
            //return json.Serialize(obj);
            return JsonConvert.SerializeObject(
                obj,
                Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }
                );
        }

        public static string ObjectToJsonStr(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        /// <summary>
        /// 把json字符串转成对象
        /// </summary>
        /// <typeparam name="T">对象</typeparam>
        /// <param name="data">json字符串</param> 
        public static T JsonToObject<T>(string data)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Deserialize<T>(data);
        }
    }
}
