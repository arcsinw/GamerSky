using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GamerSky.Core.Helper;
using GamerSky.Core.Model;
using GamerSky.Core.ViewModel;
using GamerSky.Helper;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class StrategyPage : Page
    {
        public StrategyPage()
        {
            this.InitializeComponent();
           
            pageIndexDic = new Dictionary<int, int>();
        }

        private int pageIndex;  //当前页码

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

        /// <summary>
        /// 保存不同频道的页码
        /// </summary>
        private Dictionary<int, int> pageIndexDic;

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            progress.IsActive = true;
            switch(pivot.SelectedIndex)
            {
                case 0:
                    if (!pageIndexDic.ContainsKey(0))
                    {
                        await ViewModel.LoadFocusStrategys();
                        pageIndexDic.Add(0, 1);
                    }
                    
                    break;
                case 1:
                    if (!pageIndexDic.ContainsKey(1))
                    {
                        await ViewModel.LoadAllStrategys();
                        pageIndexDic.Add(1, 1);
                    }
                    break;
                case 2:
                    if (!pageIndexDic.ContainsKey(2))
                    {
                        pageIndexDic.Add(2, 1);
                        await ViewModel.LoadGameList(1);
                    }
                    break;
            }
            pageIndex = pageIndexDic[pivot.SelectedIndex] +1;
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
            Strategy result =  e.ClickedItem as Strategy;
            if(result!=null)
            {
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(GameStrategys), result);
            }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Strategy result = e.ClickedItem as Strategy;
            if (result != null)
            {
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(GameStrategys), result);
            }
        }

        /// <summary>
        /// 订阅一个游戏攻略
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Subscribe(object sender, RoutedEventArgs e)
        {
            Strategy data = (e.OriginalSource as Button).DataContext as Strategy;
            if (data != null)
            {
                ViewModel.Subscribe(data);
            }
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            MasterDetailPage.Current.DetailFrame.Navigate(typeof(SearchPage));
            
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
                Debug.WriteLine("StrategyPage.xaml.cs"+ex.Message);
            }
        }

        /// <summary>
        /// 游戏库点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameLibGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var game = e.ClickedItem as Game;
            if (game != null)
            {
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(GameDetailPage), game.contentId);
            }
        } 
    }
}
