using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Http;
using GamerSky.IncrementalLoadingCollection;
using GamerSky.Model;

namespace GamerSky.ViewModel
{
    public class SubscribePageViewModel : ViewModelBase
    {
        public ObservableCollection<Essay> SubscribeTopic { get; set; }

        public ObservableCollection<Essay> SubscribeContent { get; set; }

        //public SubscribeIncrementalCollection SubscribeContent { get; set; }
         

        public SubscribePageViewModel()
        { 
            SubscribeTopic = new ObservableCollection<Essay>();
            
            SubscribeContent = new ObservableCollection<Essay>();
            //SubscribeContent = new SubscribeIncrementalCollection();
            //SubscribeContent.OnLoadingMoreStart += SubscribeContent_OnLoadingMoreStart;
            //SubscribeContent.OnLoadingMoreEnd += SubscribeContent_OnLoadingMoreEnd;
            
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
            //SubscribeTopic.Clear();
            //SubscribeContent.Clear();

            //await LoadSubscribeContent();
            //await LoadSubscribeTopic();
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
                    var essays = await ApiService.Instance.GetSubscribeTopic(subscribe.SourceId, pageIndex);
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

            string x = DataShareManager.Current.SubscribeList[currentSubscribeIndex].SourceId;
            List<Essay> essays = await ApiService.Instance.GetSubscribeContent(x, pageIndex);
            if (essays != null)
            {
                foreach (var item in essays)
                {
                    if (item.Type == null || !item.Type.Equals("dingyueTitle"))
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
