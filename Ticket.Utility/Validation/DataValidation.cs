using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Utility.Validation
{
    public static class DataValidation
    {
        public static bool IsDataTime(this string value)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return true;
            }
            return false;
        }
    }
}
