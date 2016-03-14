using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
    public sealed partial class SearchPage : Page
    {
        public SearchPage()
        {
            this.InitializeComponent();


            pageIndexDic = new Dictionary<int, int>();

            NavigationCacheMode = NavigationCacheMode.Required;
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

        private void Back()
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        #region pageIndex
        /// <summary>
        /// 保存不同频道的页码  pivotIndex,pageIndex
        /// </summary>
        private Dictionary<int, int> pageIndexDic;

        private int pageIndex = 1;
        #endregion 


        private string key;
        private SearchTypeEnum searchType;

        /// <summary>
        /// 搜索
        /// </summary>
        private async void Search()
        {
            int pivotIndex = pivot.SelectedIndex;
            switch (pivotIndex)
            {
                case 0:
                    searchType = SearchTypeEnum.news;
                    viewModel.News.Clear();
                    break;
                case 1:
                    searchType = SearchTypeEnum.strategy;
                    viewModel.Strategys.Clear();
                    break;
                case 2:
                    searchType = SearchTypeEnum.subscribe;
                    break;
            }
            key = keyTextBox.Text;
            pageIndex = 1;
            await viewModel.Search(key, searchType, pageIndex++);

            //记录页码
            if (pageIndexDic.ContainsKey(pivotIndex))
            {
                pageIndexDic[pivotIndex] = pageIndex;
            }
            else
            {
                pageIndexDic.Add(pivotIndex, pageIndex);
            }
        }
        
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddSubscribe(object sender, RoutedEventArgs e)
        {
            var control = e.OriginalSource as FrameworkElement;
            if (control == null) return;
            var dataContext = control.DataContext as Subscribe;
            if (dataContext == null) return;
            DataShareManager.Current.UpdateSubscribe(dataContext);
        }

        private void subscribeListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var result = e.ClickedItem as Subscribe;
            if (result != null)
            {
                this.Frame.Navigate(typeof(SubscribeContentPage), result.sourceId);
            }
        }

        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            if(e.Key == VirtualKey.Enter)
            {
                Search();
            }
        }

        /// <summary>
        /// 热门标签点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemClick(object sender, ItemClickEventArgs e)
        {
            keyTextBox.Text = (string)(e.ClickedItem);

            Search();
        }


        private bool IsDataLoading = false;
        
        
        private ScrollViewer newsScrollViewer;

        private ScrollViewer strategysScrollViewer;

        private ScrollViewer subscribeScrollViewer;

        /// <summary>
        /// 看新闻
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Essay essayResult = e.ClickedItem as Essay;
            if (essayResult == null) return;

            (Window.Current.Content as Frame)?.Navigate(typeof(EssayDetail), essayResult);
        }
        
        /// <summary>
        /// 跳转顶部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollToTop(object sender, RoutedEventArgs e)
        {
            switch(pivot.SelectedIndex)
            {
                case 0:
                    newsListView.ScrollIntoViewSmoothly(newsListView.Items[0]);
                    break;
                case 1:
                    strategysListView.ScrollIntoViewSmoothly(strategysListView.Items[0]);
                    break;
                case 2:
                    subscribeListView.ScrollIntoViewSmoothly(subscribeListView.Items[0]);
                    break;
            }
        }

        private void newsListView_Loaded(object sender, RoutedEventArgs e)
        {
            newsScrollViewer = Functions.FindChildOfType<ScrollViewer>(sender as ListView);
            if (newsScrollViewer != null)
            {
                newsScrollViewer.ViewChanged += NewsScrollViewer_ViewChanged;
            }
        }

        private async void NewsScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (newsScrollViewer != null)
            {
                if (newsScrollViewer.VerticalOffset < DeviceInformationHelper.GetScreenHeight())
                {
                    if (topPop.IsOpen)
                    {
                        topPop.IsOpen = false;
                    }
                }
                else if (newsScrollViewer.VerticalOffset > DeviceInformationHelper.GetScreenHeight())
                {
                    if (!topPop.IsOpen)
                    {
                        topPop.IsOpen = true;
                    }
                }
                if (newsScrollViewer.VerticalOffset >= newsScrollViewer.ScrollableHeight)  //ListView滚动到底,加载新数据
                {
                    if (!IsDataLoading)  //未加载数据
                    {
                        IsDataLoading = true;
                        await viewModel.Search(key, searchType, pageIndex++);
                        pageIndexDic[pivot.SelectedIndex] = pageIndex;
                        IsDataLoading = false;
                    }
                }
            }
        }

        private void strategysListView_Loaded(object sender, RoutedEventArgs e)
        {
            strategysScrollViewer = Functions.FindChildOfType<ScrollViewer>(sender as ListView);
            if (strategysScrollViewer != null)
            {
                strategysScrollViewer.ViewChanged += StrategysScrollViewer_ViewChanged;
            }
        }

        private async void StrategysScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (strategysScrollViewer != null)
            {
                if (strategysScrollViewer.VerticalOffset < DeviceInformationHelper.GetScreenHeight())
                {
                    if (topPop.IsOpen)
                    {
                        topPop.IsOpen = false;
                    }
                }
                else if (strategysScrollViewer.VerticalOffset > DeviceInformationHelper.GetScreenHeight())
                {
                    if (!topPop.IsOpen)
                    {
                        topPop.IsOpen = true;
                    }
                }
                if (strategysScrollViewer.VerticalOffset >= strategysScrollViewer.ScrollableHeight)  //ListView滚动到底,加载新数据
                {
                    if (!IsDataLoading)  //未加载数据
                    {
                        IsDataLoading = true;
                        await viewModel.Search(key, searchType, pageIndex++);
                        pageIndexDic[pivot.SelectedIndex] = pageIndex;
                        IsDataLoading = false;
                    }
                }
            }
        }


        private bool IsSubscribeHotKeyLoaded = false;
        private async void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedIndex = pivot.SelectedIndex;
            //切换当前页码
            switch(selectedIndex)
            {
                case 0:
                    if (!pageIndexDic.ContainsKey(selectedIndex))
                    {
                        await viewModel.LoadNewsHotKey();
                        pageIndexDic[selectedIndex] = 1;
                    }
                    pageIndex = pageIndexDic[selectedIndex];                   
                    break;
                case 1:
                    if(!pageIndexDic.ContainsKey(selectedIndex))
                    {
                        await viewModel.LoadStrategyHotKey();
                        pageIndexDic[selectedIndex] = 1;
                    }
                    pageIndex = pageIndexDic[selectedIndex];
                   
                    break;
                case 2:
                    if (!IsSubscribeHotKeyLoaded)
                    {
                        await viewModel.LoadSubscribeHotKey();
                        IsSubscribeHotKeyLoaded = false;
                    }
                    break;
            }
        }

        private void subscribeListView_Loaded(object sender, RoutedEventArgs e)
        {
            subscribeScrollViewer = Functions.FindChildOfType<ScrollViewer>(sender as ListView);
            if (subscribeScrollViewer != null)
            {
                subscribeScrollViewer.ViewChanged += SubscribeScrollViewer_ViewChanged;
            }
        }

        private void SubscribeScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (subscribeScrollViewer != null)
            {
                if (subscribeScrollViewer.VerticalOffset < DeviceInformationHelper.GetScreenHeight())
                {
                    if (topPop.IsOpen)
                    {
                        topPop.IsOpen = false;
                    }
                }
                else if (subscribeScrollViewer.VerticalOffset > DeviceInformationHelper.GetScreenHeight())
                {
                    if (!topPop.IsOpen)
                    {
                        topPop.IsOpen = true;
                    }
                }
            
            }
        }

        
        private async void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            await viewModel.RefreshSubscribeHotKey();
        }
    }
}
