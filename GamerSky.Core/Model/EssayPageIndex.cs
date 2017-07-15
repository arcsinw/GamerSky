using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    /// <summary>
    /// 频道的状态 当前页码 是否已下载
    /// </summary>
    public class EssayPageIndex
    {
        public int PageIndex { get; set; }
        public int ChannelId { get; set; }
    }
}
