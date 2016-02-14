using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 有攻略的游戏列表
    /// </summary>
    public class Strategy
    {
        public string favoriteCnt { get; set; }
        public int specialID { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public string thumbnailUrl { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
    }
    
}
