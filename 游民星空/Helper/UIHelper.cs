﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

namespace 游民星空.Helper
{
    public static class UIHelper
    {
        /// <summary>
        /// 用于Mobile 显示StatusBar
        /// </summary>
        public static async void ShowStatusBar()
        {
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ForegroundColor = Colors.White;
            statusBar.BackgroundOpacity = 1;
            statusBar.BackgroundColor = App.Current.Resources["ThemeColor"] as Color?;
            await statusBar.ShowAsync();
        }

        public static async void SetStatusBarColor(Color color)
        {
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ForegroundColor = Colors.White;
            statusBar.BackgroundOpacity = 1;
            statusBar.BackgroundColor = color;
            await statusBar.ShowAsync();
        }

        /// <summary>
        /// 更改标题栏样式 用于PC
        /// </summary>
        public static  void ShowView()
        {
            ApplicationView applicationView = ApplicationView.GetForCurrentView();

            //应用标题栏
            ApplicationViewTitleBar titleBar = applicationView.TitleBar;
            titleBar.BackgroundColor = App.Current.Resources["ThemeColorBrush"] as Color?;
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            
        }
    }
}
