using GamerSky.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace GamerSky.Utils
{
    public class NavigationHelper
    {
        public static void MasterFrameNavigate(Type sourcePage, object parameter)
        {
            AppShell.Current.MasterFrame.Navigate(sourcePage, parameter);
        }

        public static void MasterFrameNavigate(Type sourcePage)
        {
            AppShell.Current.MasterFrame.Navigate(sourcePage);
        }

        public static void DetailFrameNavigate(Type sourcePage)
        {
            AppShell.Current.DetailFrame.Navigate(sourcePage);
        }

        public static void DetailFrameNavigate(Type sourcePage, object parameter)
        {
            AppShell.Current.DetailFrame.Navigate(sourcePage, parameter);
        }

        public static void GoBack()
        {
            if(AppShell.Current.DetailFrame.CanGoBack)
            {
                AppShell.Current.DetailFrame.GoBack();
            }
            else if(AppShell.Current.MasterFrame.CanGoBack)
            {
                AppShell.Current.MasterFrame.GoBack();
            }
            else
            {
                Application.Current.Exit();
            }
        }
    }
}
