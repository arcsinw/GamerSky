using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using GamerSky.Core.Http;
using GamerSky.Core.Model;

namespace GamerSky.Core.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {
        public UserLoginInfo UserLoginInfo { get; set; }

        public LoginPageViewModel()
        {
            UserLoginInfo = new UserLoginInfo();
            apiService = new ApiService();

            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

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

        

        private ApiService apiService;
        public async void Login()
        {
            var loginResult = await apiService.Login(UserLoginInfo.UserPassword, UserLoginInfo.UserName);
            if (loginResult != null)
            {
                if (loginResult.errorCode.Equals("0")) //成功
                {
                    if(loginResult.result !=null)
                    {
                        DataShareManager.Current.UpdateUser(loginResult.result);
                    }
                }
                else
                {
                    await Window.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, async () =>
                    {
                        await new MessageDialog(loginResult.errorMessage).ShowAsync();
                    });
                }
            }
        }
    }
}
