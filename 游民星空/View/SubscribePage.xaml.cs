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

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace 游民星空.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SubscribePage : Page
    {
        public SubscribePage()
        {
            this.InitializeComponent();
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PivotItem_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void NavigatoToMySubscribe()
        {
            (Window.Current.Content as Frame)?.Navigate(typeof(MySubscribePage));
        }

        private void PullToRefreshBox_RefreshInvoked(DependencyObject sender, object args)
        {

        }

        public void Back()
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        public void AddSubscribe()
        {

        }
    }
}
