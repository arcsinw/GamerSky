using GamerSky.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models
{
    public class GameDetailV4
    {
        [JsonProperty(PropertyName = "Activity")]
        public string Activity { get; set; }

        [JsonProperty(PropertyName = "AllTime")]
        public string AllTime { get; set; }

        [JsonProperty(PropertyName = "AllTimeT")]
        public string AllTimeT { get; set; }

        [JsonProperty(PropertyName = "ClubId")]
        public string ClubId { get; set; }

        [JsonProperty(PropertyName = "defaultPicUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "DeputyNodeId")]
        public string DeputyNodeId { get; set; }

        [JsonProperty(PropertyName = "EnTitle")]
        public string EnglishTitle { get; set; }
        
        [JsonProperty(PropertyName = "expectCount")]
        public string ExpectCount { get; set; }

        [JsonProperty(PropertyName = "GameAuthor")]
        public string GameAuthor { get; set; }

        [JsonProperty(PropertyName = "GameDir")]
        public string GameDir { get; set; }

        [JsonProperty(PropertyName = "gameTag")]
        public List<string> GameTags { get; set; }
        
        [JsonProperty(PropertyName = "GameType")]
        public string GameType { get; set; }

        [JsonProperty(PropertyName = "gsScore")]
        public string GsScore { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string GameId { get; set; }

        [JsonProperty(PropertyName = "Intro")]
        public string Intro { get; set; }

        [JsonProperty(PropertyName = "IsFree")]
        public bool IsFree { get; set; }

        [JsonProperty(PropertyName = "isMarket")]
        public string IsMarket { get; set; }

        [JsonProperty(PropertyName = "OfficialChinese")]
        public string OfficialChinese { get; set; }

        [JsonProperty(PropertyName = "Online")]
        public string Online { get; set; }

        [JsonProperty(PropertyName = "PCTime")]
        public string PCTime { get; set; }

        [JsonProperty(PropertyName = "PCTimeT")]
        public string PCTimeT { get; set; }

        [JsonProperty(PropertyName = "Peizhi")]
        public string Peizhi { get; set; }

        /// <summary>
        /// 玩过
        /// </summary>
        [JsonProperty(PropertyName = "playCount")]
        public string PlayCount { get; set; }

        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "SteamPrice")]
        public string SteamPrice { get; set; }

        [JsonProperty(PropertyName = "SteamInitial")]
        public string SteamInitial { get; set; }

        [JsonProperty(PropertyName = "SteamFinal")]
        public string SteamFinal { get; set; }

        [JsonProperty(PropertyName = "SteamVideos")]
        public string SteamVideosJson { get; set; }

        [JsonIgnore]
        public List<SteamVideo> SteamVideos
        {
            get
            {
                if (!string.IsNullOrEmpty(SteamVideosJson))
                {
                    return JsonHelper.Deserlialize<List<SteamVideo>>(SteamVideosJson);
                }
                else
                {
                    return new List<SteamVideo>();
                }
            }
        }

        [JsonProperty(PropertyName = "SteamImages")]
        public string SteamImages { get; set; }

        /// <summary>
        /// 想玩
        /// </summary>
        [JsonProperty(PropertyName = "wantplayCount")]
        public string WantPlayCount { get; set; }
    }
}
