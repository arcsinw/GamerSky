using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
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
        public async Task<Channel> GetChannelList()
        {
            Channel channel = new Channel();
            JsonObject jsonObj = await GetJson(ServiceUri.AllChannel);
            if(jsonObj!= null)
            {
                channel.errorCode = jsonObj["errorCode"].GetString();
                channel.errorMessage = jsonObj["errorMessage"].GetString();
                JsonArray array = jsonObj["result"].GetArray();
                foreach (var item in array)
                {
                    var obj = item.GetObject();
                    channel.result.Add(new channelResult() { isTop = obj["isTop"].GetString(), nodeId = obj["nodeId"].GetString(), nodeName = obj["nodeName"].GetString() });
                }
            }
            return channel;
        }
        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Essay>>GetEssayList()
        {
            List<Essay> essayList = new List<Essay>();

            return essayList;
        }

        /// <summary>
        /// 阅读文章
        /// </summary>
        /// <returns></returns>
        public async Task ReadEssay()
        {

        }
    }
}
