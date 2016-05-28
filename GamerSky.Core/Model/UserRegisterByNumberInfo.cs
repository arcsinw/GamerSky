using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSky.Core.Helper;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 通过手机号注册的信息
    /// </summary>
    public class UserRegisterByNumberInfo : VerifiableBase
    {
        private string phoneNumber;
        /// <summary>
        /// 手机号
        /// </summary>
        [DataType(DataType.PhoneNumber,ErrorMessage = "手机号码格式不正确")]
        [Required(ErrorMessage = "手机号不能为空")]
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                Set(ref phoneNumber, value);
            }
        }
        private string userPassword;
        /// <summary>
        /// 用户名
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
        private string userName;
        /// <summary>
        /// 密码
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
    }
}
