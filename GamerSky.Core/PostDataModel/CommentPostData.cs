﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.PostModel
{
    public class CommentPostData
    {
        [JsonProperty(PropertyName = "loginToken")]
        public string LoginToken { get; set; }

        [JsonProperty(PropertyName = "topicId")]
        public string TopicId { get; set; }

        [JsonProperty(PropertyName = "topicUrl")]
        public string TopicUrl { get; set; }

        [JsonProperty(PropertyName = "topicTitle")]
        public string TopicTitle { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "replyID")]
        public string ReplyId { get; set; }
    }
}
