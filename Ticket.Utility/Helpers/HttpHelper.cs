using System;
using System.IO;
using System.Net;
using System.Text;

namespace Ticket.Utility.Helpers
{
    public class HttpHelper
    {
        public static string Post(string url, string postmap, int timeout = 10)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;

            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.Timeout = timeout * 1000;

                //设置POST的数据类型和长度
                request.ContentType = "application/json";
                byte[] data = System.Text.Encoding.UTF8.GetBytes(postmap);
                request.ContentLength = data.Length;

                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                //Log.Error("HttpService", "Thread - caught ThreadAbortException - resetting.");
                //Log.Error("Exception message: {0}", e.Message);
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                //Log.Error("HttpService", e.ToString());
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    //Log.Error("HttpService", "StatusCode : " + ((HttpWebResponse)e.Response).StatusCode);
                    //Log.Error("HttpService", "StatusDescription : " + ((HttpWebResponse)e.Response).StatusDescription);
                }
                throw new Exception(e.ToString());
            }
            catch (Exception e)
            {
                //Log.Error("HttpService", e.ToString());
                throw new Exception(e.ToString());
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }
    }
}
