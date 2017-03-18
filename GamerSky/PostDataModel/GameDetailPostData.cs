using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.PostDataModel
{
    public class GameDetailPostData : PostDataBase
    {
        public GameDetailRequest request;
    }
    public class GameDetailRequest
    {
        public string contentId;
    }
}
