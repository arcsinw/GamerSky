using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models
{
    public class GameSubList
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "des")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "image")]
        public string Image { get; set; }

        [JsonProperty(PropertyName = "nodeId")]
        public string NodeId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
