using GamerSky.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace GamerSky.Models
{
    /// <summary>
    /// 游戏库中的游戏
    /// </summary>
    public class Game : ModelBase
    {
        [JsonProperty(PropertyName = "contentId")]
        public string ContentId { get; set; }

        [JsonProperty(PropertyName = "developer")]
        public string Developer { get; set; }

        [JsonProperty(PropertyName = "gameType")]
        public string GameType { get; set; }

        [JsonProperty(PropertyName = "platform")]
        public string Platform { get; set; }

        [JsonProperty(PropertyName = "sellTime")]
        public string SellTime { get; set; }

        [JsonProperty(PropertyName = "thumbnailURL")]
        public string ThumbnailURL { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }


        /// <summary>
        /// 是否收藏 本地数据
        /// </summary>
        public bool IsFavorite { get; set; } = false;

        private DelegateCommand _toggleFavorite = default(DelegateCommand);

        public DelegateCommand ToggleFavorite => _toggleFavorite ?? (_toggleFavorite = new DelegateCommand(ExecuteToggleFavoriteCommand, CanExecuteToggleFavoriteCommand));

        private bool CanExecuteToggleFavoriteCommand()
        {
            return true;
        }

        private void ExecuteToggleFavoriteCommand()
        {
            IsFavorite = !IsFavorite;
        }


        #region Image cache
        WeakReference bitmapImage;

        public ImageSource ImageSource
        {
            get
            {
                if (bitmapImage != null)
                {
                    if (bitmapImage.IsAlive)
                    {
                        return (ImageSource)bitmapImage.Target;
                    }
                    else
                    {
                        Debug.WriteLine("Image was destroyed");
                    }
                }

                if (!string.IsNullOrEmpty(ThumbnailURL))
                {
                    Task.Factory.StartNew(() => { DownloadImage(new Uri(ThumbnailURL)); });
                }

                return null;
            }
        }


        async void DownloadImage(Uri uri)
        {
            List<Byte> bytes = new List<byte>();
            Stream streamForUI;
            using (var response = await HttpWebRequest.Create(uri).GetResponseAsync())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    byte[] buffer = new byte[4000];
                    int byteRead = 0;
                    while ((byteRead = await responseStream.ReadAsync(buffer,0,4000)) > 0)
                    {
                        bytes.AddRange(buffer.Take(byteRead));
                    }
                }
            }

            streamForUI = new MemoryStream((int)bytes.Count);
            streamForUI.Write(bytes.ToArray(), 0, bytes.Count);
            streamForUI.Seek(0, SeekOrigin.Begin);
            
            await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                BitmapImage bm = new BitmapImage();
                bm.SetSource(streamForUI.AsRandomAccessStream());

                if (bitmapImage == null)
                {
                    bitmapImage = new WeakReference(bm);
                }
                else
                {
                    bitmapImage.Target = bm;
                }

                OnPropertyChanged("ImageSource");
            });

        }
        #endregion
    }
}
