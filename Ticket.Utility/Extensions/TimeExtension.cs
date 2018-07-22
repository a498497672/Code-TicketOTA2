using System;

namespace Ticket.Utility.Extensions
{
    public static class TimeExtension
    {
        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetTimeStamp(this DateTime time)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dtNow = DateTime.Parse(DateTime.Now.ToString());
            TimeSpan toNow = dtNow.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);

            return timeStamp;
        }
    }
}
