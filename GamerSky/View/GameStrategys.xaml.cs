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
using GamerSky.Helper;
using GamerSky.ViewModel;
using Arcsinx.Toolkit.Extensions;
using Arcsinx.Toolkit.Helper;
using System.Threading.Tasks;
using GamerSky.Core.Model;
using GamerSky.Core.Helper;

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class GameStrategys : Page
    {
        private ScrollViewer scrollViewer;
        public GameStrategys()
        {
            this.InitializeComponent();
        }
 

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Strategy strategyResult = e.Parameter as Strategy;
            if (strategyResult != null)
            {
                viewModel.CurrentStrategy = strategyResult;
            }
        }
 

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Essay essayResult =  e.ClickedItem as Essay;
            if (essayResult == null) return;

            MasterDetailPage.Current.DetailFrame.Navigate(typeof(ReadEssayPage), essayResult);
        }

        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            
        }

        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViewer = Functions.FindChildOfType<ScrollViewer>(sender as ListView);
            if (scrollViewer != null)
            {
                scrollViewer.ViewChanged += scrollViewer_ViewChanged;
            }
        }
         
        private void scrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (scrollViewer != null)
            {
                if(scrollViewer.VerticalOffset <DeviceInformationHelper.GetScreenHeight())
                {
                    if(topPop.IsOpen)
                    {
                        topPop.IsOpen = false;
                    }
                }
                else if(scrollViewer.VerticalOffset> DeviceInformationHelper.GetScreenHeight())
                {
                    if (!topPop.IsOpen)
                    {
                        topPop.IsOpen = true;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listView.ScrollIntoViewSmoothly(listView.Items[0]);
        }

        private void PullToRefreshListView_RefreshRequested(object sender, EventArgs e)
        {
            viewModel.Refresh();
        }
    }
}
