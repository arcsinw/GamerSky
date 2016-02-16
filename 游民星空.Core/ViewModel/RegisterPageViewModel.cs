using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class RegisterPageViewModel
    {
        public UserRegisterByNumberInfo RegisterInfo { get; set; }

        private ApiService apiService;
        public RegisterPageViewModel()
        {
            RegisterInfo = new UserRegisterByNumberInfo();

            apiService = new ApiService();
        }


        /// <summary>
        /// 通过手机号注册
        /// </summary>
        public void RegisterByPhone()
        {
            
        }
    }
}
