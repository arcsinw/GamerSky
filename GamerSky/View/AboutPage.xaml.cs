using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GamerSky.Helper;
using GamerSky.ViewModel; 
 
namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AboutPage : Page , INotifyPropertyChanged
    {
        public AboutPage()
        {
            this.InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;
            UpdateData();

            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        #region 九幽的数据统计
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            JYHelper.TracePageEnd("AboutPage");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            JYHelper.TracePageStart("AboutPage");
        }
        #endregion

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
        }

        private ElementTheme appTheme;
        public ElementTheme AppTheme
        {
            get
            {
                return appTheme;
            }
            set
            {
                appTheme = value;
                OnPropertyChanged();
            }
        }

        public void UpdateData()
        {
            Version = Functions.GetVersion();
            Author = Functions.GetAuthor();
        }

        private void Back()
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        public void VersionHistory()
        {
            Frame.Navigate(typeof(AgreementPage), "Version");
        }

        /// <summary>
        /// 复制到剪贴板
        /// </summary>
        public async void CopyToClipboard()
        {
            DataPackage dp = new DataPackage();
            dp.SetText("473967668");
            Clipboard.SetContent(dp);
            await new MessageDialog("已复制到剪贴板").ShowAsync();
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

        private string author;
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
                OnPropertyChanged();
            }
        }

        private string version;
        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
                OnPropertyChanged();
            }
        }


    }
}