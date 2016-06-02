﻿using GamerSky.Core.Helper;
using GamerSky.Core.Model;
using GamerSky.Core.ViewModel;
using GamerSky.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public sealed partial class MasterDetailPage : Page , INotifyPropertyChanged
    {
        public static MasterDetailPage Current;
        public MasterDetailPage()
        {
            this.InitializeComponent();
            Current = this;
            SystemNavigationManager.GetForCurrentView().BackRequested += MasterDetailPage_BackRequested;

            AppTheme = DataShareManager.Current.AppTheme;
            User = DataShareManager.Current.CurrentUser;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
            
        }


        #region AppTheme
        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
            User = DataShareManager.Current.CurrentUser;
        }

        private ElementTheme appTheme;
        public ElementTheme AppTheme
        {
            get
            {
                return appTheme;
            }
            set
            {
                appTheme = value;
                OnPropertyChanged();
            }
        }
        #endregion

        private User user;
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                //DataShareManager.Current.UpdateUser(value);
                OnPropertyChanged();
            }
        }

        #region INotifyPropertyChanged member
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion



        private void MasterDetailPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            if (DetailFrame.CanGoBack)
            {
                var essayDetailPage = (DetailFrame.Content as EssayDetail);
                if (essayDetailPage != null)
                {
                    
                    essayDetailPage.CloseImageFlipView();
                }
                else
                {
                    DetailFrame.GoBack();
                }
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
            //PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/drawer_user.png", Title = "Login", SourcePage = typeof(LoginPage) });
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/icon_xinwen_h.png", Title = GlobalStringLoader.GetString("News") ,SourcePage = typeof(MainPage) });
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/icon_gonglue_h.png", Title = GlobalStringLoader.GetString("Game"), SourcePage = typeof(StrategyPage) });
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/icon_dingyue_h.png", Title = GlobalStringLoader.GetString("Subscribe"), SourcePage = typeof(SubscribePage) });
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/drawer_news.png", Title = GlobalStringLoader.GetString("Yaowen"), SourcePage = typeof(YaowenPage) });
            //PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/drawer_night.png", Title = GlobalStringLoader.GetString("Night") });
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/drawer_collect.png", Title = GlobalStringLoader.GetString("Collect"), SourcePage = typeof(FavoritePage) });
            UIHelper.ShowStatusBar();
            
        }


        private void AdaptiveVisualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (AdaptiveVisualStateGroup.CurrentState == Narrow)
            {
                DetailFrame.Visibility = DetailFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
            }
            else if(AdaptiveVisualStateGroup.CurrentState == Default)
            {
                DetailFrame.Visibility = Visibility.Visible;
            }
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = DetailFrame.CanGoBack || MasterFrame.CanGoBack ?
             AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void MasterFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateUI();
        }

        private void DetailFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateUI();
        }

        #region Click handler
        private void setting_Click(object sender, RoutedEventArgs e)
        {
            MasterFrame.Navigate(typeof(SettingsPage));
        }

        private void paneListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as PaneItem;
            MasterFrame.Navigate(item.SourcePage);
        }
          
        private void themeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataShareManager.Current.AppTheme == ElementTheme.Dark)
            {
                DataShareManager.Current.UpdateAPPTheme(false);
            }
            else
            {
                DataShareManager.Current.UpdateAPPTheme(true);
            }
            UIHelper.ShowStatusBar();
        }
        #endregion

        private void userBtn_Click(object sender, RoutedEventArgs e)
        {
            MasterDetailPage.Current.MasterFrame.Navigate(typeof(LoginPage));
        }
    }
}
