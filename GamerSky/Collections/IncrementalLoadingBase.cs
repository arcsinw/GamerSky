using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace GamerSky.Collection
{
    /// <summary>
    /// 自增集合抽象类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IncrementalLoadingBase<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        /// <summary>
        /// 是否还有可加载的项
        /// </summary>
        public bool HasMoreItems
        {
            get
            {
                return HasMoreItemsCore;
            }
        }

        /// <summary>
        /// 可在派生类中重写
        /// </summary>
        protected abstract bool HasMoreItemsCore { get;  }


        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(c => LoadMoreItemsAsyncCore(c, count));
        }

        /// <summary>
        /// 加载更多数据
        /// </summary>
        /// <param name="cancel"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        protected abstract Task<LoadMoreItemsResult> LoadMoreItemsAsyncCore(CancellationToken cancel, uint count);
 
    }
}
