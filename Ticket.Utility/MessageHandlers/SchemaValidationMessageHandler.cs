using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Linq;
using System.Xml.Schema;

namespace Ticket.Utility.MessageHandlers
{
    public class SchemaValidationMessageHandler : DelegatingHandler
    {

        private XmlSchemaSet _schemaSet;
        public SchemaValidationMessageHandler()
        {

            _schemaSet = new XmlSchemaSet();
            //_schemaSet.Add(null, "OrderSchema.xsd");
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            if (request.Content != null && request.Content.Headers.ContentType.MediaType == "application/xml")
            {
                var tcs = new TaskCompletionSource<HttpResponseMessage>();

                var task = request.Content.LoadIntoBufferAsync()  // I think this is needed so XmlMediaTypeFormatter will still have access to the content
                    .ContinueWith(t =>
                    {
                        request.Content.ReadAsStreamAsync()
                            .ContinueWith(t2 =>
                            {
                                var doc = XDocument.Load(t2.Result);
                                var msgs = new List<string>();
                                doc.Validate(_schemaSet, (s, e) => msgs.Add(e.Message));
                                if (msgs.Count > 0)
                                {
                                    var responseContent = new StringContent(String.Join(Environment.NewLine, msgs.ToArray()));
                                    tcs.TrySetException(new HttpResponseException(
                                                     new HttpResponseMessage(HttpStatusCode.BadRequest)
                                                     {
                                                         Content = responseContent
                                                     }));
                                }
                                else
                                {
                                    tcs.TrySetResult(base.SendAsync(request, cancellationToken).Result);
                                }
                            });

                    });
                return tcs.Task;
            }
            else
            {
                return base.SendAsync(request, cancellationToken);
            }

        }
    }
}
