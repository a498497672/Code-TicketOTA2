using FengjingSDK461.Enum;
using FengjingSDK461.Helpers;
using FengjingSDK461.Model.Request;
using FengjingSDK461.Model.Response;
using FengjingSDK461.Model.Result;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FengjingSDK461.Core
{
    public class TicketGateway
    {
        private readonly string _website;
        private readonly string _userId;
        private readonly string _userKey;

        public TicketGateway(OtaType otaType)
        {
            if (otaType == OtaType.Ctrip)
            {
                _website = ConfigurationManager.AppSettings["TicketCtrip:Website"];
                _userId = ConfigurationManager.AppSettings["TicketCtrip:UserId"];
                _userKey = ConfigurationManager.AppSettings["TicketCtrip:UserKey"];
            }
            else if (otaType == OtaType.TongCheng)
            {
                _website = ConfigurationManager.AppSettings["TicketTongCheng:Website"];
                _userId = ConfigurationManager.AppSettings["TicketTongCheng:UserId"];
                _userKey = ConfigurationManager.AppSettings["TicketTongCheng:UserKey"];
            }
            else if (otaType == OtaType.MeiTuan)
            {
                _website = ConfigurationManager.AppSettings["TicketMeiTuan:Website"];
                _userId = ConfigurationManager.AppSettings["TicketMeiTuan:UserId"];
                _userKey = ConfigurationManager.AppSettings["TicketMeiTuan:UserKey"];
            }
        }


        public ProductResponse GetProduct(ProductQueryRequest request)
        {
            request.Head = RequestHead();
            string url = _website + "product";
            var result = PostService(request, url);
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<ProductResponse>(result.Data);
            }
            return new ProductResponse
            {
                Head = new HeadResponse
                {
                    Code = "900001",
                    Describe = "数据格式不正确"
                }
            };
        }



        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OrderCreateResponse CreateOrder(OrderCreateRequest request)
        {
            request.Head = RequestHead();
            string url = _website + "order/create";
            var result = PostService(request, url);
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderCreateResponse>(result.Data);
            }
            return new OrderCreateResponse
            {
                Head = new HeadResponse
                {
                    Code = "900001",
                    Describe = "数据格式不正确"
                }
            };
        }

        /// <summary>
        /// 创建单个产品订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OrderSingleCreateResponse SingleCreateOrder(OrderSingleCreateRequest request)
        {
            request.Head = RequestHead();
            string url = _website + "order/singleCreate";
            var result = PostService(request, url);
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderSingleCreateResponse>(result.Data);
            }
            return new OrderSingleCreateResponse
            {
                Head = new HeadResponse
                {
                    Code = "900001",
                    Describe = "数据格式不正确"
                }
            };
        }

        /// <summary>
        /// 下单验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OrderCreateResponse VerifyOrder(OrderCreateRequest request)
        {
            request.Head = RequestHead();
            string url = _website + "order/verify";
            var result = PostService(request, url);
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderCreateResponse>(result.Data);
            }
            return new OrderCreateResponse
            {
                Head = new HeadResponse
                {
                    Code = "900001",
                    Describe = "数据格式不正确"
                }
            };
        }

        /// <summary>
        /// 下单验证
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OrderSingleCreateResponse VerifySingleOrder(OrderSingleCreateRequest request)
        {
            request.Head = RequestHead();
            string url = _website + "order/verifySingle";
            var result = PostService(request, url);
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderSingleCreateResponse>(result.Data);
            }
            return new OrderSingleCreateResponse
            {
                Head = new HeadResponse
                {
                    Code = "900001",
                    Describe = "数据格式不正确"
                }
            };
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OrderCancelResponse CancelOrder(OrderCancelRequest request)
        {
            request.Head = RequestHead();
            string url = _website + "order/cancel";
            var result = PostService(request, url);
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderCancelResponse>(result.Data);
            }
            return new OrderCancelResponse
            {
                Head = new HeadResponse
                {
                    Code = "900001",
                    Describe = "数据格式不正确"
                }
            };
        }

        /// <summary>
        /// 取消订单项
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OrderCancelResponse CancelOrderDetail(OrderCancelRequest request)
        {
            request.Head = RequestHead();
            string url = _website + "order/cancelOrderDetail";
            var result = PostService(request, url);
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderCancelResponse>(result.Data);
            }
            return new OrderCancelResponse
            {
                Head = new HeadResponse
                {
                    Code = "900001",
                    Describe = "数据格式不正确"
                }
            };
        }

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public OrderUpdateResponse UpdateOrder(OrderUpdateBody body)
        {

            var request = new OrderUpdateRequest
            {
                Head = RequestHead(),
                Body = body
            };
            string url = _website + "order/update";
            var result = PostService(request, url);
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderUpdateResponse>(result.Data);
            }
            return new OrderUpdateResponse
            {
                Head = new HeadResponse
                {
                    Code = "900001",
                    Describe = "数据格式不正确"
                }
            };
        }

        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public OrderQueryResponse QueryOrder(OrderQuery body)
        {
            var request = new OrderQueryRequest
            {
                Head = RequestHead(),
                Body = body
            };
            string url = _website + "order/query";
            var result = PostService(request, url);
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<OrderQueryResponse>(result.Data);
            }
            return new OrderQueryResponse
            {
                Head = new HeadResponse
                {
                    Code = "900001",
                    Describe = "数据格式不正确"
                }
            };
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public MessageSendResponse SendMessage(MessageSendBody body)
        {
            var request = new MessageSendRequest
            {
                Head = RequestHead(),
                Body = body
            };
            string url = _website + "message/send";
            var result = PostService(request, url);
            if (!string.IsNullOrEmpty(result.Data))
            {
                return Base64Helper.Base64EncodeToObject<MessageSendResponse>(result.Data);
            }
            return new MessageSendResponse
            {
                Head = new HeadResponse
                {
                    Code = "900001",
                    Describe = "数据格式不正确"
                }
            };
        }

        private HeadRequest RequestHead()
        {
            return new HeadRequest
            {
                InvokeTime = DateTime.Now.ToString("yyyy-MM-dd"),
                InvokeUser = _userId,
                ProtocolVersion = "V1"
            };
        }

        /// <summary>
        /// 请求方式
        /// </summary>
        /// <param name="str"></param>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public string Post(string str, string url, int timeout = 60)
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
                byte[] data = Encoding.UTF8.GetBytes(str);
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

        /// <summary>  
        /// HttpClient实现Post请求 
        /// </summary>  
        public PageResult PostService<T>(T request, string url)
        {
            var data = Base64Helper.ObjectToBase64Encode(request);
            var context = _userKey + data;
            var sign = Md5Helper.Md5Encrypt32(context);
            var securityType = "MD5";

            var str = JsonHelper.ObjectToJson(new PageResult
            {
                Data = data,
                Sign = sign,
                SecurityType = securityType
            });
            url += "?data=" + data + "&sign=" + sign + "&securityType=" + securityType;
            var responseContent = Post(str, url);

            if (!string.IsNullOrEmpty(responseContent))
            {
                var result = JsonHelper.JsonToObject<PageResult>(responseContent);
                return result;
            }
            return new PageResult();
        }
    }
}
