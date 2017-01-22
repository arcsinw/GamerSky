using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// allChannel 返回的频道信息
    /// </summary>
    public class Channel
    {
        public string isTop { get; set; }
        public int nodeId { get; set; }
        /// <summary>
        /// 栏目名
        /// </summary>
        public string nodeName { get; set; }
    }
}
