using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 订阅热点词返回数据类型
    /// </summary>
    public class Subscribe : ResultBase
    {
        public SubscribeResult[] result { get; set; }
    }
    public class SubscribeResult
    {
        public string cnt { get; set; }
        public string isHot { get; set; }
        public string sourceId { get; set; }
        public string sourceName { get; set; }
        public string thumbnailUrl { get; set; }
    }
}
