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
        public int errorCode;
        public string errorMessage;
        public EssayResult[] result;
    }
    public struct EssayResult
    {
        public string adId;
        public string authorHeaderImageURL;
        public string authorName;
        public string badges;
        public EssayResult[] childElements;
        public string commentsCount;
        public string contentId;
        public string contentType;
        public string contentURL;
        public string readingCount;
        /// <summary>
        /// 缩略图
        /// </summary>
        public string[] thumbnailURLs; 
        /// <summary>
        /// 标题
        /// </summary>
        public string title; 
        public string type;
    }
}
