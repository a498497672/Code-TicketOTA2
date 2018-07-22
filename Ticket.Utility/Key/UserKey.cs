using System;
using System.Configuration;

namespace Ticket.Utility.Key
{
    public class UserKey
    {
        /// <summary>
        /// 景区id
        /// </summary>
        public static readonly int ScenicId = Convert.ToInt32(ConfigurationManager.AppSettings["scenicId"]);
    }
}
