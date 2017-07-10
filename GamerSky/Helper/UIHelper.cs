using System;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using GamerSky.Helper;
using GamerSky.ViewModel;
using Arcsinx.Toolkit.Helper;

namespace GamerSky.Helper
{
    public static class UIHelper
    {
        public static StatusBar statusBar;
        public static DispatcherTimer dispatcherTimer;

        static UIHelper()
        {
            if (Functions.IsMobile())
            {
                statusBar = StatusBar.GetForCurrentView();
            }
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0, 0, 3);

        }
        /// <summary>
        /// Mobile 显示StatusBar
        /// PC 显示标题栏
        /// </summary>
        public static async void ShowStatusBar()
        {
            if (Functions.IsMobile())
            {
               
                statusBar.ForegroundColor = Colors.White;
                statusBar.BackgroundOpacity = 1;
                if (DataShareManager.Current.AppTheme == ElementTheme.Dark)
                {
                    statusBar.BackgroundColor = App.Current.Resources["DarkThemeColor"] as Color?;
                }
                else
                {
                    statusBar.BackgroundColor = Application.Current.Resources["LightThemeColor"] as Color?;
                }
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
            await statusBar.HideAsync();
        }
         
        /// <summary>
        /// 更改标题栏样式 用于PC
        /// </summary>
        public static  void ShowView()
        {
            ApplicationView applicationView = ApplicationView.GetForCurrentView();
            applicationView.SetPreferredMinSize(new Windows.Foundation.Size(1366, 768));
            applicationView.ShowStandardSystemOverlays();
            //应用标题栏
            ApplicationViewTitleBar titleBar = applicationView.TitleBar;
            if (DataShareManager.Current.AppTheme == ElementTheme.Dark)
            {
                titleBar.ButtonBackgroundColor = Application.Current.Resources["DarkThemeColor"] as Color?;
                titleBar.BackgroundColor = Application.Current.Resources["DarkThemeColor"] as Color?;
            }
            else
            {
                titleBar.ButtonBackgroundColor = Application.Current.Resources["LightThemeColor"] as Color?;
                titleBar.BackgroundColor = Application.Current.Resources["LightThemeColor"] as Color?;
            }
            titleBar.ForegroundColor = Colors.White;
            //var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
           
        }

        /// <summary>
        /// show message through StatusBar
        /// </summary>
        /// <param name="message"></param>
        public static async void ShowToast(string message)
        {
            if (Functions.IsMobile())
            {
                dispatcherTimer.Tick += async(sender, e) =>
                {
                    await statusBar.ProgressIndicator.HideAsync();
                    dispatcherTimer.Stop();
                };
                statusBar.ForegroundColor = Colors.White;
                statusBar.ProgressIndicator.Text = message;
                statusBar.ProgressIndicator.ProgressValue = 0;
                await  statusBar.ProgressIndicator.ShowAsync();

                dispatcherTimer.Start();
            }
            else
            {
                ShowView();
            }
        }
        

        /// <summary>
        /// show MessageDialog
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        public static async void ShowMessage(string content, string title="")
        {
            await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
            {
                await new MessageDialog(content, title).ShowAsync();
            });
        }
    }
}
