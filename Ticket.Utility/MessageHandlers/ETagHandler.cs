using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ticket.Utility.MessageHandlers
{
    public class ETagHandler: DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (request.Method == HttpMethod.Get)
            {
                if (response.Content == null)
                {
                    return response;
                }
                using (var md5 = MD5.Create())
                {
                    var responseByteArrayContent = await response.Content.ReadAsByteArrayAsync();
                    var base64ComputedHash = Convert.ToBase64String(md5.ComputeHash(responseByteArrayContent));
                    var etag = $"\"{base64ComputedHash}\"";
                    response.Headers.ETag = new EntityTagHeaderValue(etag);
                    var matchETag = request.Headers.IfNoneMatch.FirstOrDefault();
                    if (matchETag != null && matchETag.Tag == etag)
                    {
                        response = request.CreateResponse(HttpStatusCode.NotModified);
                    }
                }
            }
            return response;
        }
    }
}
