using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Utility.Helpers;

namespace Ticket.Infrastructure.KouDaiLingQian.Lib
{
    public class PayData
    {
        public PayData()
        {

        }

        //采用排序的Dictionary的好处是方便对数据包进行签名，不用再签名之前再做一次排序
        private SortedDictionary<string, object> m_values = new SortedDictionary<string, object>();

        /**
        * 设置某个字段的值
        * @param key 字段名
         * @param value 字段值
        */
        public void Put(string key, object value)
        {
            m_values[key] = value;
        }

        public void Remove(string key)
        {
            m_values.Remove(key);
        }

        public void PutAll(PayData data)
        {
            foreach (KeyValuePair<string, object> pair in data.GetAll())
            {
                if (pair.Value == null)
                {
                    //Log.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");
                    throw new Exception("PayData内部含有值为null的字段!");
                }
                m_values[pair.Key] = pair.Value;
            }

        }

        /**
        * 根据字段名获取某个字段的值
        * @param key 字段名
         * @return key对应的字段值
        */
        public object GetValue(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            return o;
        }

        public SortedDictionary<string, object> GetAll()
        {
            return m_values;
        }

        /**
         * 判断某个字段是否已设置
         * @param key 字段名
         * @return 若字段key已被设置，则返回true，否则返回false
         */
        public bool IsSet(string key)
        {
            object o = null;
            m_values.TryGetValue(key, out o);
            if (null != o)
                return true;
            else
                return false;
        }


        /**
        * @Dictionary格式转化成url参数格式
        * @ return url格式串, 该串不包含sign字段值
        */
        public string ToUrl()
        {
            string buff = "";
            foreach (KeyValuePair<string, object> pair in m_values)
            {
                if (pair.Value == null)
                {
                    //Log.Error(this.GetType().ToString(), "WxPayData内部含有值为null的字段!");
                    throw new Exception("PayData内部含有值为null的字段!");
                }
                buff += pair.Key + "=" + pair.Value + "&";
            }
            buff = buff.Trim('&');
            return buff;
        }


        /**
        * @Dictionary格式化成Json
         * @return json串数据
        */
        public string ToJson()
        {
            string jsonStr = JsonSerializeHelper.ToJson(m_values);
            return jsonStr;
        }
    }
}
