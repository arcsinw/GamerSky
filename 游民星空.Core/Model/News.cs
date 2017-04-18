using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 文章实体
    /// </summary>
    public class News
    {
        public int adId;
        public string id;
        /// <summary>
        /// main body
        /// </summary>
        public string mainBody;
        public string node;
        /// <summary>
        /// origin uri
        /// </summary>
        public string originURL;
        public int pageCount;
        public string[] pageIndexNames;
        public Subscriber[] subscribes;
        public string subTitle;
        public string templateURL;
        public string templateVersion;
        public string title;
        public string type;
        public string videoContent;
    }

    public struct Subscriber
    {
        public string subscribeld;
    }
}
