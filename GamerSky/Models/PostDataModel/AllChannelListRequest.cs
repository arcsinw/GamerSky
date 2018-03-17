using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models.PostDataModel
{
    public class AllChannelListRequest
    {
        /// <summary>
        /// 每页数据数量
        /// </summary>
        public int elementsCountPerPage;
        public int lastUpdateTime;
        /// <summary>
        /// 
        /// </summary>
        public int nodeIds;
        public int pageIndex;   //页码
        /// <summary>
        /// news | yaowen
        /// </summary>
        public string parentNodeId;

        /// <summary>
        /// 文章Id
        /// </summary>
        public string contentId;

        public string type;

        /// <summary>
        /// 文章类型（news） 
        /// /v2/TwoCorrelation (获取相关阅读)
        /// </summary>
        public string contentType;
        /// <summary>
        /// 获取攻略用
        /// </summary>
        public int pageCount;
    }
}
