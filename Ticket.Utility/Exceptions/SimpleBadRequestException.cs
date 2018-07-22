using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Utility.Exceptions
{
    public class SimpleBadRequestException : Exception
    {
        public SimpleBadRequestException()
        {
        }

        public SimpleBadRequestException(string message) : base(message)
        {
        }

        public SimpleBadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SimpleBadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
