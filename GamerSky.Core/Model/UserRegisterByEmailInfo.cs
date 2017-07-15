using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSky.Helper;
using Arcsinx.Toolkit.Helper;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 邮箱注册信息
    /// </summary>
    public class UserRegisterByEmailInfo : VerifiableBase
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
        [Required(ErrorMessage = "密码不能为空")]
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

        private string sureUserPassword;
        /// <summary>
        /// 再次输入的密码
        /// </summary>
        [Compare("UserPassword",ErrorMessage ="两次输入的密码不一致")]
        [Required(ErrorMessage ="密码不能为空")]
        public string SureUserPassword
        {
            get
            {
                return sureUserPassword;
            }
            set
            {
                Set(ref sureUserPassword, value);
            }
        }

        private string verificationCode;
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "验证码不能为空")]
        public string VerificationCode
        {
            get
            {
                return verificationCode;
            }
            set
            {
                Set(ref verificationCode, value);
            }
        }

        private string email;
        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage ="邮箱不能为空")]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                Set(ref email, value);
            }
        }

        private string answer;
        /// <summary>
        /// 密保问题答案
        /// </summary>
        [Required(ErrorMessage = "密保答案不能为空")]
        public string Answer
        {
            get
            {
                return answer;
            }
            set
            {
                Set(ref answer, value);
            }
        }

        private string question;
        /// <summary>
        /// 密保问题
        /// </summary>
        [Required(ErrorMessage = "密保问题不能为空")]
        public string Question
        {
            get
            {
                return question;
            }
            set
            {
                Set(ref question, value);
            }
        }
    }
}
