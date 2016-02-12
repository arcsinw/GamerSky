using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
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
        public ObservableCollection<SubscribeResult> Subscribes { get; set; }

        /// <summary>
        /// 攻略
        /// </summary>
        public ObservableCollection<string> Strategys { get; set; }

        /// <summary>
        /// 新闻
        /// </summary>
        public ObservableCollection<string> News { get; set; }

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

        private ApiService apiService;


        public RelayCommand SearchCommand { get; set; }

        public SearchPageViewModel()
        {
            apiService = new ApiService();
            Subscribes = new ObservableCollection<SubscribeResult>();
            Strategys = new ObservableCollection<string>();
            News = new ObservableCollection<string>();

            SearchCommand = new RelayCommand(async(parameter) =>
            {
                //List<EssayResult> essays = await apiService.SearchByKey((string)parameter,SearchTypeEnum.news,1);
                //if(essays!= null && essays.Count!=0)
                //{
                //    News.Clear();
                //    foreach (var item in essays)
                //    {
                //        News.Add(item);
                //    }
                //}
                await new MessageDialog("还没写，莫慌",(string)parameter).ShowAsync();
                Debug.WriteLine(parameter);
            });
            LoadData();
        }

        public async void LoadData()
        {
            IsActive = true;
            List<string> strategys = await apiService.GetSearchHotKey(SearchTypeEnum.strategy.ToString());
            if(strategys!= null)
            {
                foreach (var item in strategys)
                {
                    Strategys.Add(item.Trim());
                }
            }

            List<string> news = await apiService.GetSearchHotKey(SearchTypeEnum.news.ToString());
            if (news != null)
            {
                foreach (var item in news)
                {
                    News.Add(item.Trim());
                }
            }

            List<SubscribeResult> subscribes = await apiService.GetSubscribeHotKey();
            foreach (var item in subscribes)
            {
                Subscribes.Add(item);
            }

            IsActive = false;
        }

        public async Task Search(string key,SearchTypeEnum searchType,int pageIndex=1)
        {
            List < EssayResult> essayResults = await apiService.SearchByKey(key, searchType, pageIndex);

        }
    }
}
