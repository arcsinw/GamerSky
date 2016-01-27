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

        private int isProgressVisibility;
        public int IsProgressVisibility
        {
            get
            {
                return isProgressVisibility;
            }
            set
            {
                isProgressVisibility = value;
            }
        }
        public MainPage()
        {
            apiService = new ApiService();
            this.InitializeComponent();
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

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            EssayResult essay =  e.ClickedItem as EssayResult;
            if (essay == null) return;
            
            (Window.Current.Content as Frame)?.Navigate(typeof(EssayDetail), essay.contentId);
        }

        private async void ListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            List<EssayResult> essays = await apiService.LoadMoreEssay(currentChannelId, pageIndex);
            if (essays == null) return;
            foreach (var essay in essays)
            {
                MVM.EssaysDictionary[currentChannelName]?.Add(essay);
            }
           
        }
    }
}
