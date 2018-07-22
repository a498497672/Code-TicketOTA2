using FengjingSDK461.Model.Request;
using FengjingSDK461.Model.Result;
using System.Web.Http;
using System.Web.Http.Description;
using Ticket.Application;

namespace Ticket.OtaWebApi.Controllers
{
    /// <summary>
    /// 产品
    /// </summary>
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly TicketFacadeService _ticketFacadeService;
        /// <summary>
        /// 
        /// </summary>
        public ProductController(TicketFacadeService ticketFacadeService)
        {
            _ticketFacadeService = ticketFacadeService;
        }

        /// <summary>
        /// 获取产品
        /// </summary>
        /// <response code="200">The user got.</response>
        /// <response code="404">The user not found.</response>
        [Route("")]
        [ResponseType(typeof(PageResult))]
        public IHttpActionResult PostAll(PageRequest request)
        {
            if (string.IsNullOrEmpty(request.Data) || string.IsNullOrEmpty(request.Sign))
            {
                return NotFound();
            }
            var result = _ticketFacadeService.GetAll(request.Data, request.Sign);
            return Ok(result);
        }
    }
}
