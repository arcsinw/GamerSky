using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GamerSky.Core.Helper;
using GamerSky.Core.ViewModel;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

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

        /// <summary>
        /// 去除广告
        /// </summary>
        //public async void RemoveAd()
        //{
        //    await IAPHelper.BuyProductAsync(IAPHelper.Remove_Ad);
        //}
    }
}
