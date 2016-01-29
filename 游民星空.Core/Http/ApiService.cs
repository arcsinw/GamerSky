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

namespace 游民星空.Core.Http
{
    public class ApiService :ApiBaseService
    {
        private string LocalFolder = ApplicationData.Current.LocalFolder.Path;

        /// <summary>
        /// 获取频道列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ChannelResult>> GetChannelList()
        {
            AllChannelListPostData postData = new AllChannelListPostData();

            postData.deviceId = DeviceInformationHelper.GetDeviceId();
            postData.request = new request() { type = "0" };
            //postData.request.type = "0";
            Channel channel = await PostJson<AllChannelListPostData, Channel>(ServiceUri.AllChannel, postData);
            List<ChannelResult> Channels = new List<ChannelResult>();
            Channels.Add(new ChannelResult { isTop = "False", nodeId = 0, nodeName = "头条" });
            if (channel != null)
            {
                foreach (var item in channel?.result)
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
        public async Task<List<EssayResult>>GetEssayList(int nodeId,int pageIndex)
        {
            List<EssayResult> essayList = new List<EssayResult>();
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
            Essay essay = await PostJson<AllChannelListPostData, Essay>(ServiceUri.AllChannelList, postData);
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
        /// </summary>
        /// <param name="contentId">文章Id</param>
        /// <returns></returns>
        public async Task<News> ReadEssay(string contentId)
        {
            AllChannelListPostData postData = new AllChannelListPostData();
            postData.request = new request { contentId = contentId };
            postData.deviceId = DeviceInformationHelper.GetDeviceId();
            News news = await PostJson<AllChannelListPostData, News>(ServiceUri.TwoArticle, postData);
            if(news!= null)
            {
                
            }
            return news;
        }

        /// <summary>
        /// 加载更多文章
        /// </summary>
        /// <param name="nodeId">频道ID</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<List<EssayResult>> LoadMoreEssay(int nodeId,int pageIndex)
        {
            return await GetEssayList(nodeId,pageIndex);
        }
        /// <summary>
        /// 获取相关阅读
        /// </summary>
        /// <param name="contentId">文章Id</param>
        /// <returns></returns>
        public async Task<List<RelatedReadingsResult>> GetRelatedReadings(string contentId,string contentType)
        {
            AllChannelListPostData postData = new AllChannelListPostData();
            postData.request = new request { contentId = contentId ,contentType = contentType};
            postData.deviceId = DeviceInformationHelper.GetDeviceId();
            List<RelatedReadingsResult> relatedResults = new List<RelatedReadingsResult>();
            RelatedReadings readings =  await PostJson<AllChannelListPostData, RelatedReadings>(ServiceUri.TwoCorrelation, postData);
            if(readings != null)
            {
                foreach (var item in readings.result)
                {
                    relatedResults.Add(item);
                }
            }
            return relatedResults;
        }
    }
}
