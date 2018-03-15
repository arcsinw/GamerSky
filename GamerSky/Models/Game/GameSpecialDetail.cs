using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models
{
    public class GameSpecialDetail
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("subgroup")]
        public string Subgroup { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("DefaultPicUrl")]
        public string DefaultPicUrl { get; set; }

        [JsonProperty("Position")]
        public string Position { get; set; }

        [JsonProperty("gsScore")]
        public string GsScore { get; set; }

        [JsonProperty("largeImage")]
        public string LargeImage { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "wantplayCount")]
        public string WantPlayCount { get; set; }

    }
}

