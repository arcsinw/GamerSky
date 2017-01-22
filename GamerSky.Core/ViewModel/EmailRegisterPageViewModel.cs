using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Core.Http;
using GamerSky.Core.Model;

namespace GamerSky.Core.ViewModel
{
    public class EmailRegisterPageViewModel : ViewModelBase
    {
        public UserRegisterByEmailInfo RegisterInfo { get; set; }
         

        public EmailRegisterPageViewModel()
        {
            RegisterInfo = new UserRegisterByEmailInfo();
              
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
