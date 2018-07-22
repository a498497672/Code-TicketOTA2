using System;
using System.Runtime.Serialization;

namespace Ticket.Utility.Exceptions
{
    public class SimpleUnauthorizedException:Exception
    {
        public SimpleUnauthorizedException()
        {
        }

        public SimpleUnauthorizedException(string message) : base(message)
        {
        }

        public SimpleUnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SimpleUnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
