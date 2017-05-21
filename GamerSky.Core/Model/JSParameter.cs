using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 与JS通信用的参数
    /// </summary>
    public class JSParameter
    {
        /// <summary>
        /// theme | pageColorMode | pageFontSize | nullImageMode 
        /// gsTemplateContent_RelatedTopic
        /// </summary>
        //public string name;
        //public string value;

        public string type { get; set; }

        public string content1 { get; set; }

        public string content2 { get; set; }

        public string content3 { get; set; }
    }
}
