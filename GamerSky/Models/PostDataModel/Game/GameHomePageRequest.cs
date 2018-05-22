using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.PostDataModel
{
    public class GameHomePageRequest
    {
        [JsonProperty("extraField1")]
        public string ExtraField1 { get; set; } = "Position";

        [JsonProperty("extraField2")]
        public string ExtraField2 { get; set; } = "gsScore";

        [JsonProperty("elementsCountPerPage")]
        public int ElementsCountPerPage { get; set; } = 5;

        [JsonProperty("group")]
        public string Group { get; set; } = "recent-hot";

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }
    }
}
