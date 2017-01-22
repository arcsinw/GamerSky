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
    /// GameDetail 攻略 新闻
    /// </summary>
    public class GameDetailEssayResult : ResultModelBase
    {
        [JsonProperty(PropertyName = "result")]
        public List<GameDetailEssay> Result { get; set; }
    }
}
