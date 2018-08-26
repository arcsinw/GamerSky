using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Windows.UI.Xaml.Data;
using System.Collections.ObjectModel;
using GamerSky.Models;
using GamerSky.Services;

namespace GamerSky.Collection
{
    public class EssayIncrementalCollection : IncrementalLoadingBase<Essay>
    {

        public EssayIncrementalCollection(string nodeId)
        {
            this.nodeId = nodeId;
        }

        private string nodeId;
        private int pageIndex = 1; 
         
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
            System.Diagnostics.Debug.WriteLine("LoadMoreItemsAsyncCore");

            LoadMoreItemsResult result = new LoadMoreItemsResult();
            this.OnDataLoading?.Invoke(this, EventArgs.Empty);
            if (cancel.IsCancellationRequested)
            {
                result.Count = 0;
            }
            else
            {
                var essays = await ApiService.Instance.GetEssayList(nodeId, pageIndex++);
                if (essays != null)
                {
                    foreach (var item in essays)
                    {
                        if (item.Type.Equals("huandeng"))
                        {
                            foreach (var c in item.ChildElements)
                            {
                                HeaderEssays.Add(c);
                            }
                            continue;
                        }
                        Add(item);
                    }
                }
                else
                {
                    this.OnError.Invoke(this, new Exception());
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

        /// <summary>
        /// Fire while exception throw
        /// </summary>
        public event EventHandler<Exception> OnError;
        #endregion
    }
}
