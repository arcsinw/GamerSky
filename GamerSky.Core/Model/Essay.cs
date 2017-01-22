using GamerSky.Core.Common;
using Newtonsoft.Json;
using System;
using Windows.UI.Xaml.Media.Imaging;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 新闻
    /// </summary> 
    public class Essay : ModelBase
    {
        [JsonProperty(PropertyName = "adId")]
        public string AdId { get; set; }

        [JsonProperty(PropertyName = "authorHeadImageURL")]
        public string AuthorHeadImageURL { get; set; }

        [JsonProperty(PropertyName = "authorName")]
        public string AuthorName { get; set; }

        [JsonProperty(PropertyName = "badges")]
        public string[] Badges { get; set; }

        [JsonProperty(PropertyName = "childElements")]
        public Essay[] ChildElements { get; set; }

        [JsonProperty(PropertyName = "CommentsCount")]
        public string CommentsCount { get; set; }

        [JsonProperty(PropertyName = "contentId")]
        public string ContentId { get; set; }

        [JsonProperty(PropertyName = "contentType")]
        public string ContentType { get; set; }

        [JsonProperty(PropertyName = "contentURL")]
        public string ContentURL { get; set; }

        [JsonProperty(PropertyName = "readingCount")]
        public string ReadingCount { get; set; }

        private string[] urls = { "ms-appx:///Assets/image_loading.png", "ms-appx:///Assets/image_loading.png", "ms-appx:///Assets/image_loading.png" };

        /// <summary> 
        /// 缩略图
        /// </summary>
        [JsonProperty(PropertyName = "thumbnailURLs")]
        public string[] ThumbnailURLs
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

        private string title;
        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// huandeng 则为幻灯片内容
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        private bool isFavorite = false;
        /// <summary>
        /// 是否收藏 本地数据
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

    }
}
