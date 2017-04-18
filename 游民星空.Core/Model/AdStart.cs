using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 启动图
    /// </summary>
    public class AdStart
    {
        public string adId { get; set; }
        public string articleId { get; set; }
        public string coordinates { get; set; }
        public string delayTimeInSeconds { get; set; }
        public string effecttime { get; set; }
        public string failuretime { get; set; }
        public string link { get; set; }
        public string linktype { get; set; }
        public string  picAdress { get; set; }
    }
}
