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
        /// 新闻列表 AllChannelListPostData
        /// </summary>
        public const string AllChannelList = "http://appapi2.gamersky.com/v2/AllChannelList";

        /// <summary>
        /// 频道信息 ChannelPostData
        /// </summary>
        public const string AllChannel = "http://appapi2.gamersky.com/v2/allchannel";

        /// <summary>
        /// 阅读新闻 
        /// </summary>
        public const string TwoArticle = "http://appapi2.gamersky.com/v2/TwoArticle";

        /// <summary>
        /// 相关阅读
        /// </summary>
        public const string TwoCorrelation = "http://appapi2.gamersky.com/v2/TwoArticle";

        /// <summary>
        /// 获取有攻略的游戏列表
        /// </summary>
        public const string Strategy = "http://appapi2.gamersky.com/v2/strategy";

        /// <summary>
        /// 获取某一个游戏的攻略列表
        /// </summary>
        public const string GameStrategys = "http://appapi2.gamersky.com/v2/AllChannelList";


    }
}
