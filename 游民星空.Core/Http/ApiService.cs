using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
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
        public async Task<List<EssayResult>> GetGameStrategys(int specialID)
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
                postData.request = new request { elementsCountPerPage = 20, nodeIds = specialID, pageIndex = 1, parentNodeId = "strategy" };
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
            string filename = "";
            List<string> hotKeyList = new List<string>();
            if (NetworkManager.Current.Network == 4)  //无网络
            {
                //essay = await FileHelper.Current.ReadObjectAsync<Essay>(filename);
            }
            else
            {
                SearchPostData postData = new SearchPostData() { request = new SearchRequest { searchType = searchType } };
                SearchResult searchResult = await PostJson<SearchPostData, SearchResult>(ServiceUri.SearchHotDict, postData);
                for(int i =0;i<searchResult.result.Length;i++)
                {
                    hotKeyList.Add(searchResult.result[i]);
                }
            }

            return hotKeyList;
        }

        /// <summary>
        /// 获取订阅热点词
        /// </summary>
        /// <param name="type"></param>
        public async Task<List<SubscribeResult>> GetSubscribeHotKey(string type="1")
        {
            SubscribeSearchPostData postData = new SubscribeSearchPostData { request = new SubscribeSearchRequest { type = type } };
            Subscribe subscribe = await PostJson<SubscribeSearchPostData, Subscribe>(ServiceUri.Subscribe, postData);
            List<SubscribeResult> subscribeResults = new List<SubscribeResult>();
            if (subscribe!= null && subscribe.result!=null)
            {
                foreach (var item in subscribe.result)
                {
                    subscribeResults.Add(item);
                }
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
        public async Task<List<EssayResult>> SearchByKey(string searchKey,string searchType,int pageIndex)
        {
            SearchPostData postData = new SearchPostData();
            postData.request = new SearchRequest { elementsCountPerPage = "20", pageIndex = 1, searchKey = searchKey, searchType = searchType };

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
    }
}
