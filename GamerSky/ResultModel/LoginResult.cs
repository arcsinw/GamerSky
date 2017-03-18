using GamerSky.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.ResultModel
{
    public class LoginResult : ResultModelBase
    {
        [JsonProperty(PropertyName = "result")]
        public User Result { get; set; }
    }

 
}
