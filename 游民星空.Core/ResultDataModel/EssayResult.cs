using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.Model;

namespace 游民星空.Core.ResultDataModel
{
    /// <summary>
    /// 返回文章列表
    /// </summary>
   public class EssayResult : ResultBase
    {
        public List<Essay> result;
    }
}
