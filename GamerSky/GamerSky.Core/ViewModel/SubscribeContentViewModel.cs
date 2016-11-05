using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Core.Http;
using GamerSky.Core.Model;

namespace GamerSky.Core.ViewModel
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

        private string sourceId;
        public SubscribeContentViewModel(string sourceId)
        {
            apiService = new ApiService();
            SubscribeContens = new ObservableCollection<Essay>();
            HeaderSubscribe = new Essay();
            this.sourceId = sourceId;

            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        public SubscribeContentViewModel()
        {
            apiService = new ApiService();
            SubscribeContens = new ObservableCollection<Essay>();
            HeaderSubscribe = new Essay();

            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        public async Task LoadData(string sourceId, int pageIndex = 1)
        {
            this.sourceId = sourceId;
            IsActive = true;
            List<Essay> results = await apiService.GetSubscribeContent(sourceId, pageIndex);
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
            await LoadData(sourceId, pageIndex);
        }

        public async Task Refresh()
        {
            IsActive = true;
            SubscribeContens.Clear();
            await LoadData(sourceId);
            IsActive = false;
        }
    }
}
