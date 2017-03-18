using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using GamerSky.Helper;
using GamerSky.Model;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace GamerSky.View
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
            string sourceId = e.Parameter as string;
            if (sourceId != null)
            {
                if (viewModel.SubscribeContens.Count == 0)
                {
                    await viewModel.LoadData(sourceId, pageIndex);
                    pageIndex++;
                }
            }
            progressRing.IsActive = false;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Essay essayResult = e.ClickedItem as Essay;
            if (essayResult == null) return;

            MasterDetailPage.Current.DetailFrame.Navigate(typeof(ReadEssayPage), essayResult);
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

                    titleBarGrid.Opacity = (scrollViewer.VerticalOffset / 170 > 1) ? 1 : scrollViewer.VerticalOffset / 170;

                    if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight)  //ListView滚动到底,加载新数据
                    {
                        if (!IsDataLoading)  //未加载数据
                        {
                            IsDataLoading = true;
                            //IsActive = true;
                            progressRing.IsActive = true;
                            await viewModel.LoadMoreData(pageIndex++);
                            //IsActive = false;
                            progressRing.IsActive = false;
                            IsDataLoading = false;
                        }
                    }
                }
            }
        }

        

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listView.ScrollIntoViewSmoothly(listView.Items[0]);
        }
    }
}
