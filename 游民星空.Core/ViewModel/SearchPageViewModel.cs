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
using 游民星空.Core.IncrementalLoadingCollection;
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
        /// 新闻搜索结果
        /// </summary>
        public ObservableCollection<Essay> News { get; set; }
        //public EssayIncrementalCollection News { get; set; }

        /// <summary>
        /// 攻略搜索结果
        /// </summary>
        public ObservableCollection<Essay> Strategys { get; set; }

        /// <summary>
        /// 订阅搜索结果
        /// </summary>
        public ObservableCollection<Subscribe> Subscribes { get; set; }

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

        private Visibility newsGridViewVisibility;
        /// <summary>
        /// 新闻GridView's Visibility
        /// </summary>
        public Visibility NewsGridViewVisibility
        {
            get
            {
                return newsGridViewVisibility;
            }
            set
            {
                newsGridViewVisibility = value;
                OnPropertyChanged();
            }
        }

        private Visibility strategysGridViewVisibility;
        public Visibility StrategysGridViewVisibility
        {
            get
            {
                return strategysGridViewVisibility;
            }
            set
            {
                strategysGridViewVisibility = value;
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
            News = new ObservableCollection<Essay>();
            //News = new EssayIncrementalCollection("囧", SearchTypeEnum.news);
            Strategys = new ObservableCollection<Essay>();
            Subscribes = new ObservableCollection<Subscribe>();

            //LoadHotkey();

            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        /// <summary>
        /// 加载hot keyword
        /// </summary>
        //public async void LoadHotkey()
        //{
             
        //}

        /// <summary>
        /// 加载攻略热点
        /// </summary>
        /// <returns></returns>
        public async Task LoadStrategyHotKey()
        {
            IsActive = true;
            List<string> strategys = await apiService.GetSearchHotKey(SearchTypeEnum.strategy.ToString());
            if (strategys != null)
            {
                foreach (var item in strategys)
                {
                    HotStrategys.Add(item.Trim());
                }
            }
            IsActive = false;
        }

        /// <summary>
        /// 加载新闻热点
        /// </summary>
        /// <returns></returns>
        public async Task LoadNewsHotKey()
        {
            IsActive = true;
            List<string> hotNews = await apiService.GetSearchHotKey(SearchTypeEnum.news.ToString());
            if (hotNews != null)
            {
                foreach (var item in hotNews)
                {
                    HotNews.Add(item.Trim());
                }
            }
            IsActive = false;
        }

        /// <summary>
        /// 加载订阅热点
        /// </summary>
        /// <returns></returns>
        public async Task LoadSubscribeHotKey()
        {
            IsActive = true;
            List<Subscribe> subscribes = await apiService.GetSubscribeHotKey();
            if (subscribes != null)
            {
                foreach (var item in subscribes)
                {
                    if(DataShareManager.Current.SubscribeList.Any(x=>x.sourceId == item.sourceId))
                    {
                        item.Favorite = true;
                    }
                    HotSubscribes.Add(item);
                }
            }
            IsActive = false;
        }

        /// <summary>
        /// 刷新订阅热点
        /// </summary>
        /// <returns></returns>
        public async Task RefreshSubscribeHotKey()
        {
            IsActive = true;
            HotSubscribes.Clear();
            await LoadSubscribeHotKey();
            IsActive = false;
        }

        /// <summary>
        /// 加载搜索结果
        /// </summary>
        public void LoadSearchResult()
        {

        }
        /// <summary>
        /// 按关键字搜索
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="searchType">搜索类型 新闻或攻略</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task Search(string key,SearchTypeEnum searchType,int pageIndex=1)
        {
            IsActive = true;
            //List<Essay> essayResults = await apiService.SearchByKey(key, searchType, pageIndex);
            //if (essayResults == null) return;
            switch(searchType)
            {
                case SearchTypeEnum.news:
                    List<Essay> essayResults = await apiService.SearchByKey(key, searchType, pageIndex);
                    if (essayResults == null) return;
                    //News = new EssayIncrementalCollection(key, searchType, pageIndex);
                    foreach (var item in essayResults)
                    {
                        News.Add(item);
                    }
                    NewsGridViewVisibility = Visibility.Collapsed;
                    break;
                case SearchTypeEnum.strategy:
                    List<Essay> strategyResult = await apiService.SearchByKey(key, searchType, pageIndex);
                    if (strategyResult == null) return;
                    foreach (var item in strategyResult)
                    {
                        if (item.contentType.Equals("strategy"))
                        {
                            Strategys.Add(item);
                        }
                    }
                    StrategysGridViewVisibility = Visibility.Collapsed;
                    break;
                case SearchTypeEnum.subscribe: //订阅查询是本地查询
                    var result = from x in HotSubscribes
                                 where x.sourceName.Contains(key)
                                 select x;
                    HotSubscribes.Clear();
                    foreach (var item in result)
                    {
                        HotSubscribes.Add(item);
                    } 
                    break;
            }
            IsActive = false;
        }
    }
}
