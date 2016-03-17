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
using Windows.UI.Xaml.Navigation;
using 游民星空.Core.Helper;
using 游民星空.Core.Model;
using 游民星空.Core.ViewModel;
using 游民星空.Helper;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace 游民星空.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SubscribePage : Page
    {
        public SubscribePage()
        {
            this.InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;
        }
        
        private bool IsSubscribeTopicLoaded = false;
        private bool IsSubscribeContentLoaded = false;

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(pivot.SelectedIndex)
            {
                case 0:
                    if(!IsSubscribeContentLoaded)
                    {
                        await viewModel.LoadSubscribeContent();
                        IsSubscribeContentLoaded = true;
                    }
                    break;
                case 1:
                    if (!IsSubscribeTopicLoaded)
                    {
                        await viewModel.LoadSubscribeTopic();
                        IsSubscribeTopicLoaded = true;
                    }
                   break;
            }
        }

        #region 九幽的数据统计
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            JYHelper.TracePageEnd(this.BaseUri.LocalPath);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            JYHelper.TracePageStart(this.BaseUri.LocalPath);
        }
        #endregion

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Essay essayResult = e.ClickedItem as Essay;
            if (essayResult == null) return;

            (Window.Current.Content as Frame)?.Navigate(typeof(EssayDetail), essayResult);
        }

        private void NavigatoToMySubscribe()
        {
            (Window.Current.Content as Frame)?.Navigate(typeof(MySubscribePage));
        }

        /// <summary>
        /// 下拉刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            switch(pivot.SelectedIndex)
            {
                case 0:
                    await viewModel.RefreshSubscribeContent();
                    break;
                case 1:
                    await viewModel.RefreshSubscribeTopic();
                    break;
            }
        }

        public void Back()
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }


        private void ScrollToTop(object sender, RoutedEventArgs e)
        {
            subscribeContentListView.ScrollIntoViewSmoothly(subscribeContentListView.Items[0]);
        }

        private ScrollViewer scrollViewer;
        
        private bool IsDataLoading = false;
        private int pageIndex = 1;
        private async void scrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (scrollViewer != null)
            {
                if (scrollViewer.VerticalOffset < DeviceInformationHelper.GetScreenHeight())
                {
                    if (topPop.IsOpen)
                    {
                        topPop.IsOpen = false;
                    }
                }
                else if (scrollViewer.VerticalOffset > DeviceInformationHelper.GetScreenHeight())
                {
                    if (!topPop.IsOpen)
                    {
                        topPop.IsOpen = true;
                    }
                }
                if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight)
                {
                    if (!IsDataLoading)  //未加载数据
                    {
                        IsDataLoading = true;
                        progress.IsActive = true;
                        await viewModel.LoadSubscribeContent();
                        progress.IsActive = false;
                        IsDataLoading = false;
                    }
                }
            }
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var control =  e.OriginalSource as FrameworkElement;
            if (control != null)
            {
                Essay essay = control.DataContext as Essay;
                if (essay != null)
                {
                    (Window.Current.Content as Frame)?.Navigate(typeof(SubscribeContentPage), essay.contentId);
                }
            }
        }

        private void subscribeContentListView_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViewer = Functions.FindChildOfType<ScrollViewer>(sender as ListView);
            if (scrollViewer != null)
            {
                scrollViewer.ViewChanged += scrollViewer_ViewChanged;
            }
        }

        private void innerTopicListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Essay essayResult = e.ClickedItem as Essay;
            if (essayResult == null) return;

            (Window.Current.Content as Frame)?.Navigate(typeof(EssayDetail), essayResult);
        }
    }
}
