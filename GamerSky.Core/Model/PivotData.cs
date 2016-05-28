using GamerSky.Core.IncrementalLoadingCollection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
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

        //private ObservableCollection<Essay> essays = new ObservableCollection<Essay>();
        ///// <summary>
        ///// 新闻列表
        ///// </summary>
        //public ObservableCollection<Essay> Essays
        //{
        //    get
        //    {
        //        return essays;
        //    }
        //    set
        //    {
        //        essays = value;
        //        OnPropertyChanged();
        //    }
        //}

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

        //private ObservableCollection<Essay> headersEssays = new ObservableCollection<Essay>();
        ///// <summary>
        ///// 幻灯片
        ///// </summary>
        //public ObservableCollection<Essay> HeaderEssays
        //{
        //    get
        //    {
        //        return headersEssays;
        //    }
        //    set
        //    {
        //        headersEssays = value;
        //        OnPropertyChanged();
        //    }
        //}
 
      
    }
 
}
