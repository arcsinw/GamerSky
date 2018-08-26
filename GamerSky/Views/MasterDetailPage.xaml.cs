using GamerSky.Models;
using GamerSky.Utils;
using GamerSky.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Core;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MasterDetailPage : Page
    {
        public MasterDetailPage()
        {
            this.InitializeComponent();

            Current = this;

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
            DisplayInformation.GetForCurrentView().OrientationChanged += AppShell_OrientationChanged;
        }


        private void AppShell_OrientationChanged(DisplayInformation sender, object args)
        {
            if (sender.CurrentOrientation == DisplayOrientations.Portrait || sender.CurrentOrientation == DisplayOrientations.PortraitFlipped)
            {
                VisualStateManager.GoToState(this, "Narrow", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "Default", true);
            }
        }
        

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

        public static MasterDetailPage Current = null;

        public event EventHandler<VisualStateChangedEventArgs> AdaptiveVisualStateChanged;

        private void AdaptiveVisualStateGroup_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            UpdateForVisualState(); 
        }

        
        private void UpdateForVisualState()
        {
            var isNarrow = AdaptiveVisualStateGroup.CurrentState.Name == "Narrow";
            if (isNarrow)
            {
                DetailFrame.Visibility = DetailFrame.CanGoBack ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                DetailFrame.Visibility = Visibility.Visible;
            }

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = 
                DetailFrame.CanGoBack || MasterFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            AppShell.Current.AdaptiveVisualStateChanged -= Current_AdaptiveVisualStateChanged;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MasterFrame.Navigate(typeof(MainPage));
            DetailFrame.Navigate(typeof(DefaultPage));
            AppShell.Current.AdaptiveVisualStateChanged += Current_AdaptiveVisualStateChanged;
        }

        private void Current_AdaptiveVisualStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            AdaptiveVisualStateChanged?.Invoke(sender, e);
            UpdateForVisualState();
        }

        private void DetailFrame_Navigated(object sender, NavigationEventArgs e)
        {
            while (DetailFrame.BackStack.Count > 1)
            {
                DetailFrame.BackStack.RemoveAt(1);
            }

            UpdateForVisualState();
        }

        private void MasterFrame_Navigated(object sender, NavigationEventArgs e)
        {
            UpdateForVisualState();
        }
    }
}
