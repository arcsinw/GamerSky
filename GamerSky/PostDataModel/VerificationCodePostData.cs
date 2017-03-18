using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.PostDataModel
{
    /// <summary>
    /// 手机号注册 获取验证码
    /// </summary>
    public class VerificationCodePostData : PostDataBase
    {
        public VerificationCodeRequest request;
    }

    public class VerificationCodeRequest
    {
        public string codetype;

        public string email;
        /// <summary>
        /// 手机号
        /// </summary>
        public string phoneNumber;
        /// <summary>
        /// 用户名
        /// </summary>
        public string username;
    }
}
