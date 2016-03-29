using System;
using Windows.ApplicationModel.Core;
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
        /// Mobile 显示StatusBar
        /// PC 显示标题栏
        /// </summary>
        public static async void ShowStatusBar()
        {
            if (Functions.IsMobile())
            {
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.ForegroundColor = Colors.White;
                statusBar.BackgroundOpacity = 1;
                statusBar.BackgroundColor = Application.Current.Resources["ThemeColor"] as Color?;
                await statusBar.ShowAsync();
            }
            else
            {
                ShowView();
            }
        }

        /// <summary>
        /// 隐藏StatusBar
        /// </summary>
        public static async void HideStatusBar()
        {
            var statusBar = StatusBar.GetForCurrentView();
            await statusBar.HideAsync();
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
            else
            {
                ApplicationView applicationView = ApplicationView.GetForCurrentView();
                applicationView.SetPreferredMinSize(new Windows.Foundation.Size(320, 480));
                applicationView.ShowStandardSystemOverlays();
                //应用标题栏
                ApplicationViewTitleBar titleBar = applicationView.TitleBar;
                titleBar.BackgroundColor = color;
                titleBar.ForegroundColor = Colors.White;
                titleBar.ButtonBackgroundColor = color;
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
            titleBar.BackgroundColor = Application.Current.Resources["ThemeColor"] as Color?;
            titleBar.ForegroundColor = Colors.White;
            titleBar.ButtonBackgroundColor = Application.Current.Resources["ThemeColor"] as Color?;
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
           
        }

        public static async void ShowMessage(string content, string title="")
        {
            await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                await new MessageDialog(content, title).ShowAsync();
            });
        }
    }
}
