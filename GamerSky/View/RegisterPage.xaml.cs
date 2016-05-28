﻿using System;
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
using GamerSky.Core.Helper;
using GamerSky.Core.Http;
using GamerSky.Helper;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RegisterPage : Page
    {

        private ApiService apiService;
        public RegisterPage()
        {
            this.InitializeComponent();

            apiService = new ApiService();
        }

       

        private void OtherRegister()
        {
            this.Frame.Navigate(typeof(EmailRegisterPage));
        }

        private void Back()
        {
            if(Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        public void Agreement()
        {
            Frame.Navigate(typeof(AgreementPage));
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        private async void GetVerificationCode()
        {
            string phoneNumber = phoneNumberTextBlock.Text;
            string userName = userNameTextBlock.Text;

            var verificationCode = await apiService.GetVerificationCode(phoneNumber, userName, "");  
            if(verificationCode!= null && !verificationCode.errorCode.Equals("0"))
            {
                UIHelper.ShowMessage(verificationCode.errorMessage);
            }
        }

        private async void Register()
        {
            var result = await ViewModel.RegisterByPhone();
            if(result!=null && !result.Equals("0"))
            {
                UIHelper.ShowMessage(result.errorMessage);
            }
        }
    }
}
