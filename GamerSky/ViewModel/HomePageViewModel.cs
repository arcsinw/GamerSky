 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GamerSky.Helper;
using GamerSky.Core.Helper;

namespace GamerSky.ViewModel
{
    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel()
        {
            GetCacheSize();
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

        #region Properties
        private string cacheSize;
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
        #endregion

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
