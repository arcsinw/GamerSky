using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using 游民星空.Core.Helper;
using 游民星空.Core.Http;
using 游民星空.Core.Model;
using 游民星空.Core.PostDataModel;
using 游民星空.Core.ResultDataModel;

namespace 游民星空.Core.Http
{
    public class ApiService : ApiBaseService
    {
        private string LocalFolder = ApplicationData.Current.LocalFolder.Path;

        /// <summary>
        /// 获取频道列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Channel>> GetChannelList()
        {
            string filename = "channelList.json";
            ChannelResult channelResult = new ChannelResult();
            if (NetworkManager.Current.Network == 4) //无网络
            {
                channelResult.result = await FileHelper.Current.ReadObjectAsync<List<Channel>>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();

                postData.deviceId = DeviceInformationHelper.GetDeviceId();
                postData.request = new request() { type = "0" };
                channelResult = await PostJson<AllChannelListPostData, ChannelResult>(ServiceUri.AllChannel, postData);
                if (channelResult !=null && channelResult.result!=null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Channel>>(channelResult.result, filename);
                }
            }

            if(channelResult!= null && channelResult.result !=null)
            {
                channelResult.result.Add(new Channel { isTop = "False", nodeId = 0, nodeName = "头条" });
            }

            return channelResult?.result;
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
            if (NetworkManager.Current.Network == 4) //无网络
            {
                essayResult.result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
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
                if (essayResult != null && essayResult.result!=null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Essay>>(essayResult.result, filename);
                }
            }
            
            return essayResult?.result;
        }

        ///// <summary>
        ///// 加载更多文章
        ///// </summary>
        ///// <param name="nodeId">频道ID</param>
        ///// <param name="pageIndex">页码</param>
        ///// <returns></returns>
        //public async Task<List<EssayResult>> LoadMoreEssay(int nodeId, int pageIndex)
        //{
        //    return await GetEssayList(nodeId, pageIndex);
        //}\\

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
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                newsResult.result = await FileHelper.Current.ReadObjectAsync<News>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();
                postData.request = new request { contentId = contentId,pageIndex =1 };
                postData.deviceId = DeviceInformationHelper.GetDeviceId();
                newsResult = await PostJson<AllChannelListPostData, NewsResult>(ServiceUri.TwoArticle, postData);
                if (newsResult != null && newsResult.result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<News>(newsResult.result, filename);
                }

            }

