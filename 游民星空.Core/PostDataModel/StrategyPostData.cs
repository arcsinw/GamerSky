using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.PostDataModel
{
    /// <summary>
    /// 攻略 PostData
    /// </summary>
    public class StrategyPostData:PostDataBase
    {
        public StrategyRequest request { get; set; }
    }
    public class StrategyRequest
    {
        public int pageCount { get; set; }
        public int pageIndex { get; set; }
        public int type { get; set; }
    }
}
