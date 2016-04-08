using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using 游民星空.Core.Helper;
using 游民星空.Core.Http;
using 游民星空.Core.Model;
using 游民星空.Core.ResultDataModel;

namespace 游民星空.Core.ViewModel
{
    public class MainPageViewModel:ViewModelBase
    {
        ApiService apiService = new ApiService();
        
         
        /// <summary>
        /// 同时提供频道和文章列表
        /// </summary>
        public ObservableCollection<PivotData> EssaysAndChannels { get; set; }
         
        public MainPageViewModel()
        {  
                EssaysAndChannels = new ObservableCollection<PivotData>();
                
                AppTheme = DataShareManager.Current.AppTheme;
                DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;

            if (IsDesignMode)
            {
                LoadData();
            }
            else
            {
                LoadData();
            }
        }

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
        }

        private ElementTheme appTheme;
        public ElementTheme AppTheme
        {
            get
            {
                return appTheme;
            }
            set
            {
                appTheme = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 加载频道
        /// </summary>
        private async void LoadData()
        {
            List<Channel> channels = await apiService.GetChannelList();
            if (channels != null)
            {
                foreach (var item in channels)
                {
                    EssaysAndChannels.Add(new PivotData { Channel = item });
                }
            }
        }
 

        /// <summary>
        /// 加载更多数据
        /// </summary>
        /// <param name="nodeId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task LoadMoreEssay(int nodeId,int pageIndex)
        {
            List<Essay> essays = await apiService.GetEssayList(nodeId, pageIndex);
            if (essays == null) return;
            foreach (var item in essays)
            {
                if (item.type.Equals("huandeng"))
                {
                    foreach (var c in item.childElements)
                    {
                        EssaysAndChannels.Where(x => x.Channel.nodeId.Equals(nodeId)).First().HeaderEssays.Add(c);
                    }
                    continue;
                }
                EssaysAndChannels.Where(x => x.Channel.nodeId.Equals(nodeId)).First().Essays.Add(item);
            }
            
        }
         

        public async Task Refresh(int channelId)
        {
            //Essays.Clear();
            EssaysAndChannels.Where(x => x.Channel.nodeId.Equals(channelId)).First().Essays.Clear();
            EssaysAndChannels.Where(x => x.Channel.nodeId.Equals(channelId)).First().HeaderEssays.Clear();
            await LoadMoreEssay(channelId, 1);
        }
    }
}
