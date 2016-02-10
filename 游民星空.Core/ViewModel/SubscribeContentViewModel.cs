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
        public ObservableCollection<EssayResult> SubscribeContens { get; set; }


        /// <summary>
        /// 订阅图片的标题
        /// </summary>
        public EssayResult HeaderSubscribe { get; set; }

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

        private SubscribeResult subscribeResult;
        public SubscribeContentViewModel(SubscribeResult subscribeResult)
        {
            apiService = new ApiService();
            SubscribeContens = new ObservableCollection<EssayResult>();
            HeaderSubscribe = new EssayResult();
            this.subscribeResult = subscribeResult;

        }

        public SubscribeContentViewModel()
        {
            apiService = new ApiService();
            SubscribeContens = new ObservableCollection<EssayResult>();
            HeaderSubscribe = new EssayResult();
        }

        public async Task LoadData(SubscribeResult subscribeResult, int pageIndex = 1)
        {
            this.subscribeResult = subscribeResult;
            IsActive = true;
            List<EssayResult> results = await apiService.GetSubscribeContent(subscribeResult.sourceId, pageIndex);
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
            await LoadData(subscribeResult, pageIndex);
        }

        public async Task Refresh()
        {
            IsActive = true;
            SubscribeContens.Clear();
            await LoadData(subscribeResult);
            IsActive = false;
        }
    }
}
