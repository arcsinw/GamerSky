using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 图片
    /// </summary>
    public class JsImage
    {
        
        public string src { get; set; }

        public string alt { get; set; }
        /// <summary>
        /// 高清地址
        /// </summary>
        public string hdsrc { get; set; }

        public string index { get; set; }
    }
}
