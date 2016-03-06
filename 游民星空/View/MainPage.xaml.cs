using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using 游民星空.Core.Helper;
using 游民星空.Core.Http;
using 游民星空.Core.Model;
using 游民星空.Helper;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace 游民星空.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ApiService apiService;
        //当前ListView中的ScrollViewer
        private ScrollViewer scrollViewer;
        // 当前频道Id
        private int currentChannelId;
        // 当前PivotItem
        PivotItem currentItem;
 
        #region pageIndex
        /// <summary>
        /// 保存不同频道的页码
        /// </summary>
        private Dictionary<int, int> pageIndexDic;

        private int pageIndex = 1;
        #endregion 

        /// <summary>
        /// 是否正在加载数据
        /// </summary>
        private bool IsDataLoading = false;
        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
         
        private bool isActive;
        /// <summary>
        /// ProgressRing is active
        /// </summary>
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                OnPropertyChanged("IsActive");
            }
        }
         
        public MainPage()
        {
            this.InitializeComponent();
            apiService = new ApiService();
            NavigationCacheMode = NavigationCacheMode.Required;
        
            DispatcherManager.Current.Dispatcher = Dispatcher;
            pageIndexDic = new Dictionary<int, int>();
             
        }
      
        private async void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            progressRing.IsActive = true;
            await MVM.Refresh(currentChannelId);
            progressRing.IsActive = false;
        }

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("SelectionChanged");
            if (currentItem != null)
            {
                ListView listView = Functions.FindChildOfType<ListView>(currentItem);
                scrollViewer = Functions.FindChildOfType<ScrollViewer>(listView);
                if (scrollViewer != null)
                {
                    scrollViewer.ViewChanged += scrollViewer_ViewChanged;
                }
            }
            progressRing.IsActive = true;
            int index = essayPivot.SelectedIndex;
            currentChannelId = MVM.EssaysAndChannels[index].Channel.nodeId;

            //获取该频道当前页码
            if (!pageIndexDic.ContainsKey(currentChannelId)) // 频道未加载
            {
                pageIndexDic.Add(currentChannelId, 1);
                pageIndex = 1;
                await MVM.LoadMoreEssay(currentChannelId, pageIndex++);
                pageIndexDic[currentChannelId] = pageIndex;
            }
            else  //频道已加载
            {
                pageIndex = pageIndexDic[currentChannelId];
            }

            progressRing.IsActive = false;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Essay essayResult =  e.ClickedItem as Essay;
            if (essayResult == null) return;
            
            (Window.Current.Content as Frame)?.Navigate(typeof(EssayDetail), essayResult);
        }

        /// <summary>
        /// 获取当前PivotItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void essayPivot_PivotItemLoaded(Pivot sender, PivotItemEventArgs args)
        {
            Debug.WriteLine("PivotITemLoaded");
            currentItem = args.Item;

        }

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
                        progressRing.IsActive = true;
                        await MVM.LoadMoreEssay(currentChannelId, pageIndex++);
                        pageIndexDic[currentChannelId] = pageIndex;
                        progressRing.IsActive = false;
                        IsDataLoading = false;
                    }
                }
            }
        }

        private void FlipView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var essayResult = (sender as FlipView)?.SelectedItem as Essay;
            if (essayResult == null) return;

            (Window.Current.Content as Frame)?.Navigate(typeof(EssayDetail), essayResult);
        }

        /// <summary>
        /// scoll to top
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListView listView = Functions.FindChildOfType<ListView>(currentItem);
            listView.ScrollIntoViewSmoothly(listView.Items[0]);
        }
         
    }
}
