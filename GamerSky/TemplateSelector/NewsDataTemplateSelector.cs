using GamerSky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GamerSky.TemplateSelector
{
    public class NewsDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NoPicture { get; set; }
        public DataTemplate OnePicture { get; set; }

        public DataTemplate TwoPicture { get; set; }

        public DataTemplate ThreePicture { get; set; }

        public DataTemplate Topic { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is Essay essay)
            {
                if (essay.ContentType.Equals("zhuanti"))
                {
                    return Topic;
                }
                if (essay.ThumbnailURLs == null || essay.ThumbnailURLs.Length == 0)
                {
                    return NoPicture;
                }
                if (essay.ThumbnailURLs.Length == 1)
                {
                    return OnePicture;
                }
                else
                {
                    return ThreePicture;
                }
            }

            return NoPicture;
        }
    }
}
