﻿using Arcsinx.Toolkit.IncrementalCollection;
using GamerSky.IncrementalLoadingCollection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Model
{
    /// <summary>
    /// 用于绑定到Pivot的数据
    /// </summary>
    public class PivotData:ModelBase
    {  
        public Channel Channel { get; set; }

        //public IncrementalLoadingCollection<Essay> Essays { get; set; }

        private EssayIncrementalCollection essays;

        public EssayIncrementalCollection Essays
        {
            get
            {
                return essays;
            }
            set
            {
                essays = value;
                OnPropertyChanged();
            }
        }
    }
 
}
