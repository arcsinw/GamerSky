using GamerSky.Core.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.ResultModel
{
    public class GetAllReplyResult : ResultModelBase
    {

        [JsonProperty(PropertyName = "result")]
        public GetAllReplyInnerResult Result { get; set; }
         

    }

    public class GetAllReplyInnerResult
    {
        [JsonProperty(PropertyName = "comments")]
        public List<Comment> Comments { get; set; }


        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }
    }
}
