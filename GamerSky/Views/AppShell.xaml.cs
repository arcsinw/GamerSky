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

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        public static AppShell Current = null;

        public Frame MasterFrame { get { return this.masterFrame; } }

        public Frame DetailFrame { get { return this.detailFrame; } }

        #region BackRequested Handlers

        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            bool handled = e.Handled;
            this.BackRequested(ref handled);
            e.Handled = handled;
        }

        private void BackRequested(ref bool handled)
        {
            // Get a hold of the current frame so that we can inspect the app back stack.

            if (this.DetailFrame == null && this.MasterFrame == null)
                return;

            // Check to see if this is the top-most page on the app back stack.
            if (this.DetailFrame.CanGoBack && !handled)
            {
                // If not, set the event to handled and go back to the previous page in the app.
                handled = true;
                this.DetailFrame.GoBack();
            }
            else if (this.MasterFrame.CanGoBack && !handled)
            {
                // If not, set the event to handled and go back to the previous page in the app.
                handled = true;
                this.MasterFrame.GoBack();
            }
        }

        #endregion

        public List<NavMenuItem> Menus { get; set; } = new List<NavMenuItem>()
        {
            new NavMenuItem() { Icon = "ms-appx:///Assets/Images/icon_xinwen_h.png", Title = GlobalizationStringLoader.GetString("News"), DestPage = typeof(MainPage) },
            new NavMenuItem() { Icon = "ms-appx:///Assets/Images/icon_gonglue_h.png", Title = GlobalizationStringLoader.GetString("Game"), DestPage = typeof(GamePage) }
        };

        private void AdaptiveVisualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {

        }
    }
}

