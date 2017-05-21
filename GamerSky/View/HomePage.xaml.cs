using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GamerSky.Core.Helper;
using GamerSky.Core.ViewModel;
using GamerSky.Helper;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class HomePage : Page , INotifyPropertyChanged
    {
        
        public HomePage()
        {
            this.InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;
              
            DispatcherManager.Current.Dispatcher = Dispatcher;
            SystemNavigationManager.GetForCurrentView().BackRequested += HomePage_BackRequested;
            Loaded += HomePage_Loaded;
        }

        private void HomePage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if(DetailFrame.CanGoBack)
            {
                DetailFrame.GoBack();
            }
            else if(MasterFrame.CanGoBack)
            {
                MasterFrame.GoBack();
            }
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            MasterFrame.SourcePageType = typeof(MainPage);
            newsRadioButton.IsChecked = true;

            UIHelper.ShowStatusBar();

            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
             
            if (DataShareManager.Current.IsNewVersion)
            {
                UIHelper.ShowMessage(
                    "看图片",
                    "新版本更新内容");
            }
            this.Loaded -= HomePage_Loaded;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        { 
            var radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                switch ((string)radioButton.Tag)
                {
                    case "0":     // 新闻
                        if(MasterFrame.Content is MainPage)
                        {
                            (MasterFrame.Content as MainPage)?.ScrollToTop();
                        }
                        MasterFrame.Navigate(typeof(MainPage));
                        break;
                    case "1":     //攻略
                        MasterFrame.Navigate(typeof(StrategyPage));
                        break;
                    case "2":
                        break;
                    case "3":
                        MasterFrame.Navigate(typeof(SubscribePage));
                        break;
                }
            }
        }
 
         
        /// <summary>
        /// 搜索
        /// </summary>
        public void NavigateToSearchPage()
        {
            Frame.Navigate(typeof(SearchPage));
            splitView.IsSwipeablePaneOpen = false;
        }

        /// <summary>
        /// 要闻
        /// </summary>
        public void YaoWen()
        {
            this.Frame.Navigate(typeof(YaowenPage));
            splitView.IsSwipeablePaneOpen = false;
        }

        /// <summary>
        /// 收藏
        /// </summary>
        public void Favorite()
        {
            (Window.Current.Content as Frame)?.Navigate(typeof(FavoritePage));
        }

        ///// <summary>
        ///// 获取缓存大小
        ///// </summary>
        ///// <returns></returns>
        //public async Task<double> GetCacheSize()
        //{
        //    return await FileHelper.Current.GetCacheSize();
        //}
 

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
        }

        private ElementTheme appTheme;
        public ElementTheme AppTheme
        {
            get
            {
                appTheme = DataShareManager.Current.AppTheme;
                return appTheme;
            }
            set
            {
                appTheme = value;
                OnPropertyChanged();
                UIHelper.ShowStatusBar();
            }
        }

        
        #region INotifyPropertyChanged member
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        /// <summary>
        /// 是否夜间模式
        /// </summary>
        public bool IsNight
        {
            get
            {
                return DataShareManager.Current.AppTheme == ElementTheme.Dark;
            }
            set
            {
                DataShareManager.Current.UpdateAPPTheme(value);
                OnPropertyChanged();
                UIHelper.ShowStatusBar();
            }
        }

        /// <summary>
        /// 更多设置
        /// </summary>
        public void NavigateToSettings()
        {
            this.Frame.Navigate(typeof(SettingsPage));
            splitView.IsSwipeablePaneOpen = false;
        }

        /// <summary>
        /// 反馈
        /// </summary>
        public void FeedBack()
        {
            this.Frame.Navigate(typeof(Feedback));
        }

        /// <summary>
        /// 跳转到登录
        /// </summary>
        public void NavigateToLogin()
        {
            splitView.IsSwipeablePaneOpen = false;
            this.Frame.Navigate(typeof(LoginPage));
        }

        private void adaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            if(e.NewState == Narrow)
            {
                DetailFrame.Visibility = DetailFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
            }
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                DetailFrame.CanGoBack || MasterFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void MasterFrame_Navigated(object sender, NavigationEventArgs e)
        {
            DetailFrame.Navigate(e.SourcePageType,e.Parameter);
        }

        private void DetailFrame_Navigated(object sender, NavigationEventArgs e)
        {
            while(DetailFrame.BackStack.Count >1)
            {
                DetailFrame.BackStack.RemoveAt(1);
            }

        }
    }
}
