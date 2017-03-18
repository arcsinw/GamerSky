using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using GamerSky.Helper;
using GamerSky.Http;
using GamerSky.Model;
using GamerSky.PostDataModel;
using GamerSky.ResultModel;
using GamerSky.PostModel;
using System.Net;

namespace GamerSky.Http
{
    public class ApiService : ApiBaseService
    {
        private static ApiService _apiService = new ApiService();

        private ApiService()
        {

        }

        public static ApiService Instance
        {
            get
            {
                return _apiService;
            }
        }

        private string LocalFolder = ApplicationData.Current.LocalFolder.Path;

        /// <summary>
        /// 获取频道列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Channel>> GetChannelList()
        {
            string filename = "channelList.json";
            ChannelResult channelResult = new ChannelResult();
            channelResult.Result = await FileHelper.Current.ReadObjectAsync<List<Channel>>(filename);
            

            if (channelResult.Result == null)
            {
                if (ConnectionHelper.IsInternetAvailable)
                {
                    AllChannelListPostData postData = new AllChannelListPostData();

                    postData.deviceId = DeviceInformationHelper.GetDeviceId();
                    postData.request = new request() { type = "0" };
                    channelResult = await PostJson<AllChannelListPostData, ChannelResult>(ServiceUri.AllChannel, postData);
                    if (channelResult != null && channelResult.Result != null)
                    {
                        await FileHelper.Current.WriteObjectAsync(channelResult.Result, filename);
                    }
                }
                    
            }
            channelResult.Result.Insert(0,new Channel { isTop = "False", nodeId = 0, nodeName = "头条" });

            return channelResult.Result;  
        }
        
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="nodeId">频道ID</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<List<Essay>> GetEssayList(int nodeId, int pageIndex)
        {
            string filename = "essayList_"+nodeId+"_"+pageIndex+".json";
            EssayResult essayResult = new EssayResult();
            if (!ConnectionHelper.IsInternetAvailable) //无网络
            {
                essayResult.Result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();
                postData.deviceId = DeviceInformationHelper.GetDeviceId();
                postData.request = new request()
                {
                    elementsCountPerPage = 20,
                    nodeIds = nodeId,
                    pageIndex = pageIndex,
                    parentNodeId = "news",
                    type = "null",
                };
                essayResult = await PostJson<AllChannelListPostData, EssayResult>(ServiceUri.AllChannelList, postData);
                if (essayResult != null && essayResult.Result!=null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Essay>>(essayResult.Result, filename);
                }
            }
            
            return essayResult?.Result;
        }

        /// <summary>
        /// 阅读文章
        /// 适用于新闻 和 攻略
        /// </summary>
        /// <param name="contentId">文章Id</param>
        /// <returns></returns>
        public async Task<News> ReadEssay(string contentId)
        {
            string filename = "news_" + contentId + ".json";
            NewsResult newsResult = new NewsResult();
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                newsResult.Result = await FileHelper.Current.ReadObjectAsync<News>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();
                postData.request = new request { contentId = contentId,pageIndex =1 };
                postData.deviceId = DeviceInformationHelper.GetDeviceId();
                newsResult = await PostJson<AllChannelListPostData, NewsResult>(ServiceUri.TwoArticle, postData);
                if (newsResult != null && newsResult.Result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<News>(newsResult.Result, filename);
                }

            }

            return newsResult?.Result;
        }

        /// <summary>
        /// 获取某文章所有评论
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<List<Comment>> GetAllComments(string contentId,int pageIndex)
        {
            List<Comment> comments = new List<Comment>();
            if(ConnectionHelper.IsInternetAvailable)
            {
                GetAllCommentPostData postData = new GetAllCommentPostData()
                {
                    pageIndex = pageIndex,
                    pageSize = 20,
                    topicId = contentId
                };
                var result = await GetJson<AllCommentsResult>(string.Format(ServiceUri.AllComments, WebUtility.UrlEncode(JsonHelper.Serializer(postData))));
                if(result!=null)
                {
                    comments = result.Result.Result;
                }
            }
            return comments;
        }

