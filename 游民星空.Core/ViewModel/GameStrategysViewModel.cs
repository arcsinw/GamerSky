﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using 游民星空.Core.Http;
using 游民星空.Core.IncrementalLoadingCollection;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class GameStrategysViewModel:ViewModelBase
    {
        /// <summary>
        /// 攻略
        /// </summary>
        public ObservableCollection<Essay> Strategys { get; set; }

        public GameStrategysIncrementalLoadingCollection IncreStrategys { get; set; }

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

        private Strategy strategyResult;
        public GameStrategysViewModel(Strategy strategyResult)
        {
            apiService = new ApiService();
            Strategys = new ObservableCollection<Essay>();
            IncreStrategys = new GameStrategysIncrementalLoadingCollection();
            this.strategyResult = strategyResult;

            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
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


        public GameStrategysViewModel()
        {
            apiService = new ApiService();
            Strategys = new ObservableCollection<Essay>();
            IncreStrategys = new GameStrategysIncrementalLoadingCollection();
        }

        public async Task LoadData(Strategy strategyResult,int pageIndex=1)
        {
            this.strategyResult = strategyResult;
            IsActive = true;
            List<Essay> results = await apiService.GetGameStrategys(strategyResult.specialID,pageIndex);
            foreach(var item in results)
            {
                Strategys.Add(item);
                IncreStrategys.Add(item);
            }
            IsActive = false;
        }

        /// <summary>
        /// 加载更多攻略
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task LoadMoreStrategys(int pageIndex)
        {
            await LoadData(strategyResult, pageIndex);
        }

        public async Task Refresh()
        {
            IsActive = true;
            Strategys.Clear();
            IncreStrategys.Clear();
            await LoadData(strategyResult);
            IsActive = false;
        }

    }
}
