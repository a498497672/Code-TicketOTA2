using System.Collections.Generic;

namespace Ticket.Infrastructure.WxPay.Response
{
    public class JsSDKResponse
    {
        public string AppId { set; get; }
        public string Timestamp { set; get; }
        public string NonceStr { set; get; }
        public string JsapiTicket { set; get; }
        public string Signature { set; get; }
        public string ShareUrl { set; get; }
        public string ShareImg { set; get; }
        public string String1 { set; get; }
        public bool Debug { get; set; }
        private IEnumerable<string> _list;
        public IEnumerable<string> JsApiList
        {
            get
            {
                if (_list == null) { _list = new List<string>(); }
                return _list;
            }
            set { _list = value; }
        }
    }
}
