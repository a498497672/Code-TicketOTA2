using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Utility.Extensions
{
    public static class StringExtension
    {
        private const string Yes = "YES";

        public static int? ToNullableInt32(this string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static decimal? ToNullableDecimal(this string value)
        {
            decimal result;
            if (decimal.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static bool? ToNullableBoolean(this string value)
        {
            bool result;
            if (bool.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }

        public static int ToInt(this string value)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        public static double ToDouble(this string value)
        {
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        public static bool ToBooleanFromYesNoValue(this string value)
        {
            return value.ToUpper() == Yes;
        }

        public static bool? ToNullableBooleanFromYesNoValue(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return value.ToUpper() == Yes;
        }

        public static decimal ToZeroDecimal(this string value)
        {
            decimal result;
            if (decimal.TryParse(value, out result))
            {
                return result;
            }
            return 0;
        }

        public static DateTime ToDataTime(this string value)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }
            return new DateTime();
        }

        public static DateTime ToDataTimeFormat(this string value)
        {
            DateTime dt = DateTime.ParseExact(value, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
            return dt.ToString("yyyy-MM-dd").ToDataTime();
        }

    }
}
