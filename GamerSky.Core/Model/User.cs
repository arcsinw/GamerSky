using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    public class User
    {
        [JsonProperty(PropertyName = "loginToken")]
        public string LoginToken { get; set; }

        [JsonProperty(PropertyName = "pic")]
        public string Pic { get; set; } = "ms-appx:///Assets/Images/drawer_avatar.png";

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }
    }
}
