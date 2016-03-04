using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using 游民星空.Core.Helper;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 有攻略的游戏列表
    /// </summary>
    public class Strategy
    {
        public string favoriteCnt { get; set; }
        public int specialID { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string thumbnailUrl { get; set; }

        //private static BitmapImage DefaultBitmapImage = new BitmapImage { UriSource = new Uri("ms-appx:///Assets/image_loading.png") };

        //private BitmapImage thumbnail;
        //public ImageSource Thumbnail
        //{
        //    get
        //    {
        //        if (thumbnail == null)
        //        {
        //            DownloadImage(thumbnailUrl);
        //            return DefaultBitmapImage;
        //        }
        //        else
        //        {
        //            return thumbnail;
        //        }
        //    }
        //}

        //private async void DownloadImage(string url)
        //{
        //    SoftwareBitmap softwareBitmap =  await ImageDownLoadHelper.DownLoadImageByUrl(url);
        //    thumbnail.SetSource(softwareBitmap.)
        //}
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
    }
    
}