        /// <summary>
        /// 获取热门评论
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<List<Comment>> GetHotComments(string contentId, int pageIndex)
        {
            List<Comment> comments = new List<Comment>();
            if (ConnectionHelper.IsInternetAvailable)
            {
                GetAllCommentPostData postData = new GetAllCommentPostData()
                {
                    pageIndex = pageIndex,
                    pageSize = 20,
                    topicId = contentId
                };
                var result = await GetJson<AllCommentsResult>(string.Format(ServiceUri.HotComments, WebUtility.UrlEncode(JsonHelper.Serializer(postData))));
                if (result != null)
                {
                    comments = result.Result.Result;
                }
            }
            return comments;
        }

        /// <summary>
        /// 获取相关阅读
        /// </summary>
        /// <param name="contentId">文章Id</param>
        /// <returns></returns>
        public async Task<List<RelatedReadings>> GetRelatedReadings(string contentId, string contentType="news")
        {
            string filename = "relatedReadings_" + contentId + ".json";
            RelatedReadingsResult readings = new RelatedReadingsResult();
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                readings.Result = await FileHelper.Current.ReadObjectAsync<List<RelatedReadings>>(filename);
            }
            else
            {
                RelatedReadingPostData postData = new RelatedReadingPostData();
                postData.request = new RelatedReadingRequest{ contentId = contentId, contentType = contentType};
                postData.deviceId = DeviceInformationHelper.GetDeviceId();
                readings = await PostJson<RelatedReadingPostData, RelatedReadingsResult>(ServiceUri.TwoCorrelation, postData);
                if (readings != null && readings.Result != null)
                {
                    await FileHelper.Current.WriteObjectAsync(readings.Result, filename);
                }
            }
  
