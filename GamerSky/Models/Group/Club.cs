using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models.Group
{
    /// <summary>
    /// 圈子 
    /// 主题 | 话题
    /// </summary>
    public class Club
    {
        [JsonProperty(PropertyName = "backgroundImageURL")]
        public string BackgroundImage { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "managerId")]
        public string ManagerId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "recommendLevel")]
        public string RecommendLevel { get; set; }

        [JsonProperty(PropertyName = "thumbnailURL")]
        public string ThumbnailURL { get; set; }

        [JsonProperty(PropertyName = "topicsCount")]
        public string TopicsCount { get; set; }

        [JsonProperty(PropertyName = "usersCount")]
        public string UsersCount { get; set; }

    }
}
