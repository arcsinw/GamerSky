using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.PostDataModel
{
    /// <summary>
    /// 通过用户名查询账号找回密码的方式
    /// </summary>
    public class FindPasswordByNamePostData
    {
        public FindPasswordByNameRequest request;
    }
    public class FindPasswordByNameRequest
    {
        public string username;
    }
}
