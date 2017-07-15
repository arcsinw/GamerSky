using Arcsinx.Toolkit.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace GamerSky.Core.Helper
{
    public class FileHelper
    {
        private static FileHelper _current;
        public static FileHelper Current
        {
            get
            {
                if(_current== null)
                {
                    _current = new FileHelper();
                }
                return _current;
            }
        }

        private StorageFolder localFolder;

        public FileHelper()
        {
            localFolder = ApplicationData.Current.LocalFolder;
            Initial();
        }

        private async void Initial()
        {
            await localFolder.CreateFolderAsync("images_cache", CreationCollisionOption.OpenIfExists);
            await localFolder.CreateFolderAsync("data_cache", CreationCollisionOption.OpenIfExists);
            await localFolder.CreateFolderAsync("favorite_list", CreationCollisionOption.OpenIfExists);
            //await localFolder.CreateFileAsync("livetile_cache", CreationCollisionOption.OpenIfExists);
        }

        
         
        /// <summary>
        /// 写缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task WriteObjectAsync<T>(T obj,string filename,string folderName= "data_cache")
        {
            try
            {
                var folder = await localFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
              
                using (var data = await folder.OpenStreamForWriteAsync(filename, CreationCollisionOption.ReplaceExisting))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                    serializer.WriteObject(data, obj);
                }
            }
            catch(Exception e)
            {
                
            }
        }

        /// <summary>
        /// 读缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<T> ReadObjectAsync<T>(string filename,string folderName= "data_cache") where T :class
        {
            try
            {
                var folder = await localFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
                var file = await folder.GetFileAsync(filename);
                string json = await FileIO.ReadTextAsync(file);
                return JsonHelper.Deserlialize<T>(json);
                //using (var data = await folder.OpenStreamForReadAsync(filename))
                //{ 
                //    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                //    return serializer.ReadObject(data) as T;
                //}
            }
            catch (Exception e)
            {
                
                return null;
            }
        }


        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task SaveImageAsync(WriteableBitmap image,string filename)
        {
            try
            {
                if(image== null)
                {
                    return;
                }
                Guid BitmapEncoderGuid = BitmapEncoder.JpegEncoderId;
                if (filename.EndsWith("jpg"))
                    BitmapEncoderGuid = BitmapEncoder.JpegEncoderId;
                else if (filename.EndsWith("png"))
                    BitmapEncoderGuid = BitmapEncoder.PngEncoderId;
                else if (filename.EndsWith("bmp"))
                    BitmapEncoderGuid = BitmapEncoder.BmpEncoderId;
                else if (filename.EndsWith("tiff"))
                    BitmapEncoderGuid = BitmapEncoder.TiffEncoderId;
                else if (filename.EndsWith("gif"))
                    BitmapEncoderGuid = BitmapEncoder.GifEncoderId;

                var folder = await localFolder.CreateFolderAsync("images_cache", CreationCollisionOption.OpenIfExists);
                var file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoderGuid, stream);
                    Stream pixelStream = image.PixelBuffer.AsStream();
                    byte[] pixels = new byte[pixelStream.Length];
                    await pixelStream.ReadAsync(pixels, 0, pixels.Length);
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
                        (uint)image.PixelWidth,
                        (uint)image.PixelHeight,
                        96.0,
                        96.0,
                        pixels);
                    await encoder.FlushAsync();
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// 删除缓存文件
        /// </summary>
        /// <returns></returns>
        public async Task DeleteCacheFile()
        {
            try
            {
                StorageFolder folder = await localFolder.GetFolderAsync("data_cache");

                //StorageFolder folder = await localFolder.GetFolderAsync("images_cache");
                if(folder != null)
                {
                    IReadOnlyCollection<StorageFile> files = await folder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery);

                    List<IAsyncAction> list = new List<IAsyncAction>();
                    foreach (var item in files)
                    {
                        list.Add(item.DeleteAsync(StorageDeleteOption.PermanentDelete));
                    }
                    List<Task> tasks = new List<Task>();
                    list.ForEach((t) => tasks.Add(t.AsTask()));

                    await Task.Run(() => { Task.WaitAll(tasks.ToArray()); });

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("FileHelper DeleteCacheFile:"+e.Message);
            }
        }

        /// <summary>
        /// 获取缓存文件大小
        /// </summary>
        /// <returns></returns>
        public async Task<double> GetCacheSize()
        {
            try
            {
                //StorageFolder folder = await localFolder.GetFolderAsync("images_cache");
                StorageFolder folder = await localFolder.GetFolderAsync("data_cache"); 
                var files = await folder.GetFilesAsync(Windows.Storage.Search.CommonFileQuery.DefaultQuery);
                double size = 0;
                BasicProperties p;
                foreach (var item in files)
                {
                    p = await item.GetBasicPropertiesAsync();
                    size += p.Size;
                }
                return size;
            }
            catch (Exception e)
            {
                Debug.WriteLine("FileHelper GetCacheSize :" + e.Message);
                return 0;
            }
        }

        /// <summary>
        /// 缓存是否存在
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<bool> IsCacheExist(string filename)
        {
            try
            {
                IsolatedStorageFile.GetUserStoreForApplication().FileExists(localFolder.Path + "");
                var folder = await localFolder.TryGetItemAsync("images_cache");
                Debug.WriteLine(folder.Path);
                if(folder!= null)
                {
                    var file = await (folder as StorageFolder).TryGetItemAsync(filename);
                    return (file== null);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        
    }
}
