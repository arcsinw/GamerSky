using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Responses
{
    /// <summary>
    /// 用户信息 用户登录返回 
    /// </summary>
    public class LoginResponse
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "isPasswordExisted")]
        public bool IsPasswordExisted { get; set; }

        [JsonProperty(PropertyName = "loginToken")]
        public string LoginToken { get; set; }

        [JsonProperty(PropertyName = "notifyToken")]
        public string NotifyToken { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "userAvatar")]
        public string UserAvatar { get; set; }

        [JsonProperty(PropertyName = "veriPicAddr")]
        public string VeriPicAddr { get; set; }

        [JsonProperty(PropertyName = "userGroupId")]
        public string UserGroupId { get; set; }

        [JsonProperty(PropertyName = "userAuthentication")]
        public string UserAuthentication { get; set; }

        [JsonProperty(PropertyName = "userLevel")]
        public string UserLevel { get; set; }

        [JsonProperty(PropertyName = "phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "qqUserIdBound")]
        public string QQUserIdBound { get; set; }

        [JsonProperty(PropertyName = "weiXinUserIdBound")]
        public string WeiXinUserIdBound { get; set; }

        [JsonProperty(PropertyName = "weiBoUserIdBound")]
        public string WeiBoUserIdBound { get; set; }
    }
}
