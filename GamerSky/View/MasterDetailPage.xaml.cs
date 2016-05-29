using GamerSky.Core.Helper;
using GamerSky.Core.Model;
using GamerSky.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MasterDetailPage : Page
    {
        public static MasterDetailPage Current;
        public MasterDetailPage()
        {
            this.InitializeComponent();
            Current = this;
            SystemNavigationManager.GetForCurrentView().BackRequested += MasterDetailPage_BackRequested;
            
        }

        private void MasterDetailPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (DetailFrame.CanGoBack)
            {
                DetailFrame.GoBack();
            }
            else if (MasterFrame.CanGoBack)
            {
                MasterFrame.GoBack();
            }
        }

        public List<PaneItem> PaneItems { get; set; } = new List<PaneItem>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MasterFrame.Navigate(typeof(MainPage));
            DetailFrame.Navigate(typeof(DefaultPage));
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/icon_xinwen_h.png", SourcePage = typeof(MainPage) });
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/icon_gonglue_h.png", Title = GlobalStringLoader.GetString("Game"), SourcePage = typeof(StrategyPage) });
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/icon_dingyue_h.png", Title = GlobalStringLoader.GetString("News"), SourcePage = typeof(SubscribePage) });
            UIHelper.ShowStatusBar();
        }


        private void AdaptiveVisualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateBackKey();
        }

        private void UpdateBackKey()
        {
            if (AdaptiveVisualStateGroup.CurrentState == Narrow)
            {
                DetailFrame.Visibility = DetailFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
            }
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = DetailFrame.CanGoBack || MasterFrame.CanGoBack ?
             AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void MasterFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateBackKey();
        }

        private void DetailFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateBackKey();
        }

        private void setting_Click(object sender, RoutedEventArgs e)
        {
            MasterFrame.Navigate(typeof(SettingsPage));
        }

        private void paneListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as PaneItem;
            MasterFrame.Navigate(item.SourcePage);
        }
    }
}
