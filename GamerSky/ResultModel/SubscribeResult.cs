using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSky.Model;
using Newtonsoft.Json;

namespace GamerSky.ResultModel
{
    public class SubscribeResult : ResultModelBase
    {
        [JsonProperty(PropertyName = "result")]
        public List<Subscribe> Result { get; set; }
    }
}
