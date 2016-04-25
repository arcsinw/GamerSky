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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using 游民星空.Core.Helper;
using 游民星空.Core.ViewModel;
using 游民星空.Helper;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace 游民星空.View
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
             
            UIHelper.ShowStatusBar();
               
            DispatcherManager.Current.Dispatcher = Dispatcher;
            Loaded += HomePage_Loaded;
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            rootFrame.SourcePageType = typeof(MainPage);
            newsRadioButton.IsChecked = true;
 
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
             
            if (DataShareManager.Current.IsNewVersion)
            {
                UIHelper.ShowMessage(
                    "手势后退",
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
                        if(rootFrame.Content is MainPage)
                        {
                            (rootFrame.Content as MainPage)?.Button_Click(null, null);
                        }
                        rootFrame.Navigate(typeof(MainPage));
                        break;
                    case "1":     //攻略
                        rootFrame.Navigate(typeof(StrategyPage));
                        break;
                    case "2":
                        break;
                    case "3":
                        rootFrame.Navigate(typeof(SubscribePage));
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

        }
    }
}
