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
using 游民星空.Helper;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace 游民星空.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            if (Functions.IsMobile())
            {
                UIHelper.ShowStatusBar();
            }

            rootFrame.SourcePageType = typeof(MainPage);

            DispatcherManager.Current.Dispatcher = Dispatcher;
        }



        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                switch ((string)radioButton.Tag)
                {
                    case "0":     // 新闻
                        rootFrame.Navigate(typeof(MainPage));
                        break;
                    case "1":     //攻略
                        rootFrame.Navigate(typeof(StrategyPage));
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                }
            }
        }
    }
}
