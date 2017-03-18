using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.UI.Xaml.Data;
using GamerSky.Helper;

namespace GamerSky.Converters
{
    public class DateTimeToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime createTime = DateTimeHelper.UnixTimeStampToDateTime((long)value);
            TimeSpan time =  DateTime.Now - createTime;
            string timePast = string.Empty;
            if(time.TotalDays > 30)
            {
                timePast = "几个月前";
            }
            else if(time.TotalDays > 7)
            {
                timePast = (int)(time.TotalDays % 7) +"周前";
            }
            else if(time.TotalDays > 1)
            {
                timePast = (int)time.TotalDays + "天前";
            }
            else if(time.TotalHours > 1)
            {
                timePast = (int)time.TotalHours + "小时前";
            }
            else if(time.TotalMinutes > 0)
            {
                timePast = (int)time.TotalMinutes   + "分钟前";
            }
            else if(time.TotalSeconds > 0)
            {
                timePast = (int)time.TotalSeconds  + "秒前";
            }
            return timePast;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
