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
using GamerSky.Core.Model;
using GamerSky.ViewModel;

namespace GamerSky.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }
 
        /// <summary>
        /// 忘记密码
        /// </summary>
        public void ForgetPwd()
        {
            NavigationHelper.DetailFrameNavigate(typeof(FindPasswordPage));
        }

        /// <summary>
        /// 注册
        /// </summary>
        public void Register()
        {
            NavigationHelper.DetailFrameNavigate(typeof(RegisterPage));
        }

        private void Ellipse_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await AuthenticationHelper.SinaAuthenticationAsync();
        }
    }
}
