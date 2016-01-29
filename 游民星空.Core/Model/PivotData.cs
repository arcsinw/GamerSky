using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    /// <summary>
    /// 用于绑定到Pivot的数据
    /// </summary>
    public class PivotData:ModelBase
    {
        //private string key;
        //public string Key
        //{
        //    get
        //    {
        //        return key;
        //    }
        //    set
        //    {
        //        key = value;
        //        OnPropertyChanged();
        //    }
        //}

        private ChannelResult channel;
        public ChannelResult Channel
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

        private ObservableCollection<EssayResult> essays = new ObservableCollection<EssayResult>();
        /// <summary>
        /// 新闻列表
        /// </summary>
        public ObservableCollection<EssayResult> Essays
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

        private ObservableCollection<EssayResult> headersEssays = new ObservableCollection<EssayResult>();
        /// <summary>
        /// 幻灯片
        /// </summary>
        public ObservableCollection<EssayResult> HeaderEssays
        {
            get
            {
                return headersEssays;
            }
            set
            {
                headersEssays = value;
                OnPropertyChanged();
            }
        }

        //private PivotContent content;
        //public PivotContent Content
        //{
        //    get
        //    {
        //        return content;
        //    }
        //    set
        //    {
        //        content = value;
        //        OnPropertyChanged();
        //    }
        //}
      
    }

    //public class PivotContent
    //{
       
    //}
}
