using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXStudio.Framework.Unity
{
    /// <summary>
    /// 时间戳辅助类
    /// </summary>
    public class TimeStampUtility
    {
        public static DateTime FromTimeStamp(long timestamp)
        {
            if (timestamp <= 0)
                return GetDefaultDateTime();

            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timestamp.ToString() + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);

            return dtResult;
        }

        public static long GetDefaultTimeStamp()
        {
            return ToTimeStamp(GetDefaultDateTime());
        }

        public static DateTime GetDefaultDateTime()
        {
            return new DateTime(1970, 1, 1).ToLocalTime();
        }

        public static long ToTimeStamp(DateTime datetime)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            //DateTime dtNow = DateTime.Parse(DateTime.Now.ToString());
            TimeSpan toNow = datetime.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = "0000000" + timeStamp;
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);

            return long.Parse(timeStamp);
        }
    }
}
