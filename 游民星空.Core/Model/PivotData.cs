using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.IncrementalLoadingCollection;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 用于绑定到Pivot的数据
    /// </summary>
    public class PivotData:ModelBase
    { 
        private Channel channel;
        public Channel Channel
        {
            get
            {
                return channel;
            }
            set
            {
                channel = value;
                OnPropertyChanged();
            }
        }

        private EssayIncrementalCollection essays;
        /// <summary>
        /// 新闻列表
        /// </summary>
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
