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
    public sealed partial class YaowenPage : Page
    {
        public YaowenPage()
        {
            this.InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// 是否正在加载数据
        /// </summary>
        private bool IsDataLoading = false;

        private int pageIndex = 1;

        private void Back()
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

     

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Essay essayResult = e.ClickedItem as Essay;
            if (essayResult == null) return;

            (Window.Current.Content as Frame)?.Navigate(typeof(EssayDetail), essayResult);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await viewModel.LoadData(pageIndex);
            pageIndex++;
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
                if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight)  //ListView滚动到底,加载新数据
                {
                    if (!IsDataLoading)  //未加载数据
                    {
                        IsDataLoading = true;
                        //IsActive = true;
                        progressRing.IsActive = true;
                        await viewModel.LoadData(pageIndex++);
                        //IsActive = false;
                        progressRing.IsActive = false;
                        IsDataLoading = false;
                    }
                }
            }
        }


        private async void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            await viewModel.Refresh();
            pageIndex = 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listView.ScrollIntoViewSmoothly(listView.Items[0]);
        }
    }
}
