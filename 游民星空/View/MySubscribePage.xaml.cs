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
using 游民星空.Core.Helper;
using 游民星空.Core.Model;
using 游民星空.Core.ViewModel;
using 游民星空.Helper;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace 游民星空.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MySubscribePage : Page
    {
        public MySubscribePage()
        {
            this.InitializeComponent();
        }

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

        private void SubscribeListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var result = e.ClickedItem as Subscribe;
            if (result != null)
            {
                this.Frame.Navigate(typeof(SubscribeContentPage), result.sourceId);
            }
        }
   
        /// <summary>
        /// 后退
        /// </summary>
        public void Back()
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private bool isHotLoaded = false;
        private bool isAllLoaded = false;
        private bool isMyLoaded = false;

        private async void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(pivot.SelectedIndex)
            {
                case 0:
                    if (!isHotLoaded)
                    {
                        await ViewModel.LoadHotSubscribes();
                        isHotLoaded = true;
                    }
                    break;
                case 1:
                    if (!isAllLoaded)
                    {
                        await ViewModel.LoadAllSubscribes();
                        isAllLoaded = true;
                    }
                    break;
                //case 2:
                //    if(isMyLoaded)
                //    {
                //        ViewModel.LoadMySubscribes();
                //        isMyLoaded = true;
                //    }
                    
                //    break;
            }
        }
         
        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var control = e.OriginalSource as FrameworkElement;
            if (control == null) return;
            var dataContext = control.DataContext as Subscribe;
            if (dataContext == null) return;
            DataShareManager.Current.UpdateSubscribe(dataContext);
        }
         
        /// <summary>
        /// 下拉刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private async void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {
            switch(pivot.SelectedIndex)
            {
                case 0:
                    await ViewModel.HotSubscribesRefresh();
                    break;
                case 1:
                    await ViewModel.AllSubscribesRefresh();
                    break;
                case 2:
                    ViewModel.MySubscribesRefresh();
                    break;
            }
        }
    }
}
