using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using 游民星空.Core.Helper;

namespace 游民星空.Helper
{
    public static class UIHelper
    {
        /// <summary>
        /// 用于Mobile 显示StatusBar
        /// </summary>
        public static async void ShowStatusBar()
        {
            if (Functions.IsMobile())
            {
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.ForegroundColor = Colors.White;
                statusBar.BackgroundOpacity = 1;
                statusBar.BackgroundColor = App.Current.Resources["ThemeColor"] as Color?;
                await statusBar.ShowAsync();
            }
        }

        public static async void SetStatusBarColor(Color color)
        {
            if (Functions.IsMobile())
            {
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.ForegroundColor = Colors.White;
                statusBar.BackgroundOpacity = 1;
                statusBar.BackgroundColor = color;
                await statusBar.ShowAsync();
            }
        }

        /// <summary>
        /// 更改标题栏样式 用于PC
        /// </summary>
        public static  void ShowView()
        {
            ApplicationView applicationView = ApplicationView.GetForCurrentView();
            applicationView.SetPreferredMinSize(new Windows.Foundation.Size(320, 480));
            applicationView.ShowStandardSystemOverlays();
            //应用标题栏
            ApplicationViewTitleBar titleBar = applicationView.TitleBar;
            titleBar.BackgroundColor = App.Current.Resources["ThemeColorBrush"] as Color?;
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
           
        }

        public static async void ShowMessage(string content, string title="")
        {
            await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
            {
                await new MessageDialog(content, title).ShowAsync();
            });
        }
    }
}
