using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    public class GameDetail
    {
        /// <summary>
        /// 游戏详情页背景图
        /// </summary>
        [JsonProperty(PropertyName = "backgroundURL")]
        public string BackgroundURL { get; set; }

        /// <summary>
        /// 开发商
        /// </summary>
        [JsonProperty(PropertyName = "developer")]
        public string Developer { get; set; }

        [JsonProperty(PropertyName = "englishTitle")]
        public string EnglishTitle { get; set; }

        [JsonProperty(PropertyName = "gameType")]
        public string GameType { get; set; }

        [JsonProperty(PropertyName = "newsNumber")]
        public string NewsNumber { get; set; }

        [JsonProperty(PropertyName = "platform")]
        public string Platform { get; set; }

        [JsonProperty(PropertyName = "sellTime")]
        public string SellTime { get; set; }

        [JsonProperty(PropertyName = "strategyNumber")]
        public string StrategyNumber { get; set; }

        [JsonProperty(PropertyName = "thumbnailURL")]
        public string ThumbnailURL { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
