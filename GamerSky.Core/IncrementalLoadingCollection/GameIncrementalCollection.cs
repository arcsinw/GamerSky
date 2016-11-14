using GamerSky.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Xaml.Data;
using GamerSky.Core.Http;

namespace GamerSky.Core.IncrementalLoadingCollection
{
    public class GameIncrementalCollection : IncrementalLoadingBase<Game>
    {
        private ApiService apiService = new ApiService();
        private int pageIndex = 1;

        private bool hasMoreItems = true;
        protected override bool HasMoreItemsCore
        {
            get
            {
                return hasMoreItems;
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
                List<Game> games = await apiService.GetGameList(pageIndex++);
                if (games != null && games.Count !=0)
                {
                    result.Count = (uint)games.Count;
                    foreach (var item in games)
                    {
                        Add(item);
                    }
                }
                else
                {
                    hasMoreItems = false;
                }
            }
            OnDataLoaded?.Invoke(this, EventArgs.Empty);
            return result;
        }
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