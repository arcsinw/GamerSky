using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<EssayResult> Essays { get; set; }

        /// <summary>
        /// 攻略
        /// </summary>
        public ObservableCollection<string> Strategys { get; set; }

        /// <summary>
        /// 新闻
        /// </summary>
        public ObservableCollection<string> News { get; set; }

        private ApiService apiService;
        public SearchPageViewModel()
        {
            apiService = new ApiService();
            Essays = new ObservableCollection<EssayResult>();
            Strategys = new ObservableCollection<string>();
            News = new ObservableCollection<string>();
        }

        public async void LoadData()
        {
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
        }
    }
}
