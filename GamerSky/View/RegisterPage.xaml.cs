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
using GamerSky.Http; 

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RegisterPage : Page
    { 
        public RegisterPage()
        {
            this.InitializeComponent(); 
        }



        private void OtherRegister()
        {
            this.Frame.Navigate(typeof(EmailRegisterPage));
        }

        private void Back()
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
        private async void GetVerificationCode()
        {
            string phoneNumber = phoneNumberTextBlock.Text;
            string userName = userNameTextBlock.Text;

            var verificationCode = await ApiService.Instance.GetVerificationCode(phoneNumber, userName, "");
            if (verificationCode != null && !verificationCode.ErrorCode.Equals("0"))
            {
                UIHelper.ShowMessage(verificationCode.ErrorMessage);
            }
        }

        private async void Register()
        {
            var result = await ViewModel.RegisterByPhone();
            if (result != null && !result.Equals("0"))
            {
                UIHelper.ShowMessage(result.ErrorMessage);
            }
        }
    }
}
