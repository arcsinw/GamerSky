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
using GamerSky.Helper;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EmailRegisterPage : Page
    {
        public EmailRegisterPage()
        {
            this.InitializeComponent();
        }

        public void Back()
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        public void Agreement()
        {
            MasterDetailPage.Current.DetailFrame.Navigate(typeof(AgreementPage));
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        private async void Register()
        {

            var verificationCode = await ViewModel.RegisterByEmail();
            if (verificationCode != null && !verificationCode.errorCode.Equals("0"))
            {
                UIHelper.ShowMessage(verificationCode.errorMessage);
            }
        }
         
    }
}
