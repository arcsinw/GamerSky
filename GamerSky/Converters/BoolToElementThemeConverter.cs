using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace GamerSky.Converters
{
    public class BoolToElementThemeConverter : BoolToObjectConverter
    {
        public BoolToElementThemeConverter()
        {
            TrueValue = ElementTheme.Dark;
            FalseValue = ElementTheme.Light;
        }
    }
}
