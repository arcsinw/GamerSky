using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Requests.Me
{
    public class GetCollectListRequest
    {
        public string elementsCountPerPage = "20";

        public int pageIndex;
    }
}
