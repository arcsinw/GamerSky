using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 新闻列表
    /// </summary>
    [DataContract]
    public class Essay
    {
        [DataMember]
        public int errorCode;
        [DataMember]
        public string errorMessage;
        [DataMember]
        public List<EssayResult> result;
    }
    public class EssayResult
    {
        public string adId { get; set; }
        public string authorHeadImageURL { get; set; }
        public string authorName { get; set; }
        public string[] badges { get; set; }
        public EssayResult[] childElements { get; set; }
        public string commentsCount { get; set; }
        public string contentId { get; set; }
        public string contentType { get; set; }
        public string contentURL { get; set; }
        public string readingCount { get; set; }
        /// <summary> 
        /// 缩略图
        /// </summary>
        public string[] thumbnailURLs { get; set; } = { "ms-appx:///Assets/image_loading.png", "ms-appx:///Assets/image_loading.png", "ms-appx:///Assets/image_loading.png" };
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// huandeng 则为幻灯片内容
        /// </summary>
        public string type { get; set; }
    }
}
