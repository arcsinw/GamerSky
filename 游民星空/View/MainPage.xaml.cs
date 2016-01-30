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

        public MainPage()
        {
            this.InitializeComponent();
            apiService = new ApiService();
            NavigationCacheMode = NavigationCacheMode.Required;
            if(Functions.IsMobile())
            {
                UIHelper.ShowStatusBar();
            }
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
        private HashSet<EssayPageIndex> pageIndexs;
        private int pageIndex = 1;
        private int pageIndex1 = 1;
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
            pageIndexs = new HashSet<EssayPageIndex>();
            //currentChannelId =  MVM.Channels[index].nodeId;
            currentChannelId = MVM.EssaysAndChannels[index].Channel.nodeId;

            pageIndexs.Add(new EssayPageIndex { ChannelId = currentChannelId, PageIndex = 1 });

            Debug.WriteLine(MVM.EssaysAndChannels[index].Channel.nodeName);

            await MVM.LoadMoreEssay(currentChannelId, pageIndex);
            pageIndex++;

            //IsActive = false;
            progressRing.IsActive = false;
        }


        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            EssayResult essayResult =  e.ClickedItem as EssayResult;
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

                if (scrollViewer.VerticalOffset >= scrollViewer.ScrollableHeight)  //ListView滚动到底,加载新数据
                {
                    if (!IsDataLoading)  //未加载数据
                    {
                        IsDataLoading = true;
                        //IsActive = true;
                        progressRing.IsActive = true;
                        await MVM.LoadMoreEssay(currentChannelId, pageIndex);
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
            var essayResult = (sender as FlipView)?.SelectedItem as EssayResult;
            //EssayResult essay = (e.OriginalSource as FlipViewItem;
            if (essayResult == null) return;

            (Window.Current.Content as Frame)?.Navigate(typeof(EssayDetail), essayResult);
        }
    }
}
