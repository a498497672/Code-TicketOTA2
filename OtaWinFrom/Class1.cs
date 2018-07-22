using FengjingSDK461.Helpers;
using FengjingSDK461.Model.Request;
using FengjingSDK461.Model.Response;
using FengjingSDK461.Model.Result;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OtaWinFrom
{
    public class Class1
    {
        private static string apiUrl = ConfigurationManager.AppSettings["url"];
        //private static string apiUrl = "http://localhost:4688/api/";
        public static string key = ConfigurationManager.AppSettings["key"];
        public static string value = ConfigurationManager.AppSettings["value"];

        public static ProductResponse GetProduct(ProductQueryRequest request)
        {
            request.Head = new HeadRequest
            {
                InvokeTime = DateTime.Now.ToString("yyyy-MM-dd"),
                InvokeUser = key,
                ProtocolVersion = "V1"
            };
            string url = apiUrl + "product";
            var result = dooPost(request, url).Result;
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<ProductResponse>(result.Data);
            }
            return new ProductResponse { Head = new HeadResponse { Describe = "数据格式不正确" } };
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static OrderCreateResponse CreateOrder(OrderCreateRequest request)
        {
            request.Head = new HeadRequest
            {
                InvokeTime = DateTime.Now.ToString("yyyy-MM-dd"),
                InvokeUser = key,
                ProtocolVersion = "V1"
            };
            string url = apiUrl + "order/create";
            var result = dooPost(request, url).Result;
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderCreateResponse>(result.Data);
            }
            return new OrderCreateResponse { Head = new HeadResponse { Describe = "数据格式不正确" } };
        }

        /// <summary>
        /// 创建单个产品订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static OrderSingleCreateResponse SingleCreateOrder(OrderSingleCreateRequest request)
        {
            request.Head = new HeadRequest
            {
                InvokeTime = DateTime.Now.ToString("yyyy-MM-dd"),
                InvokeUser = key,
                ProtocolVersion = "V1"
            };
            string url = apiUrl + "order/singleCreate";
            var result = dooPost(request, url).Result;
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderSingleCreateResponse>(result.Data);
            }
            return new OrderSingleCreateResponse { Head = new HeadResponse { Describe = "数据格式不正确" } };
        }

        /// <summary>
        /// 下单验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static OrderSingleCreateResponse VerifyOrder(OrderSingleCreateRequest request)
        {
            request.Head = new HeadRequest
            {
                InvokeTime = DateTime.Now.ToString("yyyy-MM-dd"),
                InvokeUser = key,
                ProtocolVersion = "V1"
            };
            string url = apiUrl + "order/verify";
            var result = dooPost(request, url).Result;
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderSingleCreateResponse>(result.Data);
            }
            return new OrderSingleCreateResponse { Head = new HeadResponse { Describe = "数据格式不正确" } };
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static OrderCancelResponse CancelOrder(OrderCancelRequest request)
        {
            request.Head = new HeadRequest
            {
                InvokeTime = DateTime.Now.ToString("yyyy-MM-dd"),
                InvokeUser = key,
                ProtocolVersion = "V1"
            };
            string url = apiUrl + "order/cancel";
            var result = dooPost(request, url).Result;
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderCancelResponse>(result.Data);
            }
            return new OrderCancelResponse { Head = new HeadResponse { Describe = "数据格式不正确" } };
        }

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static OrderUpdateResponse UpdateOrder(OrderUpdateBody body)
        {
            var request = new OrderUpdateRequest
            {
                Head = new HeadRequest
                {
                    InvokeTime = DateTime.Now.ToString("yyyy-MM-dd"),
                    InvokeUser = key,
                    ProtocolVersion = "V1"
                },
                Body = body
            };
            string url = apiUrl + "order/update";
            var result = dooPost(request, url).Result;
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderUpdateResponse>(result.Data);
            }
            return new OrderUpdateResponse { Head = new HeadResponse { Describe = "数据格式不正确" } };
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static OrderQueryResponse QueryOrder(OrderQuery body)
        {
            var request = new OrderQueryRequest
            {
                Head = new HeadRequest
                {
                    InvokeTime = DateTime.Now.ToString("yyyy-MM-dd"),
                    InvokeUser = key,
                    ProtocolVersion = "V1"
                },
                Body = body
            };
            string url = apiUrl + "order/query";
            var result = dooPost(request, url).Result;
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderQueryResponse>(result.Data);
            }
            return new OrderQueryResponse { Head = new HeadResponse { Describe = "数据格式不正确" } };
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static MessageSendResponse SendMessage(MessageSendBody body)
        {
            var request = new MessageSendRequest
            {
                Head = new HeadRequest
                {
                    InvokeTime = DateTime.Now.ToString("yyyy-MM-dd"),
                    InvokeUser = key,
                    ProtocolVersion = "V1"
                },
                Body = body
            };
            string url = apiUrl + "message/send";
            var result = dooPost(request, url).Result;
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<MessageSendResponse>(result.Data);
            }
            return new MessageSendResponse { Head = new HeadResponse { Describe = "数据格式不正确" } };
        }

        /// <summary>  
        /// HttpClient实现Post请求(异步)  
        /// </summary>  
        public static async Task<PageResult> dooPost<T>(T request, string url)
        {
            var data = Base64Helper.ObjectToBase64Encode(request);
            var context = value + data;
            var sign = Md5Helper.Md5Encrypt32(context);
            var securityType = "MD5";

            url += "?data=" + data + "&sign=" + sign + "&securityType=" + securityType;
            //设置HttpClientHandler的AutomaticDecompression  
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            //创建HttpClient（注意传入HttpClientHandler）  
            using (var http = new HttpClient(handler))
            {
                //使用FormUrlEncodedContent做HttpContent  
                var content = new FormUrlEncodedContent(
                    new Dictionary<string, string>()
                    {    {"data",data},
                         {"sign",sign},
                         {"securityType", securityType}//键名必须为空  
                     });

                //await异步等待回应  

                var response = await http.PostAsync(url, content);
                //确保HTTP成功状态值  
                var httpResponseMessage = response.EnsureSuccessStatusCode();
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var ddd = await response.Content.ReadAsStringAsync();
                    var result = JsonHelper.JsonToObject<PageResult>(ddd);
                    return result;
                }
                return new PageResult();
            }

        }

        /// <summary>  
        /// HttpClient实现Get请求(异步)  
        /// </summary>  
        public static async Task<ProductResponse> dooGet()
        {
            ProductQueryRequest request = new ProductQueryRequest
            {
                Head = new HeadRequest
                {
                    InvokeTime = DateTime.Now.ToString("yyyy-MM-dd"),
                    InvokeUser = "mykawqbopdquem2dsf",
                    ProtocolVersion = "V1"
                },
                Body = new Product
                {
                    Type = 0
                }
            };
            var data = Base64Helper.ObjectToBase64Encode(request);
            var sign = Md5Helper.Md5Encrypt32(data, "dc0b52fb-c750-44f2-b5dc-f2577fd98c1b");
            var securityType = "MD5";

            string url = "http://192.168.13.43:60110/api/product?data=" + data + "&sign=" + sign + "&securityType=" + securityType;
            //创建HttpClient（注意传入HttpClientHandler）  
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };

            using (var http = new HttpClient(handler))
            {
                //await异步等待回应  
                var response = await http.GetAsync(url);
                //确保HTTP成功状态值  
                var httpResponseMessage = response.EnsureSuccessStatusCode();
                if (httpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var ddd = await response.Content.ReadAsStringAsync();
                    var result = JsonHelper.JsonToObject<PageResult>(ddd);
                    return Base64Helper.Base64EncodeToObject<ProductResponse>(result.Data);
                }
                return null;

                //await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）  

            }
        }
        /// <summary>  
        /// HttpClient实现Put请求(异步)  
        /// </summary>  
        public static async void dooPut()
        {
            var userId = 1;
            string url = "http://localhost:52824/api/register?userid=" + userId;

            //设置HttpClientHandler的AutomaticDecompression  
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            //创建HttpClient（注意传入HttpClientHandler）  
            using (var http = new HttpClient(handler))
            {
                //使用FormUrlEncodedContent做HttpContent  
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
           {"Name","修改zzl"},
           {"Info", "Put修改动作"}//键名必须为空  
        });

                //await异步等待回应  

                var response = await http.PutAsync(url, content);
                //确保HTTP成功状态值  
                response.EnsureSuccessStatusCode();
                //await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）  
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
