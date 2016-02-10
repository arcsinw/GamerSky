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
using 游民星空.Core.Model;
using 游民星空.Core.ViewModel;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace 游民星空.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class StrategyPage : Page
    {
        public StrategyPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            pageIndexDic = new Dictionary<int, int>();
        }

        /// <summary>
        /// 保存不同频道的页码
        /// </summary>
        private Dictionary<int, int> pageIndexDic;

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            progress.IsActive = true;
            if (pivot.SelectedIndex == 0)
            {
                if (!pageIndexDic.ContainsKey(0))
                {
                    await ViewModel.LoadFocusStrategys();
                    pageIndexDic.Add(0, 1);
                }

            }
            else
            {
                if (!pageIndexDic.ContainsKey(1))
                {
                    await ViewModel.LoadAllStrategys();
                    pageIndexDic.Add(1, 1);
                }
            }
            progress.IsActive = false;
        }

        /// <summary>
        /// 下拉刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            progress.IsActive = true;
            if(pivot.SelectedIndex==0)
            {
                await ViewModel.RefreshFocusStrategy();
            }
            else
            {
                await ViewModel.RefreshAllStrategy();
            }
            progress.IsActive = false;
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            StrategyResult result =  e.ClickedItem as StrategyResult;
            if(result!=null)
            {
                (Window.Current.Content as Frame)?.Navigate(typeof(GameStrategys), result);
            }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            StrategyResult result = e.ClickedItem as StrategyResult;
            if (result != null)
            {
                (Window.Current.Content as Frame)?.Navigate(typeof(GameStrategys), result);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame)?.Navigate(typeof(SearchPage));
            //this.Frame.Navigate(typeof(SearchPage));
        }

        private void PivotItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                var pivotItem = sender as PivotItem;
                if(pivotItem.ActualHeight>=320 && pivotItem.ActualHeight<=720)
                {

                }
            }
            catch(Exception ex)
            {

            }
        }

    
    }
}
