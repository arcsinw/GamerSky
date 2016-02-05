using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                await apiService.SearchByKey((string)parameter,SearchTypeEnum.news,1);
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
                    Strategys.Add(item);
                }
            }

            List<string> news = await apiService.GetSearchHotKey(SearchTypeEnum.news.ToString());
            if (news != null)
            {
                foreach (var item in news)
                {
                    News.Add(item);
                }
            }

            List<SubscribeResult> subscribes = await apiService.GetSubscribeHotKey();
            foreach (var item in subscribes)
            {
                Subscribes.Add(item);
            }

            IsActive = false;
        }
    }
}
