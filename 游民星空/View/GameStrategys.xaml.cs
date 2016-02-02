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
    public sealed partial class GameStrategys : Page
    {
        public GameStrategys()
        {
            this.InitializeComponent();
        }
        private GameStrategysViewModel viewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            progress.IsActive = true;
            StrategyResult strategyResult = e.Parameter as StrategyResult;
            if (strategyResult != null)
            {
                this.DataContext = viewModel = new GameStrategysViewModel(strategyResult);
            }
            progress.IsActive = false;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            EssayResult essayResult =  e.ClickedItem as EssayResult;
            if (essayResult == null) return;

            (Window.Current.Content as Frame)?.Navigate(typeof(EssayDetail), essayResult);
        }

        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            viewModel.Refresh();
        }
    }
}
