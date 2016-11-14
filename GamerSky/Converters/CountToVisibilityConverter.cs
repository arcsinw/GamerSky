using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using GamerSky.Core.Helper;
using GamerSky.Core.Model;

namespace GamerSky.Converters
{
    /// <summary>
    /// Count 转 bool
    /// </summary>
    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            double count = (double)value;
            
            if(count!=0)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
