using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using GamerSky.Http;
using GamerSky.Model;

namespace GamerSky.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {
        public UserLoginInfo UserLoginInfo { get; set; }

        public LoginPageViewModel()
        {
            UserLoginInfo = new UserLoginInfo();// { UserName = "arxchg", UserPassword = "qwertyx" };
        }
        
         
        public async void Login()
        {
            var loginResult = await ApiService.Instance.Login(UserLoginInfo.UserPassword, UserLoginInfo.UserName);
            if (loginResult != null)
            {
                if (loginResult.ErrorCode.Equals("0")) //成功
                {
                    if(loginResult.Result !=null)
                    {
                        DataShareManager.Current.UpdateUser(loginResult.Result);
                    }
                }
                else
                {
                    await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                    {
                        await new MessageDialog(loginResult.ErrorMessage).ShowAsync();
                    });
                }
            }
        }
    }
}
