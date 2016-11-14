using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Core.Http;
using GamerSky.Core.Model;

namespace GamerSky.Core.ViewModel
{
    public class MySubscribePageViewModel : ViewModelBase
    {
        /// <summary>
        /// 全部订阅
        /// </summary>
        public ObservableCollection<Subscribe> AllSubscribes { get; set; }

        /// <summary>
        /// 热门订阅
        /// </summary>
        public ObservableCollection<Subscribe> HotSubscribes { get; set; }

        /// <summary>
        /// 我的订阅
        /// </summary>
        public ObservableCollection<Subscribe> MySubscribes { get; set; }

        private ApiService apiService;

        private bool isActive = true;
        /// <summary>
        /// ProgressRing.IsActive
        /// </summary>
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
        public MySubscribePageViewModel()
        {
            apiService = new ApiService();
            AllSubscribes = new ObservableCollection<Subscribe>();
            HotSubscribes = new ObservableCollection<Subscribe>();
            MySubscribes = new ObservableCollection<Subscribe>();

            LoadMySubscribes();
            
            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
            MySubscribes.Clear();
            foreach (var item in DataShareManager.Current.SubscribeList)
            {
                MySubscribes.Add(item);
            }
            
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

        /// <summary>
        /// 加载所有订阅
        /// </summary>
        public async Task LoadAllSubscribes()
        {
            IsActive = true;
            List<Subscribe> allSubscribes = await apiService.GetSubscribeHotKey("1");
            if (allSubscribes != null)
            {
                foreach (var item in allSubscribes)
                {
                    if (MySubscribes.Any((x) => x.SourceId == item.SourceId))
                    {
                        item.IsFavorite = true;
                    }
                    AllSubscribes.Add(item);
                }
               
            }
            IsActive = false;
        }

        /// <summary>
        /// 加载热门订阅
        /// </summary>
        public async Task LoadHotSubscribes()
        {
            IsActive = true;
            List<Subscribe> hotSubscribes = await apiService.GetSubscribeHotKey("0");
            if (hotSubscribes != null)
            {
                foreach (var item in hotSubscribes)
                {
                    if (MySubscribes.Any((x)=>x.SourceId == item.SourceId))
                    {
                        item.IsFavorite = true;
                    }
                    HotSubscribes.Add(item);
                }
            }
            IsActive = false;
        }

        /// <summary>
        /// 加载我的订阅
        /// </summary>
        public void LoadMySubscribes()
        {
            List<Subscribe> allSubscribes = DataShareManager.Current.SubscribeList;
            if (allSubscribes != null)
            {
                foreach (var item in allSubscribes)
                {
                    MySubscribes.Add(item);
                }
            }
        }

        /// <summary>
        /// 刷新所有订阅
        /// </summary>
        public async Task AllSubscribesRefresh()
        {
            IsActive = true;
            AllSubscribes.Clear();
            await LoadAllSubscribes();
            IsActive = false;
        }

        /// <summary>
        /// 刷新热门订阅
        /// </summary>
        public async Task HotSubscribesRefresh()
        {
            IsActive = true;
            HotSubscribes.Clear();
            await LoadHotSubscribes();
            IsActive = false;
        }

        /// <summary>
        /// 刷新我的订阅
        /// </summary>
        public void MySubscribesRefresh()
        {
            MySubscribes.Clear();
            LoadMySubscribes();
        }
    }
}