            return newsResult?.result;
        }

        /// <summary>
        /// 获取相关阅读
        /// </summary>
        /// <param name="contentId">文章Id</param>
        /// <returns></returns>
        public async Task<List<RelatedReadings>> GetRelatedReadings(string contentId, string contentType)
        {
            string filename = "relatedReadings_" + contentId + ".json";
            RelatedReadingsResult readings = new RelatedReadingsResult();
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                readings.result = await FileHelper.Current.ReadObjectAsync<List<RelatedReadings>>(filename);
            }
            else
            {
                RelatedReadingPostData postData = new RelatedReadingPostData();
                postData.request = new RelatedReadingRequest{ contentId = contentId, contentType = contentType};
                postData.deviceId = DeviceInformationHelper.GetDeviceId();
                readings = await PostJson<RelatedReadingPostData, RelatedReadingsResult>(ServiceUri.TwoCorrelation, postData);
                if (readings != null && readings.result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<RelatedReadings>>(readings.result, filename);
                }
            }
  
            return readings?.result;
        }

        /// <summary>
        /// 获取有攻略的游戏 关注
        /// </summary>
        /// <returns></returns>
        public async Task<List<Strategy>> GetStrategys(int pageCount = 20,int type=0)
        {
            string filename = "focusStrategys.json";
            StrategyResult strategyResult = new StrategyResult();
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                strategyResult.result = await FileHelper.Current.ReadObjectAsync<List<Strategy>>(filename);
            }
            else
            {
                StrategyPostData postData = new StrategyPostData();
                postData.request = new StrategyRequest { pageIndex = 1, pageCount = pageCount, type = type };
                strategyResult = await PostJson<StrategyPostData, StrategyResult>(ServiceUri.Strategy, postData);
                if (strategyResult != null && strategyResult.result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Strategy>>(strategyResult.result, filename);
                }
            }

            return strategyResult?.result;
        }

        /// <summary>
        /// 获取所有有攻略的游戏
        /// </summary>
        /// <returns></returns>
        public async Task<List<Strategy>> GetAllStrategys()
        {
            string filename = "allStrategys.json";
            List<Strategy> allStrategys = new List<Strategy>();
            if (NetworkManager.Current.Network == 4)  //无网络
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
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                essayResult.result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();
                postData.request = new request { elementsCountPerPage = 20, nodeIds = specialID, pageIndex = pageIndex, parentNodeId = "strategy" };
                essayResult = await PostJson<AllChannelListPostData, EssayResult>(ServiceUri.GameStrategys, postData);
                if (essayResult != null && essayResult.result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Essay>>(essayResult.result, filename);
                }
            }

            return essayResult?.result ;
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
            if (NetworkManager.Current.Network == 4)  //无网络
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
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                subscribeResult.result = await FileHelper.Current.ReadObjectAsync<List<Subscribe>>(filename);
            }
            else
            {
                SubscribeSearchPostData postData = new SubscribeSearchPostData { request = new SubscribeSearchRequest { type = type } };
                subscribeResult = await PostJson<SubscribeSearchPostData, SubscribeResult>(ServiceUri.Subscribe, postData);
                if (subscribeResult != null && subscribeResult.result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Subscribe>>(subscribeResult.result, filename);
                }
            }
            return subscribeResult?.result;
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
            if (NetworkManager.Current.Network == 4) //无网络
            {
            }
            else
            {
                SearchPostData postData = new SearchPostData();
                postData.request = new SearchRequest { elementsCountPerPage = "20", pageIndex = pageIndex, searchKey = searchKey, searchType = searchType.ToString() };
                
                essayResult = await PostJson<SearchPostData, EssayResult>(ServiceUri.Search, postData);
            }

            return essayResult?.result;
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
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                subscribeContent.result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {
                SubscribeContentPostData postData = new SubscribeContentPostData();
                postData.request = new SubscribeContentRequest { nodeIds = sourceId, pageCount = 10, pageIndex = pageIndex };
                subscribeContent = await PostJson<SubscribeContentPostData, EssayResult>(ServiceUri.SubscribeContent, postData);

                if (subscribeContent != null && subscribeContent.result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Essay>>(subscribeContent.result, filename);
                }
            }

            return subscribeContent?.result;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="passWord">用户名</param>
        /// <param name="userName">密码</param>
        /// <returns></returns>
        public async Task<VerificationCode> Login(string passWord, string userName)
        {
            LoginPostData postData = new LoginPostData();
            postData.request = new LoginPostDataRequest { passWord = passWord, userName = userName };
            var verificationCode = await PostJson<LoginPostData, VerificationCode>(ServiceUri.Login, postData);
            return verificationCode;
        }

        /// <summary>
        /// 获取要闻
        /// </summary>
        /// <returns></returns>
        public async Task<List<Essay>> GetYaowen(int pageIndex=1)
        {
            string filename = "yaowen.json";
            EssayResult essayResult = new EssayResult();
            if (NetworkManager.Current.Network == 4) //无网络
            {
                essayResult.result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {
                YaowenPostData postData = new YaowenPostData();
                postData.request = new YaowenRequest(){ pageIndex = pageIndex };
                essayResult = await PostJson<YaowenPostData, EssayResult>(ServiceUri.AllChannelList, postData);
                if(essayResult!=null && essayResult.result!= null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Essay>>(essayResult.result, filename);
                }
            }

            return essayResult?.result;
        }

        /// <summary>
        /// 获取应用启动图
        /// </summary>
        /// <returns></returns>
        public async Task<List<AdStart>> GetStartImage()
        {
            string filename = "adStart" + ".json";
            List<AdStart> adStarts = new List<AdStart>();
            if (NetworkManager.Current.Network == 4)
            {
                adStarts = await FileHelper.Current.ReadObjectAsync<List<AdStart>>(filename);
            }
            else
            {
                AdStartPostData postData = new AdStartPostData();
                AdStartResult adStartResult = await PostJson<AdStartPostData, AdStartResult>(ServiceUri.AdStart, postData);
                
                if (adStarts != null && adStartResult.result != null)
                {
                    foreach (var item in adStartResult.result)
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
        /// 通过邮件注册
        /// </summary>
        /// <param name="answer">密保问题答案</param>
        /// <param name="password">密码</param>
        /// <param name="email">邮箱</param>
        /// <param name="question">密保问题</param>
        /// <param name="userName">用户名</param>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="phoneVerificationCode">手机验证码</param>
        /// <returns></returns>
        public async Task<VerificationCode> RegisterByEmail(string answer,string password,string email,string question,string userName,string phoneNumber="",string phoneVerificationCode="")
        {
            EmailRegisterPostData postData = new EmailRegisterPostData();
            postData.request = new EmailRegisterRequest()
            {
                answer = answer,
                confirmpassword = password,
                email = email,
                password = password,
                phoneNumber = phoneNumber,
                phoneVerificationCode = phoneVerificationCode,
                question = question,
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
            if (NetworkManager.Current.Network == 4)
            {
                essayResult.result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(filename);
            }
            else
            {
                SubscribeTopicPostData postData = new SubscribeTopicPostData();
                postData.request = new SubscribeTopicRequest
                {
                    elementsCountPerPage = "20",
                    lastUpdateTime = Functions.getUnixTimeStamp().ToString(),
                    nodeIds = nodeIds,
                    pageIndex = pageIndex,
                    parentNodeId = "dingyue",
                    type = "dingyueTopic"
                };
                essayResult = await PostJson<SubscribeTopicPostData, EssayResult>(ServiceUri.Subscribe, postData);
                if (essayResult != null && essayResult.result != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<Essay>>(essayResult.result, filename);
                }
            }
            return essayResult?.result;
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
        //    if (NetworkManager.Current.Network == 4)
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
    }
}
