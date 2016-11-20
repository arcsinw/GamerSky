using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.PostDataModel
{
    public class GetAllCommentPostData
    {
        public string topicId { get; set; }
        public int pageIndex { get; set; }
        public string pageSize { get; set; } = "20";
    }
}
