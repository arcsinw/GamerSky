using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models
{
    public class SteamVideo
    {
        [JsonProperty(PropertyName = "SteamId")]
        public string SteamId { get; set; }

        [JsonProperty(PropertyName = "MoviesId")]
        public string MoviesId { get; set; }

        [JsonProperty(PropertyName = "MoviesName")]
        public string MoviesName { get; set; }

        [JsonProperty(PropertyName = "MoviesThumbnail")]
        public string MoviesThumbnail { get; set; }

        [JsonProperty(PropertyName = "MoviesWebm480")]
        public string MoviesWebm480 { get; set; }

        [JsonProperty(PropertyName = "MoviesWebmMax")]
        public string MoviesWebmMax { get; set; }

        [JsonProperty(PropertyName = "MyMoviesWebm480")]
        public string MyMoviesWebm480 { get; set; }

        [JsonProperty(PropertyName = "MyMoviesWebmMax")]
        public string MyMoviesWebmMax { get; set; }

        [JsonProperty(PropertyName = "MoviesHighlight")]
        public string MoviesHighlight { get; set; }

        [JsonProperty(PropertyName = "CrawlHistoryId")]
        public string CrawlHistoryId { get; set; }

        [JsonProperty(PropertyName = "MyMoviesThumbnail")]
        public string MyMoviesThumbnail { get; set; }
    }
}
