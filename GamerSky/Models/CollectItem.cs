using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models
{
    /// <summary>
    /// 用户收藏
    /// </summary>
    public class CollectItem
    {
        [JsonProperty(PropertyName = "commentsCount")]
        public string CommentsCount { get; set; }

        [JsonProperty(PropertyName = "contenttype")]
        public string ContentType { get; set; }

        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "thumbnailURL")]
        public string ThumbnailURL { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

    }
}
