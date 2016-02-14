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

        private ObservableCollection<Essay> essays = new ObservableCollection<Essay>();
        /// <summary>
        /// 新闻列表
        /// </summary>
        public ObservableCollection<Essay> Essays
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

        private ObservableCollection<Essay> headersEssays = new ObservableCollection<Essay>();
        /// <summary>
        /// 幻灯片
        /// </summary>
        public ObservableCollection<Essay> HeaderEssays
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
