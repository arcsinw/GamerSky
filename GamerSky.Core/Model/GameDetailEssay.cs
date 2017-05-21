using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// GameDetail 页面中的测试 新闻
    /// </summary>
    public class GameDetailEssay
    {
        [JsonProperty(PropertyName = "adId")]
        public string AdId { get; set; }

        [JsonProperty(PropertyName = "contentId")]
        public string ContentId { get; set; }

        [JsonProperty(PropertyName = "contentType")]
        public string ContentType { get; set; }

        [JsonProperty(PropertyName = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
