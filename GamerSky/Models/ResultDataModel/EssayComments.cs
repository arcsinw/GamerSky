using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models.ResultDataModel
{
    public class EssayComments
    {
        [JsonProperty(PropertyName = "topic_id")]
        public string TopicId { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public List<Comment> Result { get; set; }
    }
}
