using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using 游民星空.Core.Helper;
using 游民星空.Core.Model;

namespace 游民星空.Converter
{
    /// <summary>
    /// Count 转 bool
    /// </summary>
    public class CountToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var list = (AlphaKeyGroup < StrategyResult >) value ;
            if (list.InternalList.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
