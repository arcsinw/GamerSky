using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.Helper;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class MainPageViewModel
    {
        ApiService apiService = new ApiService();
        /// <summary>
        /// PivotHeaders 
        /// </summary>
        public ObservableCollection<ChannelResult> Channels { get; set; }

        public ObservableCollection<EssayResult> Essays { get; set; }

        public MainPageViewModel()
        {
            Channels = new ObservableCollection<ChannelResult>();
            Essays = new ObservableCollection<EssayResult>();

            LoadData();
        }

        private async void LoadData()
        {
           Channel channel = await apiService.GetChannelList();
            foreach (var item in channel.result)
            {
                Channels.Add(new ChannelResult() { isTop = item.isTop, nodeId = item.nodeId, nodeName = item.nodeName });
            }

            AllChannelListPostData postData = new AllChannelListPostData();
            //postData.deviceId = DeviceInformationHelper.GetDeviceId();
            //postData.deviceType = DeviceInformationHelper.GetOS();
            //postData.osVersion = DeviceInformationHelper.GetOSVer();
            postData.request.elementsCountPerPage = 20;
            postData.request.nodeIds = 0;
            postData.request.pageIndex = 1;
            postData.request.parentNodeId = "news";
            postData.request.type = "null";
             await apiService.GetEssayList(postData);
        }
    }
}
