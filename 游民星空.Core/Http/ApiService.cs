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


        public async Task<List<Channel>> GetChannelList()
        {
            List<Channel> channelList = new List<Channel>();
            JsonObject jsonObj = await GetJson(ServiceUri.AllChannel);
            if(jsonObj!= null)
            {

            }
            return channelList;
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
    }
}
