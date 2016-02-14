using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class LoginPageViewModel
    {
        public UserLoginInfo UserLoginInfo { get; set; }

        public LoginPageViewModel()
        {
            UserLoginInfo = new UserLoginInfo();
            apiService = new ApiService();
        }
        private ApiService apiService;
        public void Login()
        {
            //apiService.
        }
    }
}
