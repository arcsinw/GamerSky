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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace 游民星空.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SubscribeContentPage : Page
    {
        public SubscribeContentPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            progressRing.IsActive = true;
            SubscribeResult subscribeResult = e.Parameter as SubscribeResult;
            if (subscribeResult != null)
            {
                await viewModel.LoadData(subscribeResult);
            }
            progressRing.IsActive = false;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            EssayResult essayResult = e.ClickedItem as EssayResult;
            if (essayResult == null) return;

            (Window.Current.Content as Frame)?.Navigate(typeof(EssayDetail), essayResult);
        }

        private async void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            progressRing.IsActive = true;
            await viewModel.Refresh();
            progressRing.IsActive = false;
        }

        private ScrollViewer scrollViewer;
        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViewer = Functions.FindChildOfType<ScrollViewer>(sender as ListView);
            if (scrollViewer != null)
            {
                scrollViewer.ViewChanged += scrollViewer_ViewChanged;
            }
        }

        private bool IsDataLoading = false;
        private int pageIndex = 1;
        private async void scrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (scrollViewer != null)
            {
                if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight)  //ListView滚动到底,加载新数据
                {
                    if (!IsDataLoading)  //未加载数据
                    {
                        IsDataLoading = true;
                        //IsActive = true;
                        progressRing.IsActive = true;
                        await viewModel.LoadMoreData(pageIndex);
                        pageIndex++;
                        //IsActive = false;
                        progressRing.IsActive = false;
                        IsDataLoading = false;
                    }
                }
            }
        }

        private void FlipView_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }
    }
}
