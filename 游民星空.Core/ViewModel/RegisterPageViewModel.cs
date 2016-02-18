using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class RegisterPageViewModel : ViewModelBase
    {
        public UserRegisterByNumberInfo RegisterInfo { get; set; }

        private ApiService apiService;
        public RegisterPageViewModel()
        {
            RegisterInfo = new UserRegisterByNumberInfo();

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


        /// <summary>
        /// 通过手机号注册
        /// </summary>
        public void RegisterByPhone()
        {
            
        }
    }
}
