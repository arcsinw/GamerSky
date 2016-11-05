using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Core.Http;
using GamerSky.Core.IncrementalLoadingCollection;
using GamerSky.Core.Model;

namespace GamerSky.Core.ViewModel
{
    public class SubscribePageViewModel : ViewModelBase
    {
        public ObservableCollection<Essay> SubscribeTopic { get; set; }

        public ObservableCollection<Essay> SubscribeContent { get; set; }

        //public SubscribeIncrementalCollection SubscribeContent { get; set; }

        private ApiService apiService;

        public SubscribePageViewModel()
        {
            apiService = new ApiService();
            SubscribeTopic = new ObservableCollection<Essay>();
            
            SubscribeContent = new ObservableCollection<Essay>();
            //SubscribeContent = new SubscribeIncrementalCollection();
            //SubscribeContent.OnLoadingMoreStart += SubscribeContent_OnLoadingMoreStart;
            //SubscribeContent.OnLoadingMoreEnd += SubscribeContent_OnLoadingMoreEnd;


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
            if (subscribeList != null)
            {
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
            }
            IsActive = false;
        }

         
        private int currentSubscribeIndex = 0; //当前订阅index
        private int pageIndex = 1;
        /// <summary>
        /// 加载订阅内容 由ViewModel保存当前页码
        /// </summary>
        public async Task LoadSubscribeContent()
        {
            IsActive = true;
            if(DataShareManager.Current.SubscribeList.Count==0)
            {
                IsActive = false;
                return;
            } 

            string x = DataShareManager.Current.SubscribeList[currentSubscribeIndex].sourceId;
            List<Essay> essays = await apiService.GetSubscribeContent(x, pageIndex);
            if (essays != null)
            {
                foreach (var item in essays)
                {
                    if (!item.type.Equals("dingyueTitle"))
                    {
                        SubscribeContent.Add(item);
                    }
                }
            }
            if (currentSubscribeIndex == (DataShareManager.Current.SubscribeList.Count - 1))
            {
                pageIndex++;
            }
            currentSubscribeIndex = ++currentSubscribeIndex % DataShareManager.Current.SubscribeList.Count;

            IsActive = false;
        }

        /// <summary>
        /// 刷新订阅专题
        /// </summary>
        public async Task RefreshSubscribeTopic()
        {
            SubscribeTopic.Clear();
            await LoadSubscribeTopic();
        }

        /// <summary>
        /// 刷新订阅内容
        /// </summary>
        public async Task RefreshSubscribeContent()
        {
            SubscribeContent.Clear();
            pageIndex = 1;
            currentSubscribeIndex = 0;
            await LoadSubscribeContent();
        }
    }
}
