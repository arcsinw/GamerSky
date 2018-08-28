using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models.Me
{
    /// <summary>
    /// 用户信息 GetUserInfo获取 
    /// </summary>
    public class UserInfo
    {
        [JsonProperty(PropertyName = "achievementsCount")]
        public string AchievementsCount { get; set; }

        [JsonProperty(PropertyName = "experience")]
        public string Experience { get; set; }

        [JsonProperty(PropertyName = "fansCount")]
        public string FansCount { get; set; }

        [JsonProperty(PropertyName = "followsCount")]
        public string FollowsCount { get; set; }

        [JsonProperty(PropertyName = "level")]
        public string Level { get; set; }

        [JsonProperty(PropertyName = "playStationAccount")]
        public string PlayStationAccount { get; set; }

        [JsonProperty(PropertyName = "steamAccount")]
        public string SteamAccount { get; set; }

        [JsonProperty(PropertyName = "userAuthentication")]
        public string UserAuthentication { get; set; }

        [JsonProperty(PropertyName = "userGroupId")]
        public string UserGroupId { get; set; }

        [JsonProperty(PropertyName = "userHeadImageURL")]
        public string UserHeadImageURL { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }
        
        [JsonProperty(PropertyName = "xBoxAccount")]
        public string XBoxAccount { get; set; }
    }
}
