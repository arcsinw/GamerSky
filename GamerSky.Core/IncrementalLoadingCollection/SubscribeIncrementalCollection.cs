using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using GamerSky.Core.Http;
using GamerSky.Core.Model;
using GamerSky.Core.ViewModel;

namespace GamerSky.Core.IncrementalLoadingCollection
{
    /// <summary>
    /// 订阅内容自增集合
    /// </summary>
    public class SubscribeIncrementalCollection : IncrementalLoadingBase<Essay>
    {
        private ApiService apiService = new ApiService();

        private int pageIndex = 1;
        private int currentSubscribeIndex = 0; //当前订阅index
        protected override bool HasMoreItemsCore
        {
            get
            {
                return true;
            }
        }

        protected override async Task<LoadMoreItemsResult> LoadMoreItemsAsyncCore(CancellationToken cancel, uint count)
        {
            var actualCount = 0;
            //开始加载
            this.OnLoadingMoreStart?.Invoke(this, EventArgs.Empty);
            //如果操作取消则不再加载
            LoadMoreItemsResult result = new LoadMoreItemsResult();
            if (cancel.IsCancellationRequested)
            {
                result.Count = 0;
            }
            else
            {
                List<Subscribe> subscribeList = DataShareManager.Current.SubscribeList;
                if (subscribeList == null || subscribeList.Count == 0)
                {
                    //完成加载
                    this.OnLoadingMoreEnd?.Invoke(this, EventArgs.Empty);
                    return new LoadMoreItemsResult { Count = 0 };
                }
                //加载数据方式 轮流加载
                try
                {
                    string x = DataShareManager.Current.SubscribeList[currentSubscribeIndex].sourceId;
                    List<Essay> essays = await apiService.GetSubscribeContent(x, pageIndex);
                    if (essays != null)
                    {
                        foreach (var item in essays)
                        {
                            if (!item.type.Equals("dingyueTitle"))
                            {
                                Add(item);
                            }
                        }
                    }
                    if (currentSubscribeIndex == (DataShareManager.Current.SubscribeList.Count - 1))
                    {
                        pageIndex++;
                    }
                    currentSubscribeIndex = ++currentSubscribeIndex % DataShareManager.Current.SubscribeList.Count;
               
                }
                catch (Exception)
                {
                    result.Count = 0;
                }
            }
            //完成加载
            this.OnLoadingMoreEnd?.Invoke(this, EventArgs.Empty);
            result.Count = (uint)actualCount;
            
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
