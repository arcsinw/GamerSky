using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.PostDataModel
{
    /// <summary>
    /// 通过手机或邮箱注册
    /// </summary>
    public class EmailRegisterPostData : PostDataBase
    {
        public EmailRegisterRequest request;
    }
    public class EmailRegisterRequest
    {
        /// <summary>
        /// 密保问题答案
        /// </summary>
        public string answer;
        /// <summary>
        /// 密保问题
        /// </summary>
        public string question;
        /// <summary>
        /// 手机号码 此处不填
        /// </summary>
        public string phoneNumber;
        /// <summary>
        /// 验证码 此处不填
        /// </summary>
        public string phoneVerificationCode;
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email;
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName;
        /// <summary>
        /// 密码
        /// </summary>
        public string password;
        /// <summary>
        /// 确认密码
        /// </summary>
        public string confirmpassword;
    }
}
