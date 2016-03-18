using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 游戏库中的游戏
    /// </summary>
    public class Game
    {
        public string contentId { get; set; }
        public string developer { get; set; }
        public string gameType { get; set; }
        public string platform { get; set; }
        public string sellTime { get; set; }
        public string thumbnailURL { get; set; }
        public string title { get; set; }
    }
}
