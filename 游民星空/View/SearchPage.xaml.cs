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

            //NavigationCacheMode = NavigationCacheMode.Required;
        }


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
                default:
                    searchType = SearchTypeEnum.news;
                    break;
                    //case 2:
                    //    searchType = SearchTypeEnum.
            }
            key = keyTextBox.Text;

            await viewModel.Search(key, searchType, pageIndex);

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
        
        private void subscribeListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var result = e.ClickedItem as Subscribe;
            if (result != null)
            {
                this.Frame.Navigate(typeof(SubscribeContentPage), result);
            }
        }


        /// <summary>
        /// 点击热门标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemClick(object sender, ItemClickEventArgs e)
        {
            keyTextBox.Text = (string)(e.ClickedItem);

            Search();
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViewer = Functions.FindChildOfType<ScrollViewer>(sender as ListView);
            if (scrollViewer != null)
            {
                scrollViewer.ViewChanged += scrollViewer_ViewChanged;
            }
        }

        private bool IsDataLoading = false;
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
                        await viewModel.Search(key, searchType, pageIndex);
                        pageIndexDic[pivot.SelectedIndex] = pageIndex;
                        IsDataLoading = false;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private ScrollViewer scrollViewer;

        private ScrollViewer strategysScrollViewer;
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch(pivot.SelectedIndex)
            {
                case 0:
                    newsListView.ScrollIntoViewSmoothly(newsListView.Items[0]);
                    break;
                case 1:
                    strategyListView.ScrollIntoViewSmoothly(strategysListView.Items[0]);
                    break;
            }
        }
    }
}
