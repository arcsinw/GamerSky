using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Core.PostDataModel;
using GamerSky.Core.Model;
using GamerSky.Core.Http;

namespace GamerSky.ViewModel
{
    public class RegisterPageViewModel : ViewModelBase
    {
        public UserRegisterByNumberInfo RegisterInfo { get; set; }
         
        public RegisterPageViewModel()
        {
            RegisterInfo = new UserRegisterByNumberInfo();
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
