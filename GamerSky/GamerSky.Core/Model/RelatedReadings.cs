using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSky.Core.ResultDataModel;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 相关阅读
    /// </summary>
    public class RelatedReadings 
    {
        public string adId { get; set; }
        public string contentId { get; set; }
        public string contentType { get; set; }
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
