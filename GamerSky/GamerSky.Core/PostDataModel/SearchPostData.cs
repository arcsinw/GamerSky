using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.PostDataModel
{
    public class SearchPostData : PostDataBase
    {
        public SearchRequest request;
    }
    public class SearchRequest
    {
        public string searchType;

        public string elementsCountPerPage; //twosearch

        public int pageIndex;//twosearch

        public string searchKey;//twosearch

    }
}
