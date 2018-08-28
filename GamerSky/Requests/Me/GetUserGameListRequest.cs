using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Requests.Me
{
    /// <summary>
    /// 获取用户游戏列表
    /// </summary>
    public class GetUserGameListRequest
    {
        public string elementsPerPage = "1";

        public string extraField1 = "GameType, Position, AllTimeT";

        public string extraField2 = "gsScore, gameTag, Like, wantplayCount, myComment, myScore";

        public int pageIndex = 1;

        /// <summary>
        /// xiangWan || wanGuo
        /// </summary>
        public string type = "xiangWan";

        public string userId;
    }
}
