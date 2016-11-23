using GamerSky.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.ResultModel
{
    public class AllCommentsResult : ResultModelBase
    {
        [JsonProperty(PropertyName = "result")]
        public AllCommentsResultInner Result { get; set; }
    }

    public class AllCommentsResultInner
    {
        [JsonProperty(PropertyName = "topic_id")]
        public string TopicId { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public List<Comment> Result { get; set; }
    }
}
