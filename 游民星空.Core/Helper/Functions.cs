using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Helper
{
    public class Functions
    {
        /// <summary>
        /// 获取当前Unix timestamp
        /// </summary>
        /// <returns></returns>
        public long getUnixTimeStamp()
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
    }
}
