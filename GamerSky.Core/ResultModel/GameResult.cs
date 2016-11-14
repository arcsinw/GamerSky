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
    /// 游戏库中游戏列表
    /// </summary>
    public class GameResult : ResultModelBase
    {
        [JsonProperty(PropertyName = "result")]
        public List<Game> Result { get; set; }
    }
}
