using GamerSky.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Xaml.Data;
using GamerSky.Core.Http;
using System.Collections.ObjectModel;

namespace GamerSky.Core.IncrementalLoadingCollection
{
    public class EssayIncrementalCollection : IncrementalLoadingBase<Essay>
    {

        public EssayIncrementalCollection(int nodeId)
        {
            this.nodeId = nodeId;
        }

        private int nodeId;
        private int pageIndex = 1;
        private ApiService apiService = new ApiService();
         
        private ObservableCollection<Essay> headerEssays = new ObservableCollection<Essay>();
        /// <summary>
        /// 幻灯片
        /// </summary>
        public ObservableCollection<Essay> HeaderEssays
        {
            get
            {
                return headerEssays;
            }
            set
            {
                headerEssays = value;
            }
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
            this.OnDataLoading?.Invoke(this, EventArgs.Empty);
            if (cancel.IsCancellationRequested)
            {
                result.Count = 0;
            }
            else
            {
                var essays = await apiService.GetEssayList(nodeId, pageIndex++);
                if (essays != null)
                {
                    foreach (var item in essays)
                    {
                        if (item.type.Equals("huandeng"))
                        {
                            foreach (var c in item.childElements)
                            {
                                HeaderEssays.Add(c);
                            }
                            continue;
                        }
                        Add(item);
                    }
                }
            }
            this.OnDataLoaded?.Invoke(this, EventArgs.Empty);
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
