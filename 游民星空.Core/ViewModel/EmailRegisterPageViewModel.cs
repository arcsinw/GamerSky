using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
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
    }
}
