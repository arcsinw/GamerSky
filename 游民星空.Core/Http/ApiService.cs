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
                channelResult = await FileHelper.Current.ReadObjectAsync<ChannelResult>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();

                postData.deviceId = DeviceInformationHelper.GetDeviceId();
                postData.request = new request() { type = "0" };
                channelResult = await PostJson<AllChannelListPostData, ChannelResult>(ServiceUri.AllChannel, postData);
                await FileHelper.Current.WriteObjectAsync<ChannelResult>(channelResult, filename);

            }
            List<Channel> Channels = new List<Channel>();

            Channels.Add(new Channel { isTop = "False", nodeId = 0, nodeName = "头条" });
            if (channelResult != null && channelResult.result!=null)
            {
                foreach (var item in channelResult.result)
                {
                    Channels.Add(new Channel() { isTop = item.isTop, nodeId = item.nodeId, nodeName = item.nodeName });
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
        public async Task<List<Essay>> GetEssayList(int nodeId, int pageIndex)
        {
            string filename = "essayList_"+nodeId+"_"+pageIndex+".json";
            EssayResult essayResult = new EssayResult();
            if (NetworkManager.Current.Network == 4) //无网络
            {
                essayResult = await FileHelper.Current.ReadObjectAsync<EssayResult>(filename);
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
                essayResult = await PostJson<AllChannelListPostData, EssayResult>(ServiceUri.AllChannelList, postData);
                await FileHelper.Current.WriteObjectAsync<EssayResult>(essayResult, filename);

            }
            List<Essay> essayList = new List<Essay>();

            if (essayResult != null && essayResult.result !=null)
            {
                foreach (var item in essayResult.result)
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
            NewsResult newsResult = new NewsResult();
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                newsResult = await FileHelper.Current.ReadObjectAsync<NewsResult>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();
                postData.request = new request { contentId = contentId,pageIndex =1 };
                postData.deviceId = DeviceInformationHelper.GetDeviceId();
                newsResult = await PostJson<AllChannelListPostData, NewsResult>(ServiceUri.TwoArticle, postData);
                await FileHelper.Current.WriteObjectAsync<NewsResult>(newsResult, filename);

            }

            return newsResult.result;
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
                readings = await FileHelper.Current.ReadObjectAsync<RelatedReadingsResult>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();
                postData.request = new request { contentId = contentId, contentType = contentType };
                postData.deviceId = DeviceInformationHelper.GetDeviceId();
                readings = await PostJson<AllChannelListPostData, RelatedReadingsResult>(ServiceUri.TwoCorrelation, postData);

                await FileHelper.Current.WriteObjectAsync<RelatedReadingsResult>(readings, filename);

            }
            List<RelatedReadings> relatedResults = new List<RelatedReadings>();

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
        public async Task<List<Strategy>> GetStrategys(int pageCount = 20,int type=0)
        {
            string filename = "focusStrategys.json";
            StrategyResult strategyResult = new StrategyResult();
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                strategyResult = await FileHelper.Current.ReadObjectAsync<StrategyResult>(filename);
            }
            else
            {
                
                StrategyPostData postData = new StrategyPostData();
                postData.request = new StrategyRequest { pageIndex = 1, pageCount = pageCount, type = type };
                strategyResult = await PostJson<StrategyPostData, StrategyResult>(ServiceUri.Strategy, postData);

                await FileHelper.Current.WriteObjectAsync<StrategyResult>(strategyResult, filename);

            }
            List<Strategy> strategys = new List<Strategy>();

            if (strategyResult != null)
            {
                foreach (var item in strategyResult.result)
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
        public async Task<List<Strategy>> GetAllStrategys()
        {
            string filename = "allStrategys.json";
            List<Strategy> allStrategys = await GetStrategys(10000,1);

            if (NetworkManager.Current.Network == 4)  //无网络
            {
                allStrategys = await FileHelper.Current.ReadObjectAsync<List<Strategy>>(filename);
            }
            else
            {
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
                essayResult = await FileHelper.Current.ReadObjectAsync<EssayResult>(filename);
            }
            else
            {
                AllChannelListPostData postData = new AllChannelListPostData();
                postData.request = new request { elementsCountPerPage = 20, nodeIds = specialID, pageIndex = pageIndex, parentNodeId = "strategy" };
                essayResult = await PostJson<AllChannelListPostData, EssayResult>(ServiceUri.GameStrategys, postData);
                await FileHelper.Current.WriteObjectAsync<EssayResult>(essayResult, filename);
            }

            List<Essay> essayList = new List<Essay>();

            if (essayResult != null)
            {
                foreach (var item in essayResult.result)
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
        /// 获取热门/全部订阅
        /// </summary>
        /// <param name="type">type=0 热门 type=1 全部</param>
        public async Task<List<Subscribe>> GetSubscribeHotKey(string type="1")
        {
            string filename = "subscribeHotKey_" + type + ".json";
            List<Subscribe> subscribes = new List<Subscribe>();

            if (NetworkManager.Current.Network == 4)  //无网络
            {
                subscribes = await FileHelper.Current.ReadObjectAsync<List<Subscribe>>(filename);
            }
            else
            {
                SubscribeSearchPostData postData = new SubscribeSearchPostData { request = new SubscribeSearchRequest { type = type } };
                SubscribeResult subscribeResult = await PostJson<SubscribeSearchPostData, SubscribeResult>(ServiceUri.Subscribe, postData);
                if (subscribeResult != null )
                {
                    foreach (var item in subscribeResult.result)
                    {
                        subscribes.Add(item);
                    }
                }
                await FileHelper.Current.WriteObjectAsync<List<Subscribe>>(subscribes, filename);
            }
            return subscribes;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="searchType"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<List<Essay>> SearchByKey(string searchKey,SearchTypeEnum searchType,int pageIndex)
        {
            if(NetworkManager.Current.Network==4) //无网络
            {

            }
            SearchPostData postData = new SearchPostData();
            postData.request = new SearchRequest { elementsCountPerPage = "20", pageIndex = 1, searchKey = searchKey, searchType = searchType.ToString() };

            List<Essay> essayResults = new List<Essay>();

            EssayResult essay = await PostJson<SearchPostData, EssayResult>(ServiceUri.Search, postData);
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
        public async Task<List<Essay>> GetSubscribeContent(string sourceId, int pageIndex = 1)
        {
            EssayResult subscribeContent = new EssayResult();
            string filename = "subscribeContent_" + sourceId + ".json";
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                subscribeContent = await FileHelper.Current.ReadObjectAsync<EssayResult>(filename);
            }
            else
            {
                SubscribeContentPostData postData = new SubscribeContentPostData();
                postData.request = new SubscribeContentRequest { nodeIds = sourceId, pageCount = 10, pageIndex = pageIndex };
                subscribeContent = await PostJson<SubscribeContentPostData, EssayResult>(ServiceUri.SubscribeContent, postData);
            }
            List<Essay> subscribeContentResult = new List<Essay>();

            if (subscribeContent != null && subscribeContent.result != null)
            {
                foreach (var item in subscribeContent.result)
                {
                    subscribeContentResult.Add(item);
                }
            }
            
            return subscribeContentResult;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="passWord"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        //public async Task<bool> Login(string passWord, string userName)
        //{
        //    LoginPostData postData = new LoginPostData() { passWord = passWord, userName = userName };
        //    var result = await PostJson<LoginPostData, bool>(ServiceUri.Login, postData);

        //}

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
                essayResult = await FileHelper.Current.ReadObjectAsync<EssayResult>(filename);
            }
            else
            {
                YaowenPostData postData = new YaowenPostData();
                postData.request = new YaowenRequest(){ pageIndex = pageIndex };
                essayResult = await PostJson<YaowenPostData, EssayResult>(ServiceUri.AllChannelList, postData);
            }
            List<Essay> essayList = new List<Essay>();
            if (essayResult != null && essayResult.result!=null)
            {
                await FileHelper.Current.WriteObjectAsync<EssayResult>(essayResult, filename);
                foreach (var item in essayResult.result)
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
        public async Task<List<AdStart>>GetStartImage()
        {
            AdStartPostData postData = new AdStartPostData();
            AdStartResult adStart = await PostJson<AdStartPostData, AdStartResult>(ServiceUri.AdStart, postData);

            List<AdStart> adStartResults = new List<AdStart>();
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
            FindPasswordByName result = await PostJson<FindPasswordByNamePostData, FindPasswordByName>(ServiceUri.FindPassword,postData);
            
        }
    }
}
