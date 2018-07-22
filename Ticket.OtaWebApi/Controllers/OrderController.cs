using FengjingSDK461.Model.Request;
using FengjingSDK461.Model.Result;
using System.Web.Http;
using System.Web.Http.Description;
using Ticket.Application;

namespace Ticket.OtaWebApi.Controllers
{
    /// <summary>
    /// 订单
    /// </summary>
    [RoutePrefix("api/order")]
    public class OrderController : ApiController
    {
        private readonly OrderFacadeService _orderFacadeService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderFacadeService"></param>
        public OrderController(OrderFacadeService orderFacadeService)
        {
            _orderFacadeService = orderFacadeService;
        }

        /// <summary>
        /// 获取订单
        /// </summary>
        /// <response code="200">The user got.</response>
        /// <response code="404">The user not found.</response>
        [Route("query")]
        [ResponseType(typeof(PageResult))]
        public IHttpActionResult PostQueryOrder(PageRequest request)
        {
            if (string.IsNullOrEmpty(request.Data) || string.IsNullOrEmpty(request.Sign))
            {
                return NotFound();
            }
            var result = _orderFacadeService.QueryOrder(request.Data, request.Sign);
            return Ok(result);
        }

        /// <summary>
        /// 下单验证接口
        /// </summary>
        /// <response code="200">The user got.</response>
        /// <response code="404">The user not found.</response>
        [Route("verify")]
        [ResponseType(typeof(PageResult))]
        public IHttpActionResult PostVerifyOrder(PageRequest request)
        {
            if (string.IsNullOrEmpty(request.Data) || string.IsNullOrEmpty(request.Sign))
            {
                return NotFound();
            }
            var result = _orderFacadeService.VerifyOrder(request.Data, request.Sign);
            return Ok(result);
        }

        /// <summary>
        /// 下单验证接口--单个产品
        /// </summary>
        /// <response code="200">The user got.</response>
        /// <response code="404">The user not found.</response>
        [Route("verifySingle")]
        [ResponseType(typeof(PageResult))]
        public IHttpActionResult PostVerifySingleOrder(PageRequest request)
        {
            if (string.IsNullOrEmpty(request.Data) || string.IsNullOrEmpty(request.Sign))
            {
                return NotFound();
            }
            var result = _orderFacadeService.VerifySingleOrder(request.Data, request.Sign);
            return Ok(result);
        }

        /// <summary>
        /// 创建单个产品订单
        /// </summary>
        /// <response code="200">The user got.</response>
        /// <response code="404">The user not found.</response>
        [Route("singleCreate")]
        [ResponseType(typeof(PageResult))]
        public IHttpActionResult PostCreateSingleOrder(PageRequest request)
        {
            if (string.IsNullOrEmpty(request.Data) || string.IsNullOrEmpty(request.Sign))
            {
                return NotFound();
            }
            var result = _orderFacadeService.PaySingleOrder(request.Data, request.Sign);
            return Ok(result);
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <response code="200">The user got.</response>
        /// <response code="404">The user not found.</response>
        [Route("create")]
        [ResponseType(typeof(PageResult))]
        public IHttpActionResult PostCreateOrder(PageRequest request)
        {
            if (string.IsNullOrEmpty(request.Data) || string.IsNullOrEmpty(request.Sign))
            {
                return NotFound();
            }
            var result = _orderFacadeService.PayOrder(request.Data, request.Sign);
            return Ok(result);
        }



        /// <summary>
        /// 取消订单
        /// </summary>
        /// <response code="200">The user got.</response>
        /// <response code="404">The user not found.</response>
        [Route("cancel")]
        [ResponseType(typeof(PageResult))]
        public IHttpActionResult PostCancelOrder(PageRequest request)
        {
            if (string.IsNullOrEmpty(request.Data) || string.IsNullOrEmpty(request.Sign))
            {
                return NotFound();
            }
            var result = _orderFacadeService.CancelOrder(request.Data, request.Sign);
            return Ok(result);
        }

        /// <summary>
        /// 取消订单项
        /// </summary>
        /// <response code="200">The user got.</response>
        /// <response code="404">The user not found.</response>
        [Route("cancelOrderDetail")]
        [ResponseType(typeof(PageResult))]
        public IHttpActionResult PostCancelOrderDetail(PageRequest request)
        {
            if (string.IsNullOrEmpty(request.Data) || string.IsNullOrEmpty(request.Sign))
            {
                return NotFound();
            }
            var result = _orderFacadeService.CancelOrderDetail(request.Data, request.Sign);
            return Ok(result);
        }

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <response code="200">The user got.</response>
        /// <response code="404">The user not found.</response>
        [Route("update")]
        [ResponseType(typeof(PageResult))]
        public IHttpActionResult PostUpdateOrder(PageRequest request)
        {
            if (string.IsNullOrEmpty(request.Data) || string.IsNullOrEmpty(request.Sign))
            {
                return NotFound();
            }
            var result = _orderFacadeService.UpdateOrder(request.Data, request.Sign);
            return Ok(result);
        }
    }
}
