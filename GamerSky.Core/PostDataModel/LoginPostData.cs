using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.PostDataModel
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginPostData : PostDataBase
    {
        public LoginPostDataRequest request;
    }
    public class LoginPostDataRequest
    {
        public string passWord;
        public string userName;
    }
}
