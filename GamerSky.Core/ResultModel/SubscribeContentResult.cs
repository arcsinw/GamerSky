using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSky.Core.Model;
using Newtonsoft.Json;

namespace GamerSky.Core.ResultModel
{
    public class SubscribeContentResult : ResultModelBase
    {
        [JsonProperty(PropertyName = "result")]
        public SubscribeContent[] Result { get; set; }

    }
}
