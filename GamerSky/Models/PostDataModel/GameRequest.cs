using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models.PostDataModel
{
    /// <summary>
    /// 获取游戏库中游戏列表
    /// </summary>
    public class GameRequest
    {
        public string date { get; set; }

        public string elementsCountPerPage = "10";
        
        /// <summary>
        /// PC 1751
        /// PS4 1758
        /// PS3 1759
        /// XBOX One 1760
        /// XBOX 360 1752
        /// </summary>
        public string nodeIds { get; set; } = "hot";

        public int pageIndex;

        public string type;
    }
}
