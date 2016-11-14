using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 文章实体
    /// </summary>
    public class News
    {
        [JsonProperty(PropertyName = "adId")]
        public int AdId { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// main body
        /// </summary>
        [JsonProperty(PropertyName = "mainBody")]
        public string MainBody { get; set; }

        [JsonProperty(PropertyName = "node")]
        public string Node { get; set; }

        /// <summary>
        /// origin uri
        /// </summary>
        [JsonProperty(PropertyName = "originURL")]
        public string OriginURL { get; set; }

        [JsonProperty(PropertyName = "pageCount")]
        public int PageCount { get; set; }

        [JsonProperty(PropertyName = "pageIndexNames")]
        public string[] PageIndexNames { get; set; }

        [JsonProperty(PropertyName = "subscribes")]
        public Subscriber[] Subscribes { get; set; }

        [JsonProperty(PropertyName = "subTitle")]
        public string SubTitle { get; set; }

        [JsonProperty(PropertyName = "templateURL")]
        public string TemplateURL { get; set; }

        [JsonProperty(PropertyName = "templateVersion")]
        public string TemplateVersion { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "videoContent")]
        public string VideoContent { get; set; }
    }

    public struct Subscriber
    {
        public string subscribeld;
    }
}
