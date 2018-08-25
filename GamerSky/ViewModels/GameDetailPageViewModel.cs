﻿using GalaSoft.MvvmLight;
using GamerSky.Interfaces;
using GamerSky.Models;
using GamerSky.Models.ResultDataModel;
using GamerSky.Services;
using GamerSky.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace GamerSky.ViewModels
{
    public class GameDetailPageViewModel : ViewModelBase, INavigable
    {
        public GameDetailPageViewModel()
        {
            if (IsInDesignMode)
            {
                LoadDesignTimeData();
            }
            
        }

        //public GameDetailPageViewModel(string gameId)
        //{
        //    if (IsInDesignMode)
        //    {
        //        LoadDesignTimeData();
        //    }
        //    else
        //    {
        //        LoadData(gameId);
        //    }
        //}

        private GameDetailV4 gameDetail;
        public GameDetailV4 GameDetail
        {
            get
            {
                return gameDetail;
            }
            set
            {
                gameDetail = value;
                RaisePropertyChanged();
            }
        }


        public void LoadDesignTimeData()
        {
            //string json = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":{\"id\":1019230,\"Title\":\"十三号星期五：杀手谜题\",\"GameType\":\"策略游戏\",\"GameMake\":\"Blue Wizard Digital\",\"GameAuthor\":\"Blue Wizard Digital\",\"ClubId\":\"\",\"GameDir\":\"friday - the - 13th - killer - puzzle\",\"Activity\":\"\",\"Position\":\"\",\"EnTitle\":\"Friday the 13th: Killer Puzzle\",\"Intro\":\" < p >《十三号星期五：杀手谜题》是根据经典恐怖电影《十三号星期五》改编的一款策略解谜游戏，游戏由Blue Wizard Digital工作室开发。不同于电影的恐怖风格，本作采用卡通画面，玩家要控制有点萌的杀手杰森·沃赫斯，使用砍刀、鱼叉、手机等道具，杀掉关卡中的所有受害者。</ p > \",\"AllTimeT\":\"2018 / 4 / 13 0:00:00\",\"AllTime\":\"2018 - 04 - 13\",\"SteamVideos\":\"[{\"SteamId\":\"795100\",\"MoviesId\":\"256712589\",\"MoviesName\":\"Trailer\",\"MoviesThumbnail\":\"https://steamcdn-a.akamaihd.net/steam/apps/256712589/movie.293x165.jpg?t=1522097291\",\"MoviesWebm480\":\"http://steamcdn-a.akamaihd.net/steam/apps/256712589/movie480.webm?t=1522097291\",\"MoviesWebmMax\":\"http://steamcdn-a.akamaihd.net/steam/apps/256712589/movie_max.webm?t=1522097291\",\"MyMoviesWebm480\":\"\",\"MyMoviesWebmMax\":\"\",\"MoviesHighlight\":\"true\",\"CrawlHistoryId\":18495,\"MyMoviesThumbnail\":\"https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301546190.jpg\"},{\"SteamId\":\"795100\",\"MoviesId\":\"256709987\",\"MoviesName\":\"Preview\",\"MoviesThumbnail\":\"https://steamcdn-a.akamaihd.net/steam/apps/256709987/movie.293x165.jpg?t=1519761562\",\"MoviesWebm480\":\"http://steamcdn-a.akamaihd.net/steam/apps/256709987/movie480.webm?t=1519761562\",\"MoviesWebmMax\":\"http://steamcdn-a.akamaihd.net/steam/apps/256709987/movie_max.webm?t=1519761562\",\"MyMoviesWebm480\":\"\",\"MyMoviesWebmMax\":\"\",\"MoviesHighlight\":\"true\",\"CrawlHistoryId\":18495,\"MyMoviesThumbnail\":\"https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301548640.jpg\"}]\",\"SteamImages\":\"https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212300553486.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212300588269.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301015477.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301048730.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301076840.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301099388.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301153652.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301205742.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301307912.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301339594.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301357385.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301372385.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301397833.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301422994.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301446610.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301472888.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301509807.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/21/201807212301528203.jpg\",\"Peizhi\":\"<div class=\"PZXQ\">\r\n<ul class=\"PZ DD\">\r\n  <li class=\"tit\">最低配置\r\n  </li>\r\n  <li class=\"txt\">\r\n  <div class=\"tit\">系统</div>\r\n  <div class=\"txt\"><span>Windows 7</span></div>\r\n  </li>\r\n  <li class=\"txt\">\r\n  <div class=\"tit\">CPU</div>\r\n  <div class=\"txt\"><span>Intel Core i3及以上</span></div>\r\n  </li>\r\n  <li class=\"txt\">\r\n  <div class=\"tit\">内存</div>\r\n  <div class=\"txt\"><span>2 GB</span></div>\r\n  </li>\r\n  <li class=\"txt\">\r\n  <div class=\"tit\">硬盘</div>\r\n  <div class=\"txt\"><span>300 MB</span></div>\r\n  </li>\r\n  <li class=\"txt\">\r\n  <div class=\"tit\">显卡</div>\r\n  <div class=\"txt\"><span>集成显卡</span></div>\r\n  </li>\r\n</ul>\r\n<ul class=\"PZ TJ\">\r\n  <li class=\"tit\">推荐配置\r\n  </li>\r\n  <li class=\"txt\">\r\n  <div class=\"tit\">系统</div>\r\n  <div class=\"txt\"><span>Windows 7&nbsp;</span></div>\r\n  </li>\r\n  <li class=\"txt\">\r\n  <div class=\"tit\">CPU</div>\r\n  <div class=\"txt\"><span>Intel Core i3及以上</span></div>\r\n  </li>\r\n  <li class=\"txt\">\r\n  <div class=\"tit\">内存</div>\r\n  <div class=\"txt\"><span>4 GB</span></div>\r\n  </li>\r\n  <li class=\"txt\">\r\n  <div class=\"tit\">硬盘</div>\r\n  <div class=\"txt\"><span>300 MB</span></div>\r\n  </li>\r\n  <li class=\"txt\">\r\n  <div class=\"tit\">显卡</div>\r\n  <div class=\"txt\"><span>显存1GB以及上</span></div>\r\n  </li>\r\n</ul>\r\n</div>\",\"DeputyNodeId\":\"20046\",\"PCTime\":\"2018-04-13\",\"PCTimeT\":\"2018/4/13 0:00:00\",\"OfficialChinese\":\"1\",\"IsFree\":\"True\",\"OnLine\":\"45\",\"SteamPrice\":\"0\",\"SteamInitial\":\"\",\"SteamFinal\":\"0\",\"DiscountPercent\":\"\",\"DiscountText\":\"\",\"PS4Time\":\"\",\"PS4TimeT\":\"\",\"Ps4Chinese\":\"\",\"PS4HuiMian\":\"\",\"XboxOneTime\":\"\",\"XboxOneTimeT\":\"\",\"XboxChinese\":\"\",\"XboxHuiMian\":\"\",\"NintendoSwitchTime\":\"\",\"NintendoSwitchTimeT\":\"\",\"NsChinese\":\"\",\"iOSTime\":\"\",\"AndroidTime\":\"\",\"PS3Time\":\"\",\"Xbox360Time\":\"\",\"WiiUTime\":\"\",\"DSTime\":\"\",\"PSVitaTime\":\"\",\"gsScore\":\"7.6\",\"wantplayCount\":10,\"gameTag\":[\"独立游戏\",\"动作\",\"恐怖\",\"改编\",\"多人\",\"策略\",\"单人\",\"解谜\",\"合作\",\"休闲\",\"搞笑\",\"独立\",\"免费\",\"日本动画\",\"鲜血\"],\"isMarket\":2,\"playCount\":10,\"expectCount\":10,\"defaultPicUrl\":\"http://imgs.gamersky.com/ku/2018/ku_fridaythe13thkillerpuzzle.jpg\"}}";
            string json = "{\"errorCode\":0,\"errorMessage\":\"\",\"result\":{\"id\":295573,\"Title\":\"\u9965\u8352\",\"GameType\":\"\u5192\u9669\",\"GameMake\":\"Klei Entertainment\",\"GameAuthor\":\"Klei Entertainment\",\"ClubId\":\"\",\"GameDir\":\"Dont-Starve\",\"Activity\":\"\",\"Position\":\"\",\"EnTitle\":\"Don't Starve\",\"Intro\":\"<p>\u3000\u3000\u300A\u9965\u8352\u300B\u662F\u4E00\u6B3E\u7531\u300A\u95EA\u514B\u300B\u5236\u9020\u7EC4Klei Entertainment\u5236\u9020\u53D1\u884C\u7684\u4E00\u6B3E\u52A8\u4F5C\u5192\u9669\u7C7B\u6C42\u751F\u6E38\u620F\uFF0C\u672C\u4F5C\u7684\u6545\u4E8B\u8BB2\u8FF0\u7684\u662F\u5173\u4E8E\u4E00\u540D\u79D1\u5B66\u5BB6\u88AB\u6076\u9B54\u4F20\u9001\u5230\u4E86\u5F02\u4E16\u754C\u8352\u91CE\uFF0C\u4ED6\u5FC5\u9700\u7528\u672C\u4EBA\u7684\u806A\u6167\u5728\u6B8B\u9177\u7684\u91CE\u5916\u73AF\u5883\u4E2D\u6C42\u751F\u3002</p>\\r\\n<p>\u3000\u3000\u672C\u4F5C\u64CD\u4F5C\u7B80\u5355\uFF0C\u5E76\u4E14\u62E5\u6709\u9ED1\u6697\u548C\u8D85\u81EA\u7136\u7684\u5361\u901A\u7F8E\u672F\u98CE\u683C\u3002\u6E38\u620F\u4E2D\u4E0D\u4F1A\u6709\u4EFB\u4F55\u660E\u663E\u7684\u64CD\u4F5C\u63D0\u793A\uFF0C\u73A9\u5BB6\u53EF\u4EE5\u5728\u8BE5\u4F5C\u4E2D\u4F53\u9A8C\u5230\u81EA\u5DF1\u63A2\u7D22\u7684\u4E50\u8DA3\u3002</p>\\r\\n<p><strong><font style=\\\"color: rgb(255, 162, 0); font-size: 10.5pt;\\\">\u5C0F\u7F16\u8BC4\u8BED\uFF1A</font></strong></p>\\r\\n<p>\u3000\u3000\u66FE\u7ECF\u4E3A\u6211\u4EEC\u5E26\u6765\u300A\u95EA\u5BA2\u300B\u53CA\u300A\u5FCD\u8005\u5370\u8BB0\u300B\u4E24\u6B3E\u4EE4\u4EBA\u5370\u8C61\u6DF1\u523B\u7684\u4F5C\u54C1\u7684Klei\u518D\u6B21\u4E3A\u73A9\u5BB6\u5E26\u6765\u72EC\u5177\u4E00\u683C\u7684\u6C42\u751F\u6E38\u620F\u300A\u9965\u8352\u300B\uFF0C\u8FD9\u6B3E\u5145\u6EE1\u7740\u8BE1\u5F02\u60CA\u609A\uFF0C\u540C\u6837\u4EE5\u7C97\u7CD9\u7684\u624B\u7ED8\u98CE\u63CF\u7ED8\u7684\u4F5C\u54C1\u4F7F\u4EBA\u7B2C\u4E00\u773C\u770B\u5230\u5C31\u96BE\u4EE5\u5FD8\u5374\u3002\u6E38\u620F\u51E0\u4E4E\u6CA1\u6709\u5267\u60C5\uFF0C\u4E5F\u6CA1\u6709\u56FA\u5B9A\u7684\u4EFB\u52A1\uFF0C\u6211\u4EEC\u552F\u4E00\u7684\u4EFB\u52A1\u5C31\u662F\u201C\u6D3B\u4E0B\u53BB\u201D\uFF0C\u56E0\u6B64\u81EA\u7531\u5EA6\u53EF\u4EE5\u8BF4\u662F\u65E0\u9650\u7684\uFF0C\u4F60\u53EF\u4EE5\u4E00\u6574\u5929\u4EC0\u4E48\u4E5F\u4E0D\u505A\uFF0C\u4E5F\u53EF\u4EE5\u8FFD\u4E00\u53EA\u5154\u5B50\u8FFD\u4E00\u5929\uFF0C\u53EA\u8981\u4F60\u80FD\u5B58\u6D3B\u4E0B\u6765\uFF0C\u90A3\u4E48\u4F60\u505A\u7684\u4EFB\u4F55\u4E8B\u90FD\u662F\u6709\u610F\u4E49\u7684\u3002</p>\\r\\n<p><strong><font style=\\\"color: rgb(255, 162, 0); font-size: 10.5pt;\\\">\u6E38\u620F\u7279\u8272\uFF1A</font></strong></p>\\r\\n<p><span style=\\\"font-family: Wingdings;\\\">\u3000\u3000u</span>\u5B8C\u5584\u7684\u7269\u54C1\u5236\u4F5C\u7CFB\u7EDF\uFF0C\u5145\u6EE1\u521B\u9020\u7684\u4E50\u8DA3\u3002</p>\\r\\n<p><span style=\\\"font-family: Wingdings;\\\">\u3000\u3000u</span>\u6709\u8DA3\u7684\u6545\u4E8B\u80CC\u666F\uFF0C\u5F88\u6709\u4EE3\u5165\u611F\u3002</p>\\r\\n<p><span style=\\\"font-family: Wingdings;\\\">\u3000\u3000u</span>\u54E5\u7279\u5F0F\u7684\u753B\u98CE\uFF0C\u589E\u52A0\u4E86\u6E38\u620F\u7684\u8868\u73B0\u529B\u3002</p>\\r\\n<p><span style=\\\"font-family: Wingdings;\\\">\u3000\u3000u</span>\u5730\u56FE\u5DE8\u5927\uFF0C\u6C99\u76D2\u5F0F\u73A9\u6CD5\uFF0C\u63A2\u7D22\u5143\u7D20\u4E30\u5BCC\u3002</p>\",\"AllTimeT\":\"2013/4/23 0:00:00\",\"AllTime\":\"2013-04-23\",\"SteamVideos\":\"[{\\\"SteamId\\\":\\\"219740\\\",\\\"MoviesId\\\":\\\"2028604\\\",\\\"MoviesName\\\":\\\"Don't Starve Launch Trailer\\\",\\\"MoviesThumbnail\\\":\\\"https://steamcdn-a.akamaihd.net/steam/apps/2028604/movie.293x165.jpg?t=1447357797\\\",\\\"MoviesWebm480\\\":\\\"http://steamcdn-a.akamaihd.net/steam/apps/2028604/movie480.webm?t=1447357797\\\",\\\"MoviesWebmMax\\\":\\\"http://steamcdn-a.akamaihd.net/steam/apps/2028604/movie_max.webm?t=1447357797\\\",\\\"MyMoviesWebm480\\\":\\\"\\\",\\\"MyMoviesWebmMax\\\":\\\"\\\",\\\"MoviesHighlight\\\":\\\"true\\\",\\\"CrawlHistoryId\\\":8181,\\\"MyMoviesThumbnail\\\":\\\"https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190945082169.jpg\\\"},{\\\"SteamId\\\":\\\"219740\\\",\\\"MoviesId\\\":\\\"2028889\\\",\\\"MoviesName\\\":\\\"Don't Starve Powers Trailer\\\",\\\"MoviesThumbnail\\\":\\\"https://steamcdn-a.akamaihd.net/steam/apps/2028889/movie.293x165.jpg?t=1447358243\\\",\\\"MoviesWebm480\\\":\\\"http://steamcdn-a.akamaihd.net/steam/apps/2028889/movie480.webm?t=1447358243\\\",\\\"MoviesWebmMax\\\":\\\"http://steamcdn-a.akamaihd.net/steam/apps/2028889/movie_max.webm?t=1447358243\\\",\\\"MyMoviesWebm480\\\":\\\"\\\",\\\"MyMoviesWebmMax\\\":\\\"\\\",\\\"MoviesHighlight\\\":\\\"true\\\",\\\"CrawlHistoryId\\\":8181,\\\"MyMoviesThumbnail\\\":\\\"https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190945098658.jpg\\\"},{\\\"SteamId\\\":\\\"219740\\\",\\\"MoviesId\\\":\\\"2028453\\\",\\\"MoviesName\\\":\\\"Don't Starve - Wilson's Intro Trailer\\\",\\\"MoviesThumbnail\\\":\\\"https://steamcdn-a.akamaihd.net/steam/apps/2028453/movie.293x165.jpg?t=1447357610\\\",\\\"MoviesWebm480\\\":\\\"http://steamcdn-a.akamaihd.net/steam/apps/2028453/movie480.webm?t=1447357610\\\",\\\"MoviesWebmMax\\\":\\\"http://steamcdn-a.akamaihd.net/steam/apps/2028453/movie_max.webm?t=1447357610\\\",\\\"MyMoviesWebm480\\\":\\\"\\\",\\\"MyMoviesWebmMax\\\":\\\"\\\",\\\"MoviesHighlight\\\":\\\"false\\\",\\\"CrawlHistoryId\\\":8181,\\\"MyMoviesThumbnail\\\":\\\"https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190945099091.jpg\\\"},{\\\"SteamId\\\":\\\"219740\\\",\\\"MoviesId\\\":\\\"2028102\\\",\\\"MoviesName\\\":\\\"Don't Starve Trailer\\\",\\\"MoviesThumbnail\\\":\\\"https://steamcdn-a.akamaihd.net/steam/apps/2028102/movie.293x165.jpg?t=1447357002\\\",\\\"MoviesWebm480\\\":\\\"http://steamcdn-a.akamaihd.net/steam/apps/2028102/movie480.webm?t=1447357002\\\",\\\"MoviesWebmMax\\\":\\\"http://steamcdn-a.akamaihd.net/steam/apps/2028102/movie_max.webm?t=1447357002\\\",\\\"MyMoviesWebm480\\\":\\\"\\\",\\\"MyMoviesWebmMax\\\":\\\"\\\",\\\"MoviesHighlight\\\":\\\"false\\\",\\\"CrawlHistoryId\\\":8181,\\\"MyMoviesThumbnail\\\":\\\"https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190945108010.jpg\\\"}]\",\"SteamImages\":\"https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190944339203.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190944447565.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190944477677.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190944527423.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190944541242.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190944572370.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190944592693.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190945024791.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190945059181.jpg,https://img1.gamersky.com/thirdparty_steam/2018/07/19/201807190945076358.jpg\",\"Peizhi\":\"<div class=\\\"PZXQ\\\">\\r\\n<ul class=\\\"PZ DD\\\">\\r\\n  <li class=\\\"tit\\\">\u6700\u4F4E\u914D\u7F6E </li>\\r\\n  <li class=\\\"txt\\\">\\r\\n  <div class=\\\"tit\\\">\u7CFB\u7EDF</div>\\r\\n  <div class=\\\"txt\\\"><span>Windows XP / Vista / 7 / 8</span></div>\\r\\n  </li>\\r\\n  <li class=\\\"txt\\\">\\r\\n  <div class=\\\"tit\\\">CPU</div>\\r\\n  <div class=\\\"txt\\\"><span>1.7GHz\u6216\u662F\u66F4\u9AD8</span></div>\\r\\n  </li>\\r\\n  <li class=\\\"txt\\\">\\r\\n  <div class=\\\"tit\\\">\u5185\u5B58</div>\\r\\n  <div class=\\\"txt\\\"><span>1G RAM</span></div>\\r\\n  </li>\\r\\n  <li class=\\\"txt\\\">\\r\\n  <div class=\\\"tit\\\">\u786C\u76D8</div>\\r\\n  <div class=\\\"txt\\\"><span>500M</span></div>\\r\\n  </li>\\r\\n  <li class=\\\"txt\\\">\\r\\n  <div class=\\\"tit\\\">\u663E\u5361</div>\\r\\n  <div class=\\\"txt\\\"><span>AMD Radeon HD 5450 / \u72EC\u663E\u6216\u66F4\u597D</span></div>\\r\\n  </li>\\r\\n  <li class=\\\"txt\\\">\\r\\n  <div class=\\\"tit\\\">\u58F0\u5361</div>\\r\\n  <div class=\\\"txt\\\"><span>100% Directx9.0c\u517C\u5BB9\u58F0\u5361\u548C\u9A71\u52A8\u7A0B\u5E8F</span></div>\\r\\n  </li>\\r\\n</ul>\\r\\n<ul class=\\\"PZ TJ\\\">\\r\\n  <li class=\\\"tit\\\">\u63A8\u8350\u914D\u7F6E </li>\\r\\n  <li class=\\\"txt\\\">\\r\\n  <div class=\\\"tit\\\">\u7CFB\u7EDF</div>\\r\\n  <div class=\\\"txt\\\"><span>-</span></div>\\r\\n  </li>\\r\\n  <li class=\\\"txt\\\">\\r\\n  <div class=\\\"tit\\\">CPU</div>\\r\\n  <div class=\\\"txt\\\"><span>-</span></div>\\r\\n  </li>\\r\\n  <li class=\\\"txt\\\">\\r\\n  <div class=\\\"tit\\\">\u5185\u5B58</div>\\r\\n  <div class=\\\"txt\\\"><span>-</span></div>\\r\\n  </li>\\r\\n  <li class=\\\"txt\\\">\\r\\n  <div class=\\\"tit\\\">\u786C\u76D8</div>\\r\\n  <div class=\\\"txt\\\"><span>-</span></div>\\r\\n  </li>\\r\\n  <li class=\\\"txt\\\">\\r\\n  <div class=\\\"tit\\\">\u663E\u5361</div>\\r\\n  <div class=\\\"txt\\\"><span>-</span></div>\\r\\n  </li>\\r\\n  <li class=\\\"txt\\\">\\r\\n  <div class=\\\"tit\\\">\u58F0\u5361</div>\\r\\n  <div class=\\\"txt\\\"><span>-</span></div>\\r\\n  </li>\\r\\n</ul>\\r\\n</div>\",\"DeputyNodeId\":\"20053\",\"PCTime\":\"2013-04-23\",\"PCTimeT\":\"2013/4/23 0:00:00\",\"OfficialChinese\":\"2\",\"IsFree\":\"False\",\"OnLine\":\"1683\",\"SteamPrice\":\"24\",\"SteamInitial\":\"24\",\"SteamFinal\":\"24\",\"DiscountPercent\":\"\",\"DiscountText\":\"\",\"PS4Time\":\"2014-01-07\",\"PS4TimeT\":\"2014/1/7 0:00:00\",\"Ps4Chinese\":\"1\",\"PS4HuiMian\":\"\",\"XboxOneTime\":\"\",\"XboxOneTimeT\":\"\",\"XboxChinese\":\"\",\"XboxHuiMian\":\"\",\"NintendoSwitchTime\":\"\",\"NintendoSwitchTimeT\":\"\",\"NsChinese\":\"\",\"iOSTime\":\"\",\"AndroidTime\":\"\",\"PS3Time\":\"\",\"Xbox360Time\":\"\",\"WiiUTime\":\"\",\"DSTime\":\"\",\"PSVitaTime\":\"2014-09-02\",\"gsScore\":\"9.1\",\"wantplayCount\":3967,\"gameTag\":[\"\u751F\u5B58\",\"\u6C99\u76D2\",\"\u52A8\u4F5C\",\"\u6050\u6016\",\"\u5F00\u653E\u4E16\u754C\",\"\u5192\u9669\",\"\u591A\u4EBA\",\"\u5355\u4EBA\",\"\u63A2\u7D22\",\"\u6A21\u62DF\",\"\u56F0\u96BE\",\"\u751F\u5B58\u6050\u6016\",\"\u72EC\u7ACB\",\"\u53EF\u6A21\u7EC4\u5316\",\"\u5DE5\u827A\",\"\u91CD\u73A9\u4EF7\u503C\",\"\u4E8C\u7DAD\",\"\u7C7BRogue\",\"\u6C38\u4E45\u6B7B\u4EA1\",\"\u5782\u76F4\u5377\u8F74\"],\"isMarket\":2,\"playCount\":1313,\"expectCount\":3968,\"defaultPicUrl\":\"http://imgs.gamersky.com/ku/2017/ku_dontstarve.jpg\"}}";
            var result = JsonHelper.Deserlialize<ResultDataTemplate<GameDetailV4>>(json);
            GameDetail = result?.Result;

            //var videos = JsonHelper.Deserlialize <List<SteamVideo>>(result.Result.SteamVideos);

        }

        public async Task LoadGameDetail(string gameId)
        {
            GameDetail = await ApiService.Instance.GetGameDetailV4Async(gameId);
        }

        public async Task LoadGameSpecial(string nodeId)
        {

        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            
        }

        public async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is GameSpecialDetail game)
            {
                await LoadGameDetail(game.Id.ToString());
            }
            else if (e.Parameter is GameSpecial gameSpecial)
            {
                await LoadGameSpecial(gameSpecial.NodeId);
            }
        }
    }
}
