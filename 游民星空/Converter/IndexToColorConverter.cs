using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace 游民星空.Converter
{
    /// <summary>
    /// 新闻头条幻灯片下方小圆点
    /// </summary>
    public class IndexToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (int.Parse(value.ToString()) == int.Parse(parameter.ToString()))
            {
                return App.Current.Resources["ThemeColorBrush"] as SolidColorBrush ;
            }
            else
            {
                return new SolidColorBrush(Windows.UI.Colors.White);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
