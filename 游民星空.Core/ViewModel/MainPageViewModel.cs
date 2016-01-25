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
            List<ChannelResult> channels = await apiService.GetChannelList();
            foreach (var item in channels)
            {
                Channels.Add(item);
            }

            AllChannelListPostData postData = new AllChannelListPostData();
            //postData.deviceId = DeviceInformationHelper.GetDeviceId();
            //postData.deviceType = DeviceInformationHelper.GetOS();
            //postData.osVersion = DeviceInformationHelper.GetOSVer();
            postData.request = new request() {
                elementsCountPerPage = 20,
                nodeIds = 0,
                pageIndex = 1,
                parentNodeId = "news",
                type = "null",
             };
             List<EssayResult> essays = await apiService.GetEssayList(postData);
            foreach (var item in essays)
            {
                Essays.Add(item);
            }
        }

        public void NavigateToEssay()
        {

        }
    }
}
