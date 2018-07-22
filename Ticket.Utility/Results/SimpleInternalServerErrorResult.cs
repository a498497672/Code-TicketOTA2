using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Ticket.Utility.Results
{
    /// <summary>
    /// 服务器错误
    /// </summary>
    public class SimpleInternalServerErrorResult : IHttpActionResult
    {
        public SimpleInternalServerErrorResult(HttpRequestMessage request, string content)
        {
            Request = request;
            Content = content;
        }

        public HttpRequestMessage Request { get; }
        public string Content { get; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                RequestMessage = Request,
                Content = new StringContent(Content)
            };
            return Task.FromResult(response);
        }
    }
}
