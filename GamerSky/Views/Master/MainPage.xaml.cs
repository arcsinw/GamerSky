using GamerSky.Controls;
using GamerSky.Models;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
 
namespace GamerSky.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Current = this;
        }

        public static MainPage Current = null;

        private void Current_VisualStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            switch(e.NewState.Name)
            {
                case "Narrow":
                    ShowModuleGrid();
                    //moduleGrid.Visibility = Visibility.Visible;
                    break;
                case "Default":
                    HideModuleGrid();
                    //moduleGrid.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MasterDetailPage.Current.AdaptiveVisualStateChanged += Current_VisualStateChanged;

            if (e.Parameter is NavMenuItem menu)
            {
                frame.Navigate(menu.DestPage);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            MasterDetailPage.Current.AdaptiveVisualStateChanged -= Current_VisualStateChanged;
        }

        private void SimpleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is SimpleRadioButton radioButton)
            {
                switch (radioButton.Tag)
                {
                    case "0":
                        frame.Navigate(typeof(NewsPage));
                        break;
                    case "1":
                        frame.Navigate(typeof(GamePage));
                        break;                                               
                    case "2":                                               
                        frame.Navigate(typeof(GroupPage));
                        break;                                               
                    case "3":                                               
                        frame.Navigate(typeof(OriginalPage));
                        break;                                               
                    case "4":                                               
                        frame.Navigate(typeof(MyPage));
                        break;
                }
            }
        }

        public void ShowModuleGrid()
        {
            moduleGrid.Visibility = Visibility.Visible;
            ShowStoryboard.Begin();
        }

        public void HideModuleGrid()
        {
            HideStoryboard.Begin();
            //moduleGrid.Visibility = Visibility.Collapsed;
        }
    }
}
