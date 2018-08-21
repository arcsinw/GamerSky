using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models
{
    /// <summary>
    /// allChannel 返回的频道信息
    /// </summary>
    public class Channel
    {
        [JsonProperty(PropertyName = "isTop")]
        public string IsTop { get; set; }

        [JsonProperty(PropertyName = "nodeId")]
        public string NodeId { get; set; }

        /// <summary>
        /// 栏目名
        /// </summary>
        [JsonProperty(PropertyName = "nodeName")]
        public string NodeName { get; set; }
    }
}
