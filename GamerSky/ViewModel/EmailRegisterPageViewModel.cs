using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Http;
using GamerSky.Model;

namespace GamerSky.ViewModel
{
    public class EmailRegisterPageViewModel : ViewModelBase
    {
        public UserRegisterByEmailInfo RegisterInfo { get; set; }
         

        public EmailRegisterPageViewModel()
        {
            RegisterInfo = new UserRegisterByEmailInfo();
        }
        
        /// <summary>
        /// 通过邮箱注册
        /// </summary>
        /// <returns></returns>
        public async Task<VerificationCode> RegisterByEmail()
        {
            var result = await ApiService.Instance.RegisterByEmail(RegisterInfo.Answer, RegisterInfo.UserPassword, RegisterInfo.Email, RegisterInfo.Question, RegisterInfo.UserName);
            return result;
        }
    }
}
