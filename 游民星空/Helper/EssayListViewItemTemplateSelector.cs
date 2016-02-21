using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using 游民星空.Core.Model;

namespace 游民星空.Helper
{
    /// <summary>
    /// 文章列表ListView的ItemTemplate选择器
    /// </summary>
    public class EssayListViewItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OnePicDt { get; set; }
        public DataTemplate ThreePicDt { get; set; }

        public DataTemplate NoPicDt { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            Essay essay = item as Essay;
            if (essay.thumbnailURLs == null || essay.thumbnailURLs.Length==0) return NoPicDt;
            if (essay.thumbnailURLs.Length == 1)
            {
                return OnePicDt;
            }
            else
            {
                return ThreePicDt;
            }
        }
    }
}
