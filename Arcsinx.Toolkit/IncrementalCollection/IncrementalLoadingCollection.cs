using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using System.Threading;

namespace Arcsinx.Toolkit.IncrementalCollection
{
    /// <summary>
    /// 增量加载集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IncrementalLoadingCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        /// <summary>
        /// 通过页码增量加载
        /// </summary>
        /// <param name="func">页码增量加载方法</param>
        /// <param name="onDataLoadedAction">数据加载完成</param>
        /// <param name="onDataLoadingAction">数据加载中</param>
        public IncrementalLoadingCollection(Func<uint, int, Task<IEnumerable<T>>> func, System.Action onDataLoadedAction = null, System.Action onDataLoadingAction = null, Action<Exception> onErrorAction = null)
        {
            Page = 0;
            this.pageFunc = func;
            this.HasMoreItems = true;

            this.onDataLoadedAction = onDataLoadedAction;
            this.onDataLoadingAction = onDataLoadingAction;
            this.onErrorAction = onErrorAction;
        }

        /// <summary>
        /// 通过时间戳加载
        /// </summary>
        /// <param name="func">通过时间戳加载数据的方法</param>
        /// <param name="onDataLoadedAction">数据加载完成</param>
        /// <param name="onDataLoadingAction">数据加载中</param>
        public IncrementalLoadingCollection(Func<uint, string, Task<IEnumerable<T>>> func, System.Action onDataLoadedAction = null, System.Action onDataLoadingAction = null, Action<Exception> onErrorAction = null)
        {
            TimeStamp = string.Empty;
            this.timeStampFunc = func;
            this.HasMoreItems = true;

            this.onDataLoadingAction = onDataLoadingAction;
            this.onDataLoadedAction = onDataLoadedAction;
            this.onErrorAction = onErrorAction;
        }

        #region Fields
        private bool isBusy = false;

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStamp { get; set; } = string.Empty;

        /// <summary>
        /// 当前页数
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// 按页码加载
        /// </summary>
        private Func<uint, int, Task<IEnumerable<T>>> pageFunc;

        /// <summary>
        /// 按时间戳加载
        /// </summary>
        private Func<uint, string, Task<IEnumerable<T>>> timeStampFunc;

        /// <summary>
        /// 数据加载完成Action
        /// </summary>
        private System.Action onDataLoadedAction;

        /// <summary>
        /// 数据正在加载Action
        /// </summary>
        private System.Action onDataLoadingAction;

        /// <summary>
        /// 加载中出现错误
        /// </summary>
        private Action<Exception> onErrorAction;
        #endregion

        /// <summary>
        /// 是否有更多数据
        /// </summary>
        public bool HasMoreItems
        {
            get; private set;
        }
        
        /// <summary>
        /// 清除数据并重新加载
        /// </summary>
        /// <returns></returns>
        public async Task ClearAndReloadAsync()
        {
            Clear();
            Page = 0;
            TimeStamp = string.Empty;
            HasMoreItems = true;
            await LoadMoreItemsAsync(0);
        }

        /// <summary>
        /// 没有更多数据
        /// </summary>
        public void NoMore()
        {
            this.HasMoreItems = false;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            if (isBusy)
            {
                return Task.Run(() => new LoadMoreItemsResult { Count = 0 }).AsAsyncOperation();
            }

            isBusy = true;

            return AsyncInfo.Run((c) => LoadMoreItemsAsync(c, count));
        }

        private async Task<LoadMoreItemsResult> LoadMoreItemsAsync(CancellationToken token, uint count)
        {
            try
            {
                onDataLoadingAction?.Invoke();
        
                if (timeStampFunc != null)
                {
                    var items = await timeStampFunc(0, TimeStamp);
                    if (items != null && items.Any())
                    {
                        foreach (var item in items)
                        {
                            this.Add(item);
                        }
                    }
                    else
                    {
                        NoMore();
                    }
                }
                else
                {
                    var items = await pageFunc(0, ++Page);
                    if (items != null && items.Any())
                    {
                        foreach (var item in items)
                        {
                            this.Add(item);
                        }
                    }
                    else
                    {
                        NoMore();
                    }
                }
            }
            catch (Exception e)
            {
                onErrorAction?.Invoke(e);
                NoMore();
            }
            finally
            {
                isBusy = false;
                onDataLoadedAction?.Invoke();
            }

            return new LoadMoreItemsResult { Count = (uint)this.Count };
        }
    }
}
