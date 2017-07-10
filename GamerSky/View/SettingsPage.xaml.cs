using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.System;
using Windows.UI.Xaml.Controls;
using GamerSky.Helper;
using GamerSky.ViewModel;
using Arcsinx.Toolkit.Helper;

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class SettingsPage : Page
    {

        public SettingsPageViewModel viewModel { get; set; }

        public SettingsPage()
        {
            this.InitializeComponent();
            
            viewModel = new SettingsPageViewModel();

        }

        private void Back()
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        public void NavigateToAbout()
        {
            MasterDetailPage.Current.DetailFrame.Navigate(typeof(AboutPage));
        }

        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private async void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                if (DispatcherManager.Current.Dispatcher == null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
                else
                {
                    if (DispatcherManager.Current.Dispatcher.HasThreadAccess)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                    }
                    else
                    {
                        await DispatcherManager.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                            delegate ()
                            {
                                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                            });
                    }
                }
            }
        }
        #endregion

        private string version;
        public string Version
        {
            get
            {
                return Functions.GetVersion();
            }
            private set
            {
                version = value;
                OnPropertyChanged();
            }
        }

        public void GetVersion()
        {
            Version = Functions.GetVersion();
        }

        /// <summary>
        /// 检测新版本
        /// </summary>
        public async void CheckUpdate()
        {
            string uri = "ms-windows-store://pdp/?ProductId=9NBLGGH5Q5TJ";
            await Launcher.LaunchUriAsync(new Uri(uri));
        }

        public void StartImage()
        {
            MasterDetailPage.Current.DetailFrame.Navigate(typeof(AdStartPage));
        }

        private async void transparentTileSwitch_Toggled(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (transparentTileSwitch.IsOn)
            {
                bool result = await LiveTileHelper.PinSecondaryTile("X");
                transparentTileSwitch.IsOn = result;
            }
            else
            {
                await LiveTileHelper.UnPinSecondaryTile();
            }

        }

        /// <summary>
        /// 去除广告
        /// </summary>
        //public async void RemoveAd()
        //{
        //    await IAPHelper.BuyProductAsync(IAPHelper.Remove_Ad);
        //}
    }
}
