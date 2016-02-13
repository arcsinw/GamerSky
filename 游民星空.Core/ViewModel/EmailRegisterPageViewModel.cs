using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class EmailRegisterPageViewModel
    {
        public UserRegisterByEmailInfo RegisterInfo { get; set; }

        public EmailRegisterPageViewModel()
        {
            RegisterInfo = new UserRegisterByEmailInfo();
        }
    }
}
