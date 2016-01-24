using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    public class ChannelPostData
    {
        public string errorCode;
        public string errorMessage;
        public result[] result;
    }

    public struct result
    {
        public string isTop;
        public int nodeId;
        public string nodeName;
    }
}