            return readings?.Result;
        }

        /// <summary>
        /// 获取有攻略的游戏 关注
        /// </summary>
        /// <returns></returns>
        public async Task<List<Strategy>> GetStrategys(int pageCount = 20,int type=0)
        {
            string filename = "focusStrategys.json";
            StrategyResult strategyResult = new StrategyResult();
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                strategyResult.Result = await FileHelper.Current.ReadObjectAsync<List<Strategy>>(filename);
            }
            else
            {
                StrategyPostData postData = new StrategyPostData();
                postData.request = new StrategyRequest { pageIndex = 1, pageCount = pageCount, type = type };
                strategyResult = await PostJson<StrategyPostData, StrategyResult>(ServiceUri.Strategy, postData);
                if (strategyResult != null && strategyResult.Result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Strategy>>(strategyResult.Result, filename);
                }
            }

            return strategyResult?.Result;
        }

        /// <summary>
        /// 获取所有有攻略的游戏
        /// </summary>
        /// <returns></returns>
        public async Task<List<Strategy>> GetAllStrategys()
        {
            string filename = "allStrategys.json";
            List<Strategy> allStrategys = new List<Strategy>();
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                allStrategys = await FileHelper.Current.ReadObjectAsync<List<Strategy>>(filename);
            }
            else
            {
                allStrategys = await GetStrategys(10000, 1);

                if (allStrategys != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Strategy>>(allStrategys, filename);
                }
            }
            return allStrategys;
        }

        /// <summary>
        /// 获取某个游戏的所有攻略
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public async Task<List<Essay>> GetGameStrategys(int specialID,int pageIndex=1)
        {
            EssayResult essayResult = new EssayResult();
            string filename = "gameStrategys_" + specialID + ".json";
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                essayResult.Result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();
                postData.request = new request { elementsCountPerPage = 20, nodeIds = specialID, pageIndex = pageIndex, parentNodeId = "strategy" };
                essayResult = await PostJson<AllChannelListPostData, EssayResult>(ServiceUri.GameStrategys, postData);
                if (essayResult != null && essayResult.Result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Essay>>(essayResult.Result, filename);
                }
            }

            return essayResult?.Result ;
        }
        
        /// <summary>
        /// 获取搜索热点词
        /// </summary>
        /// <param name="searchType">热点词类型 strategy news shouyou</param>
        /// <returns></returns>
        public async Task<List<string>> GetSearchHotKey(string searchType)
        {
            string filename = "searchHotKey_"+searchType+".json";
            SearchResult searchResult = new SearchResult();
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                searchResult.result = await FileHelper.Current.ReadObjectAsync<List<string>>(filename);
            }
            else
            {
                SearchPostData postData = new SearchPostData() { request = new SearchRequest { searchType = searchType } };
                searchResult = await PostJson<SearchPostData, SearchResult>(ServiceUri.SearchHotDict, postData);

                if (searchResult != null && searchResult.result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<string>>(searchResult.result, filename);
                }
            }

            return searchResult?.result;
        }

        /// <summary>
        /// 获取热门/全部订阅
        /// </summary>
        /// <param name="type">type=0 热门 type=1 全部</param>
        public async Task<List<Subscribe>> GetSubscribeHotKey(string type="1")
        {
            string filename = "subscribeHotKey_" + type + ".json";
            SubscribeResult subscribeResult = new SubscribeResult();
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                subscribeResult.Result = await FileHelper.Current.ReadObjectAsync<List<Subscribe>>(filename);
            }
            else
            {
                SubscribeSearchPostData postData = new SubscribeSearchPostData { request = new SubscribeSearchRequest { type = type } };
                subscribeResult = await PostJson<SubscribeSearchPostData, SubscribeResult>(ServiceUri.Subscribe, postData);
                if (subscribeResult != null && subscribeResult.Result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Subscribe>>(subscribeResult.Result, filename);
                }
            }
            return subscribeResult?.Result;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>搜索结果不缓存</remarks>
        /// <param name="searchKey"></param>
        /// <param name="searchType"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<List<Essay>> SearchByKey(string searchKey,SearchTypeEnum searchType,int pageIndex)
        {
            EssayResult essayResult = new EssayResult();
            if (!ConnectionHelper.IsInternetAvailable) //无网络
            {
            }
            else
            {
                SearchPostData postData = new SearchPostData();
                postData.request = new SearchRequest { elementsCountPerPage = "20", pageIndex = pageIndex, searchKey = searchKey, searchType = searchType.ToString() };
                
                essayResult = await PostJson<SearchPostData, EssayResult>(ServiceUri.Search, postData);
            }

            return essayResult?.Result;
        }

        /// <summary>
        /// 获取订阅栏目内的内容
        /// </summary>
        /// <param name="sourceId">栏目Id</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        public async Task<List<Essay>> GetSubscribeContent(string sourceId, int pageIndex = 1)
        {
            EssayResult subscribeContent = new EssayResult();
            string filename = "subscribeContent_" + sourceId + ".json";
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                subscribeContent.Result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {
                SubscribeContentPostData postData = new SubscribeContentPostData();
                postData.request = new SubscribeContentRequest { nodeIds = sourceId, pageCount = 10, pageIndex = pageIndex };
                subscribeContent = await PostJson<SubscribeContentPostData, EssayResult>(ServiceUri.SubscribeContent, postData);

                if (subscribeContent != null && subscribeContent.Result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Essay>>(subscribeContent.Result, filename);
                }
            }

            return subscribeContent?.Result;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="passWord">用户名</param>
        /// <param name="userName">密码</param>
        /// <returns></returns>
        public async Task<LoginResult> Login(string passWord, string userName)
        {
            LoginPostData postData = new LoginPostData();
            postData.request = new LoginPostDataRequest { passWord = passWord, userName = userName };
            var result = await PostJson<LoginPostData, LoginResult>(ServiceUri.Login, postData);
            return result;
        }

        public async Task SinaLogin()
        {
            
        }

        /// <summary>
        /// Add a comment
        /// </summary>
        /// <param name="loginToken"></param>
        /// <param name="topicId"></param>
        /// <param name="topicUrl"></param>
        /// <param name="topicTitle"></param>
        /// <param name="content"></param>
        /// <param name="replyId"></param>
        /// <returns></returns>
        public async Task<AddCommentResult> AddComment(string loginToken,string topicId,
            string topicUrl,string topicTitle,string content,string replyId)
        {
            CommentPostData postData = new CommentPostData
            {
                LoginToken = loginToken,
                TopicId = topicId,
                TopicTitle = topicTitle,
                TopicUrl = topicUrl,
                Content = content,
                ReplyId = replyId
            };

            var result = await GetJson<AddCommentResult>(string.Format(ServiceUri.AddComment, WebUtility.UrlEncode(JsonHelper.Serializer(postData))));
            return result;
        }

        /// <summary>
        /// 获取评论回复
        /// </summary>
        public async Task<List<Comment>> GetAllReply(string userId,int pageIndex)
        {
            string jsonData = "{\"userId\":" + userId + ",\"pageIndex\":"+pageIndex + ",\"pageSize\":20}";
            string url = string.Format(ServiceUri.GetAllReply, jsonData);
            GetAllReplyResult result = await GetJson<GetAllReplyResult>(url);
            return result?.Result.Comments;
        }

        /// <summary>
        /// 给评论回复点赞
        /// {"action":"ding","topicId":855635,"commentID":1080276461,"commentUserID":"531939"}
        /// </summary>
        public async Task AddLikeComment(string topicId,string commentId,string commentUserId)
        {
            string jsonData = "{ \"action\":\"ding\",\"topicId\":" + topicId + ",\"commentID\":" + commentId + ",\"commentUserID\":" + commentUserId + "}";
            string url = string.Format(ServiceUri.LikeReply, jsonData);
            LikeCommentResult result = await GetJson<LikeCommentResult>(url);
        }

        /// <summary>
        /// 获取要闻
        /// </summary>
        /// <returns></returns>
        public async Task<List<Essay>> GetYaowen(int pageIndex=1)
        {
            string filename = "yaowen.json";
            EssayResult essayResult = new EssayResult();
            if (!ConnectionHelper.IsInternetAvailable) //无网络
            {
                essayResult.Result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {
                YaowenPostData postData = new YaowenPostData();
                postData.request = new YaowenRequest(){ pageIndex = pageIndex };
                essayResult = await PostJson<YaowenPostData, EssayResult>(ServiceUri.AllChannelList, postData);
                if(essayResult!=null && essayResult.Result!= null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Essay>>(essayResult.Result, filename);
                }
            }

            return essayResult?.Result;
        }

        /// <summary>
        /// 获取应用启动图
        /// </summary>
        /// <returns></returns>
        public async Task<List<AdStart>> GetStartImage()
        {
            string filename = "adStart" + ".json";
            List<AdStart> adStarts = new List<AdStart>();
            if (!ConnectionHelper.IsInternetAvailable)
            {
                adStarts = await FileHelper.Current.ReadObjectAsync<List<AdStart>>(filename);
            }
            else
            {
                AdStartPostData postData = new AdStartPostData();
                AdStartResult adStartResult = await PostJson<AdStartPostData, AdStartResult>(ServiceUri.AdStart, postData);
                
                if (adStarts != null && adStartResult?.Result != null)
                {
                    foreach (var item in adStartResult.Result)
                    {
                        adStarts.Add(item);
                    }
                }
                await FileHelper.Current.WriteObjectAsync<List<AdStart>>(adStarts,filename);

            }
            return adStarts;
        }

        /// <summary>
        /// 获取手机号注册验证码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="codetype"></param>
        /// <returns></returns>
        public async Task<VerificationCode> GetVerificationCode(string phoneNumber,string username,string email,string codetype="1")
        {
            VerificationCodePostData postData = new VerificationCodePostData();
            postData.request = new VerificationCodeRequest() { codetype = codetype, email = email, phoneNumber = phoneNumber, username = username };

            VerificationCode verificationCode = await PostJson<VerificationCodePostData, VerificationCode>(ServiceUri.GetVerificationCode, postData);
            return verificationCode;
        }

        /// <summary>
        /// 通过邮箱注册
        /// </summary>
        /// <param name="answer">密保问题答案</param>
        /// <param name="password">密码</param>
        /// <param name="email">邮箱</param>
        /// <param name="question">密保问题</param>
        /// <param name="userName">用户名</param>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="phoneVerificationCode">手机验证码</param>
        /// <returns></returns>
        public async Task<VerificationCode> RegisterByEmail(string answer,string password,string email,string question,string userName)
        {
            EmailRegisterPostData postData = new EmailRegisterPostData();
            postData.request = new EmailRegisterRequest()
            {
                answer = answer,
                confirmpassword = password,
                email = email,
                password = password,
                phoneNumber = "",
                phoneVerificationCode = "",
                question = question,
                userName = userName
            };
            VerificationCode verificationCode = await PostJson<EmailRegisterPostData, VerificationCode>(ServiceUri.RegisterByEmail, postData);

            return verificationCode;
        }

        /// <summary>
        /// 通过手机号注册
        /// </summary>
        /// <param name="answer">密保问题答案</param>
        /// <param name="password">密码</param>
        /// <param name="email">邮箱</param>
        /// <param name="question">密保问题</param>
        /// <param name="userName">用户名</param>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="phoneVerificationCode">手机验证码</param>
        /// <returns></returns>
        public async Task<VerificationCode> RegisterByPhone(string password,  string userName, string phoneNumber , string phoneVerificationCode)
        {
            EmailRegisterPostData postData = new EmailRegisterPostData();
            postData.request = new EmailRegisterRequest()
            {
                answer = "",
                confirmpassword = password,
                email = "",
                password = password,
                phoneNumber = phoneNumber,
                phoneVerificationCode = phoneVerificationCode,
                question = "",
                userName = userName
            };
            VerificationCode verificationCode = await PostJson<EmailRegisterPostData, VerificationCode>(ServiceUri.RegisterByEmail, postData);

            return verificationCode;
        }

        /// <summary>
        /// 通过用户名查找能找回密码的方式
        /// </summary>
        /// <returns></returns>
        public async Task FindWayByName(string username)
        {
            FindPasswordByNamePostData postData = new FindPasswordByNamePostData();
            postData.request = new FindPasswordByNameRequest() { username = username };
            FindPasswordByName result = await PostJson<FindPasswordByNamePostData, FindPasswordByName>(ServiceUri.FindPassword,postData);
            
        }

        /// <summary>
        /// 获取订阅专题
        /// </summary>
        /// <param name="nodeIds">48,51</param>
        /// <returns></returns>
        public async Task<List<Essay>> GetSubscribeTopic(string nodeIds,int pageIndex)
        {
            string filename = "subscribeTopic_" + nodeIds + "_"+pageIndex+ ".json";
            EssayResult essayResult = new EssayResult();
            if (!ConnectionHelper.IsInternetAvailable)
            {
                essayResult.Result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {
                SubscribeTopicPostData postData = new SubscribeTopicPostData();
                postData.request = new SubscribeTopicRequest
                {
                    elementsCountPerPage = "20",
                    lastUpdateTime = Functions.GetUnixTimeStamp().ToString(),
                    nodeIds = nodeIds,
                    pageIndex = pageIndex,
                    parentNodeId = "dingyue",
                    type = "dingyueTopic"
                };
                essayResult = await PostJson<SubscribeTopicPostData, EssayResult>(ServiceUri.SubscribeTopic, postData);
                if (essayResult != null && essayResult.Result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Essay>>(essayResult.Result, filename);
                }
            }
            return essayResult?.Result;
        }

        /// <summary>
        /// 获取订阅内容 （订阅页面）
        /// </summary>
        /// <param name="nodeIds">48,51</param>
        /// <returns></returns>
        //public async Task<EssayResult> GetSubscribeContent(string nodeIds,int pageIndex)
        //{
        //    string filename = "subscribeContent_" + nodeIds + "_" + pageIndex + ".json";
        //    EssayResult essayResult = new EssayResult();
        //    if (!ConnectionHelper.IsInternetAvailable)
        //    {
        //        essayResult.result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
        //    }
        //    else
        //    {
        //        SubscribeContentPostData postData = new SubscribeContentPostData();
        //        postData.request = new SubscribeContentRequest { nodeIds = nodeIds, pageIndex = pageIndex, type = "newsList" };
        //        essayResult = await PostJson<SubscribeContentPostData, EssayResult>(ServiceUri.SubscribeContent, postData);
        //    }
        //        return essayResult;
        //}
        
        /// <summary>
        /// 对订阅进行操作
        /// </summary>
        /// <param name="operate"></param>
        /// <param name="subscribeId"></param>
        /// <returns></returns>
        public async Task<VerificationCode> EditSubscribe(SubscribeOperateEnum operate,string subscribeId)
        {
            EditSubscribePostData postData = new EditSubscribePostData();
            postData.request = new EditSubscribeRequest { operate = operate.ToString(), subscribeId = subscribeId };
            VerificationCode result = await PostJson<EditSubscribePostData, VerificationCode>(ServiceUri.EditSubscription, postData);
            return result;
        }

        /// <summary>
        /// 获取游戏库中游戏列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Game>> GetGameList(int pageIndex)
        {
            string filename = "GameList_"+pageIndex+".json";
            List<Game> gameList = new List<Game>();
            if (!ConnectionHelper.IsInternetAvailable)
            {
                gameList = await FileHelper.Current.ReadObjectAsync<List<Game>>(filename);
            }
            else
            {
                GamePostData postData = new GamePostData();
                postData.request = new GamePostDataRequest() { pageIndex = pageIndex };
                var result = await PostJson<GamePostData, GameResult>(ServiceUri.GameList, postData);
                if(result!= null)
                {
                    foreach (var item in result.Result)
                    {
                        gameList.Add(item);
                    }
                    await FileHelper.Current.WriteObjectAsync<List<Game>>(gameList,filename);
                }
            }
            return gameList;
        }

        /// <summary>
        /// 游戏发售表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public async Task<List<Game>> GetGameReleaseList(int pageIndex,GameNodeIdEnum nodeId)
        {
            string filename = "GameReleaseList_" + pageIndex + ".json";
            List<Game> gameList = new List<Game>();
            if (!ConnectionHelper.IsInternetAvailable)
            {
                gameList = await FileHelper.Current.ReadObjectAsync<List<Game>>(filename);
            }
            else
            {
                GamePostData postData = new GamePostData();
                postData.request = new GamePostDataRequest() { pageIndex = pageIndex ,nodeIds = nodeId.ToString()};
                var result = await PostJson<GamePostData, GameResult>(ServiceUri.GameList, postData);
                if (result != null)
                {
                    foreach (var item in result.Result)
                    {
                        gameList.Add(item);
                    }
                    await FileHelper.Current.WriteObjectAsync(gameList, filename);
                }
            }
            return gameList;
        }

        /// <summary>
        /// 获取游戏详情
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public async Task<GameDetail> GetGameDetail(string contentId)
        {
            GameDetail gameDetail = new GameDetail();
            string fileName = "GameDetail_" + contentId + ".json";
            if (!ConnectionHelper.IsInternetAvailable)
            {
                gameDetail = await FileHelper.Current.ReadObjectAsync<GameDetail>(fileName);
            }
            else
            {
                GameDetailPostData postData = new GameDetailPostData();
                postData.request = new GameDetailRequest() { contentId = contentId };
                var gameDetailResult = await PostJson<GameDetailPostData, GameDetailResult>(ServiceUri.GameDetail, postData);
                gameDetail = gameDetailResult?.Result;
            }
            return gameDetail;
        }

        /// <summary>
        /// 获取GameDetail页面中 攻略或新闻
        /// </summary>
        /// <returns></returns>
        public async Task<List<GameDetailEssay>> GetGameDetailItem(string contentId, int pageIndex,string contentType)
        {
            List<GameDetailEssay> essay = new List<GameDetailEssay>();
            if (!ConnectionHelper.IsInternetAvailable)
            { 
            }
            else
            {
                GameDetailEssayPostData postData = new GameDetailEssayPostData();
                postData.request = new GameDetailEssayRequest() { contentId = contentId, contentType = contentType, elementsCountPerPage = 10, pageIndex = pageIndex };
                var gameDetailResult = await PostJson<GameDetailEssayPostData, GameDetailEssayResult>(ServiceUri.TwoCorrelation, postData);
                essay = gameDetailResult?.Result;
            }
            return essay;
        }

        /// <summary>
        /// 获取GameDetail页面中 攻略
        /// </summary>
        /// <returns></returns>
        public async Task<List<GameDetailEssay>> GetGameDetailStrategys(string contentId,int pageIndex)
        {
            List<GameDetailEssay> essays = new List<GameDetailEssay>();
            if (!ConnectionHelper.IsInternetAvailable)
            {
            }
            else
            {
                essays =  await GetGameDetailItem(contentId, pageIndex, "strategy");
            }
            return essays;
        }

        /// <summary>
        /// 获取GameDetail页面中 新闻
        /// </summary>
        /// <returns></returns>
        public async Task<List<GameDetailEssay>> GetGameDetailNews(string contentId, int pageIndex)
        {
            List<GameDetailEssay> essays = new List<GameDetailEssay>();
            if (!ConnectionHelper.IsInternetAvailable)
            {
            }
            else
            {
                essays = await GetGameDetailItem(contentId, pageIndex, "news");
            }
            return essays;
        }
    }
}
