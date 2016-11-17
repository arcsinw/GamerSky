using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GamerSky.Core.Model;

namespace GamerSky.Helper
{
    /// <summary>
    /// 文章列表ListView的ItemTemplate选择器
    /// </summary>
    public class EssayListViewItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OnePicDt { get; set; }
        public DataTemplate ThreePicDt { get; set; }
        public DataTemplate NoPicDt { get; set; }
        public DataTemplate TopicDt { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            Essay essay = item as Essay;
            if(essay==null)
            {
                return null;
            }
            if (essay.ContentType.Equals("zhuanti"))
            {
                return TopicDt;
            }
            if (essay.ThumbnailURLs == null || essay.ThumbnailURLs.Length == 0)
            {
                return NoPicDt;
            }
            if (essay.ThumbnailURLs.Length == 1)
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
