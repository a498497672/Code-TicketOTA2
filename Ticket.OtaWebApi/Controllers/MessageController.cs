using FengjingSDK461.Model.Request;
using FengjingSDK461.Model.Result;
using System.Web.Http;
using System.Web.Http.Description;
using Ticket.Application;

namespace Ticket.OtaWebApi.Controllers
{
    /// <summary>
    /// 消息
    /// </summary>
    [RoutePrefix("api/message")]
    public class MessageController : ApiController
    {
        private readonly MessageFacadeService _messageFacadeService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageFacadeService"></param>
        public MessageController(MessageFacadeService messageFacadeService)
        {
            _messageFacadeService = messageFacadeService;
        }
        /// <summary>
        /// (重)发送入园凭证地址手机短信
        /// </summary>
        /// <response code="200">The user got.</response>
        /// <response code="404">The user not found.</response>
        [Route("send")]
        [ResponseType(typeof(PageResult))]
        public IHttpActionResult PostSendMessage(PageRequest request)
        {
            if (string.IsNullOrEmpty(request.Data) || string.IsNullOrEmpty(request.Sign))
            {
                return NotFound();
            }
            var result = _messageFacadeService.SendMessage(request.Data, request.Sign);
            return Ok(result);
        }
    }
}
