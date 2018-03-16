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
            MasterDetailPage.Current.MasterFrame.Navigate(sourcePage, parameter);
        }

        public static void MasterFrameNavigate(Type sourcePage)
        {
            MasterDetailPage.Current.MasterFrame.Navigate(sourcePage);
        }

        public static void DetailFrameNavigate(Type sourcePage)
        {
            MasterDetailPage.Current.DetailFrame.Navigate(sourcePage);
        }

        public static void DetailFrameNavigate(Type sourcePage, object parameter)
        {
            MasterDetailPage.Current.DetailFrame.Navigate(sourcePage, parameter);
        }

        public static void GoBack()
        {
            if(MasterDetailPage.Current.DetailFrame.CanGoBack)
            {
                MasterDetailPage.Current.DetailFrame.GoBack();
            }
            else if(MasterDetailPage.Current.MasterFrame.CanGoBack)
            {
                MasterDetailPage.Current.MasterFrame.GoBack();
            }
            else
            {
                Application.Current.Exit();
            }
        }
    }
}
