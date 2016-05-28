using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.PostDataModel
{
    public class SubscribeTopicPostData : PostDataBase
    {
        public SubscribeTopicRequest request;
    }

    public class SubscribeTopicRequest
    {
        public string elementsCountPerPage;
        public string lastUpdateTime;
        public string nodeIds;
        public int pageIndex;
        public string parentNodeId;
        public string type;
    }
}
