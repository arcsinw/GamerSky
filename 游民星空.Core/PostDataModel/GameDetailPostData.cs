﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.PostDataModel
{
    public class GameDetailPostData : PostDataBase
    {
        public GameDetailRequest request;
    }
    public class GameDetailRequest
    {
        public string contentId;
    }
}
