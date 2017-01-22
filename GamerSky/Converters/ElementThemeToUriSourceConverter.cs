using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GamerSky.Converters
{
    public class ElementThemeToUriSourceConverter  : IValueConverter
    {  
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ( (ElementTheme)value == ElementTheme.Dark)
            {
                return new Uri("ms-appx:///Assets/Images/drawer_day.png");
            }
            else
            {
                return new Uri("ms-appx:///Assets/Images/drawer_night.png");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
