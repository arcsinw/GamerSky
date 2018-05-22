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
        public DataTemplate NoPictureDT { get; set; }
        public DataTemplate OnePictureDT { get; set; }

        public DataTemplate TwoPictureDT { get; set; }

        public DataTemplate ThreePictureDT { get; set; }

        public DataTemplate TopicDT { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is Essay essay)
            {
                if (essay.ContentType.Equals("zhuanti"))
                {
                    return TopicDT;
                }
                if (essay.ThumbnailURLs == null || essay.ThumbnailURLs.Length == 0)
                {
                    return NoPictureDT;
                }
                if (essay.ThumbnailURLs.Length == 1)
                {
                    return OnePictureDT;
                }
                else
                {
                    return ThreePictureDT;
                }
            }

            return NoPictureDT;
        }
    }
}
