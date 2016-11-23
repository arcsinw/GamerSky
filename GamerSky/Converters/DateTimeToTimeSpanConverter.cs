using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.UI.Xaml.Data;
using GamerSky.Core.Helper;

namespace GamerSky.Converters
{
    public class DateTimeToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime createTime = DateTimeHelper.UnixTimeStampToDateTime((long)value);
            TimeSpan time =  DateTime.Now - createTime;
            if(time.TotalDays > 30)
            {
                return "几个月前";
            }
            else if(time.TotalDays > 7)
            {
                return "几周前";
            }
            else if(time.TotalDays > 0)
            {
                return time.TotalDays + "天前";
            }
            else if(time.TotalHours > 1)
            {
                return time.TotalHours + "小时前";
            }
            else if(time.TotalMinutes > 0)
            {
                return time.TotalMinutes + "分钟前";
            }
            else if(time.TotalSeconds > 0)
            {
                return time.TotalSeconds + "秒前";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
