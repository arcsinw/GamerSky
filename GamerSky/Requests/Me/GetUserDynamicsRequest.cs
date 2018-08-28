using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Requests.Me
{
    /// <summary>
    /// 获取用户动态
    /// </summary>
    public class GetUserDynamicsRequest
    {
        public string elementsPerPage = "0";

        public int pageIndex;

        public List<string> userIds = new List<string>();
    }
}
