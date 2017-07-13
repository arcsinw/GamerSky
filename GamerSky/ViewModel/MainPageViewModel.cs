using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GamerSky.Helper;
using GamerSky.Http;
using GamerSky.Model;
using GamerSky.ResultModel;
using GamerSky.IncrementalLoadingCollection;
using Arcsinx.Toolkit.IncrementalCollection;
using Arcsinx.Toolkit.Controls;

namespace GamerSky.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        /// <summary>
        /// 同时提供频道和文章列表
        /// </summary>
        public ObservableCollection<PivotData> EssaysAndChannels { get; set; }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; OnPropertyChanged(); }
        }


        public MainPageViewModel()
        {
            EssaysAndChannels = new ObservableCollection<PivotData>();

            LoadData();
            GetCacheSize();
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
                    //EssaysAndChannels.Add(new PivotData
                    //{
                    //    Channel = item,
                    //    Essays = new IncrementalLoadingCollection<Essay>((count, pageIndex) =>
                    //    {
                    //        return LoadEssayAsync(count, pageIndex, item.nodeId);
                    //    },
                    //     () => { IsActive = false; }, () => { IsActive = true; }, (e) => { IsActive = false; ToastService.SendToast(((Exception)e).Message); })
                    //});
                }
            }
        }

        private async Task<IEnumerable<Essay>> LoadEssayAsync(uint count, int pageIndex, int nodeId)
        {
            List<Essay> essays = new List<Essay>();
            var result = await ApiService.Instance.GetEssayList(nodeId, pageIndex++);
            if (result != null)
            {
                foreach (var item in result)
                {
                    if (item.Type.Equals("huandeng"))
                    {
                        foreach (var c in item.ChildElements)
                        {
                            //HeaderEssays.Add(c);
                        }
                        continue;
                    }
                    essays.Add(item);
                }
            }
            return essays;
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
                    //foreach (var c in item.ChildElements)
                    //{
                    //    EssaysAndChannels.Where(x => x.Channel.nodeId.Equals(nodeId)).First().HeaderEssays.Add(c);
                    //}
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
