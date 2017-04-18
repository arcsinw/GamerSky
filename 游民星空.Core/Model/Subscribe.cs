using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 订阅热点词返回数据类型
    /// </summary>
    public class Subscribe : ModelBase
    {
        public string cnt { get; set; }
        public string isHot { get; set; }
        public string sourceId { get; set; }
        public string sourceName { get; set; }
        public string thumbnailUrl { get; set; }

        private static BitmapImage defaultBitmap = new BitmapImage { UriSource = new Uri("ms-appx:///Assets/image_loading.png") };
        public static BitmapImage DefaultBitmap
        {
            get
            {
                return defaultBitmap;
            }
        }

        private bool favorite = false;
        /// <summary>
        /// 是否收藏 本地数据
        /// </summary>
        public bool Favorite
        {
            get
            {
                return favorite;
            }
            set
            {
                favorite = value;
                OnPropertyChanged();
            }
        }
    }
}
