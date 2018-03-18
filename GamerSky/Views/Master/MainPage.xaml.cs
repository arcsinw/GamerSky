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


namespace GamerSky.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Current_VisualStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            switch(e.NewState.Name)
            {
                case "Narrow":
                    moduleGrid.Visibility = Visibility.Visible;
                    break;
                case "Default":
                    moduleGrid.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MasterDetailPage.Current.AdaptiveVisualStateChanged += Current_VisualStateChanged;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            MasterDetailPage.Current.AdaptiveVisualStateChanged -= Current_VisualStateChanged;
        }
    }
}
