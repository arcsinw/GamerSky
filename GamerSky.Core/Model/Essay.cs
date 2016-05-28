﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;
using GamerSky.Core.Helper;
using GamerSky.Core.Http;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 新闻
    /// </summary>
    public class Essay : ModelBase
    {
        public string adId { get; set; }
        public string authorHeadImageURL { get; set; }
        public string authorName { get; set; }
        public string[] badges { get; set; }

        public Essay[] childElements { get; set; }
        
        public string commentsCount { get; set; }
        public string contentId { get; set; }
        public string contentType { get; set; }
        public string contentURL { get; set; }
        public string readingCount { get; set; }

        private string[] urls = { "ms-appx:///Assets/image_loading.png", "ms-appx:///Assets/image_loading.png", "ms-appx:///Assets/image_loading.png" };
        /// <summary> 
        /// 缩略图
        /// </summary>
        public string[] thumbnailURLs
        {
            get
            {
                return urls;
            }
            set
            {
                urls = value;
                OnPropertyChanged();
            }
        }

        private static BitmapImage defaultBitmap = new BitmapImage { UriSource = new Uri("ms-appx:///Assets/image_loading.png") };
        public static BitmapImage DefaultBitmap
        {
            get
            {
                return defaultBitmap;
            }
        } 

        //private ImageSource[] thumbnail;
        //public ImageSource[] Thumbnail
        //{
        //    get
        //    {
        //        if(thumbnail== null)
        //        {
        //            return defaultBitmap;
        //        }
        //        else
        //        {
        //            return thumbnail;
        //        }
        //    }
        //}

        //private SoftwareBitmapSource[] thumbnails;
        //public SoftwareBitmapSource[] Thumbnails
        //{
        //    get
        //    {
        //        if (thumbnailURLs != null)
        //        {
        //            SetBitmap();
        //            if (thumbnailURLs.Length == 1)
        //            {
        //                return new SoftwareBitmapSource[1] { defaultBitmapSource };
        //            }
        //            else if (thumbnailURLs.Length == 3)
        //            {
        //                return new SoftwareBitmapSource[3] { defaultBitmapSource, defaultBitmapSource, defaultBitmapSource };
        //            }
        //        }
        //        return new SoftwareBitmapSource[1] { defaultBitmapSource };
        //    }
        //    set
        //    {
        //        SetBitmap();
        //    }
        //}

        //private async void SetBitmap()
        //{
        //    if (thumbnailURLs != null)
        //    {
        //        if (thumbnailURLs.Length == 1)
        //        {
        //            thumbnails = new SoftwareBitmapSource[1];
        //        }
        //        else if (thumbnailURLs.Length == 3)
        //        {
        //            thumbnails = new SoftwareBitmapSource[3];
        //        }
        //        for (int i = 0; i < thumbnailURLs.Length; i++)
        //        {

        //            var softwareBitmap = await ImageDownLoadHelper.DownLoadImageByUri(thumbnailURLs[i]);
        //            thumbnails[i] = new SoftwareBitmapSource();
        //            await thumbnails[i].SetBitmapAsync(softwareBitmap);
        //            OnPropertyChanged("Thumbnails");
        //        }
        //    }
        //}
        //#endregion
        private string _title;
        /// <summary>
        /// 标题
        /// </summary>
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// huandeng 则为幻灯片内容
        /// </summary>
        public string type { get; set; }

        private bool isFavorite = false;
        /// <summary>
        /// 是否收藏 程序内添加数据
        /// </summary>
        public bool IsFavorite
        {
            get
            {
                return isFavorite;
            }
            set
            {
                isFavorite = value;
                OnPropertyChanged();
            }
        }
    }
}
