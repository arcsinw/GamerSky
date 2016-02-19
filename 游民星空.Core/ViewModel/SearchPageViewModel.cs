using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using 游民星空.Core.Helper;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class SearchPageViewModel : ViewModelBase
    {
        /// <summary>
        /// 订阅页面
        /// </summary>
        public ObservableCollection<Subscribe> HotSubscribes { get; set; }

        /// <summary>
        /// 攻略热点词
        /// </summary>
        public ObservableCollection<string> HotStrategys { get; set; }

        /// <summary>
        /// 新闻热点词
        /// </summary>
        public ObservableCollection<string> HotNews { get; set; }

        /// <summary>
        /// 新闻结果页
        /// </summary>
        public ObservableCollection<Essay> News { get; set; }

        /// <summary>
        /// 攻略结果页
        /// </summary>
        public ObservableCollection<Essay> Strategys { get; set; }

        #region Properties
        private bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                OnPropertyChanged("IsActive");
            }
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
        #endregion

        private ApiService apiService;

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
        }
        
        public SearchPageViewModel()
        {
            apiService = new ApiService();
            HotSubscribes = new ObservableCollection<Subscribe>();
            HotStrategys = new ObservableCollection<string>();
            HotNews = new ObservableCollection<string>();

            
            LoadData();

            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        public async void LoadData()
        {
            IsActive = true;
            List<string> strategys = await apiService.GetSearchHotKey(SearchTypeEnum.strategy.ToString());
            if(strategys!= null)
            {
                foreach (var item in strategys)
                {
                    HotStrategys.Add(item.Trim());
                }
            }

            List<string> hotNews = await apiService.GetSearchHotKey(SearchTypeEnum.news.ToString());
            if (hotNews != null)
            {
                foreach (var item in hotNews)
                {
                    HotNews.Add(item.Trim());
                }
            }

            List<Subscribe> subscribes = await apiService.GetSubscribeHotKey();
            foreach (var item in subscribes)
            {
                HotSubscribes.Add(item);
            }

            IsActive = false;
        }

        /// <summary>
        /// 按关键字搜索
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="searchType">搜索类型 新闻或攻略</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<List<Essay>> Search(string key,SearchTypeEnum searchType,int pageIndex=1)
        {
            List<Essay> essayResults = await apiService.SearchByKey(key, searchType, pageIndex);
            return essayResults;
        }
    }
}
