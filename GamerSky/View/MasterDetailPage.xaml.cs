using GamerSky.Core.Helper;
using GamerSky.Core.Model;
using GamerSky.Core.ViewModel;
using GamerSky.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
            
        }


        #region Properties
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

        private bool _isNoImg;

        public bool IsNoImgMode
        {
            get { return _isNoImg; }
            set { _isNoImg = value; }
        }


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
        #endregion

        #region INotifyPropertyChanged member
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
          
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
            drawer.DrawerOpened = false;
            NavigationHelper.MasterFrameNavigate(typeof(SearchPage));
        }

        public void NavigateToReplyPage()
        {
            drawer.DrawerOpened = false;
            NavigationHelper.MasterFrameNavigate(typeof(ReplyPage));
        }

        /// <summary>
        /// 要闻
        /// </summary>
        public void YaoWen()
        {
            drawer.DrawerOpened = false;
            NavigationHelper.MasterFrameNavigate(typeof(YaowenPage));
        }

        /// <summary>
        /// 收藏
        /// </summary>
        public void Favorite()
        {
            drawer.DrawerOpened = false;
            NavigationHelper.MasterFrameNavigate(typeof(FavoritePage));
        }

       
        /// <summary>
        /// 是否夜间模式
        /// </summary>
        public bool IsNight
        {
            get
            {
                return DataShareManager.Current.AppTheme == ElementTheme.Dark;
            }
            set
            {
                DataShareManager.Current.UpdateAPPTheme(value);

                UIHelper.ShowStatusBar();
            }
        }

        /// <summary>
        /// 更多设置
        /// </summary>
        public void NavigateToSettings()
        {
            drawer.DrawerOpened = false;
            NavigationHelper.MasterFrameNavigate(typeof(SettingsPage));
        }

        /// <summary>
        /// 反馈
        /// </summary>
        public void FeedBack()
        {
            drawer.DrawerOpened = false;
            NavigationHelper.MasterFrameNavigate(typeof(Feedback));
        }

        /// <summary>
        /// 跳转到登录
        /// </summary>
        public void NavigateToLogin()
        {
            drawer.DrawerOpened = false;
            NavigationHelper.MasterFrameNavigate(typeof(LoginPage));
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public async void ClearCache()
        {
            CacheSize = "删除缓存中...";
            await FileHelper.Current.DeleteCacheFile();
            double cache = await FileHelper.Current.GetCacheSize();
            CacheSize = GetFormatSize(cache);
        }

        private string cacheSize;
        public string CacheSize
        {
            get
            {
                return cacheSize;
            }
            set
            {
                cacheSize = value;
                OnPropertyChanged();
            }
        }

        public async void GetCacheSize()
        {
            double size = await FileHelper.Current.GetCacheSize();
            CacheSize = GetFormatSize(size);
        }

        private string GetFormatSize(double size)
        {
            if (size < 1024)
            {
                return size + "byte";
            }
            else if (size < 1024 * 1024)
            {
                return Math.Round(size / 1024, 2) + "KB";
            }
            else if (size < 1024 * 1024 * 1024)
            {
                return Math.Round(size / 1024 / 1024, 2) + "MB";
            }
            else
            {
                return Math.Round(size / 1024 / 1024 / 2014, 2) + "GB";
            }
        }
        #endregion


        private void MasterDetailPage_BackRequested(object sender, BackRequestedEventArgs e)
        {
            e.Handled = true;
            if (DetailFrame.CanGoBack)
            { 
                var essayDetailPage = (DetailFrame.Content as EssayDetailPage);
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

            AppTheme = DataShareManager.Current.AppTheme;
            User = DataShareManager.Current.CurrentUser;
            IsNoImgMode = DataShareManager.Current.IsNoImage;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;

            SystemNavigationManager.GetForCurrentView().BackRequested += MasterDetailPage_BackRequested;
            DisplayInformation.GetForCurrentView().OrientationChanged += MasterDetailPage_OrientationChanged;
        }


        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
            User = DataShareManager.Current.CurrentUser;
            IsNoImgMode = DataShareManager.Current.IsNoImage;
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
          
    }
}
