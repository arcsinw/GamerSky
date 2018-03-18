using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models
{
    public class YaowenRequest
    {
        public int elementsCountPerPage = 20;
        public string lastUpdateTime;
        public string nodeIds = "0";
        public int pageIndex;
        public string parentNodeId = "yaowen";
        public string type = "";
    }
}
