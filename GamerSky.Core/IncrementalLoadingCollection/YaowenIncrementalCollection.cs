using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using GamerSky.Core.Http;
using GamerSky.Core.Model;

namespace GamerSky.Core.IncrementalLoadingCollection
{
    /// <summary>
    /// 要闻自增集合
    /// </summary>
    public class YaowenIncrementalCollection : IncrementalLoadingBase<Essay>
    { 

        #region member
        //private bool isLoading = false;     //是否正在加载
        //private bool hasMoreItems = false;
        private int pageIndex = 1;            //当前页码
        #endregion

        #region Override Method
        protected override  bool HasMoreItemsCore
        {
            get
            {
                return true;
            }
        }

        protected override async Task<LoadMoreItemsResult> LoadMoreItemsAsyncCore(CancellationToken cancel, uint count)
        {
            LoadMoreItemsResult result = new LoadMoreItemsResult();
            //开始加载
            this.OnDataLoading?.Invoke(this, EventArgs.Empty);
            if (cancel.IsCancellationRequested)
            {
                result.Count = 0;
            }
            else
            {
                List<Essay> essays = await ApiService.Instance.GetYaowen(pageIndex++);
                if (essays != null)
                {
                    foreach (var item in essays)
                    {
                        Add(item);
                    }
                }
            }
            this.OnDataLoaded?.Invoke(this, EventArgs.Empty);
            return result;
        }
        

        #endregion

        #region 公共事件
        /// <summary>
        /// 开始加载时发生
        /// </summary>
        public event EventHandler OnDataLoading;
        /// <summary>
        /// 加载完成后发生
        /// </summary>
        public event EventHandler OnDataLoaded;
        #endregion
    
    }
}
