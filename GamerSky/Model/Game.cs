using GamerSky.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Model
{
    /// <summary>
    /// 游戏库中的游戏
    /// </summary>
    public class Game
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
    }
}
