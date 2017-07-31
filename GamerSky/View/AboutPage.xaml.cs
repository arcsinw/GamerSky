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
using Arcsinx.Toolkit.Helper;

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        public AboutPage()
        {
            this.InitializeComponent();
        } 
         

        public void VersionHistory()
        {
            Frame.Navigate(typeof(AgreementPage), "Version");
        }

        /// <summary>
        /// 复制QQ到剪贴板
        /// </summary>
        public async void CopyQQToClipboard()
        {
            DataPackage dp = new DataPackage();
            dp.SetText("473967668");
            Clipboard.SetContent(dp);
            await new MessageDialog("已复制到剪贴板").ShowAsync();
        }

        /// <summary>
        /// 复制alipay到剪贴板
        /// </summary>
        public async void CopyPayToClipboard()
        {
            DataPackage dp = new DataPackage();
            dp.SetText("2579477466@qq.com");
            Clipboard.SetContent(dp);
            await new MessageDialog("已复制到剪贴板").ShowAsync();
        }
    }
}