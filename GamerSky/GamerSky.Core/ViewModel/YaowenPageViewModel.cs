﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Core.Http;
using GamerSky.Core.IncrementalLoadingCollection;
using GamerSky.Core.Model;

namespace GamerSky.Core.ViewModel
{
    public class YaowenPageViewModel : ViewModelBase
    {  
        private YaowenIncrementalCollection yaowens;
        public YaowenIncrementalCollection Yaowens
        {
            get
            {
                return yaowens;
            }
            set
            {
                yaowens = value;
                OnPropertyChanged();
            }
        }

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
        public YaowenPageViewModel()
        {
            apiService = new ApiService(); 
            Yaowens = new YaowenIncrementalCollection();
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

        public void Refresh()
        {
            IsActive = true; 
            YaowenIncrementalCollection s = new YaowenIncrementalCollection();
            Yaowens = s;
            s.OnDataLoaded += S_OnDataLoaded;
            s.OnDataLoading += S_OnDataLoading;
            IsActive = false; 
        }

        private void S_OnDataLoading(object sender, EventArgs e)
        {
            IsActive = true;
        }

        private void S_OnDataLoaded(object sender, EventArgs e)
        {
            IsActive = false;
        }
    }
}
