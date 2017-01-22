using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 用于从WebView中打开网页的js参数
    /// </summary>
    public class OpenPage
    {
        /// <summary>
        /// 通过Id打开页面
        /// </summary>
        public string PageId;
        /// <summary>
        /// 通过Url打开页面
        /// </summary>
        public string PageUrl;
        /// <summary>
        /// 打开方式
        /// </summary>
        public OpenMethodEnum OpenMethod;
    }

    public enum OpenMethodEnum
    {
        /// <summary>
        /// 通过Id打开文章
        /// </summary>
        OpenArticleWithId,
        /// <summary>
        /// 通过Url打开文章
        /// </summary>
        OpenArticleWithUrl,
        /// <summary>
        /// 通过Id打开专题
        /// </summary>
        OpenTopicWithId,
        /// <summary>
        /// 通过Id订阅专题
        /// </summary>
        SubscribeTopicWithUrl,
        /// <summary>
        /// 通过Id取消专题订阅
        /// </summary>
        CancelSubscribeWithId,

    }
}
