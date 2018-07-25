using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models.Group
{
    public class Topic
    {
        [JsonProperty(PropertyName = "clubContentId")]
        public string ClubContentId { get; set; }
        
        [JsonProperty(PropertyName = "clubId")]
        public string ClubId { get; set; }

        [JsonProperty(PropertyName = "clubName")]
        public string ClubName { get; set; }

        [JsonProperty(PropertyName = "topicContent")]
        public string TopicContent { get; set; }

        [JsonProperty(PropertyName = "topicId")]
        public string TopicId { get; set; }

        [JsonProperty(PropertyName = "imageURLs")]
        public List<Image> ImageURLs { get; set; }

        [JsonProperty(PropertyName = "createTime")]
        public string CreateTime { get; set; }

        [JsonProperty(PropertyName = "praisesCount")]
        public int PraisesCount { get; set; }

        [JsonProperty(PropertyName = "commentsCount")]
        public int CommentsCount { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "userGroupId")]
        public string UserGroupId { get; set; }

        [JsonProperty(PropertyName = "userHeadImageURL")]
        public string UserHeadImageURL { get; set; }

        [JsonProperty(PropertyName = "userAuthentication")]
        public string UserAuthentication { get; set; }

        [JsonProperty(PropertyName = "userAuthenticationIconURL")]
        public string UserAuthenticationIconURL { get; set; }

        [JsonProperty(PropertyName = "userLevel")]
        public string UserLevel { get; set; }

        [JsonProperty(PropertyName = "videoTitle")]
        public string VideoTitle { get; set; }

        [JsonProperty(PropertyName = "videoThumbnailURL")]
        public string VideoThumbnailURL { get; set; }

        [JsonProperty(PropertyName = "videoOriginURL")]
        public string VideoOriginURL { get; set; }
    }

    public class Image
    {

        [JsonProperty(PropertyName = "height")]
        public double Height { get; set; }

        [JsonProperty(PropertyName = "width")]
        public double Width { get; set; }

        [JsonProperty(PropertyName = "isGIF")]
        public bool IsGif { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
