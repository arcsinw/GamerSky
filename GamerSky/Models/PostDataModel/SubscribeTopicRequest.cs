using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models.PostDataModel
{
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
