using GamerSky.Models;
using GamerSky.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace GamerSky.Views
{
    public sealed partial class AppShell : Page
    {
        public AppShell()
        {
            this.InitializeComponent();

            Current = this; 
        }

        public static AppShell Current = null;

        public event EventHandler<VisualStateChangedEventArgs> AdaptiveVisualStateChanged;


        public List<NavMenuItem> Menus { get; set; } = new List<NavMenuItem>()
        {
            new NavMenuItem() { Icon = "ms-appx:///Assets/Images/icon_xinwen_h.png", Title = GlobalizationStringLoader.GetString("News"), DestPage = typeof(MainPage) },
            new NavMenuItem() { Icon = "ms-appx:///Assets/Images/icon_gonglue_h.png", Title = GlobalizationStringLoader.GetString("Game"), DestPage = typeof(GamePage) }
        };

        private void VisualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            AdaptiveVisualStateChanged?.Invoke(sender, e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame.Navigate(typeof(MasterDetailPage));
        }
    }
}

