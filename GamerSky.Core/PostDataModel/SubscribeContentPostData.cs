using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.PostDataModel
{
    /// <summary>
    /// 获取某个订阅栏目的内容
    /// </summary>
    public class SubscribeContentPostData : PostDataBase
    {
        public SubscribeContentRequest request;
    }

    public class SubscribeContentRequest
    {
        public string parentNodeId { get; set; } = "dingyue";
        /// <summary>
        /// dingyueTopic | dingyueList | newsList
        /// </summary>
        public string type { get; set; } = "dingyueList";
        /// <summary>
        /// 对应订阅栏目中的sourceId
        /// </summary>
        public string nodeIds { get; set; }  
        //public string lastUpdateTime { get; set; }
        public string elementsCountPerPage { get; set; } = "20";
        public int pageCount { get; set; }
        public int pageIndex { get; set; }
    }
}
