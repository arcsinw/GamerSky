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
    public class GameDetailViewModel : ViewModelBase
    {
        private ApiService apiService;
        private GameDetail gameDetail;
        public GameDetail GameDetail
        {
            get
            {
                return gameDetail;
            }
            set
            {
                gameDetail = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GameDetailEssay> GameDetailNews { get; set; }

        public ObservableCollection<GameDetailEssay> GameDetailStrategys { get; set; }

        public GameDetailViewModel()
        {
            apiService = new ApiService();
            GameDetail = new GameDetail();
            GameDetailNews = new ObservableCollection<GameDetailEssay>();
            GameDetailStrategys = new ObservableCollection<GameDetailEssay>();
        }

        public string contentId { get; set; }
        public async void LoadGameDetail()
        {
            GameDetail = await apiService.GetGameDetail(contentId);
        }

        /// <summary>
        /// 加载新闻
        /// </summary>
        /// <param name="pageIndex"></param>
        public async void LoadGameNews(int pageIndex)
        {
            var result =await apiService.GetGameDetailNews(contentId, pageIndex);
            if(result!= null)
            {
                foreach (var item in result)
                {
                    GameDetailNews.Add(item);
                }
            }
        }

        /// <summary>
        /// 加载攻略
        /// </summary>
        /// <param name="pageIndex"></param>
        public async void LoadGameStrategys(int pageIndex)
        {
            await apiService.GetGameDetailStrategys(contentId, pageIndex);
        }

        public async void RefreshGameNews()
        {
            await apiService.GetGameDetailNews(contentId, 1);
        }

        public async void RefreshStrategys()
        {
            await apiService.GetGameDetailStrategys(contentId, 1);
        }
    }
}
