using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ticket.SaleWebApi.Application;
using Ticket.Utility.Logger;

namespace Ticket.SaleWebApi.Controllers
{
    /// <summary>
    /// 同程
    /// </summary>
    [RoutePrefix("api/tongCheng")]
    public class TongChengController : ApiController
    {
        private readonly TongChengFacadeService _tongChengFacadeService;
        private readonly SimpleLogger _logger = new SimpleLogger();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tongChengFacadeService"></param>
        public TongChengController(TongChengFacadeService tongChengFacadeService)
        {
            _tongChengFacadeService = tongChengFacadeService;
        }

        /// <summary>
        /// 同程
        /// </summary>
        /// <response code="200">The user got.</response>
        /// <response code="404">The user not found.</response>
        [Route("handler")]
        public IHttpActionResult PostHandler()
        {
            string request = Request.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrEmpty(request))
            {
                return NotFound();
            }
            _logger.Info(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  : " + request);
            var result = _tongChengFacadeService.Handler(request);
            return Ok(result);
        }
    }
}
