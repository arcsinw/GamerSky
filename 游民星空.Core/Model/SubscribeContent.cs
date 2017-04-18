using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 订阅栏目内的内容
    /// </summary>
    public class SubscribeContent
    {
        public int commentCount { get; set; }
        public contentDetailUrls[] contentDetailUrls { get; set; }
        public string generalId { get; set; }
        public string origin { get; set; }
        public string publicTime { get; set; }
        public string thumbnailUrl { get; set; }
        public string title { get; set; }
    }
    public class contentDetailUrls
    {
        public string contentDetailUrl { get; set; }
        public int pageIndex { get; set; }
        public string subTitle { get; set; }
    }
}
