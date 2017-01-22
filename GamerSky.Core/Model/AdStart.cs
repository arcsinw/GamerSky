using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 启动图
    /// </summary>
    public class AdStart
    {
        [JsonProperty(PropertyName = "adId")]
        public string AdId { get; set; }

        [JsonProperty(PropertyName = "articleId")]
        public string ArticleId { get; set; }

        [JsonProperty(PropertyName = "coordinates")]
        public string Coordinates { get; set; }

        [JsonProperty(PropertyName = "delayTimeInSeconds")]
        public string DelayTimeInSeconds { get; set; }

        [JsonProperty(PropertyName = "effecttime")]
        public string Effecttime { get; set; }

        [JsonProperty(PropertyName = "failuretime")]
        public string Failuretime { get; set; }

        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }

        [JsonProperty(PropertyName = "linktype")]
        public string Linktype { get; set; }

        [JsonProperty(PropertyName = "picAdress")]
        public string  PicAdress { get; set; }
    }
}
