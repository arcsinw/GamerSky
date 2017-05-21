using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSky.Core.Model;
using Newtonsoft.Json;

namespace GamerSky.Core.ResultModel
{
    /// <summary>
    /// 返回文章列表
    /// </summary>
   public class EssayResult : ResultModelBase
    {
        [JsonProperty(PropertyName = "result")]
        public List<Essay> Result { get; set; }
    }
}
