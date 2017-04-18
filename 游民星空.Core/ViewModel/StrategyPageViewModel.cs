﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using 游民星空.Core.Helper;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class StrategyPageViewModel:ViewModelBase
    {
        #region Properties
        /// <summary>
        /// 关注攻略
        /// </summary>
        public ObservableCollection<Strategy> FocusStrategys { get; set; }

        /// <summary>
        /// 所有攻略
        /// </summary>
        public ObservableCollection<AlphaKeyGroup<Strategy>> AllStrategys { get; set; }

        /// <summary>
        /// 游戏库中游戏列表
        /// </summary>
        public ObservableCollection<Game> Games { get; set; }

        private ApiService apiService;

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
                OnPropertyChanged();
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

        public StrategyPageViewModel()
        {
            apiService = new ApiService();

            FocusStrategys = new ObservableCollection<Strategy>();

            AllStrategys = new ObservableCollection<AlphaKeyGroup<Strategy>>();

            Games = new ObservableCollection<Game>();

           
            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;

          
        }

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
        }

        

        /// <summary>
        /// 加载关注攻略
        /// </summary>
        public async Task LoadFocusStrategys()
        {
            await apiService.GetGameList(1);
            IsActive = true;
            List<Strategy> strategys = await apiService.GetStrategys();
            if (strategys != null)
            {
                foreach (var item in strategys)
                {
                    FocusStrategys.Add(item);
                }
            }
            IsActive = false;
        }

        /// <summary>
        /// 加载所有攻略
        /// </summary>
        public async Task LoadAllStrategys()
        {
            IsActive = true;
            List<Strategy> strategys = await apiService.GetAllStrategys();
            if (strategys != null)
            {
                //按拼音分组
                List<AlphaKeyGroup<Strategy>> groupData = AlphaKeyGroup<Strategy>.CreateGroups(
                    strategys, (Strategy s) => s.title, true);

                foreach (var item in groupData)
                {
                    AllStrategys.Add(item);
                }
            }
            IsActive = false;
        }

        /// <summary>
        /// 加载游戏库中游戏列表
        /// </summary>
        /// <returns></returns>
        public async Task LoadGameList(int pageIndex)
        {
            IsActive = true;
            var gameList = await apiService.GetGameList(pageIndex);
            if(gameList!= null)
            {
                foreach (var item in gameList)
                {
                    Games.Add(item);
                }
            }
            IsActive = false;
        }

        /// <summary>
        /// 刷新关注攻略
        /// </summary>
        public async Task RefreshFocusStrategy()
        {
            IsActive = true;
            FocusStrategys.Clear();
            await LoadFocusStrategys();
            IsActive = false;
        }

        /// <summary>
        /// 刷新所有攻略
        /// </summary>
        public async Task RefreshAllStrategy()
        {
            IsActive = true;
            AllStrategys.Clear();
            await LoadAllStrategys();
            IsActive = false;
        }

        /// <summary>
        /// 刷新游戏库游戏列表
        /// </summary>
        public async Task RefreshGameList()
        {
            IsActive = true;
            Games.Clear();
            await LoadGameList(1);
            IsActive = false;
        }

        public async void Subscribe(Strategy strategy)
        {
            VerificationCode code = await apiService.EditSubscribe(SubscribeOperateEnum.add, strategy.specialID.ToString());

            //本地存储订阅列表
            //DataShareManager.Current.UpdateSubscribe(strategy);
        }  
    }
}
