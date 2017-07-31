using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Core.Http;
using GamerSky.IncrementalLoadingCollection;
using GamerSky.Core.Model;
using Arcsinx.Toolkit.IncrementalCollection;
using Arcsinx.Toolkit.Controls;

namespace GamerSky.ViewModel
{
    public class SubscribePageViewModel : ViewModelBase
    {
        #region Properties
        public ObservableCollection<Essay> SubscribeTopic { get; set; }
         
        public IncrementalLoadingCollection<Essay> SubscribeContent { get; set; }

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
        #endregion

        public SubscribePageViewModel()
        {
            SubscribeTopic = new ObservableCollection<Essay>();
             
            SubscribeContent = new IncrementalLoadingCollection<Essay>(LoadSubscribeContent, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { IsActive = false; ToastService.SendToast(((Exception)e).Message); });
            
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        private async Task<IEnumerable<Essay>> LoadSubscribeContent(uint count, int pageIndex)
        {
            List<Essay> essays = new List<Essay>();
            if (DataShareManager.Current.SubscribeList.Count == 0)
            {
                return essays;
            }

            string x = DataShareManager.Current.SubscribeList[currentSubscribeIndex].SourceId;
            List<Essay> result = await ApiService.Instance.GetSubscribeContent(x, pageIndex);
            if (result != null && result.Any())
            {
                foreach (var item in result)
                {
                    if (!string.IsNullOrEmpty(item.Type) && item.Type.Equals("xinwen"))
                    {
                        essays.Add(item);
                    }
                }
            }
            else 
            {
                SubscribeContent.NoMore();
            }

            if (currentSubscribeIndex == (DataShareManager.Current.SubscribeList.Count - 1))
            {
                pageIndex++;
            }
            currentSubscribeIndex = ++currentSubscribeIndex % DataShareManager.Current.SubscribeList.Count;

            return essays;
        }
         

        private async void Current_ShareDataChanged()
        {
            SubscribeTopic.Clear();

            await SubscribeContent.ClearAndReloadAsync();
            await LoadSubscribeTopic();
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
                    var essays = await ApiService.Instance.GetSubscribeTopic(subscribe.SourceId, 1);
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
            await SubscribeContent.ClearAndReloadAsync();
            pageIndex = 1;
            currentSubscribeIndex = 0; 
        }
    }
}
