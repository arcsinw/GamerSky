using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using GamerSky.Core.Helper;
using GamerSky.Core.Http;
using GamerSky.Core.Model;

namespace GamerSky.Core.IncrementalLoadingCollection
{
    /// <summary>
    /// 文章自增集合
    /// </summary>
    public class SearchIncrementalCollection : IncrementalLoadingBase<Essay>
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        private string key;
        /// <summary>
        /// 搜索类型
        /// </summary>
        private SearchTypeEnum searchType;
        /// <summary>
        /// 当前页码
        /// </summary>
        private int pageIndex;

        private ApiService apiService;

        public SearchIncrementalCollection(string key, SearchTypeEnum searchType, int pageIndex=1)
        {
            apiService = new ApiService();
            this.key = key;
            this.searchType = searchType;
            this.pageIndex = pageIndex;
        }

        protected override bool HasMoreItemsCore
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
            this.OnLoadingMoreStart?.Invoke(this, EventArgs.Empty);
            //如果操作取消则不再加载
            if (cancel.IsCancellationRequested)
            {
                result.Count = 0;
            }
            else
            {
                List<Essay> essayResults = await apiService.SearchByKey(key, searchType, pageIndex++);
                if (essayResults != null)
                {
                    foreach (var item in essayResults)
                    {
                        Add(item);
                    }
                }
            }
            //完成加载
            this.OnLoadingMoreEnd?.Invoke(this, EventArgs.Empty);
            return result;
        }

        #region 公共事件
        /// <summary>
        /// 开始加载时发生
        /// </summary>
        public event EventHandler OnLoadingMoreStart;
        /// <summary>
        /// 加载完成时发生
        /// </summary>
        public event EventHandler OnLoadingMoreEnd; 
        #endregion
    }
}
