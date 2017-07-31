using GamerSky.Core.Helper;
using GamerSky.Core.Model;
using GamerSky.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace GamerSky.ViewModel
{
    public class MasterDetailViewModel : ViewModelBase
    {
        public MasterDetailViewModel()
        {
            AppTheme = DataShareManager.Current.AppTheme;
            User = DataShareManager.Current.CurrentUser;
            IsNoImgMode = DataShareManager.Current.IsNoImage;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
            User = DataShareManager.Current.CurrentUser;
            IsNoImgMode = DataShareManager.Current.IsNoImage;
        }

        #region Properties
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

        private bool _isNoImg;

        public bool IsNoImgMode
        {
            get { return _isNoImg; }
            set { _isNoImg = value; }
        }


        private User user;
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                //DataShareManager.Current.UpdateUser(value);
                OnPropertyChanged();
            }
        }

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

        /// <summary>
        /// 是否夜间模式
        /// </summary>
        public bool IsNight
        {
            get
            {
                return DataShareManager.Current.AppTheme == ElementTheme.Dark;
            }
            set
            {
                DataShareManager.Current.UpdateAPPTheme(value);

                if (value)
                {
                    AppTheme = ElementTheme.Dark;
                }
                else
                {
                    AppTheme = ElementTheme.Light;
                }

                UIHelper.ShowStatusBar();
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
