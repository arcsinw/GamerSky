using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using 游民星空.Core.Helper;
using 游民星空.Core.Http;
using 游民星空.Core.Model;
using 游民星空.Core.ViewModel;

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

        private Visibility isProgressVisible;
        /// <summary>
        /// ProgressRing
        /// </summary>
        public Visibility IsProgressVisible
        {
            get
            {
                return isProgressVisible;
            }
            set
            {
                isProgressVisible = value;
                OnPropertyChanged();
            }
        }


        public MainPage()
        {
            this.InitializeComponent();
            apiService = new ApiService();

            NavigationCacheMode = NavigationCacheMode.Required;
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
        private int pageIndex = 1;
        #endregion 

        private bool IsDataLoaded = false;


        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {

        }

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = essayPivot.SelectedIndex;

            currentChannelId =  MVM.Channels[index].nodeId;

            Debug.WriteLine(MVM.Channels[index].nodeName);

            await MVM.LoadMoreEssay(currentChannelId, pageIndex);
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            EssayResult essay =  e.ClickedItem as EssayResult;
            if (essay == null) return;
            
            (Window.Current.Content as Frame)?.Navigate(typeof(EssayDetail), essay.contentId);
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
                    IsProgressVisible = Visibility.Visible;
                    await MVM.LoadMoreEssay(currentChannelId, pageIndex);
                    pageIndex++;
                    IsProgressVisible = Visibility.Collapsed;
                }
            }
        }

        private void FlipView_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var essay = (sender as FlipView)?.SelectedItem as EssayResult;
            //EssayResult essay = (e.OriginalSource as FlipViewItem;
            if (essay == null) return;

            (Window.Current.Content as Frame)?.Navigate(typeof(EssayDetail), essay.contentId);
        }
    }
}
