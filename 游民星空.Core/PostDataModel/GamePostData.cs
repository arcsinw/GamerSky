using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.PostDataModel
{
    /// <summary>
    /// 获取游戏库中游戏列表
    /// </summary>
    public class GamePostData : PostDataBase
    {
        public GamePostDataRequest request { get; set; }
    }
    public class GamePostDataRequest
    {
        public string date;
        public string elementsCountPerPage = "10";
        public string nodeIds = "hot";
        public int pageIndex;
        public string type;
    }
}
