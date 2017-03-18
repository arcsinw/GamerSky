using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.ResultModel
{
    public class AddCommentResult : ResultModelBase
    {
        [JsonProperty(PropertyName = "result")]
        public CommentResultData Result { get; set; }
    }
    public class CommentResultData
    {
        [JsonProperty(PropertyName = "commentId")]
        public string CommentId { get; set; }
    }

}
