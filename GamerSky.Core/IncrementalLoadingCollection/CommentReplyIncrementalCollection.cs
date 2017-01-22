using GamerSky.Core.Http;
using GamerSky.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Xaml.Data;
using GamerSky.Core.ViewModel;

namespace GamerSky.Core.IncrementalLoadingCollection
{
    public class CommentReplyIncrementalCollection : IncrementalLoadingBase<Comment>
    {  
        private int _pageIndex = 1;

        private bool _hasMoreItems = true;
        protected override bool HasMoreItemsCore
        {
            get
            {
                return _hasMoreItems;
            }
        }



        protected override async Task<LoadMoreItemsResult> LoadMoreItemsAsyncCore(CancellationToken cancel, uint count)
        {
            LoadMoreItemsResult result = new LoadMoreItemsResult();

            if (DataShareManager.Current.CurrentUser == null)
            {
                _hasMoreItems = false;
                return result;
            }

            OnDataLoading?.Invoke(this, EventArgs.Empty);
            if (cancel.IsCancellationRequested)
            {
                result.Count = 0;
            }
            else
            {
                List<Comment> comments = await ApiService.Instance.GetAllReply(DataShareManager.Current.CurrentUser.UserId, _pageIndex++);
                if(comments !=null)
                {
                    foreach (var item in comments)
                    {
                        Add(item);
                    }
                }
                else
                {
                    _hasMoreItems = false;
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
