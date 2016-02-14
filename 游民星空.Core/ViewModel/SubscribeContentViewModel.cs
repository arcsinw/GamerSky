using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class SubscribeContentViewModel : ViewModelBase
    {
        /// <summary>
        /// 订阅内容列表
        /// </summary>
        public ObservableCollection<Essay> SubscribeContens { get; set; }


        /// <summary>
        /// 订阅图片的标题
        /// </summary>
        public Essay HeaderSubscribe { get; set; }

        private ApiService apiService;

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

        private Subscribe subscribe;
        public SubscribeContentViewModel(Subscribe subscribe)
        {
            apiService = new ApiService();
            SubscribeContens = new ObservableCollection<Essay>();
            HeaderSubscribe = new Essay();
            this.subscribe = subscribe;
        }

        public SubscribeContentViewModel()
        {
            apiService = new ApiService();
            SubscribeContens = new ObservableCollection<Essay>();
            HeaderSubscribe = new Essay();
        }

        public async Task LoadData(Subscribe subscribe, int pageIndex = 1)
        {
            this.subscribe = subscribe;
            IsActive = true;
            List<Essay> results = await apiService.GetSubscribeContent(subscribe.sourceId, pageIndex);
            foreach (var item in results)
            {
                if (item.type.Equals("dingyueTitle"))
                {
                    HeaderSubscribe.title = item.title;
                    HeaderSubscribe.thumbnailURLs = item.thumbnailURLs;
                }
                else
                {
                    SubscribeContens.Add(item);
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
            await LoadData(subscribe, pageIndex);
        }

        public async Task Refresh()
        {
            IsActive = true;
            SubscribeContens.Clear();
            await LoadData(subscribe);
            IsActive = false;
        }
    }
}
