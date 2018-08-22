using Arcsinx.Toolkit.Controls;
using Arcsinx.Toolkit.Interfaces;
using GamerSky.Controls;
using GamerSky.Core.Model;
using GamerSky.Helper;
using GamerSky.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GamerSky.View
{ 
    public sealed partial class MasterDetailPage : Page , IBackKeyPressManager
    {
        public static MasterDetailPage Current;
        public MasterDetailPage()
        {
            this.InitializeComponent();
            Current = this;
            GlobalDialog.InitializeDialog(rootGrid, null);
        }
         

        public bool isIgnore { get; set; }

        public void UnRegisterBackKeyPress() => isIgnore = true;

        public void RegisterBackKeyPress() => isIgnore = false;
         
          
        #region UI

        private void MasterDetailPage_OrientationChanged(DisplayInformation sender, object args)
        {
            if (sender.CurrentOrientation == DisplayOrientations.Portrait || sender.CurrentOrientation == DisplayOrientations.PortraitFlipped)
            {
                VisualStateManager.GoToState(this, "Narrow", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "Default", true);
            }
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
            else if (AdaptiveVisualStateGroup.CurrentState == Default)
            {
                DetailFrame.Visibility = Visibility.Visible;

            }
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = DetailFrame.CanGoBack || MasterFrame.CanGoBack ?
             AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void MasterFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.SourcePageType != typeof(MainPage) &&
                e.SourcePageType != typeof(SubscribePage) &&
                e.SourcePageType != typeof(StrategyPage))
            {
                moduleGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                moduleGrid.Visibility = Visibility.Visible;
            }

            UpdateUI();
        }

        private void DetailFrame_Navigated(object sender, NavigationEventArgs e)
        {
            //while(DetailFrame.BackStackDepth > 1)
            //{
            //    DetailFrame.BackStack.RemoveAt(1);
            //}
            UpdateUI();
        }

        #endregion
        
        #region Drawer functions
        /// <summary>
        /// 搜索
        /// </summary>
        public void NavigateToSearchPage()
        {
            NavigationHelper.MasterFrameNavigate(typeof(SearchPage));
        }

        public void NavigateToReplyPage()
        {
            NavigationHelper.MasterFrameNavigate(typeof(ReplyPage));
        }

        /// <summary>
        /// 要闻
        /// </summary>
        public void YaoWen()
        {
            NavigationHelper.MasterFrameNavigate(typeof(YaowenPage));
        }

        /// <summary>
        /// 收藏
        /// </summary>
        public void Favorite()
        {
            NavigationHelper.MasterFrameNavigate(typeof(FavoritePage));
        }

       
      

        /// <summary>
        /// 更多设置
        /// </summary>
        public void NavigateToSettings()
        {
            NavigationHelper.MasterFrameNavigate(typeof(SettingsPage));
        }

        /// <summary>
        /// 反馈
        /// </summary>
        public void FeedBack()
        {
            NavigationHelper.MasterFrameNavigate(typeof(Feedback));
        }

        /// <summary>
        /// 跳转到登录
        /// </summary>
        public void NavigateToLogin()
        {
            if (DataShareManager.Current.CurrentUser == null)
            {
                NavigationHelper.MasterFrameNavigate(typeof(LoginPage));
            }
            else
            {
                ToastService.SendToast("未开发");
            }
        }

        
        #endregion


        private void MasterDetailPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (isIgnore)
            {
                return;
            }

            e.Handled = true;
            if (DetailFrame.CanGoBack)
            { 
                var essayDetailPage = (DetailFrame.Content as ReadEssayPage);
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
            else
            {
                Application.Current.Exit();
            }
        }

        public List<PaneItem> PaneItems { get; set; } = new List<PaneItem>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        { 
            DetailFrame.Navigate(typeof(DefaultPage));
            MasterFrame.Navigate(typeof(MainPage));
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/icon_xinwen_h.png", Title = GlobalStringLoader.GetString("UNews"), SourcePage = typeof(MainPage) });
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/icon_gonglue_h.png", Title = GlobalStringLoader.GetString("UGame"), SourcePage = typeof(StrategyPage) });
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/icon_dingyue_h.png", Title = GlobalStringLoader.GetString("USubscribe"), SourcePage = typeof(SubscribePage) });
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/drawer_news.png", Title = GlobalStringLoader.GetString("UYaowen"), SourcePage = typeof(YaowenPage) });
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/drawer_comment_reply.png", Title = GlobalStringLoader.GetString("UCommentReply"), SourcePage = typeof(ReplyPage) });
            PaneItems.Add(new PaneItem() { Icon = "ms-appx:///Assets/Images/drawer_collect.png", Title = GlobalStringLoader.GetString("UCollection"), SourcePage = typeof(FavoritePage) });
            UIHelper.ShowStatusBar();
             
            SystemNavigationManager.GetForCurrentView().BackRequested += MasterDetailPage_BackRequested;
            DisplayInformation.GetForCurrentView().OrientationChanged += MasterDetailPage_OrientationChanged;
             

            //if (DataShareManager.Current.IsNewVersion)
            //{
            //    new MessageDialog().Show();
            //}
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
                ViewModel.AppTheme = ElementTheme.Light;
                DataShareManager.Current.UpdateAPPTheme(false);
            }
            else
            {
                ViewModel.AppTheme = ElementTheme.Dark;
                DataShareManager.Current.UpdateAPPTheme(true);
            }

            UIHelper.ShowStatusBar();
        }
          
    }
}
