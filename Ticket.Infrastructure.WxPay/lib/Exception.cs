using System;

namespace Ticket.Infrastructure.WxPay
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}