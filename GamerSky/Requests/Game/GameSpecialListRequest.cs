﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Requests
{
    public class GameSpecialListRequest
    {
        [JsonProperty("nodeId")]
        public int NodeId { get; set; } = 1;

        [JsonProperty("elementsCountPerPage")]
        public int ElementsCountPerPage { get; set; }
        
        [JsonProperty("pageIndex")]
        public int PageIndex { get; set; }
    }
}