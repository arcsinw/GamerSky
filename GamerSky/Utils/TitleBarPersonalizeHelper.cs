using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace GamerSky.Utils
{
    public class TitleBarPersonalizeHelper
    {
        public static void PersonalizeTitleBar()
        {
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(320, 400));

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            if (titleBar != null)
            {
                Color titleBarColor = (Color)App.Current.Resources["ThemeColor"];
                //titleBar.InactiveBackgroundColor = (Color)App.Current.Resources["SystemChromeMediumColor"];
                titleBar.BackgroundColor = titleBarColor;
                titleBar.ButtonBackgroundColor = titleBarColor;
            }
        }
    }
}
