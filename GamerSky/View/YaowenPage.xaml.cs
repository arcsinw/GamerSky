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
using GamerSky.Model;
using Arcsinx.Toolkit.Extensions;
using Arcsinx.Toolkit.Helper;

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class YaowenPage : Page
    {
        public YaowenPage()
        {
            this.InitializeComponent(); 
        }

        private ScrollViewer scrollViewer;

        private void Back()
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
          
         
        private void listView_Loaded(object sender, RoutedEventArgs e)
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
            }
        }
         
        private async void listView_RefreshRequested(object sender, EventArgs e)
        {
            viewModel.Refresh();
            await LiveTileHelper.UpdatePrimaryTile();
        }

        private void PullToRefreshListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Essay essayResult = e.ClickedItem as Essay;
            if (essayResult == null) return;

            MasterDetailPage.Current.DetailFrame.Navigate(typeof(ReadEssayPage), essayResult);
        }

        /// <summary>
        /// 跳转到顶部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scrollBtn_Click(object sender, RoutedEventArgs e)
        {
            listView.ScrollIntoViewSmoothly(listView.Items[0]);
        }
    }
}
