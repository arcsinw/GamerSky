using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    public class Strategy
    {
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
        public StrategyResult[] result { get; set; }
    }
    public class StrategyResult
    {
        public string favoriteCnt { get; set; }
        public string specialID { get; set; }
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
