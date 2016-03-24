using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    public class GameDetail
    {
        /// <summary>
        /// 游戏详情页背景图
        /// </summary>
        public string backgroundURL { get; set; }
        /// <summary>
        /// 开发商
        /// </summary>
        public string developer { get; set; }
        public string englishTitle { get; set; }
        public string gameType { get; set; }
        public string newsNumber { get; set; }
        public string platform { get; set; }
        public string sellTime { get; set; }
        public string strategyNumber { get; set; }
        public string thumbnailURL { get; set; }
        public string title { get; set; }
    }
}
