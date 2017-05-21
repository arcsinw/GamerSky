﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSky.Core.Model;
using Newtonsoft.Json;

namespace GamerSky.Core.ResultModel
{
    public class RelatedReadingsResult : ResultModelBase
    {
        [JsonProperty(PropertyName = "result")]
        public List<RelatedReadings> Result { get; set; }

    }
}
