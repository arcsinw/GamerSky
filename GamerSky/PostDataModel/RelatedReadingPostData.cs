using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.PostDataModel
{
    /// <summary>
    /// 相关阅读
    /// </summary>
    public class RelatedReadingPostData : PostDataBase
    {
        public RelatedReadingRequest request;
    }

    public class RelatedReadingRequest
    {
        public string contentId;
        /// <summary>
        /// news
        /// </summary>
        public string contentType;
        public string elementsCountPerPage = "6";
        public int pageIndex = 1;
    }    

}
