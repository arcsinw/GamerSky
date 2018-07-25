using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models.ResultDataModel
{
    public class UserComments
    {
        [JsonProperty(PropertyName = "comments")]
        public List<Comment> Comments { get; set; }
        
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }
    }
}
