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
    public sealed partial class GameDetailPage : Page
    {
        public GameDetailPage()
        {
            this.InitializeComponent();
            this.Loaded += GameDetailPage_Loaded;
        }

        public void Back()
        {
            var frame = (Window.Current.Content as Frame);
            if (frame == null) return;
            if(frame.CanGoBack)
            {
                frame.GoBack();
            }
        }

        private void GameDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.LoadGameDetail();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var para = e.Parameter as string;
            if (para != null)
            {
                viewModel.contentId = para;
            }
        }

        private void Pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(pivot.SelectedIndex)
            {
                case 0:
                    viewModel.LoadGameNews(1);
                    break;
                case 1:
                    viewModel.LoadGameStrategys(1);
                    break;
            }
        }

 

      
    }
}
