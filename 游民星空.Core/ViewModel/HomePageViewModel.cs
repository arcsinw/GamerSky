using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using 游民星空.Core.Helper;

namespace 游民星空.Core.ViewModel
{
    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel()
        {
        }


        /// <summary>
        /// 清空缓存
        /// </summary>
        public async void ClearCache()
        {
            CacheSize = "正在删除缓存";
            await FileHelper.Current.DeleteCacheFile();
            GetCacheSize();
        }

        private string cacheSize = "0";
        public string CacheSize
        {
            get
            {
                return cacheSize;
            }
            set
            {
                cacheSize = value;
                OnPropertyChanged();
            }
        }

        public async void GetCacheSize()
        {
            double size = await FileHelper.Current.GetCacheSize();
            CacheSize = (size / 1024 /1024).ToString("f2");
        }

        
    }
}
