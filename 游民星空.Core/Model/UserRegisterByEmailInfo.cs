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
    /// 邮箱注册信息
    /// </summary>
    public class UserRegisterByEmailInfo : VerifiableBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string UserPassword { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; }
        /// <summary>
        /// 再次输入的密码
        /// </summary>
        [Compare("UserName",ErrorMessage ="两次输入的密码不一致")]
        public string SureUserPassword { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "验证码不能为空")]
        public string VerificationCode { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Required(ErrorMessage ="邮箱不能为空")]
        public string Email { get; set; }
        /// <summary>
        /// 密保问题答案
        /// </summary>
        [Required(ErrorMessage = "密保答案不能为空")]
        public string Answer { get; set; }
        /// <summary>
        /// 密保问题
        /// </summary>
        [Required(ErrorMessage = "密保问题不能为空")]
        public string Question { get; set; }
    }
}
