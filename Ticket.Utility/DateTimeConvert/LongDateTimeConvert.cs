using Newtonsoft.Json.Converters;

namespace Ticket.Utility.DateTimeConvert
{
    public class LongDateTimeConvert : IsoDateTimeConverter
    {
        public LongDateTimeConvert()
            : base()
        {
            base.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
}
