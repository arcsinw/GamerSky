using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.ResultModel
{
    public class LikeCommentResult : ResultModelBase
    { 
        [JsonProperty(PropertyName = "result")]
        public string Result { get; set; } 
    }
}
