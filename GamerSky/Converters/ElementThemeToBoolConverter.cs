using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GamerSky.Converters
{
    public class ElementThemeToBoolConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
            {
                return ElementTheme.Dark;
            }
            else
            {
                return ElementTheme.Light;
            }
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((ElementTheme)value == ElementTheme.Dark)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
