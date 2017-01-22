using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 订阅栏目内的内容
    /// </summary>
    public class SubscribeContent
    {
        [JsonProperty(PropertyName = "commentCount")]
        public int CommentCount { get; set; }

        [JsonProperty(PropertyName = "contentDetailUrls")]
        public contentDetailUrls[] ContentDetailUrls { get; set; }

        [JsonProperty(PropertyName = "generalId")]
        public string GeneralId { get; set; }

        [JsonProperty(PropertyName = "origin")]
        public string Origin { get; set; }

        [JsonProperty(PropertyName = "publicTime")]
        public string PublicTime { get; set; }

        [JsonProperty(PropertyName = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
    public class contentDetailUrls
    {
        [JsonProperty(PropertyName = "contentDetailUrl")]
        public string ContentDetailUrl { get; set; }

        [JsonProperty(PropertyName = "pageIndex")]
        public int PageIndex { get; set; }

        [JsonProperty(PropertyName = "subTitle")]
        public string SubTitle { get; set; }
    }
}
