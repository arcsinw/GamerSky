using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GamerSky.Core.Model;

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class StrategyPage : Page
    {
        #region Properties

        private int pageIndex;  //当前页码

        /// <summary>
        /// 保存不同频道的页码
        /// </summary>
        private Dictionary<int, int> pageIndexDic = new Dictionary<int, int>(); 
        #endregion

        public StrategyPage()
        {
            this.InitializeComponent();
            
        }
         
        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(!pageIndexDic.ContainsKey(pivot.SelectedIndex))
            {
                switch(pivot.SelectedIndex)
                {
                    case 0:
                        pageIndexDic.Add(pivot.SelectedIndex, 1);
                        await ViewModel.LoadFocusStrategys();
                        break;
                    case 1:
                        pageIndexDic.Add(pivot.SelectedIndex, 1);
                        await ViewModel.LoadAllStrategys();
                        break;
                    case 2:
                        pageIndexDic.Add(pivot.SelectedIndex, 1);
                        //await ViewModel.LoadGameList(pageIndex);
                        break;
                }
                
            }
            pageIndex = pageIndexDic[pivot.SelectedIndex] ++;
        }

        /// <summary>
        /// 下拉刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            
            switch (pivot.SelectedIndex)
            {
                case 0:
                    await ViewModel.RefreshFocusStrategy();
                    break;
                case 1:
                    await ViewModel.RefreshAllStrategy();
                    break;
                case 2:
                    ViewModel.RefreshGameList();
                    break;
            }
            
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

        public void NavigateToGameSalePage()
        {
            MasterDetailPage.Current.DetailFrame.Navigate(typeof(GameSalePage));
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
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(GameDetailPage), game.ContentId);
            }
        }

        private async void PullToRefreshListView_RefreshRequested(object sender, System.EventArgs e)
        {
            switch (pivot.SelectedIndex)
            {
                case 0:
                    await ViewModel.RefreshFocusStrategy();
                    break;
                case 1:
                    await ViewModel.RefreshAllStrategy();
                    break;
                case 2:
                    ViewModel.RefreshGameList();
                    break;
            }
        }
    }
}
