using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Core.Model;
using GamerSky.Core.Http;

namespace GamerSky.ViewModel
{
    public class SubscribeContentViewModel : ViewModelBase
    {
        /// <summary>
        /// 订阅内容列表
        /// </summary>
        public ObservableCollection<Essay> SubscribeContens { get; set; }

        private Essay headerSubscribe = new Essay();
        /// <summary>
        /// 订阅图片的标题
        /// </summary>
        public Essay HeaderSubscribe
        {
            get
            {
                return headerSubscribe;
            }
            set
            {
                headerSubscribe = value;
                OnPropertyChanged();
            }
        }
         

        /// <summary>
        /// ProgressRing IsActive
        /// </summary>
        private bool isActive = true;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                OnPropertyChanged();
            }
        }

        private string sourceId;
        public SubscribeContentViewModel(string sourceId)
        { 
            SubscribeContens = new ObservableCollection<Essay>();
            HeaderSubscribe = new Essay();
            this.sourceId = sourceId;
        }

        public SubscribeContentViewModel()
        { 
            SubscribeContens = new ObservableCollection<Essay>();
            HeaderSubscribe = new Essay();
        }

        public async Task LoadData(string sourceId, int pageIndex = 1)
        {
            this.sourceId = sourceId;
            IsActive = true;
            List<Essay> results = await ApiService.Instance.GetSubscribeContent(sourceId, pageIndex);
            if (results != null)
            {
                foreach (var item in results)
                {
                    if (item.Type.Equals("dingyueTitle"))
                    {
                        HeaderSubscribe.Title = item.Title;
                        HeaderSubscribe.ThumbnailURLs = item.ThumbnailURLs;
                    }
                    else
                    {
                        SubscribeContens.Add(item);
                    }
                }
            }
            IsActive = false;
        }

        /// <summary>
        /// 加载更多订阅内内容
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task LoadMoreData(int pageIndex)
        {
            await LoadData(sourceId, pageIndex);
        }

        public override async void Refresh()
        {
            IsActive = true;
            SubscribeContens.Clear();
            LoadData(sourceId);
            IsActive = false;
        }
    }
}
