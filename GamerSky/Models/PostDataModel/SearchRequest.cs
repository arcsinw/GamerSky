using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models.PostDataModel
{
    public class SearchRequest
    {
        public string searchType;

        public string elementsCountPerPage; 

        public int pageIndex;

        public string searchKey;    
    }
}
