using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.PostDataModel
{
    public class GameDetailEssayPostData : PostDataBase
    {
        public GameDetailEssayRequest request;
    }

    public class GameDetailEssayRequest
    {
        public string contentId { get; set; }
        public string contentType { get; set; }
        public int elementsCountPerPage { get; set; }
        public int pageIndex { get; set; }
    }
}
