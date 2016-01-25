using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// v2/AllChannelList Post data format
    /// </summary>
    [DataContract]
    public class AllChannelListPostData
    {
        [DataMember]
        public string appVersion = "2.0.7";
        [DataMember]
        public string deviceId;
        [DataMember]
        public string deviceType;
        [DataMember]
        public string os;
        [DataMember]
        public string osVersion;
        [DataMember]
        public request request;
    }

    public class request
    {
        /// <summary>
        /// 每页数据数量
        /// </summary>
        public int elementsCountPerPage;
        public int lastUpdateTime;
        /// <summary>
        /// 用于区别不同的类别 0为要闻
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
    }
}
