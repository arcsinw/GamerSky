using System;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;
using 游民星空.Core.Helper;
using 游民星空.Core.Http;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 新闻列表
    /// </summary>
    public class Essay
    {
        public int errorCode;
        public string errorMessage;
        public List<EssayResult> result;
    }
    public class EssayResult : ModelBase
    {
        public string adId { get; set; }
        public string authorHeadImageURL { get; set; }
        public string authorName { get; set; }
        public string[] badges { get; set; }
        public EssayResult[] childElements { get; set; }
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
        //private static BitmapImage defaultBitmap = new BitmapImage { UriSource = new Uri("ms-appx:///Assets/image_loading.png") };

        //private List<BitmapImage> thumbnailBitmap = new List<BitmapImage>();
        ///// <summary>
        ///// 新闻缩略图  
        ///// 先返回默认图的BitmapImage 图片下载完成后赋值 OnPropertyChanged()
        ///// </summary>
        //public List<BitmapImage> ThumbnailBitmap
        //{
        //    get
        //    {
        //        if (thumbnailBitmap.Count == 0 && thumbnailURLs!=null)
        //        {
        //            for (int i = 0; i < thumbnailURLs.Length; i++)
        //            {
        //                thumbnailBitmap.Add(defaultBitmap);
        //            }
        //        }
        //        for (int i = 0; i < thumbnailURLs.Length; i++)
        //        {
        //            DownloadImage(thumbnailURLs[i]);

        //        }

        //        return thumbnailBitmap;
        //    }
        //    set
        //    {
        //        thumbnailBitmap = value;
        //    }

        //}

        //private async void DownloadImage(string url)
        //{
        //    try
        //    {
        //        HttpClient hc = new HttpClient();
        //        HttpResponseMessage resp = await hc.GetAsync(new Uri(url));
        //        resp.EnsureSuccessStatusCode();
        //        IInputStream inputStream = await resp.Content.ReadAsInputStreamAsync();
        //        IRandomAccessStream memStream = new InMemoryRandomAccessStream();
        //        await RandomAccessStream.CopyAsync(inputStream, memStream);
        //        BitmapImage bitmap = new BitmapImage();
        //        await bitmap.SetSourceAsync(memStream);
        //        await DispatcherManager.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //        {
        //            thumbnailBitmap.Add(bitmap);
        //                //触发UI绑定属性的改变
        //                OnPropertyChanged("ThumbnailBitmap");
        //        });

        //        //BitmapDecoder decoder = await BitmapDecoder.CreateAsync(memStream);
        //        //SoftwareBitmap softBmp = await decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
        //        //return softBmp;
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        /// <summary>
        /// download through image's url
        /// </summary>
        /// <param name="url"></param>
        //private async void DownloadImage(string uri)
        //{
        //    BitmapImage bitmap = new BitmapImage();

        //    HttpResponseMessage resposne = await new HttpClient().GetAsync(new Uri(uri));

        //    IInputStream inputStream = await resposne.Content.ReadAsInputStreamAsync();
        //    IRandomAccessStream randomStream = new InMemoryRandomAccessStream();
        //    await RandomAccessStream.CopyAsync(inputStream, randomStream);

        //    await bitmap.SetSourceAsync(randomStream);

        //    IBuffer buffer = await HttpBaseService.SendGetRequestAsBytes(uri);
        //    byte[] bytes = buffer.ToArray();



        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        ms.Write(bytes, 0, bytes.Length);

        //        IRandomAccessStream randomStreams = new InMemoryRandomAccessStream();
        //        await RandomAccessStream.CopyAsync(inputStream, randomStreams);

        //        await bitmap.SetSourceAsync(ms.AsRandomAccessStream());

        //    }

        //    await DispatcherManager.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
        //    {
        //        thumbnailBitmap.Add(bitmap);
        //        //触发UI绑定属性的改变
        //        OnPropertyChanged("ThumbnailBitmap");
        //    });
        //    //HttpWebRequest request = WebRequest.CreateHttp(new Uri(uri));
        //    //request.BeginGetResponse(DownloadImageComplete, request);
        //}

        ///// <summary>
        ///// image download callback
        ///// </summary>
        ///// <param name="result"></param>
        //private async void DownloadImageComplete(IAsyncResult result)
        //{
        //    HttpWebRequest request = result.AsyncState as HttpWebRequest;
        //    HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);
        //    // 读取网络的数据
        //    using (Stream stream = response.GetResponseStream())
        //    {
        //        int length = int.Parse(response.Headers["Content-Length"]);
        //        // 注意需要把数据流重新复制一份，否则会出现跨线程错误
        //        // 网络下载到的图片数据流，属于后台线程的对象，不能在UI上使用
        //        using (Stream streamForUI = new MemoryStream(length))
        //        {
        //            byte[] buffer = new byte[length];
        //            int read = 0;
        //            do
        //            {
        //                read = stream.Read(buffer, 0, length);
        //                streamForUI.Write(buffer, 0, read);
        //            }
        //            while (read == length);
        //            streamForUI.Seek(0, SeekOrigin.Begin);
        //            // 触发UI线程处理位图和UI更新
        //            //var frame = Window.Current.Content as Frame;

        //            await DispatcherManager.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,() =>
        //            {
        //                BitmapImage bm = new BitmapImage();
        //                bm.SetSource(streamForUI.AsRandomAccessStream());
        //                thumbnailBitmap.Add(bm);
        //                //触发UI绑定属性的改变
        //                OnPropertyChanged("ThumbnailBitmap");
        //            });
        //        }
        //    }
        //}
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
    }
}
