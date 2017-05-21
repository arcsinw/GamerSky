using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Helper
{
    public class DateTimeHelper
    {
        private static DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0);

        /// <summary>
        /// Converte unix time stamp to DateTime
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime UnixTimeStampToDateTime(long timeStamp)
        {
            return startTime.AddMilliseconds(timeStamp);
        }

        /// <summary>
        /// Get current Unix timestamp
        /// </summary>
        /// <returns></returns>
        public static string GetUnixTimeStamp()
        {
            return ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000 * 100).ToString();
        }
    }
}
