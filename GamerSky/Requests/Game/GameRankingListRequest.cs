using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Requests
{
    public class GameRankingListRequest
    {
        [JsonProperty(PropertyName = "annualClass")]
        public string AnnualClass { get; set; }

        [JsonProperty("extraField1")]
        public string ExtraField1 { get; set; } = "Position";

        [JsonProperty("extraField2")]
        public string ExtraField2 { get; set; } = "gsScore";

        [JsonProperty("elementsCountPerPage")]
        public int ElementsCountPerPage { get; set; } = 5;

        [JsonProperty("gameClass")]
        public int GameClass { get; set; } = 0;

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }

        /// <summary>
        /// hot 最期待游戏
        /// fractions 高分榜
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; } 
    }
}
