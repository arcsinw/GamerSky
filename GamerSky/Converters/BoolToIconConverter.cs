﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace GamerSky.Converters
{
    public class BoolToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        { 
            if ((bool)value)
            {
                //return new Uri("ms-appx:///Assets/Images/Favorited.png");
                //return "&#xE1CF;";
                return "M10.0006294250488,0L13.0900001525879,6.58374786376953 20,7.63938903808594 14.9993705749512,12.7637481689453 16.1806297302246,20 10.0006294250488,16.5837478637695 3.81937026977539,20 5,12.7637481689453 0,7.63938903808594 6.91062927246094,6.58374786376953 10.0006294250488,0z";
            }
            else
            {
                //return new Uri("ms-appx:///Assets/Images/UnFavorited.png");
                //return "&#xE1CE;";
                return "M9.99937057495117,3.54877471923828L7.92562484741211,7.96440124511719 3.28937530517578,8.67313385009766 6.64499282836914,12.1112823486328 5.85249900817871,16.9668960571289 9.99937057495117,14.6750259399414 14.1468715667725,16.9668960571289 13.3537483215332,12.1112823486328 16.7093753814697,8.67313385009766 12.0731258392334,7.96440124511719 9.99937057495117,3.54877471923828z M9.99937057495117,0L13.0893707275391,6.58374786376953 20,7.63938903808594 14.9993705749512,12.7637481689453 16.179370880127,20 9.99937057495117,16.5837478637695 3.81937026977539,20 4.99937057495117,12.7637481689453 0,7.63938903808594 6.90937042236328,6.58374786376953 9.99937057495117,0z";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
