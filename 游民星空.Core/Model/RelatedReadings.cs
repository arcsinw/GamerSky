using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 相关阅读
    /// </summary>
    public class RelatedReadings
    {
        public string errorCode;
        public string errorMessage;
        public RelatedReadingsResult[] result;
    }

    public class RelatedReadingsResult
    {
        public string adId;
        public string contentId;
        public string contentType;
        /// <summary>
        /// 缩略图
        /// </summary>
        public string thumbnailUrl;
        /// <summary>
        /// 标题
        /// </summary>
        public string title;
    }
}
