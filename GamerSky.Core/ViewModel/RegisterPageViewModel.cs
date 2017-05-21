using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Core.Http;
using GamerSky.Core.Model;
using GamerSky.Core.PostDataModel;

namespace GamerSky.Core.ViewModel
{
    public class RegisterPageViewModel : ViewModelBase
    {
        public UserRegisterByNumberInfo RegisterInfo { get; set; }
         
        public RegisterPageViewModel()
        {
            RegisterInfo = new UserRegisterByNumberInfo();
             

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


        /// <summary>
        /// 通过手机号注册
        /// </summary>
        public async Task<VerificationCode> RegisterByPhone()
        {
            EmailRegisterPostData postData = new EmailRegisterPostData();
            postData.request = new EmailRegisterRequest {
                phoneNumber = RegisterInfo.PhoneNumber,
                userName = RegisterInfo.UserName,
                password = RegisterInfo.UserPassword,
                confirmpassword = RegisterInfo.UserPassword,
                phoneVerificationCode = RegisterInfo.VerificationCode };

            var result =  await ApiService.Instance.RegisterByPhone(RegisterInfo.UserPassword, RegisterInfo.UserName, RegisterInfo.PhoneNumber, RegisterInfo.VerificationCode);
            return result;
        }
    }
}
