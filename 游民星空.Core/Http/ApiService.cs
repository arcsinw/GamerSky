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
            //postData.deviceType = DeviceInformationHelper.GetOS();
            //postData.os = "android";
            //postData.osVersion = DeviceInformationHelper.GetOSVer();
            postData.request = new request() { type = "0" };
            //postData.request.type = "0";
            Channel channel = await PostJson<AllChannelListPostData,Channel>(ServiceUri.AllChannel, postData);
            List<ChannelResult> Channels = new List<ChannelResult>();
            foreach (var item in channel?.result)
            {
                Channels.Add(new ChannelResult() { isTop = item.isTop, nodeId = item.nodeId, nodeName = item.nodeName });
            }
            //JsonObject jsonObj = await GetJson(ServiceUri.AllChannel);
            //if(jsonObj!= null)
            //{
            //    channel.errorCode = jsonObj["errorCode"].GetString();
            //    channel.errorMessage = jsonObj["errorMessage"].GetString();
            //    JsonArray array = jsonObj["result"].GetArray();
            //    foreach (var item in array)
            //    {
            //        var obj = item.GetObject();
            //        channel.result.Add(new ChannelResult() { isTop = obj["isTop"].GetString(), nodeId = obj["nodeId"].GetString(), nodeName = obj["nodeName"].GetString() });
            //    }
            //}
            return Channels;
        }
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<EssayResult>>GetEssayList(AllChannelListPostData postData)
        {
            List<EssayResult> essayList = new List<EssayResult>();
            postData.deviceId = DeviceInformationHelper.GetDeviceId();
            //postData.deviceType = DeviceInformationHelper.GetOS();
            //postData.osVersion = DeviceInformationHelper.GetOSVer();
            Essay essay = await PostJson<AllChannelListPostData, Essay>(ServiceUri.AllChannelList, postData);

            foreach (var item in essay?.result)
            {
                essayList.Add(item);
            }
            //too hard
            //JsonObject jsonObj = await PostJson(ServiceUri.AllChannelList,postData);
            //if(jsonObj!= null)
            //{
            //    JsonArray array = jsonObj["result"].GetArray();
            //    foreach (var item in array)
            //    {
            //        var obj = item.GetObject();
            //        essayList.Add(new EssayResult() { adId = obj["addId"].GetString(),
            //            authorHeaderImageURL = obj["authorHeaderImageURL"].GetString(),
            //            authorName = obj["authorName"].GetString(),
            //            badges = obj["badges"].GetString(),
            //            childElements = obj["childElements"].GetArray(),
            //        });
            //    }
            //}
            return essayList;
        }

        /// <summary>
        /// 阅读文章
        /// </summary>
        /// <returns></returns>
        public async Task<News> ReadEssay(AllChannelListPostData postData)
        {
            postData.deviceId = DeviceInformationHelper.GetDeviceId();
            //postData.deviceType = DeviceInformationHelper.GetOS();
            //postData.osVersion = DeviceInformationHelper.GetOSVer();
            News news = await PostJson<AllChannelListPostData, News>(ServiceUri.TwoArticle, postData);
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
            AllChannelListPostData postData = new AllChannelListPostData();
            postData.request = new request { nodeIds = nodeId, pageIndex = pageIndex };
            return await GetEssayList(postData);
        }
    }
}
