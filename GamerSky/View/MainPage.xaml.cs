using GamerSky.Core.Helper;
using GamerSky.Core.Model;
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
        private int CurrentChannelId { get; set; }

        private int CurrentPivotIndex { get; set; }
        
        private PivotItem CurrentPivotItem { get; set; }

        private Dictionary<int, ListView> EssayListviewDic { get; set; } = new Dictionary<int, ListView>();

        public MainPage()
        {
            this.InitializeComponent();
        }
         
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Essay essayResult = e.ClickedItem as Essay;
            if (essayResult == null) return;
            if (!essayResult.ContentType.Equals("zhuanti"))
            {
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(EssayDetailPage), essayResult);
            }
            else
            {
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(SubscribeContentPage), essayResult.ContentId);
            }
        }
          
        private void FlipView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var essayResult = (sender as FlipView)?.SelectedItem as Essay;
            if (essayResult == null) return;

            MasterDetailPage.Current.DetailFrame.Navigate(typeof(EssayDetailPage), essayResult);
        }

        public void ScrollToTop()
        {
            if (EssayListviewDic[CurrentPivotIndex] != null)
            { 
                EssayListviewDic[CurrentPivotIndex]?.ScrollIntoViewSmoothly(EssayListviewDic[CurrentPivotIndex].Items[0]); 
            }
        }

        private void essayPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentPivotIndex = essayPivot.SelectedIndex;
        }

        #region Pane functions
        /// <summary>
        /// 搜索
        /// </summary>
        public void NavigateToSearchPage()
        {
            NavigationHelper.MasterFrameNavigate(typeof(SearchPage));
        }

        /// <summary>
        /// 要闻
        /// </summary>
        public void YaoWen()
        {
            NavigationHelper.MasterFrameNavigate(typeof(YaowenPage));
        }

        /// <summary>
        /// 收藏
        /// </summary>
        public void Favorite()
        {
            NavigationHelper.MasterFrameNavigate(typeof(FavoritePage));
        }

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
        }


        public ElementTheme AppTheme
        {
            get
            {
                return DataShareManager.Current.AppTheme;
            }
            set
            {
                DataShareManager.Current.AppTheme = value;
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

                UIHelper.ShowStatusBar();
            }
        }

        /// <summary>
        /// 更多设置
        /// </summary>
        public void NavigateToSettings()
        {
            NavigationHelper.MasterFrameNavigate(typeof(SettingsPage));
        }

        /// <summary>
        /// 反馈
        /// </summary>
        public void FeedBack()
        {
            NavigationHelper.MasterFrameNavigate(typeof(Feedback));
        }

        /// <summary>
        /// 跳转到登录
        /// </summary>
        public void NavigateToLogin()
        {
            NavigationHelper.MasterFrameNavigate(typeof(LoginPage));
        } 
        #endregion
        
        private void innerTopicListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void PullToRefreshListView_RefreshRequested(object sender, EventArgs e)
        {
            ViewModel.RefreshEssays(CurrentPivotIndex);
        }

        private void essayPivot_PivotItemLoaded(Pivot sender, PivotItemEventArgs args)
        {
            CurrentPivotItem = args.Item;
            ListView listView = Functions.FindChildOfType<ListView>(CurrentPivotItem);
            if (listView != null)
            {
                EssayListviewDic[CurrentPivotIndex] = listView;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            ViewModel.RefreshEssays(CurrentPivotIndex);
        }
    }
}
