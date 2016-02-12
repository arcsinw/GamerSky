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

            viewModel = new SearchPageViewModel();

            pageIndexDic = new Dictionary<int, int>();
         
            NavigationCacheMode = NavigationCacheMode.Required;
        }
        
        public SearchPageViewModel viewModel { get; set; }

        private void Back()
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        #region pageIndex
        /// <summary>
        /// 保存不同频道的页码
        /// </summary>
        private Dictionary<int, int> pageIndexDic;

        private int pageIndex = 1;
        #endregion 

        private async void Search()
        {
            int pivotIndex = pivot.SelectedIndex;
            SearchTypeEnum searchType;
            switch (pivotIndex)
            {
                case 0:
                    searchType = SearchTypeEnum.news;
                    break;
                case 1:
                    searchType = SearchTypeEnum.strategy;
                    break;
                default:
                    searchType = SearchTypeEnum.news;
                    break;
                //case 2:
                //    searchType = SearchTypeEnum.
            }
            string key = keyTextBox.Text;
            await viewModel.Search(key, searchType, pageIndex);
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void strategyListView_ItemClick(object sender, ItemClickEventArgs e)
        {
          
        }

        private void subscribeListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var result = e.ClickedItem as SubscribeResult;
            if (result != null)
            {
                this.Frame.Navigate(typeof(SubscribeContentPage), result);
            }
        }
    }
}
