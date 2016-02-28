using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using 游民星空.Core.Http;
using 游民星空.Core.IncrementalLoadingCollection;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class SubscribePageViewModel : ViewModelBase
    {
        public ObservableCollection<Essay> SubscribeTopic { get; set; }

        //public ObservableCollection<Essay> SubscribeContent { get; set; }

        public SubscribeIncrementalCollection SubscribeContent { get; set; }

        private ApiService apiService;

        public SubscribePageViewModel()
        {
            apiService = new ApiService();
            SubscribeTopic = new ObservableCollection<Essay>();
            
            //SubscribeContent = new ObservableCollection<Essay>();
            SubscribeContent = new SubscribeIncrementalCollection();
            SubscribeContent.OnLoadingMoreStart += SubscribeContent_OnLoadingMoreStart;
            SubscribeContent.OnLoadingMoreEnd += SubscribeContent_OnLoadingMoreEnd;


            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        private void SubscribeContent_OnLoadingMoreEnd(object sender, EventArgs e)
        {
            IsActive = false;
        }

        private void SubscribeContent_OnLoadingMoreStart(object sender, EventArgs e)
        {
            IsActive = true;
        }

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
        }

        private ElementTheme appTheme;
        public ElementTheme AppTheme
        {
            get
            {
                return appTheme;
            }
            set
            {
                appTheme = value;
                OnPropertyChanged();
            }
        }

        private bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 加载订阅专题
        /// </summary>
        public async Task LoadSubscribeTopic()
        {
            IsActive = true;
            var subscribeList = DataShareManager.Current.SubscribeList;
            if (subscribeList == null || subscribeList.Count == 0) return;
            int pageIndex = subscribeList.Count;
            foreach (var subscribe in subscribeList)
            {
                var essays = await apiService.GetSubscribeTopic(subscribe.sourceId, pageIndex);
                if (essays != null)
                {
                    foreach (var item in essays)
                    {
                        SubscribeTopic.Add(item);
                    }
                }
            } 
            
            IsActive = false;
        }

        /// <summary>
        /// 加载订阅内容
        /// </summary>
        public async Task LoadSubscribeContent(string sourceId,int pageIndex)
        {
            IsActive = true;
            var essays = await apiService.GetSubscribeContent(sourceId, pageIndex);
            if(essays!= null)
            {
                foreach (var item in essays)
                {
                    SubscribeContent.Add(item);
                }
            }
            IsActive = false;
        }

        /// <summary>
        /// 刷新订阅专题
        /// </summary>
        public async Task RefreshSubscribeTopic()
        {
            IsActive = true;
            SubscribeTopic.Clear();
            await LoadSubscribeTopic();
            IsActive = false;
        }

        /// <summary>
        /// 加载订阅内容
        /// </summary>
        public void RefreshSubscribeContent()
        {
            IsActive = true;
            SubscribeContent.Clear();
            //await LoadSubscribeContent();
            IsActive = false;
        }
    }
}
