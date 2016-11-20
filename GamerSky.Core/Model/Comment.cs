using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    public class Comment
    {
        [JsonProperty(PropertyName="comment_id")]
        public string CommentId { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public List<Comment> Comments { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "create_time")]
        [JsonConverter(typeof(DateTime))]
        public DateTime CreateTime { get; set; }

        [JsonProperty(PropertyName = "from")]
        public string From { get; set; }

        [JsonProperty(PropertyName = "img_url")]
        public string ImgUrl { get; set; }

        [JsonProperty(PropertyName = "ip_location")]
        public string IpLocation { get; set; }

        [JsonProperty(PropertyName = "nickname")]
        public string NickName { get; set; }

        [JsonProperty(PropertyName = "support_count")]
        public string SupportCount { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }
    }
}
