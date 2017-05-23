using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using GamerSky.Core.Model;

namespace GamerSky.Converter
{
    /// <summary>
    /// 将订阅状态转换为Button的Style
    /// </summary>
    public class BoolToStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
       
            bool favorite = (bool)value;
            if(favorite)  //已收藏
            {
                return App.Current.Resources["UnFavoriteButtonStyle"] as Style;
            }
            else
            {
                return App.Current.Resources["FavoriteButtonStyle"] as Style;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
