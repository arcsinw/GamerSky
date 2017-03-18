using GamerSky.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Xaml.Data;
using GamerSky.Http;
using System.Diagnostics;

namespace GamerSky.IncrementalLoadingCollection
{
    public class EssayCommentsCollection : IncrementalLoadingBase<Comment>
    {
         
        /// <summary>
        /// 当前页码
        /// </summary>
        private int pageIndex;

        private string contentId { get; set; } 
       
        public EssayCommentsCollection(string contentId, int pageIndex = 1)
        { 
            this.contentId = contentId; 
            this.pageIndex = pageIndex;
        }



        public bool hasMoreItems { get; set; } = true;
        protected override bool HasMoreItemsCore
        {
            get
            {
                return hasMoreItems;
            }
        }

        protected override async Task<LoadMoreItemsResult> LoadMoreItemsAsyncCore(CancellationToken cancel, uint count)
        {
            Debug.WriteLine("EssayCommentsCollection LoadMoreItemsAsyncCore");
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
                List<Comment> essayResults = await ApiService.Instance.GetAllComments(contentId, pageIndex++);
                if (essayResults != null && essayResults.Count !=0)
                {
                    foreach (var item in essayResults)
                    {
                        Add(item);
                    }
                }
                else
                {
                    hasMoreItems = false;
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
