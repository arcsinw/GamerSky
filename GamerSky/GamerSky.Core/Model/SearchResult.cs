using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 搜索推荐热点词结果
    /// </summary>
    public class SearchResult
    {
        public string errorCode { get; set; }

        public string errorMessage { get; set; }

        public List<string> result { get; set; }
    }
}
