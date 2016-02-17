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

        

        private void hotSubscribeListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var result = e.ClickedItem as Subscribe;
            if (result != null)
            {
                this.Frame.Navigate(typeof(SubscribeContentPage), result);
            }
        }

        private void allSubscribeListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var result = e.ClickedItem as Subscribe;
            if (result != null)
            {
                this.Frame.Navigate(typeof(SubscribeContentPage), result);
            }
        }

        private void mySubscribeListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

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
                case 2:
                    if(isMyLoaded)
                    {
                        isMyLoaded = true;
                    }
                    //await ViewModel.LoadMySubscribes();
                    break;
            }
        }

        private void PivotItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        public void AddSubscribe()
        {

        }

        private async void allPullToRefresh_RefreshInvoked(DependencyObject sender, object args)
        {
            await ViewModel.AllSubscribesRefresh();
        }

        private async void hotPullToRefresh_RefreshInvoked(DependencyObject sender, object args)
        {
            await ViewModel.HotSubscribesRefresh();
        }
    }
}
