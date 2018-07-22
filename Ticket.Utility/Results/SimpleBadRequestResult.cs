using Ticket.Utility.Messages;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Ticket.Utility.Results
{
    public class SimpleBadRequestResult : IHttpActionResult
    {
        public SimpleBadRequestResult(HttpRequestMessage request, ErrorMessage errorMessage)
        {
            Request = request;
            ErrorMessage = errorMessage;
        }

        public HttpRequestMessage Request { get; }
        public ErrorMessage ErrorMessage { get; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = Request.CreateResponse(HttpStatusCode.BadRequest, ErrorMessage);
            return Task.FromResult(response);
        }
    }
}
