using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models
{
    public class GameReview
    {
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "create_time")]
        public string CreateTime { get; set; }

        [JsonProperty(PropertyName = "DefaultPicUrl")]
        public string DefaultPicUrl { get; set; }

        [JsonProperty(PropertyName = "gameTag")]
        public List<string> GameTags { get; set; }

        [JsonProperty(PropertyName = "GameTyp")]
        public string GameType { get; set; }

        [JsonProperty(PropertyName = "gsScore")]
        public string Score { get; set; }

        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "img_URL")]
        public string ImgUrl { get; set; }

        [JsonProperty(PropertyName = "like")]
        public int Like { get; set; }

        [JsonProperty(PropertyName = "likeType")]
        public string LikeType { get; set; }

        [JsonProperty(PropertyName = "modify_time")]
        public string ModifyTime { get; set; }

        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }

        [JsonProperty(PropertyName = "platform")]
        public string PlatForm { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public float Rating { get; set; }

        [JsonProperty(PropertyName = "reviewCount")]
        public int ReviewCount { get; set; }

        [JsonProperty(PropertyName = "reviewid")]
        public string ReviewId { get; set; }

        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "userGroupId")]
        public string UserGroupId { get; set; }

        [JsonProperty(PropertyName = "userLevel")]
        public string UserLevel { get; set; }

        [JsonProperty(PropertyName = "wantplayCount")]
        public string WantPlayCount { get; set; }

    }
}
