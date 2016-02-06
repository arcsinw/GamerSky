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
            ViewModel = new HomePageViewModel();

            rootFrame.SourcePageType = typeof(MainPage);

            DispatcherManager.Current.Dispatcher = Dispatcher;
        }


        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private async void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                if (DispatcherManager.Current.Dispatcher == null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
                else
                {
                    if (DispatcherManager.Current.Dispatcher.HasThreadAccess)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                    }
                    else
                    {
                        await DispatcherManager.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                            delegate ()
                            {
                                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                            });
                    }
                }
            }
        } 
        #endregion

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


        public async void GetCacheSize()
        {
            CacheSize = await FileHelper.Current.GetCacheSize();
        }

      

        /// <summary>
        /// 搜索
        /// </summary>
        public void NavigateToSearchPage()
        {
            rootFrame.Navigate(typeof(SearchPage));
            splitView.IsSwipeablePaneOpen = false;
        }

        /// <summary>
        /// 要闻
        /// </summary>
        public void YaoWen()
        {

        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public async void ClearCache()
        {
            await FileHelper.Current.DeleteCacheFile();
            GetCacheSize();
        }

        private double cacheSize;
        public double CacheSize
        {
            get
            {
                return cacheSize;
            }
            set
            {
                cacheSize = value;
                OnPropertyChanged();
            }
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
    }
}
