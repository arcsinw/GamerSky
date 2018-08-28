using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using System.Net;
using GamerSky.Models; 
using GamerSky.Models.PostDataModel;
using GamerSky.Utils;
using GamerSky.Models.ResultDataModel;
using GamerSky.Enums;
using GamerSky.Models.Group;
using GamerSky.Requests;
using GamerSky.Responses;
using GamerSky.Models.Me;
using GamerSky.Requests.Me;

namespace GamerSky.Services
{
    public class ApiService : ApiBaseService
    {
        #region Singleton
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
        #endregion

        private string LocalFolder = ApplicationData.Current.LocalFolder.Path;

        /// <summary>
        /// 获取频道列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Channel>> GetChannelList()
        {
            string filename = "channelList.json";

            ResultDataTemplate<List<Channel>> channelResult = new ResultDataTemplate<List<Channel>>();
            
            channelResult.Result = await FileHelper.Current.ReadObjectAsync<List<Channel>>(filename);
            
            if (channelResult.Result == null)
            {
                PostDataTemplate<AllChannelListRequest> postData = new PostDataTemplate<AllChannelListRequest>()
                {
                    deviceId = DeviceInformationHelper.GetDeviceId(),
                    request = new AllChannelListRequest() { type = "0" }
                };
                
                channelResult = await PostJson<PostDataTemplate<AllChannelListRequest>, ResultDataTemplate<List<Channel>>>(ServiceUri.AllChannel, postData);
                if (channelResult != null && channelResult.Result != null)
                {
                    await FileHelper.Current.WriteObjectAsync(channelResult.Result, filename);
                }
            }

            channelResult.Result.Insert(0,new Channel { IsTop = "False", NodeId = "0", NodeName = "头条" });
            channelResult.Result.Insert(1, new Channel { IsTop = "False", NodeId = "9", NodeName = "订阅" });
            channelResult.Result.Insert(1, new Channel { IsTop = "False", NodeId = "9", NodeName = "视频" });
            return channelResult.Result;  
        }
        
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="nodeId">频道ID</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<List<Essay>> GetEssayList(string nodeId, int pageIndex)
        {
            string filename = "essayList_"+nodeId+"_"+pageIndex+".json";
            ResultDataTemplate<List<Essay>> essayResult = new ResultDataTemplate<List<Essay>>();
            if (!ConnectionHelper.IsInternetAvailable) //无网络
            {
                essayResult.Result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {

                PostDataTemplate<AllChannelListRequest> postData = new PostDataTemplate<AllChannelListRequest>()
                {
                    request = new AllChannelListRequest()
                    {
                        elementsCountPerPage = 20,
                        nodeIds = nodeId,
                        pageIndex = pageIndex,
                        parentNodeId = "news",
                    }
                };

                essayResult = await PostJson<PostDataTemplate<AllChannelListRequest>, ResultDataTemplate<List<Essay>>>(ServiceUri.AllChannelList, postData);
                if (essayResult != null && essayResult.Result!=null)
                {
                    await FileHelper.Current.WriteObjectAsync(essayResult.Result, filename);
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
        public async Task<News> GetEssay(string contentId)
        {
            string filename = "news_" + contentId + ".json";
            ResultDataTemplate<News> newsResult = new ResultDataTemplate<News>();
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                newsResult.Result = await FileHelper.Current.ReadObjectAsync<News>(filename);
            }
            else
            {
                PostDataTemplate<AllChannelListRequest> postData = new PostDataTemplate<AllChannelListRequest>()
                {
                    request = new AllChannelListRequest
                    {
                        contentId = contentId,
                        pageIndex = 1
                    },
                };

                newsResult = await PostJson<PostDataTemplate<AllChannelListRequest>, ResultDataTemplate<News>>(ServiceUri.TwoArticle, postData);
                if (newsResult != null && newsResult.Result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<News>(newsResult.Result, filename);
                }
            }

            return newsResult?.Result;
        }

       

        #region Isolated APIs
        ///// <summary>
        ///// 获取某文章所有评论
        ///// </summary>
        ///// <param name="contentId"></param>
        ///// <param name="pageIndex"></param>
        ///// <returns></returns>
        //public async Task<List<Comment>> GetAllComments(string contentId,int pageIndex)
        //{
        //    List<Comment> comments = new List<Comment>();
        //    if(ConnectionHelper.IsInternetAvailable)
        //    {
        //        GetAllCommentPostData postData = new GetAllCommentPostData()
        //        {
        //            pageIndex = pageIndex,
        //            pageSize = 20,
        //            topicId = contentId
        //        };
        //        var result = await GetJson<ResultDataTemplate<EssayComments>>(string.Format(ServiceUri.AllComments, WebUtility.UrlEncode(JsonHelper.Serializer(postData))));
        //        if(result!=null)
        //        {
        //            comments = result.Result.Result;
        //        }
        //    }
        //    return comments;
        //}

        ///// <summary>
        ///// 获取热门评论
        ///// </summary>
        ///// <param name="contentId"></param>
        ///// <param name="pageIndex"></param>
        ///// <returns></returns>
        //public async Task<List<Comment>> GetHotComments(string contentId, int pageIndex)
        //{
        //    List<Comment> comments = new List<Comment>();
        //    if (ConnectionHelper.IsInternetAvailable)
        //    {
        //        GetAllCommentPostData postData = new GetAllCommentPostData()
        //        {
        //            pageIndex = pageIndex,
        //            pageSize = 20,
        //            topicId = contentId
        //        };
        //        var result = await GetJson<AllCommentsResult>(string.Format(ServiceUri.HotComments, WebUtility.UrlEncode(JsonHelper.Serializer(postData))));
        //        if (result != null)
        //        {
        //            comments = result.Result.Result;
        //        }
        //    }
        //    return comments;
        //}

        ///// <summary>
        ///// 获取相关阅读
        ///// </summary>
        ///// <param name="contentId">文章Id</param>
        ///// <returns></returns>
        //public async Task<List<RelatedReadings>> GetRelatedReadings(string contentId, string contentType="news")
        //{
        //    string filename = "relatedReadings_" + contentId + ".json";
        //    RelatedReadingsResult readings = new RelatedReadingsResult();
        //    if (!ConnectionHelper.IsInternetAvailable)  //无网络
        //    {
        //        readings.Result = await FileHelper.Current.ReadObjectAsync<List<RelatedReadings>>(filename);
        //    }
        //    else
        //    {
        //        RelatedReadingPostData postData = new RelatedReadingPostData();
        //        postData.request = new RelatedReadingRequest{ contentId = contentId, contentType = contentType};
        //        postData.deviceId = DeviceInformationHelper.GetDeviceId();
        //        readings = await PostJson<RelatedReadingPostData, RelatedReadingsResult>(ServiceUri.TwoCorrelation, postData);
        //        if (readings != null && readings.Result != null)
        //        {
        //            await FileHelper.Current.WriteObjectAsync(readings.Result, filename);
        //        }
        //    }

        //    return readings?.Result;
        //} 
        #endregion

        /// <summary>
        /// 获取有攻略的游戏 关注
        /// </summary>
        /// <returns></returns>
        public async Task<List<Strategy>> GetStrategys(int pageCount = 20,int type=0)
        {
            string filename = "focusStrategys.json";
            ResultDataTemplate<List<Strategy>> strategyResult = new ResultDataTemplate<List<Strategy>>();
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                strategyResult.Result = await FileHelper.Current.ReadObjectAsync<List<Strategy>>(filename);
            }
            else
            {
                PostDataTemplate<StrategyRequest> postData = new PostDataTemplate<StrategyRequest>
                {
                    request = new StrategyRequest { pageIndex = 1, pageCount = pageCount, type = type }
                };

                strategyResult = await PostJson<PostDataTemplate<StrategyRequest>, ResultDataTemplate<List<Strategy>>>(ServiceUri.Strategy, postData);
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
        public async Task<List<Essay>> GetGameStrategys(string specialID, int pageIndex=1)
        {
            ResultDataTemplate<List<Essay>> essayResult = new ResultDataTemplate<List<Essay>>();
            string filename = "gameStrategys_" + specialID + ".json";
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                essayResult.Result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {
                PostDataTemplate<AllChannelListRequest> postData = new PostDataTemplate<AllChannelListRequest>()
                {
                    request = new AllChannelListRequest()
                    {
                        elementsCountPerPage = 20,
                        nodeIds = specialID,
                        pageIndex = pageIndex,
                        parentNodeId = "strategy"
                    }
                };
                
                essayResult = await PostJson<PostDataTemplate<AllChannelListRequest>, ResultDataTemplate<List<Essay>>>(ServiceUri.GameStrategys, postData);
                if (essayResult != null && essayResult.Result != null)
                {
                    await FileHelper.Current.WriteObjectAsync(essayResult.Result, filename);
                }
            }

            return essayResult?.Result ;
        }
        
        /// <summary>
        /// 获取搜索热点词
        /// </summary>
        /// <param name="searchType">热点词类型 strategy news shouyou</param>
        /// <returns></returns>
        public async Task<List<string>> GetSearchHotKey(string searchType = "strategy")
        {
            string filename = "searchHotKey_"+searchType+".json";
            ResultDataTemplate<List<string>> searchResult = new ResultDataTemplate<List<string>>();
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                searchResult.Result = await FileHelper.Current.ReadObjectAsync<List<string>>(filename);
            }
            else
            {
                PostDataTemplate<SearchRequest> postData = new PostDataTemplate<SearchRequest>()
                {
                    request = new SearchRequest
                    {
                        searchType = searchType
                    }
                };
                searchResult = await PostJson<PostDataTemplate<SearchRequest>, ResultDataTemplate<List<string>>>(ServiceUri.SearchHotDict, postData);

                if (searchResult != null && searchResult.Result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<string>>(searchResult.Result, filename);
                }
            }

            return searchResult?.Result;
        }

        /// <summary>
        /// 获取热门/全部订阅
        /// </summary>
        /// <param name="type">type=0 热门 type=1 全部</param>
        public async Task<List<Subscribe>> GetSubscribeHotKey(string type="1")
        {
            string filename = "subscribeHotKey_" + type + ".json";
            ResultDataTemplate<List<Subscribe>> subscribeResult = new ResultDataTemplate<List<Subscribe>>();
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                subscribeResult.Result = await FileHelper.Current.ReadObjectAsync<List<Subscribe>>(filename);
            }
            else
            {
                PostDataTemplate<SubscribeSearchRequest> postData = new PostDataTemplate<SubscribeSearchRequest>
                {
                    request = new SubscribeSearchRequest { type = type }
                };

                subscribeResult = await PostJson<PostDataTemplate<SubscribeSearchRequest>, ResultDataTemplate<List<Subscribe>>>(ServiceUri.Subscribe, postData);
                if (subscribeResult != null && subscribeResult.Result != null)
                {
                    await FileHelper.Current.WriteObjectAsync(subscribeResult.Result, filename);
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
            ResultDataTemplate<List<Essay>> essayResult = new ResultDataTemplate<List<Essay>>();
            if (!ConnectionHelper.IsInternetAvailable) //无网络
            {
            }
            else
            {
                PostDataTemplate<SearchRequest> postData = new PostDataTemplate<SearchRequest>()
                {
                    request = new SearchRequest()
                    {
                        elementsCountPerPage = "20",
                        pageIndex = pageIndex,
                        searchKey = searchKey,
                        searchType = searchType.ToString()
                    }
                };
                
                essayResult = await PostJson<PostDataTemplate<SearchRequest>, ResultDataTemplate<List<Essay>>>(ServiceUri.Search, postData);
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
            ResultDataTemplate<List<Essay>> subscribeContent = new ResultDataTemplate<List<Essay>>();
            string filename = "subscribeContent_" + sourceId + ".json";
            if (!ConnectionHelper.IsInternetAvailable)  //无网络
            {
                subscribeContent.Result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {
                PostDataTemplate<SubscribeContentRequest> postData = new PostDataTemplate<SubscribeContentRequest>
                {
                    request = new SubscribeContentRequest { nodeIds = sourceId, pageCount = 10, pageIndex = pageIndex }
                };

                subscribeContent = await PostJson<PostDataTemplate<SubscribeContentRequest>, ResultDataTemplate<List<Essay>>>(ServiceUri.SubscribeContent, postData);

                if (subscribeContent != null && subscribeContent.Result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Essay>>(subscribeContent.Result, filename);
                }
            }

            return subscribeContent?.Result;
        }

        ///// <summary>
        ///// 登录
        ///// </summary>
        ///// <param name="passWord">用户名</param>
        ///// <param name="userName">密码</param>
        ///// <returns></returns>
        //public async Task<LoginResult> Login(string passWord, string userName)
        //{
        //    PostDataTemplate<LoginRequest> postData = new PostDataTemplate<LoginRequest>();
        //    postData.request = new LoginPostDataRequest { passWord = passWord, userName = userName };
        //    var result = await PostJson<LoginPostData, LoginResult>(ServiceUri.Login, postData);
        //    return result;
        //}
         
        public void SinaLogin()
        {
            
        }

        //#region Comment
        ///// <summary>
        ///// Add a comment
        ///// </summary>
        ///// <param name="loginToken"></param>
        ///// <param name="topicId"></param>
        ///// <param name="topicUrl"></param>
        ///// <param name="topicTitle"></param>
        ///// <param name="content"></param>
        ///// <param name="replyId"></param>
        ///// <returns></returns>
        //public async Task<AddCommentResult> AddComment(string loginToken, string topicId,
        //    string topicUrl, string topicTitle, string content, string replyId)
        //{
        //    CommentRequest postData = new CommentRequest
        //    {
        //        LoginToken = loginToken,
        //        TopicId = topicId,
        //        TopicTitle = topicTitle,
        //        TopicUrl = topicUrl,
        //        Content = content,
        //        ReplyId = replyId
        //    };

        //    var result = await GetJson<AddCommentResult>(string.Format(ServiceUri.AddComment, WebUtility.UrlEncode(JsonHelper.Serializer(postData))));
        //    return result;
        //}

        
        ///// <summary>
        ///// 给评论回复点赞
        ///// {"action":"ding","topicId":855635,"commentID":1080276461,"commentUserID":"531939"}
        ///// </summary>
        //public async Task AddLikeComment(string topicId, string commentId, string commentUserId)
        //{
        //    string jsonData = "{ \"action\":\"ding\",\"topicId\":" + topicId + ",\"commentID\":" + commentId + ",\"commentUserID\":" + commentUserId + "}";
        //    string url = string.Format(ServiceUri.LikeReply, jsonData);
        //    LikeCommentResult result = await GetJson<LikeCommentResult>(url);
        //}

        //#endregion

        /// <summary>
        /// 获取要闻
        /// </summary>
        /// <returns></returns>
        public async Task<List<Essay>> GetYaowen(int pageIndex=1)
        {
            string filename = "yaowen.json";
            ResultDataTemplate<List<Essay>> essayResult = new ResultDataTemplate<List<Essay>>();
            if (!ConnectionHelper.IsInternetAvailable) //无网络
            {
                essayResult.Result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {
                PostDataTemplate<YaowenRequest> postData = new PostDataTemplate<YaowenRequest>()
                {
                    request = new YaowenRequest()
                    {
                        pageIndex = pageIndex
                    }
                };
                
                essayResult = await PostJson<PostDataTemplate<YaowenRequest>, ResultDataTemplate<List<Essay>>>(ServiceUri.AllChannelList, postData);
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
                PostDataTemplate<string> postData = new PostDataTemplate<string>();
                ResultDataTemplate<List<AdStart>> adStartResult = await PostJson<PostDataTemplate<string>, ResultDataTemplate<List<AdStart>>>(ServiceUri.AdStart, postData);
                
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
            PostDataTemplate<VerificationCodeRequest> postData = new PostDataTemplate<VerificationCodeRequest>
            {
                request = new VerificationCodeRequest() { codetype = codetype, email = email, phoneNumber = phoneNumber, username = username }
            };

            VerificationCode verificationCode = await PostJson<PostDataTemplate<VerificationCodeRequest>, VerificationCode>(ServiceUri.GetVerificationCode, postData);
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
            PostDataTemplate<AccountRegisterRequest> postData = new PostDataTemplate<AccountRegisterRequest>
            {
                request = new AccountRegisterRequest()
                {
                    answer = answer,
                    confirmpassword = password,
                    email = email,
                    password = password,
                    phoneNumber = "",
                    phoneVerificationCode = "",
                    question = question,
                    userName = userName
                }
            };
            VerificationCode verificationCode = await PostJson<PostDataTemplate<AccountRegisterRequest>, VerificationCode>(ServiceUri.RegisterByEmail, postData);

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
            PostDataTemplate<AccountRegisterRequest> postData = new PostDataTemplate<AccountRegisterRequest>
            {
                request = new AccountRegisterRequest()
                {
                    answer = "",
                    confirmpassword = password,
                    email = "",
                    password = password,
                    phoneNumber = phoneNumber,
                    phoneVerificationCode = phoneVerificationCode,
                    question = "",
                    userName = userName
                }
            };

            VerificationCode verificationCode = await PostJson<PostDataTemplate<AccountRegisterRequest>, VerificationCode>(ServiceUri.RegisterByEmail, postData);

            return verificationCode;
        }

        ///// <summary>
        ///// 通过用户名查找能找回密码的方式
        ///// </summary>
        ///// <returns></returns>
        //public async Task FindWayByName(string username)
        //{
        //    FindPasswordByNamePostData postData = new FindPasswordByNamePostData();
        //    postData.request = new FindPasswordByNameRequest() { username = username };
        //    FindPasswordByName result = await PostJson<FindPasswordByNamePostData, FindPasswordByName>(ServiceUri.FindPassword,postData);
            
        //}

        /// <summary>
        /// 获取订阅专题
        /// </summary>
        /// <param name="nodeIds">48,51</param>
        /// <returns></returns>
        public async Task<List<Essay>> GetSubscribeTopic(string nodeIds,int pageIndex)
        {
            string filename = "subscribeTopic_" + nodeIds + "_"+pageIndex+ ".json";
            ResultDataTemplate<List<Essay>> essayResult = new ResultDataTemplate<List<Essay>>();
            if (!ConnectionHelper.IsInternetAvailable)
            {
                essayResult.Result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {
                PostDataTemplate<SubscribeTopicRequest> postData = new PostDataTemplate<SubscribeTopicRequest>
                {
                    request = new SubscribeTopicRequest
                    {
                        elementsCountPerPage = "20",
                        lastUpdateTime = Functions.GetUnixTimeStamp().ToString(),
                        nodeIds = nodeIds,
                        pageIndex = pageIndex,
                        parentNodeId = "dingyue",
                        type = "dingyueTopic"
                    }
                };

                essayResult = await PostJson<PostDataTemplate<SubscribeTopicRequest>, ResultDataTemplate<List<Essay>>>(ServiceUri.SubscribeTopic, postData);
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
            PostDataTemplate<EditSubscribeRequest> postData = new PostDataTemplate<EditSubscribeRequest>
            {
                request = new EditSubscribeRequest { operate = operate.ToString(), subscribeId = subscribeId }
            };

            VerificationCode result = await PostJson<PostDataTemplate<EditSubscribeRequest>, VerificationCode>(ServiceUri.EditSubscription, postData);
            return result;
        }

        #region GamePage-Old Edition

        /// <summary>
        /// 获取游戏库中游戏列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Game>> GetGameList(int pageIndex)
        {
            string filename = "GameList_" + pageIndex + ".json";
            List<Game> gameList = new List<Game>();
            if (!ConnectionHelper.IsInternetAvailable)
            {
                gameList = await FileHelper.Current.ReadObjectAsync<List<Game>>(filename);
            }
            else
            {
                PostDataTemplate<GameRequest> postData = new PostDataTemplate<GameRequest>
                {
                    request = new GameRequest() { pageIndex = pageIndex }
                };

                var result = await PostJson<PostDataTemplate<GameRequest>, ResultDataTemplate<List<Game>>>(ServiceUri.GameList, postData);
                if (result != null)
                {
                    foreach (var item in result.Result)
                    {
                        gameList.Add(item);
                    }
                    await FileHelper.Current.WriteObjectAsync<List<Game>>(gameList, filename);
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
        public async Task<List<Game>> GetGameReleaseList(int pageIndex, GameNodeIdEnum nodeId)
        {
            string filename = "GameReleaseList_" + pageIndex + ".json";
            List<Game> gameList = new List<Game>();
            if (!ConnectionHelper.IsInternetAvailable)
            {
                gameList = await FileHelper.Current.ReadObjectAsync<List<Game>>(filename);
            }
            else
            {
                PostDataTemplate<GameRequest> postData = new PostDataTemplate<GameRequest>
                {
                    request = new GameRequest() { pageIndex = pageIndex, nodeIds = nodeId.ToString() }
                };

                var result = await PostJson<PostDataTemplate<GameRequest>, ResultDataTemplate<List<Game>>>(ServiceUri.GameList, postData);
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
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<GameDetail> GetGameDetailAsync(string gameId)
        {
            GameDetail gameDetail = new GameDetail();
            string fileName = "GameDetail_" + gameId + ".json";
            if (!ConnectionHelper.IsInternetAvailable)
            {
                gameDetail = await FileHelper.Current.ReadObjectAsync<GameDetail>(fileName);
            }
            else
            {
                PostDataTemplate<GameDetailRequest> postData = new PostDataTemplate<GameDetailRequest>
                {
                    request = new GameDetailRequest() { contentId = gameId }
                };

                var gameDetailResult = await PostJson<PostDataTemplate<GameDetailRequest>, ResultDataTemplate<GameDetail>>(ServiceUri.GameDetail, postData);
                gameDetail = gameDetailResult?.Result;
            }
            return gameDetail;
        }

        /// <summary>
        /// 获取GameDetail页面中 攻略或新闻
        /// </summary>
        /// <returns></returns>
        public async Task<List<Essay>> GetGameDetailItem(string contentId, int pageIndex, string contentType)
        {
            List<Essay> essay = new List<Essay>();
            if (!ConnectionHelper.IsInternetAvailable)
            {
            }
            else
            {
                PostDataTemplate<GameDetailEssayRequest> postData = new PostDataTemplate<GameDetailEssayRequest>
                {
                    request = new GameDetailEssayRequest() { contentId = contentId, contentType = contentType, elementsCountPerPage = 10, pageIndex = pageIndex }
                };

                var gameDetailResult = await PostJson<PostDataTemplate<GameDetailEssayRequest>, ResultDataTemplate<List<Essay>>>(ServiceUri.TwoCorrelation, postData);
                essay = gameDetailResult?.Result;
            }

            return essay;
        }

        /// <summary>
        /// 获取GameDetail页面中 攻略
        /// </summary>
        /// <returns></returns>
        public async Task<List<Essay>> GetGameDetailStrategys(string contentId, int pageIndex)
        {
            List<Essay> essays = new List<Essay>();
            if (!ConnectionHelper.IsInternetAvailable)
            {
            }
            else
            {
                essays = await GetGameDetailItem(contentId, pageIndex, "strategy");
            }
            return essays;
        }

        /// <summary>
        /// 获取GameDetail页面中 新闻
        /// </summary>
        /// <returns></returns>
        public async Task<List<Essay>> GetGameDetailNews(string contentId, int pageIndex)
        {
            List<Essay> essays = new List<Essay>();
            if (!ConnectionHelper.IsInternetAvailable)
            {
            }
            else
            {
                essays = await GetGameDetailItem(contentId, pageIndex, "news");
            }
            return essays;
        }


        #endregion

        #region 游戏

        /// <summary>
        /// 新游推荐
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount">查看更多中设置为20</param>
        /// <returns></returns>
        public async Task<ResultDataTemplate<List<GameSpecialDetail>>> GetGameSpecialDetail(int pageIndex = 1, int pageCount = 4)
        {
            PostDataTemplate<GameSpecialDetailRequest> postData = new PostDataTemplate<GameSpecialDetailRequest>()
            {
                request = new GameSpecialDetailRequest()
                {
                    ElementsCountPerPage = 4,
                    ExtraField1 = "Position",
                    ExtraField2 = "gsScore",
                    ExtraField3 = "largeImage,description",
                    NodeId = "13",
                    PageIndex = pageIndex,
                }
            };

            ResultDataTemplate<List<GameSpecialDetail>> result = await PostJson<PostDataTemplate<GameSpecialDetailRequest>, ResultDataTemplate<List<GameSpecialDetail>>>(ServiceUri.GameSpecialDetail, postData);

            return result;
        }

        /// <summary>
        /// 近期热门
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<ResultDataTemplate<List<GameSpecialDetail>>> GetGameHomePage(int pageIndex = 1)
        {
            PostDataTemplate<GameHomePageRequest> postData = new PostDataTemplate<GameHomePageRequest>()
            {
                request = new GameHomePageRequest()
                {
                    ElementsCountPerPage = 5,
                    ExtraField1 = "Position",
                    ExtraField2 = "gsScore",
                    Group = "recent-hot",
                    PageIndex = pageIndex,
                }
            };

            ResultDataTemplate<List<GameSpecialDetail>> result = await PostJson<PostDataTemplate<GameHomePageRequest>, ResultDataTemplate<List<GameSpecialDetail>>>(ServiceUri.GameHomePage, postData);

            return result;
        }

        /// <summary>
        /// 特色专题
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount">5 || 20 （20为特色专题独立页面）</param>
        /// <returns></returns>
        public async Task<ResultDataTemplate<List<GameSpecial>>> GetGameSpecialList(int pageIndex = 1, int pageCount = 5)
        {
            PostDataTemplate<GameSpecialListRequest> postData = new PostDataTemplate<GameSpecialListRequest>()
            {
                request = new GameSpecialListRequest()
                { 
                    PageIndex = pageIndex,
                    ElementsCountPerPage = pageCount,
                }
            };

            ResultDataTemplate<List<GameSpecial>> result = await PostJson<PostDataTemplate<GameSpecialListRequest>, ResultDataTemplate<List<GameSpecial>>>(ServiceUri.GameSpecialList, postData);

            return result;
        }

        /// <summary>
        /// 即将上市
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<ResultDataTemplate<List<GameSpecialDetail>>> GetNewSellGames(int pageIndex = 1)
        {
            PostDataTemplate<GameHomePageRequest> postData = new PostDataTemplate<GameHomePageRequest>()
            {
                request = new GameHomePageRequest()
                {
                    ElementsCountPerPage = 5,
                    ExtraField1 = "Position",
                    ExtraField2 = "gsScore",
                    Group = "new-selling",
                    PageIndex = pageIndex,
                }
            };

            ResultDataTemplate<List<GameSpecialDetail>> result = await PostJson<PostDataTemplate<GameHomePageRequest>, ResultDataTemplate<List<GameSpecialDetail>>>(ServiceUri.GameHomePage, postData);

            return result;
        }

        /// <summary>
        /// 高分榜
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<ResultDataTemplate<List<GameSpecialDetail>>> GetHighRank(int pageIndex = 1)
        {
            PostDataTemplate<GameRankingListRequest> postData = new PostDataTemplate<GameRankingListRequest>()
            {
                request = new GameRankingListRequest()
                {
                    Type = "fractions",
                    PageIndex = pageIndex,
                }
            };

            ResultDataTemplate<List<GameSpecialDetail>> result = await PostJson<PostDataTemplate<GameRankingListRequest>, ResultDataTemplate<List<GameSpecialDetail>>>(ServiceUri.GameRankingList, postData);
            return result;
        }

        /// <summary>
        /// 热门榜
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<ResultDataTemplate<List<GameSpecialDetail>>> GetMostExpected(int pageIndex = 1)
        {
            PostDataTemplate<GameRankingListRequest> postData = new PostDataTemplate<GameRankingListRequest>()
            {
                request = new GameRankingListRequest()
                {
                    Type = "hot",
                    PageIndex = pageIndex,
                }
            };

            ResultDataTemplate<List<GameSpecialDetail>> result = await PostJson<PostDataTemplate<GameRankingListRequest>, ResultDataTemplate<List<GameSpecialDetail>>>(ServiceUri.GameRankingList, postData);
            return result;
        }

        /// <summary>
        /// 高分FPS
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<ResultDataTemplate<List<GameSpecialDetail>>> GetHighRankedFPS(int pageIndex = 1)
        {
            PostDataTemplate<GameRankingListRequest> postData = new PostDataTemplate<GameRankingListRequest>()
            {
                request = new GameRankingListRequest()
                {
                    Type = "fractions",
                    GameClass = 20066,
                    PageIndex = pageIndex,
                }
            };

            ResultDataTemplate<List<GameSpecialDetail>> result = await PostJson<PostDataTemplate<GameRankingListRequest>, ResultDataTemplate<List<GameSpecialDetail>>>(ServiceUri.GameRankingList, postData);
            return result;
        }

        /// <summary>
        /// 高分ACT
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<ResultDataTemplate<List<GameSpecialDetail>>> GetHighRankedACT(int pageIndex = 1)
        {
            PostDataTemplate<GameRankingListRequest> postData = new PostDataTemplate<GameRankingListRequest>()
            {
                request = new GameRankingListRequest()
                {
                    Type = "fractions",
                    GameClass = 20042,
                    PageIndex = pageIndex,
                }
            };

            ResultDataTemplate<List<GameSpecialDetail>> result = await PostJson<PostDataTemplate<GameRankingListRequest>, ResultDataTemplate<List<GameSpecialDetail>>>(ServiceUri.GameRankingList, postData);
            return result;
        }

        /// <summary>
        /// 最期待游戏
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<ResultDataTemplate<List<GameSpecialDetail>>> GetMostExpectedGames(int pageIndex = 1)
        {
            PostDataTemplate<GameHomePageRequest> postData = new PostDataTemplate<GameHomePageRequest>()
            {
                request = new GameHomePageRequest()
                {
                    ElementsCountPerPage = 5,
                    ExtraField1 = "Position",
                    ExtraField2 = "gsScore",
                    Group = "most-expected",
                    PageIndex = pageIndex,
                }
            };

            ResultDataTemplate<List<GameSpecialDetail>> result = await PostJson<PostDataTemplate<GameHomePageRequest>, ResultDataTemplate<List<GameSpecialDetail>>>(ServiceUri.GameHomePage, postData);

            return result;
        }
        
        /// <summary>
        /// 获取游戏Detail
        /// </summary>
        /// <param name="gameId"></param>
        /// <returns></returns>
        public async Task<GameDetailV4> GetGameDetailV4Async(string gameId)
        {
            PostDataTemplate<GameDetailRequestV4> postData = new PostDataTemplate<GameDetailRequestV4>()
            {
                request = new GameDetailRequestV4()
                {
                    gameId = gameId
                }
            };

            ResultDataTemplate<GameDetailV4> result = await PostJson<PostDataTemplate<GameDetailRequestV4>, ResultDataTemplate<GameDetailV4>> (ServiceUri.GetGame, postData);

            return result?.Result;
        }
        
        /// <summary>
        /// 热门点评
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<List<GameReview>> GetGameReview(int pageIndex)
        {
            PostDataTemplate<GameReviewRequest> postData = new PostDataTemplate<GameReviewRequest>()
            {
                request = new GameReviewRequest()
                {
                    extraField1 = "Position,GameType,wantplayCount,scoreUserCount",
                    extraField2 = "gsScore,gameTag",
                    pageIndex = pageIndex,
                    type = "wanGuoZuiXin",
                }
            };

            ResultDataTemplate<List<GameReview>> result = await PostJson<PostDataTemplate<GameReviewRequest>, ResultDataTemplate<List<GameReview>>>(ServiceUri.ReviewList, postData);

            return result?.Result;
        }

        /// <summary>
        /// 获取一个专题下的 子专题
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public async Task<List<GameSubList>> GetGameSpecialSubListAsync(string nodeId)
        {
            PostDataTemplate<GameSpecialSubListRequest> postData = new PostDataTemplate<GameSpecialSubListRequest>()
            {
                request = new GameSpecialSubListRequest()
                {
                    nodeId = nodeId,
                }
            };

            ResultDataTemplate<List<GameSubList>> result = await PostJson<PostDataTemplate<GameSpecialSubListRequest>, ResultDataTemplate<List<GameSubList>>>(ServiceUri.GameSpecialSubList, postData);

            return result?.Result ?? new List<GameSubList>();
        }

        /// <summary>
        /// 特色专题 点击
        /// 第一步 获取SubList
        /// 第二部 合并结果
        /// </summary>
        public async Task<List<Tuple<string,List<GameDetailV4>>>> GetGameSpecialSubjectContentAsync (string nodeId, int pageIndex = 1)
        {
            var subList = await GetGameSpecialSubListAsync(nodeId);
            List<Tuple<string, List<GameDetailV4>>> result = new List<Tuple<string, List<GameDetailV4>>>();

            foreach (var item in subList)
            {
                PostDataTemplate<GameSpecialDetailRequest> postData = new PostDataTemplate<GameSpecialDetailRequest>()
                {
                    request = new GameSpecialDetailRequest()
                    {
                        ExtraField1 = "Position,DeputyNodeId,EnTitle,AllTime,PCTime,PS4Time,XboxOneTime,NintendoSwitchTime",
                        ExtraField2 = "gsScore,wantplayCount,gameTag,playCount,isMarket",
                        ExtraField3 = "description",
                        PageIndex = pageIndex,
                        NodeId = nodeId,
                        ElementsCountPerPage = 1000
                    }
                };

                ResultDataTemplate<List<GameDetailV4>> resultData = await PostJson<PostDataTemplate<GameSpecialDetailRequest>, ResultDataTemplate<List<GameDetailV4>>>(ServiceUri.GameSpecialDetail, postData);
                Tuple<string, List<GameDetailV4>> tmp = new Tuple<string, List<GameDetailV4>>
                (
                    item.Title, resultData?.Result
                );
                result.Add(tmp);
            }
            
            return result;
        }
        #endregion

        #region 首页
        /// <summary>
        /// 首页 -> 订阅
        /// </summary>
        public async Task<List<Essay>> GetSubscribe(int pageIndex = 1)
        {
            PostDataTemplate<Requests.AllChannelListRequest> postData = new PostDataTemplate<Requests.AllChannelListRequest>
            {
                request = new Requests.AllChannelListRequest
                {
                    elementsCountPerPage = 20,
                    nodeIds = "101, 99, 100, 102, 118, 137, 126, 130, 120, 129, 119",
                    pageIndex = pageIndex,
                    parentNodeId = "dingyue",
                    type = "newsList"
                }
            };

            ResultDataTemplate<List<Essay>> result = await PostJson<PostDataTemplate<Requests.AllChannelListRequest>, ResultDataTemplate<List<Essay>>>(ServiceUri.AllChannelList, postData);
            return result?.Result;
        }
        #endregion

        #region 原创
        /// <summary>
        /// 全部栏目
        /// </summary>
        public async void GetAllColumn()
        {
            
        }

        #endregion

        #region 圈子
        /// <summary>
        /// 获取所有主题
        /// </summary>
        public async Task<List<Club>> GetClubsListAsync()
        {
            PostDataTemplate<ClubsListRequest> postData = new PostDataTemplate<ClubsListRequest>()
            {
                request = new ClubsListRequest()
                {
                    elementsPerPage = 20,
                    pageIndex = 1,
                    type = "tuiJian"
                }
            };
            ResultDataTemplate<List<Club>> result = 
                await PostJson<PostDataTemplate<ClubsListRequest>, ResultDataTemplate<List<Club>>>(ServiceUri.ClubsList, postData);
            
            return result.Result;    
        }

        /// <summary>
        /// 获取发帖 
        /// 点击话题的内容
        /// </summary>
        public async Task<List<Topic>> GetTopicsListAsync(int pageIndex, int subjectId)
        {
            PostDataTemplate<TopicsListRequest> postData = new PostDataTemplate<TopicsListRequest>()
            {
                request = new TopicsListRequest()
                {
                    pageIndex = pageIndex,
                    subjectId = subjectId,
                }
            };
            ResultDataTemplate<List<Topic>> result =
                await PostJson<PostDataTemplate<TopicsListRequest>, ResultDataTemplate<List<Topic>>>(ServiceUri.TopicsList, postData);
            
            return result.Result;
        }

        /// <summary>
        /// 获取全部话题
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<List<Club>> GetSubjectsListAsync(int pageIndex)
        {
            PostDataTemplate<SubjectsListRequest> postData = new PostDataTemplate<SubjectsListRequest>()
            {
                request = new SubjectsListRequest()
                {
                    pageIndex = pageIndex,
                }
            };
            ResultDataTemplate<List<Club>> result =
                await PostJson<PostDataTemplate<SubjectsListRequest>, ResultDataTemplate<List<Club>>>(ServiceUri.SubjectList, postData);

            return result?.Result;
        }

        #endregion

        #region 用户相关

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        public async Task<LoginResponse> Login(string userName, string password, string verifyCode = "")
        {
            PostDataTemplate<Requests.Me.LoginRequest> postData = new PostDataTemplate<Requests.Me.LoginRequest>()
            {
                request = new Requests.Me.LoginRequest()
                {
                    userInfo = userName,
                    password = password,
                    veriCode = verifyCode,
                }
            };

            var result = await PostJson<PostDataTemplate<Requests.Me.LoginRequest>, ResultDataTemplate<LoginResponse>>(ServiceUri.TwoLogin, postData);
            return result?.Result;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public async Task<UserInfo> GetUserInfo(string userId)
        {
            PostDataTemplate<GetUserInfoRequest> postData = new PostDataTemplate<GetUserInfoRequest>()
            {
                request = new GetUserInfoRequest()
                {
                    userId = userId,
                }
            };

            var result = await PostJson<PostDataTemplate<GetUserInfoRequest>, ResultDataTemplate<UserInfo>>(ServiceUri.TwoLogin, postData);
            return result?.Result;
        }

        
        /// <summary>
        /// 获取用户信息 TODO
        /// </summary>
        public async Task<UserInfo> GetUserSubscriptionColumns()
        {
            PostDataTemplate<GetUserInfoRequest> postData = new PostDataTemplate<GetUserInfoRequest>()
            {
                request = new GetUserInfoRequest()
                {
                    
                }
            };

            var result = await PostJson<PostDataTemplate<GetUserInfoRequest>, ResultDataTemplate<UserInfo>>(ServiceUri.TwoLogin, postData);
            return result?.Result;
        }

        /// <summary>
        /// 获取用户收藏
        /// </summary>
        /// <returns></returns>
        public async Task<List<CollectItem>> GetCollectList(int pageIndex = 1)
        {
            PostDataTemplate<GetCollectListRequest> postData = new PostDataTemplate<GetCollectListRequest>()
            {
                request = new GetCollectListRequest()
                {
                    pageIndex = pageIndex,
                }
            };

            var result = await PostJson<PostDataTemplate<GetCollectListRequest>, ResultDataTemplate<List<CollectItem>>>(ServiceUri.GetCollectList, postData);
            return result?.Result;
        }

        /// <summary>
        /// 获取用户收藏
        /// </summary>
        /// <returns></returns>
        public async Task GetUserDynamics(int pageIndex = 1)
        {
            PostDataTemplate<GetUserDynamicsRequest> postData = new PostDataTemplate<GetUserDynamicsRequest>()
            {
                request = new GetUserDynamicsRequest()
                {
                    pageIndex = pageIndex,
                }
            };

            var result = await PostJson<PostDataTemplate<GetUserDynamicsRequest>, ResultDataTemplate<List<CollectItem>>>(ServiceUri.GetUserDynamics, postData);
            
        }

        /// <summary>
        /// 获取用户游戏列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task GetUserGameList(int pageIndex = 1)
        {
            PostDataTemplate<GetUserGameListRequest> postData = new PostDataTemplate<GetUserGameListRequest>()
            {
                request = new GetUserGameListRequest()
                {
                    pageIndex = pageIndex,
                }
            };

            var result = await PostJson<PostDataTemplate<GetUserGameListRequest>, ResultDataTemplate<List<CollectItem>>>(ServiceUri.GetUserGameList, postData);

        }

        /// <summary>
        /// 获取用户的所有评论回复
        /// </summary>
        public async Task<List<Comment>> GetAllReply(string userId, int pageIndex)
        {
            string jsonData = "{\"userId\":" + userId + ",\"pageIndex\":" + pageIndex + ",\"pageSize\":20}";
            string url = string.Format(ServiceUri.GetUserComment, jsonData);
            ResultDataTemplate<UserComments> comments = await GetJson<ResultDataTemplate<UserComments>>(url);
            return comments.Result.Comments;
        }


        #endregion
    }
}
