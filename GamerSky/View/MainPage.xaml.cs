using GamerSky.Core.Helper;
using GamerSky.Core.ViewModel;
using GamerSky.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private PivotItem currentItem;

        public MainPage()
        {
            this.InitializeComponent();
        }
        
        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {

        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = essayPivot.SelectedIndex;
            int currentChannelId = ViewModel.EssaysAndChannels[index].Channel.nodeId;

            

            //获取该频道当前页码
            int pageIndex = 1;
            await ViewModel.LoadMoreEssay(currentChannelId, pageIndex++);
               
        }

        private void FlipView_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        public void ScrollToTop()
        {
            if (currentItem != null)
            {
                ListView listView = Functions.FindChildOfType<ListView>(currentItem);
                if (listView != null)
                {
                    listView.ScrollIntoViewSmoothly(listView.Items[0]);
                }
            }
        }

        private void essayPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
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


    
    }
}
