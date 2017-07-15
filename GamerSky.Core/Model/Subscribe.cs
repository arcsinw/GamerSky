using Arcsinx.Toolkit.Common;
using Newtonsoft.Json;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 订阅热点词返回值
    /// </summary>
    public class Subscribe : ModelBase
    {
        [JsonProperty(PropertyName = "cnt")]
        public string ReadingCount { get; set; }

        [JsonProperty(PropertyName = "isHot")]
        public string IsHot { get; set; }

        [JsonProperty(PropertyName = "sourceId")]
        public string SourceId { get; set; }

        [JsonProperty(PropertyName = "sourceName")]
        public string SourceName { get; set; }

        [JsonProperty(PropertyName = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }


        private bool isFavorite = false;
        /// <summary>
        /// 是否收藏 本地数据
        /// </summary>
        [JsonProperty(PropertyName = "Favorite")]
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
