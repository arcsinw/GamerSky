using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    [DataContract]
    public class Essay
    {
        public int errorCode;
        public string errorMessage;
        public EssayResult[] result;
    }
    public struct EssayResult
    {
        public int adId;
        public string authorHeaderImageURL;
        public string authorName;
        public string badges;
        public string childElements;
        public string commentsCount;
        public string contentId;
        public string contentType;
        public string contentURL;
        public string readingCount;
        public string[] thumbnailURLs;
        public string title;
        public string type;
    }
}
