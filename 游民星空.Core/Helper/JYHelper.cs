
using JYAnalyticsUniversal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Helper
{
    /// <summary>
    /// 九幽信息
    /// </summary>
    public class JYHelper
    {
        /// <summary>
        /// AppKey
        /// </summary>
        public const  string AppKey = "2c7fdea4792fb5b5e3031dbf3f99ff15";

        /// <summary>
        /// 阅读次数事件Id
        /// </summary>
        private const string ReadCount_Id = "GamerSky.X_ReadCount";

        /// <summary>
        /// 是否开启数据统计
        /// </summary>
        //public static bool IsTrace = true;

        public async static void StartTraceAsync()
        {
            //await JYAnalytics.StartTrackAsync(AppKey);
        }

        public async static void EndTraceAsync()
        {
            //await JYAnalytics.EndTrackAsync();
        }

        public static void TracePageStart(string pageName)
        {
            //JYAnalytics.TrackPageEnd(pageName);
        }

        public static void TracePageEnd(string pageName)
        {
            //JYAnalytics.TrackPageEnd(pageName);
        }

        /// <summary>
        /// 统计事件发生次数
        /// </summary>
        /// <param name="eventId">当前统计的事件ID</param>
        public static void TraceEvent(string eventId)
        {
            //JYAnalytics.TrackEvent(eventId);
        }

        /// <summary>
        /// 统计阅读新闻次数
        /// </summary>
        public static void TraceRead()
        {
            //JYAnalytics.TrackEvent(ReadCount_Id);
        }

        /// <summary>
        /// 统计发生次数
        /// </summary>
        /// <param name="eventId">当前统计的事件ID</param>
        /// <param name="label">当前统计的事件参数</param>
        public static void TraceEvent(string eventId,string label)
        {
            //TraceEvent(eventId, label);
        }

        /// <summary>
        /// 统计错误
        /// </summary>
        /// <param name="id"></param>
        public static void TraceError(string id)
        {
            //TraceError(id);
        }

    }
}
