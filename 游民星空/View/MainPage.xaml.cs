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
        private ScrollViewer scrollViewer;

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

        //private DispatcherTimer dt;

        public MainPage()
        {
            this.InitializeComponent();
            apiService = new ApiService();
            NavigationCacheMode = NavigationCacheMode.Required;
        
            DispatcherManager.Current.Dispatcher = Dispatcher;
            pageIndexDic = new Dictionary<int, int>();

            //dt = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1000) };
        }
        /// <summary>
        /// 当前频道Id
        /// </summary>
        private int currentChannelId;
        /// <summary>
        /// 当前频道名
        /// </summary>
        private string currentChannelName;

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


        private async void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            //IsActive = true;
            progressRing.IsActive = true;
            await MVM.Refresh(currentChannelId);
            //IsActive = false;
            progressRing.IsActive = false;
        }

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //IsActive = true;
            progressRing.IsActive = true;
            int index = essayPivot.SelectedIndex;
            currentChannelId = MVM.EssaysAndChannels[index].Channel.nodeId;

            //获取该频道当前页码
            if(!pageIndexDic.ContainsKey(currentChannelId)) // 频道未加载
            {
                pageIndexDic.Add(currentChannelId, 1);
                pageIndex = 1;
                await MVM.LoadMoreEssay(currentChannelId, pageIndex++);
                pageIndexDic[currentChannelId] = pageIndex;

                //popup
                //channelTextBlock.Text = MVM.EssaysAndChannels[index].Channel.nodeName.Trim();
                //popup.IsOpen = true;
                //两秒后关闭
                //dt.Start();
                //dt.Tick += Dt_Tick;
                
            }
            else  //频道已加载
            {
                pageIndex = pageIndexDic[currentChannelId];
            }

            progressRing.IsActive = false;
        }

        //private void Dt_Tick(object sender, object e)
        //{
        //    popup.IsOpen = false;
        //    dt.Stop();
        //}

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Essay essayResult =  e.ClickedItem as Essay;
            if (essayResult == null) return;
            
            (Window.Current.Content as Frame)?.Navigate(typeof(EssayDetail), essayResult);
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
                        //IsActive = true;
                        progressRing.IsActive = true;
                        await MVM.LoadMoreEssay(currentChannelId, pageIndex++);
                        pageIndexDic[currentChannelId] = pageIndex;
                        //IsActive = false;
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

        private void essayListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {

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
        PivotItem currentItem = null;

        private void essayPivot_PivotItemLoaded(Pivot sender, PivotItemEventArgs args)
        {
            currentItem = args.Item;
        }
    }
}
