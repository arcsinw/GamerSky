using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class HomePage : Page
    {
        private HomePageViewModel ViewModel { get; set; }
        public HomePage()
        {
            this.InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            if (Functions.IsMobile())
            {
                UIHelper.ShowStatusBar();
            }
            else
            {
                UIHelper.ShowView();
            }
            ViewModel = new HomePageViewModel();

            rootFrame.SourcePageType = typeof(MainPage);
            newsRadioButton.IsChecked = true;

            DispatcherManager.Current.Dispatcher = Dispatcher;
        }


       
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                switch ((string)radioButton.Tag)
                {
                    case "0":     // 新闻
                        rootFrame.Navigate(typeof(MainPage));
                        break;
                    case "1":     //攻略
                        rootFrame.Navigate(typeof(StrategyPage));
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                }
            }
        }

        private void ListViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {

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

        }

        
        ///// <summary>
        ///// 获取缓存大小
        ///// </summary>
        ///// <returns></returns>
        //public async Task<double> GetCacheSize()
        //{
        //    return await FileHelper.Current.GetCacheSize();
        //}

        /// <summary>
        /// 更改日/夜间模式
        /// </summary>
        public void ChangeDisplayModel()
        {

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
    }
}
