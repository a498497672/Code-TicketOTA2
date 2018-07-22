using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Utility.Extensions
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this Enum value)
        {
            var enumType = value.GetType();
            var displayAttribute = enumType.GetField(value.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .SingleOrDefault() as DisplayAttribute;
            if (displayAttribute == null)
            {
                return value.ToString();
            }
            return displayAttribute.Name;
        }

        public static string GetDescriptionByName<T>(this T enumItemName)
        {
            FieldInfo fi = enumItemName.GetType().GetField(enumItemName.ToString());
            if (fi == null)
            {
                return "";
            }
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : enumItemName.ToString();
        }
    }
}
