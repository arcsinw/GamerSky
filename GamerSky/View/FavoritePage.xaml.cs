using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using GamerSky.Core.Helper;
using GamerSky.Core.Model;
using GamerSky.Core.ViewModel;
using GamerSky.Helper;
using GamerSky.Core.Common;
 

namespace GamerSky.View
{ 
    public sealed partial class FavoritePage : Page ,INotifyPropertyChanged
    {
        public FavoritePage()
        {
            this.InitializeComponent();

            FavoriteEssays = new ObservableCollection<Essay>();

            AppTheme = DataShareManager.Current.AppTheme;
            FavoriteEssays = DataShareManager.Current.FavoriteList;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
            
        }


        private bool CanExecuteDeleteItemCommand(Essay item)
        {
            return true;
        }

        private void ExecuteDeleteItemCommand(Essay item)
        {
            FavoriteEssays.Remove(item);
        }

        private DelegateCommand<Essay> _deleteItem = default(DelegateCommand<Essay>);

        public DelegateCommand<Essay> DeleteItem => _deleteItem ?? (_deleteItem = new DelegateCommand<Essay>(ExecuteDeleteItemCommand, CanExecuteDeleteItemCommand));
         
        public ObservableCollection<Essay> FavoriteEssays { get; set; }
         

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

        public void Back()
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
            FavoriteEssays = DataShareManager.Current.FavoriteList;
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

        private bool isActive = false;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 下拉刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            Refresh();
        }

        private void Refresh()
        {
            IsActive = true;
            FavoriteEssays.Clear();
            IsActive = false;
        }

        private void refreshAppbar_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void findAppbar_Click(object sender, RoutedEventArgs e)
        {

        }
        bool isMutiple = false;
        private void selectAppbar_Click(object sender, RoutedEventArgs e)
        {
            if (!isMutiple)
            {
                listView.SelectionMode = ListViewSelectionMode.Multiple;
                listView.IsItemClickEnabled = false;
                isMutiple = true;
            }
            else
            {
                listView.SelectionMode = ListViewSelectionMode.None;
                listView.IsItemClickEnabled = true;
                isMutiple = false;
            }
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Essay essayResult = e.ClickedItem as Essay;
            if (essayResult == null) return;
            if (!essayResult.ContentType.Equals("zhuanti"))
            {
               NavigationHelper.DetailFrameNavigate(typeof(ReadEssayPage), essayResult);
            }
            else
            {
                NavigationHelper.DetailFrameNavigate(typeof(SubscribeContentPage), essayResult.ContentId);
            }
        }
    }
}
