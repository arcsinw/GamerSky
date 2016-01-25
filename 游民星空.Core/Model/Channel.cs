using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// allChannel 返回的频道信息
    /// </summary>
    public class Channel
    {
        public string errorCode;
        public string errorMessage;
        public List<ChannelResult> result;
    }
    public class ChannelResult
    {
        public string isTop;
        public string nodeId;
        /// <summary>
        /// 栏目名
        /// </summary>
        public string nodeName;
    }
}
