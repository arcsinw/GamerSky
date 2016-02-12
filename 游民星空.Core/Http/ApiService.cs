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

namespace 游民星空.Core.Http
{
    public class ApiService : ApiBaseService
    {
        private string LocalFolder = ApplicationData.Current.LocalFolder.Path;

        /// <summary>
        /// 获取频道列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ChannelResult>> GetChannelList()
        {
            string filename = "channelList.json";
            Channel channel = new Channel();
            if (NetworkManager.Current.Network == 4) //无网络
            {
                channel = await FileHelper.Current.ReadObjectAsync<Channel>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();

                postData.deviceId = DeviceInformationHelper.GetDeviceId();
                postData.request = new request() { type = "0" };
                channel = await PostJson<AllChannelListPostData, Channel>(ServiceUri.AllChannel, postData);
                await FileHelper.Current.WriteObjectAsync<Channel>(channel, filename);

            }
            List<ChannelResult> Channels = new List<ChannelResult>();

            Channels.Add(new ChannelResult { isTop = "False", nodeId = 0, nodeName = "头条" });
            if (channel != null && channel.result!=null)
            {
                foreach (var item in channel.result)
                {
                    Channels.Add(new ChannelResult() { isTop = item.isTop, nodeId = item.nodeId, nodeName = item.nodeName });
                }
            }

            return Channels;
        }
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="nodeId">频道ID</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<List<EssayResult>> GetEssayList(int nodeId, int pageIndex)
        {
            string filename = "essayList_"+nodeId+"_"+pageIndex+".json";
            Essay essay = new Essay();
            if (NetworkManager.Current.Network == 4) //无网络
            {
                essay = await FileHelper.Current.ReadObjectAsync<Essay>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();
                postData.deviceId = DeviceInformationHelper.GetDeviceId();
                //postData.deviceType = DeviceInformationHelper.GetOS();
                //postData.osVersion = DeviceInformationHelper.GetOSVer();
                postData.request = new request()
                {
                    elementsCountPerPage = 20,
                    nodeIds = nodeId,
                    pageIndex = pageIndex,
                    parentNodeId = "news",
                    type = "null",
                };
                essay = await PostJson<AllChannelListPostData, Essay>(ServiceUri.AllChannelList, postData);
                await FileHelper.Current.WriteObjectAsync<Essay>(essay, filename);

            }
            List<EssayResult> essayList = new List<EssayResult>();

            if (essay != null)
            {
                foreach (var item in essay.result)
                {
                    essayList.Add(item);
                }
            }

            return essayList;
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
            News news = new News();
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                news = await FileHelper.Current.ReadObjectAsync<News>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();
                postData.request = new request { contentId = contentId,pageIndex =1 };
                postData.deviceId = DeviceInformationHelper.GetDeviceId();
                news = await PostJson<AllChannelListPostData, News>(ServiceUri.TwoArticle, postData);
                await FileHelper.Current.WriteObjectAsync<News>(news, filename);

            }
          
            return news;

        }

        /// <summary>
        /// 获取相关阅读
        /// </summary>
        /// <param name="contentId">文章Id</param>
        /// <returns></returns>
        public async Task<List<RelatedReadingsResult>> GetRelatedReadings(string contentId, string contentType)
        {
            string filename = "relatedReadings_" + contentId + ".json";
            RelatedReadings readings = new RelatedReadings();
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                readings = await FileHelper.Current.ReadObjectAsync<RelatedReadings>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();
                postData.request = new request { contentId = contentId, contentType = contentType };
                postData.deviceId = DeviceInformationHelper.GetDeviceId();
                readings = await PostJson<AllChannelListPostData, RelatedReadings>(ServiceUri.TwoCorrelation, postData);

                await FileHelper.Current.WriteObjectAsync<RelatedReadings>(readings, filename);

            }
            List<RelatedReadingsResult> relatedResults = new List<RelatedReadingsResult>();

            if (readings != null)
            {
                foreach (var item in readings.result)
                {
                    relatedResults.Add(item);
                }
            }

