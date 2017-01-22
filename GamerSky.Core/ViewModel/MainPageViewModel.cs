using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GamerSky.Core.Helper;
using GamerSky.Core.Http;
using GamerSky.Core.Model;
using GamerSky.Core.ResultModel;
using GamerSky.Core.IncrementalLoadingCollection;

namespace GamerSky.Core.ViewModel
{
    public class MainPageViewModel:ViewModelBase
    {  
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
            LoadData();
            GetCacheSize();
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
            List<Channel> channels = await ApiService.Instance.GetChannelList();
            if (channels != null)
            {
                foreach (var item in channels)
                {
                    EssaysAndChannels.Add(new PivotData { Channel = item,Essays = new EssayIncrementalCollection(item.nodeId) });
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
            List<Essay> essays = await ApiService.Instance.GetEssayList(nodeId, pageIndex);
            if (essays == null) return;
            foreach (var item in essays)
            {
                if (item.Type.Equals("huandeng"))
                {
                    foreach (var c in item.ChildElements)
                    {
                        //EssaysAndChannels.Where(x => x.Channel.nodeId.Equals(nodeId)).First().HeaderEssays.Add(c);
                    }
                    continue;
                }
                EssaysAndChannels.Where(x => x.Channel.nodeId.Equals(nodeId)).First().Essays.Add(item);
            }
            
        }

        public void RefreshEssays(int index)
        {
            if(index<0 || index > EssaysAndChannels.Count)
            {
                return;
            }

            EssaysAndChannels[index].Essays.Clear();
            EssayIncrementalCollection e = new EssayIncrementalCollection(EssaysAndChannels[index].Channel.nodeId);
            EssaysAndChannels[index].Essays = e;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public async void ClearCache()
        {
            CacheSize = "删除缓存中...";
            await FileHelper.Current.DeleteCacheFile();
            double cache = await FileHelper.Current.GetCacheSize();
            CacheSize = GetFormatSize(cache);
        }

        private string cacheSize;
        public string CacheSize
        {
            get
            {
                return cacheSize;
            }
            set
            {
                cacheSize = value;
                OnPropertyChanged();
            }
        }

        public async void GetCacheSize()
        {
            double size = await FileHelper.Current.GetCacheSize();
            CacheSize = GetFormatSize(size);
        }

        private string GetFormatSize(double size)
        {
            if (size < 1024)
            {
                return size + "byte";
            }
            else if (size < 1024 * 1024)
            {
                return Math.Round(size / 1024, 2) + "KB";
            }
            else if (size < 1024 * 1024 * 1024)
            {
                return Math.Round(size / 1024 / 1024, 2) + "MB";
            }
            else
            {
                return Math.Round(size / 1024 / 1024 / 2014, 2) + "GB";
            }
        }

        
    }
}
