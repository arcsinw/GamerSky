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
            //AppTheme = DataShareManager.Current.AppTheme;
            //DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;

        }


    

        /// <summary>
        /// 清空缓存
        /// </summary>
        public async void ClearCache()
        {
            CacheSize = "删除缓存中...";
            await FileHelper.Current.DeleteCacheFile();
            double cache = await FileHelper.Current.GetCacheSize();
            CacheSize = GetFormatSize(cache);
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
            CacheSize = GetFormatSize(size);
        }

        private string GetFormatSize(double size)
        {
            if (size < 1024)
            {
                return size + "byte";
            }
            else if (size < 1024 * 1024)
            {
                return Math.Round(size / 1024, 2) + "KB";
            }
            else if (size < 1024 * 1024 * 1024)
            {
                return Math.Round(size / 1024 / 1024, 2) + "MB";
            }
            else
            {
                return Math.Round(size / 1024 / 1024 / 2014, 2) + "GB";
            }
        }
    }
}
