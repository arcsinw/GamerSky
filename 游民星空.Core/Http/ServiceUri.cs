using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Http
{
    /// <summary>
    /// api uri
    /// </summary>
    static class ServiceUri
    {
        /// <summary>
        /// 新闻 AllChannelListPostData
        /// </summary>
        public const string AllChannelList = "http://appapi2.gamersky.com/v2/AllChannelList";

        /// <summary>
        /// 返回分类信息 ChannelPostData
        /// </summary>
        public const string AllChannel = "http://appapi2.gamersky.com/v2/allchannel";

        /// <summary>
        /// 阅读新闻 
        /// </summary>
        public const string TwoArticle = "http://appapi2.gamersky.com/v2/TwoArticle";
    }
}
