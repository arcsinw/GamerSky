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

        /// <summary>
        /// 获取搜索热点关键词
        /// </summary>
        public const string SearchHotDict = "http://appapi2.gamersky.com/v2/SearchHotDict";

        /// <summary>
        /// 订阅热点列表
        /// </summary>
        public const string Subscribe = "http://appapi2.gamersky.com/v2/subscribe";

        /// <summary>
        /// 搜索
        /// </summary>
        public const string Search = "http://appapi2.gamersky.com/v2/TwoSearch";

        /// <summary>
        /// 订阅栏目内的内容
        /// </summary>
        public const string SubscribeContent = "http://appapi2.gamersky.com/v2/AllChannelList";

        /// <summary>
        /// 启动页广告？
        /// </summary>
        public const string AdStart = "http://appapi2.gamersky.com/v2/adstart";

        /// <summary>
        /// 登录
        /// </summary>
        public const string Login = "http://appapi2.gamersky.com/v2/login";

        /// <summary>
        /// 对订阅进行操作
        /// </summary>
        public const string EditSubscription = "http://appapi2.gamersky.com/v2/Editsubscription";
    }
}
