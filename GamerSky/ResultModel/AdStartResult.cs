using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSky.Model;
using Newtonsoft.Json;

namespace GamerSky.ResultModel
{
    /// <summary>
    /// 返回启动图
    /// </summary>
    public class AdStartResult
    {
        [JsonProperty(PropertyName = "result")]
        public AdStart[] Result { get; set; }

    }
}
