﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.ResultDataModel
{
    /// <summary>
    /// 返回数据类型基类
    /// </summary>
    public class ResultBase
    {
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }
}
