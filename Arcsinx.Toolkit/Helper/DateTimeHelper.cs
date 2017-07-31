using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Arcsinx.Toolkit.Helper
{
    public static class DateTimeHelper
    {
        private static DateTime startTime = new DateTime(1970, 1, 1,8,0,0);

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
            //Debug.WriteLine( ((DateTime.UtcNow.Ticks - 621355968000000000) / 100000).ToString());
            //return ((DateTime.UtcNow.Ticks - 621355968000000000) / 100000).ToString();

            string timeStamp = ((DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000).ToString();
            Debug.WriteLine(timeStamp);
            return timeStamp;
        }

        /// <summary>
        /// 在root中查找第一个T类型的子元素
        /// </summary>
        /// <typeparam name="T">查找的类型</typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static T FindChildOfType<T>(DependencyObject root) where T : class
        {
            var queue = new Queue<DependencyObject>();
            queue.Enqueue(root);
            while(queue.Count > 0)
            {
                var current = queue.Dequeue();
                for (int i = VisualTreeHelper.GetChildrenCount(current) - 1; 0 <= i; i--)
                {
                    var child = VisualTreeHelper.GetChild(current, i);
                    var tmp = child as T;
                    if (tmp!=null)
                    {
                        return tmp;
                    }
                    queue.Enqueue(child);
                }
            }
            return null;
        }
    }
}
