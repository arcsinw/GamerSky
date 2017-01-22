using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;

namespace GamerSky.Core.Helper
{
    public class ImageDownLoadHelper
    {

        public static StorageFolder localFolder = ApplicationData.Current.LocalFolder;

        /// <summary>
        /// 通过uri下载图片
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<SoftwareBitmap> DownLoadImageByUrl(string uri)
        {
            try
            {
                HttpClient hc = new HttpClient();
                HttpResponseMessage resp = await hc.GetAsync(new Uri(uri));
                resp.EnsureSuccessStatusCode();
                IInputStream inputStream = await resp.Content.ReadAsInputStreamAsync();
                IRandomAccessStream memStream = new InMemoryRandomAccessStream();
                await RandomAccessStream.CopyAsync(inputStream, memStream);
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(memStream);
         
                SoftwareBitmap softBmp = await decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                string fileName = uri;
                //await WriteToFile(softwareBitmap,)
                Debug.WriteLine($"Image Download Success !!! {fileName}");
                return softBmp;
            }
            catch (Exception e)
            {
                Debug.WriteLine("ImageDownLoadHelper DownLoadImageByUrl:" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// 将Bitmap写入存储区
        /// </summary>
        /// <param name="softwareBitmap"></param>
        /// <returns></returns>
        public static async Task<string>WriteToFile(SoftwareBitmap softwareBitmap)
        {
            string fileName = Path.GetRandomFileName();

            if(softwareBitmap!= null)
            {
                //save image file to cache
                StorageFile file = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                    encoder.SetSoftwareBitmap(softwareBitmap);
                    await encoder.FlushAsync();
                }
            }
            return fileName;
        }

        /// <summary>
        /// 从LocalFolder文件中读出图片
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<SoftwareBitmap> ReadFromFile(string fileName)
        {
            StorageFile file = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                return softwareBitmap;
            }
        }

        /// <summary>
        /// 从ApplicationUri中读图片
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static async Task<SoftwareBitmap> ReadFromApplicationUri(string uri)
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(uri));
            //StorageFile file = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                return softwareBitmap;
            }
        }


    }
}
