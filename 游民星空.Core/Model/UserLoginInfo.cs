using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.Helper;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 用户登录时的输入信息
    /// </summary>
    public class UserLoginInfo : VerifiableBase
    {

        private string userName;
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                Set(ref userName, value);
            }
        }

        private string userPassword;
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage ="密码不能为空")]
        public string UserPassword
        {
            get
            {
                return userPassword;
            }
            set
            {
                Set(ref userPassword, value);
            }
        }
    }
}
