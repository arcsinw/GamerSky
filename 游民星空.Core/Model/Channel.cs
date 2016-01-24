using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    public class Channel
    {
        public string errorCode;
        public string errorMessage;
        public channelResult[] result;
    }
    public struct channelResult
    {
        public string isTop;
        public string nodeId;
        public string nodeName;
    }
}
