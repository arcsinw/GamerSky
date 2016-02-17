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
            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;

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

        /// <summary>
        /// 清空缓存
        /// </summary>
        public async void ClearCache()
        {
            CacheSize = "删除缓存中...";
            await FileHelper.Current.DeleteCacheFile();
            
        }

        private string cacheSize = "0 MB";
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
            CacheSize = (size / 1024 / 1024).ToString("f2") + " MB";
        }

        /// <summary>
        /// 更改日/夜间模式
        /// </summary>
        public void ChangeDisplayMode(bool isNight)
        {
            DataShareManager.Current.UpdateAPPTheme(isNight);
        }

        private bool isNight;
        public bool IsNight
        {
            get
            {
                return isNight;
            }
            set
            {
                isNight = value;
                ChangeDisplayMode(isNight);
            }
        }
    }
}
