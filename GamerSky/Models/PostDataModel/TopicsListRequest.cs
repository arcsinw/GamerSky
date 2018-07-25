using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models.PostDataModel
{
    public class TopicsListRequest
    {
        public int maxRepliesCount = 3;
        public int pageIndex;
        public int subjectId;
        public string topicType = "quanBu";
        public string filterType = "quanBu";
        public int elementsPerPage = 20;
        public List<int> clubIds = new List<int>() { 2, 7 };
        public string orderType = "faBuShiJian";
    }
}
