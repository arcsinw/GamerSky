using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSky.Core.Model;

namespace GamerSky.Core.ResultDataModel
{
    /// <summary>
    /// 返回文章列表
    /// </summary>
   public class EssayResult : ResultBase
    {
        public List<Essay> result;
    }
}
