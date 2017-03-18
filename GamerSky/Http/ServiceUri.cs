﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Http
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
        /// 游戏详情页 攻略|新闻
        /// </summary>
        public const string TwoCorrelation = "http://appapi2.gamersky.com/v2/TwoCorrelation";

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
        /// 游民登录
        /// </summary>
        public const string Login = "http://appapi2.gamersky.com/v2/login";

        /// <summary>
        /// 微博登录
        /// 先使用OAuth获取code 再通过get获取用户信息
        /// </summary>
        public const string SinaLogin = "http://i.gamersky.com/api/logincheck?callback=9ca67470f6e25515f28192743109efc6";

        /// <summary>
        /// 评论文章
        /// {"loginToken":"C8DF2EEEFB1B0E35F94E7177586117697B9E617762DCCE389A04364C49F1E2BEEA6CDDBF7C63981BE132C977590C90ED7BB71F205BBC4647ADB51B96CDB5CBE44C5CA258AC7CF1E2BD2D0F656959B1B6DE277D411E44AF1FC226AE495414149149F9F7E0BF7194A2FA52FA1FAD7FD86C1D902FFBCE116E472076B0DFB85B7B51A506DF2ABA59717AC4EA7091FD2EE7B249867EB0B6D58C16F80A64C9FC697C45AD2355B6707BFFEF93A2B7FCEC7CC36AD4AD02C2F672A175E8C76CE967061C054D12FE40C7EB2104920DDF835FC8B382D50ED87D2664B79E12B79E2816B6422F45E2FB6BD37FF908DDD71912310BCB6C385988628E7A93B8D51AE900A5E9C2F0A48D9CE8483FCD8689F1BF6711FF42B4C2AD01BE718D017EBE7EB21C57E12CA08B685A70BCEDFBF2F01B6134B734947DF90D524A73CD6AAC43F9919EB475CF9A448307178F5384BB70BB07583191B4754CA2BABB36EDE5811B2E052CAC215FCBD36A0CBB","topicId":855635,"topicUrl":"http://www.gamersky.com/news/201701/855635.shtml","topicTitle":"玩家称已通关《生化危机7》 血统纯正棒呆了","content":"test","replyID":1080276440}
        /// {0} CommentPostData
        /// </summary>
        public const string AddComment = "http://cm.gamersky.com/appapi/AddComment?jsondata={0}";

        /// <summary>
        /// 获取一篇新闻的所有评论
        /// </summary>
        public const string AllComments = "http://cm.gamersky.com/appapi/GetAllComment?jsondata={0}";

        /// <summary>
        /// 获取一篇新闻的热门评论
        /// {0} {"topicId":868327,"pageIndex":1,"pageSize":3}
        /// </summary>
        public const string HotComments = "http://cm.gamersky.com/appapi/GetHotComment?jsondata={0}";

        /// <summary>
        /// 获取所有评论回复
        /// {"userId":531939,"pageIndex":2,"pageSize":20}
        /// </summary>
        public const string GetAllReply = "http://cm.gamersky.com/appapi/GetUserComment?jsondata={0}";

        /// <summary>
        /// 对评论回复点赞
        /// {"action":"ding","topicId":855635,"commentID":1080276461,"commentUserID":"531939"} url encode
        /// </summary>
        public const string LikeReply = "http://cm.gamersky.com/appapi/AddLike?jsondata={0}";

        /// <summary>
        /// 对订阅进行操作
        /// </summary>
        public const string EditSubscription = "http://appapi2.gamersky.com/v2/Editsubscription";

        /// <summary>
        /// 手机号注册获取验证码
        /// </summary>
        public const string GetVerificationCode = "http://appapi2.gamersky.com/v2/GetVerificationCode";

        /// <summary>
        /// 用邮箱注册账号
        /// </summary>
        public const string RegisterByEmail = "http://appapi2.gamersky.com/v2/SubmitRegistrationInfo";

        /// <summary>
        /// 找回密码第一步 输入用户名
        /// </summary>
        public const string FindPassword = "http://appapi2.gamersky.com/v2/TwoGetCodeInformation";

        /// <summary>
        /// 订阅专题
        /// </summary>
        public const string SubscribeTopic = "http://appapi2.gamersky.com/v2/AllChannelList";

        /// <summary>
        /// 游戏库游戏列表
        /// </summary>
        public const string GameList = "http://appapi2.gamersky.com/v2/TwoGameList";

        /// <summary>
        /// 游戏detail
        /// </summary>
        public const string GameDetail = "http://appapi2.gamersky.com/v2/TwoGameDetails";
    }
}
