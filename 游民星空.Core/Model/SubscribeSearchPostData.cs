using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 订阅热点搜索词
    /// </summary>
    public class SubscribeSearchPostData : PostDataBase
    {
        public SubscribeSearchRequest request;
    }
    public class SubscribeSearchRequest
    {
        public string type;
    }
}
