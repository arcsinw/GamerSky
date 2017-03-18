﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Http;
using GamerSky.Model;

namespace GamerSky.ViewModel
{
    public class GameDetailViewModel : ViewModelBase
    { 
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


        public GameDetailViewModel()
        { 
            GameDetail = new GameDetail();
            GameDetailNews = new ObservableCollection<GameDetailEssay>();
            GameDetailStrategys = new ObservableCollection<GameDetailEssay>();

            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;

            if(Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            { 
                LoadGameDetail();
                LoadGameNews(1);
                LoadGameStrategys(1);
            }
        }
        

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
        }

        public string contentId { get; set; } = "353478";

        public async void LoadGameDetail()
        {
            GameDetail = await ApiService.Instance.GetGameDetail(contentId);
        }

        /// <summary>
        /// 加载新闻
        /// </summary>
        /// <param name="pageIndex"></param>
        public async void LoadGameNews(int pageIndex)
        {
            var result =await ApiService.Instance.GetGameDetailNews(contentId, pageIndex);
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
            var result = await ApiService.Instance.GetGameDetailStrategys(contentId, pageIndex);
            if(result !=null)
            {
                foreach (var item in result)
                {
                    GameDetailStrategys.Add(item);
                }
            }
        }

        public async void RefreshGameNews()
        {
            await ApiService.Instance.GetGameDetailNews(contentId, 1);
        }

        public async void RefreshStrategys()
        {
            await ApiService.Instance.GetGameDetailStrategys(contentId, 1);
        }
    }
}
