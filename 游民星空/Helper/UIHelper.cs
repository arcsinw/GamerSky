using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace 游民星空.Helper
{
    public static class UIHelper
    {
        public static async void ShowStatusBar()
        {
            var statusBar = StatusBar.GetForCurrentView();
            statusBar.ForegroundColor = Colors.White;
            statusBar.BackgroundOpacity = 1;
            statusBar.BackgroundColor = App.Current.Resources["ThemeColor"] as Color?;
            await statusBar.ShowAsync();
        }
    }
}
