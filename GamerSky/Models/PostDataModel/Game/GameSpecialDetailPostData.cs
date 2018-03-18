using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.PostDataModel
{
    public class GameSpecialDetailRequest
    {
        [JsonProperty("nodeId")]
        public int NodeId { get; set; }

        [JsonProperty("extraField1")]
        public string ExtraField1 { get; set; }

        [JsonProperty("extraField2")]
        public string ExtraField2 { get; set; }

        [JsonProperty("elementsCountPerPage")]
        public int ElementsCountPerPage { get; set; }

        [JsonProperty("extraField3")]
        public string ExtraField3 { get; set; }

        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }
    }
}
    