            return relatedResults;
        }

        /// <summary>
        /// 获取有攻略的游戏 关注
        /// </summary>
        /// <returns></returns>
        public async Task<List<StrategyResult>> GetStrategys(int pageCount = 20,int type=0)
        {
            string filename = "focusStrategys.json";
            Strategy strategy = new Strategy();
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                strategy = await FileHelper.Current.ReadObjectAsync<Strategy>(filename);
            }
            else
            {
                
                StrategyPostData postData = new StrategyPostData();
                postData.request = new StrategyRequest { pageIndex = 1, pageCount = pageCount, type = type };
                strategy = await PostJson<StrategyPostData, Strategy>(ServiceUri.Strategy, postData);
                await FileHelper.Current.WriteObjectAsync<Strategy>(strategy, filename);

            }
            List<StrategyResult> strategys = new List<StrategyResult>();

            if (strategy != null)
            {
                foreach (var item in strategy.result)
                {
                    strategys.Add(item);
                }
            }

            return strategys;
        }

        /// <summary>
        /// 获取所有有攻略的游戏
        /// </summary>
        /// <returns></returns>
        public async Task<List<StrategyResult>> GetAllStrategys()
        {
            string filename = "allStrategys.json";
            List<StrategyResult> allStrategys = await GetStrategys(10000,1);

            if (NetworkManager.Current.Network == 4)  //无网络
            {
                allStrategys = await FileHelper.Current.ReadObjectAsync<List<StrategyResult>>(filename);
            }
            else
            {
                if (allStrategys != null)
                {
                    await FileHelper.Current.WriteObjectAsync<List<StrategyResult>>(allStrategys, filename);
                }
            }
            return allStrategys;
        }

        /// <summary>
        /// 获取某个游戏的所有攻略
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public async Task<List<EssayResult>> GetGameStrategys(int specialID,int pageIndex=1)
        {
            Essay essay = new Essay();
            string filename = "gameStrategys_" + specialID + ".json";
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                essay = await FileHelper.Current.ReadObjectAsync<Essay>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();
                postData.request = new request { elementsCountPerPage = 20, nodeIds = specialID, pageIndex = pageIndex, parentNodeId = "strategy" };
                essay = await PostJson<AllChannelListPostData, Essay>(ServiceUri.GameStrategys, postData);
                await FileHelper.Current.WriteObjectAsync<Essay>(essay, filename);
            }

            List<EssayResult> essayList = new List<EssayResult>();

            if (essay != null)
            {
                foreach (var item in essay.result)
                {
                    essayList.Add(item);
                }
            }

            return essayList;
        }


        /// <summary>
        /// 获取搜索热点词
        /// </summary>
        /// <param name="searchType">热点词类型 strategy news shouyou</param>
        /// <returns></returns>
        public async Task<List<string>> GetSearchHotKey(string searchType)
        {
            string filename = "searchHotKey_"+searchType+".json";
            List<string> hotKeyList = new List<string>();
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                hotKeyList = await FileHelper.Current.ReadObjectAsync<List<string>>(filename);
            }
            else
            {
                SearchPostData postData = new SearchPostData() { request = new SearchRequest { searchType = searchType } };
                SearchResult searchResult = await PostJson<SearchPostData, SearchResult>(ServiceUri.SearchHotDict, postData);
                for(int i =0;i<searchResult.result.Length;i++)
                {
                    hotKeyList.Add(searchResult.result[i]);
                }
                await FileHelper.Current.WriteObjectAsync<List<string>>(hotKeyList, filename);
            }

            return hotKeyList;
        }

        /// <summary>
        /// 获取订阅热点词
        /// </summary>
        /// <param name="type"></param>
        public async Task<List<SubscribeResult>> GetSubscribeHotKey(string type="1")
        {
            string filename = "subscribeHotKey_" + type + ".json";
            List<SubscribeResult> subscribeResults = new List<SubscribeResult>();

            if (NetworkManager.Current.Network == 4)  //无网络
            {
                subscribeResults = await FileHelper.Current.ReadObjectAsync<List<SubscribeResult>>(filename);
            }
            else
            {
                SubscribeSearchPostData postData = new SubscribeSearchPostData { request = new SubscribeSearchRequest { type = type } };
                Subscribe subscribe = await PostJson<SubscribeSearchPostData, Subscribe>(ServiceUri.Subscribe, postData);
                if (subscribe != null && subscribe.result != null)
                {
                    foreach (var item in subscribe.result)
                    {
                        subscribeResults.Add(item);
                    }
                }
                await FileHelper.Current.WriteObjectAsync<List<SubscribeResult>>(subscribeResults, filename);
            }
            return subscribeResults;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="searchType"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<List<EssayResult>> SearchByKey(string searchKey,SearchTypeEnum searchType,int pageIndex)
        {
            SearchPostData postData = new SearchPostData();
            postData.request = new SearchRequest { elementsCountPerPage = "20", pageIndex = 1, searchKey = searchKey, searchType = searchType.ToString() };

            List<EssayResult> essayResults = new List<EssayResult>();

            Essay essay = await PostJson<SearchPostData, Essay>(ServiceUri.Search, postData);
            if(essay!= null && essay.result!=null)
            {
                foreach (var item in essay.result)
                {
                    essayResults.Add(item);
                }
            }

            return essayResults;
        }

        /// <summary>
        /// 获取订阅栏目内的内容
        /// </summary>
        /// <param name="sourceId">栏目Id</param>
        /// <param name="pageIndex">当前页码</param>
        /// <returns></returns>
        public async Task<List<EssayResult>> GetSubscribeContent(string sourceId, int pageIndex = 1)
        {
            Essay subscribeContent = new Essay();
            string filename = "subscribeContent_" + sourceId + ".json";
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                subscribeContent = await FileHelper.Current.ReadObjectAsync<Essay>(filename);
            }
            else
            {
                SubscribeContentPostData postData = new SubscribeContentPostData();
                postData.request = new SubscribeContentRequest { nodeIds = sourceId, pageCount = 10, pageIndex = pageIndex };
                subscribeContent = await PostJson<SubscribeContentPostData, Essay>(ServiceUri.SubscribeContent, postData);
            }
            List<EssayResult> subscribeContentResult = new List<EssayResult>();

            if (subscribeContent != null && subscribeContent.result != null)
            {
                foreach (var item in subscribeContent.result)
                {
                    subscribeContentResult.Add(item);
                }
            }
            
            return subscribeContentResult;
        }

        //public async Task<bool> Login(string passWord,string userName)
        //{
        //    LoginPostData postData = new LoginPostData() { passWord = passWord, userName = userName };
        //    var result = await PostJson<LoginPostData, bool>(ServiceUri.Login, postData);

        //}

        /// <summary>
        /// 获取要闻
        /// </summary>
        /// <returns></returns>
        public async Task<List<EssayResult>> GetYaowen(int pageIndex=1)
        {
            string filename = "yaowen.json";
            Essay essay = new Essay();
            if (NetworkManager.Current.Network == 4) //无网络
            {
                essay = await FileHelper.Current.ReadObjectAsync<Essay>(filename);
            }
            else
            {
                YaowenPostData postData = new YaowenPostData();
                postData.request = new YaowenRequest(){ pageIndex = pageIndex };
                essay = await PostJson<YaowenPostData, Essay>(ServiceUri.AllChannelList, postData);
            }
            List<EssayResult> essayList = new List<EssayResult>();
            if (essay != null && essay.result!=null)
            {
                await FileHelper.Current.WriteObjectAsync<Essay>(essay, filename);
                foreach (var item in essay.result)
                {
                    essayList.Add(item);
                }
            }
            return essayList;
        }

        /// <summary>
        /// 获取应用启动图
        /// </summary>
        /// <returns></returns>
        public async Task<List<AdStartResult>>GetStartImage()
        {
            AdStartPostData postData = new AdStartPostData();
            AdStart adStart = await PostJson<AdStartPostData, AdStart>(ServiceUri.AdStart, postData);

            List<AdStartResult> adStartResults = new List<AdStartResult>();
            if (adStart!=null && adStart.result!=null)            
            {
                foreach (var item in adStart.result)
                {
                    adStartResults.Add(item);
                }
            }
            return adStartResults;
        }

        /// <summary>
        /// 获取手机号注册验证码
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="username"></param>
        /// <param name="email"></param>
        /// <param name="codetype"></param>
        /// <returns></returns>
        public async Task GetVerificationCode(string phoneNumber,string username,string email,string codetype="1")
        {
            VerificationCodePostData postData = new VerificationCodePostData();
            postData.request = new VerificationCodeRequest() { codetype = codetype, email = email, phoneNumber = phoneNumber, username = username };

            VerificationCode verificationCode = await PostJson<VerificationCodePostData, VerificationCode>(ServiceUri.GetVerificationCode, postData);

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
        public async Task RegisterByEmail(string answer,string password,string email,string question,string userName,string phoneNumber="",string phoneVerificationCode="")
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

        }

        /// <summary>
        /// 通过用户名查找能找回密码的方式
        /// </summary>
        /// <returns></returns>
        public async Task FindWayByName(string username)
        {
            FindPasswordByNamePostData postData = new FindPasswordByNamePostData();
            postData.request = new FindPasswordByNameRequest() { username = username };
            FindPasswordByName result = await PostJson<FindPasswordByNamePostData, FindPasswordByName>(ServiceUri.FindPassword,);
            
        }
    }
}
