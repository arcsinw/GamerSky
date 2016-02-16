using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace 游民星空.Core.ViewModel
{
    /// <summary>
    /// ViewModel之间共享数据
    /// </summary>
    public class DataShareManager
    {
        private ElementTheme appTheme;
        /// <summary>
        /// 日/夜间模式
        /// </summary>
        public ElementTheme AppTheme
        {
            get
            {
                return appTheme;
            }
        }
        private bool isBigFont;
        /// <summary>
        /// 是否大字体
        /// </summary>
        public bool IsBigFont
        {
            get
            {
                return isBigFont;
            }
        }

        private bool isNoImage;
        /// <summary>
        /// 是否无图模式
        /// </summary>
        public bool IsNoImage
        {
            get
            {
                return isNoImage;
            }
        }

        /// <summary>
        /// 收藏列表
        /// </summary>
        private List<string> favoriteList;
        public List<string> FavoriteList
        {
            get
            {
                return favoriteList;
            }
        }

        private static DataShareManager current;
        public static DataShareManager Current
        {
            get
            {
                if(current == null)
                {
                    current = new DataShareManager();
                }
                return current;
            }
        }

        public DataShareManager()
        {
            LoadData();
        }

        private void LoadData()
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            if(roamingSettings.Values.ContainsKey("APP_THEME"))
            {
                appTheme = int.Parse(roamingSettings.Values["APP_THEME"].ToString()) == 0 ? ElementTheme.Light : ElementTheme.Dark;
            }
            else
            {
                appTheme = ElementTheme.Light;
            }

            if (roamingSettings.Values.ContainsKey("BIG_FONT"))
            {
                isBigFont = bool.Parse(roamingSettings.Values["BIG_FONT"].ToString());
            }
            else
            {
                isBigFont = false;
            }

            if (roamingSettings.Values.ContainsKey("NO_IMAGES_MODE"))
            {
                isNoImage = bool.Parse(roamingSettings.Values["NO_IMAGES_MODE"].ToString());
            }
            else
            {
                isNoImage = false;
            }
        }

        private void OnShareDataChanged()
        {
            if(ShareDataChanged!=null)
            {
                ShareDataChanged();
            }
        }

        public void UpdateAPPTheme(bool dark)
        {
            appTheme = dark ? ElementTheme.Dark : ElementTheme.Light;
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["APP_THEME"] = ( appTheme == ElementTheme.Light ? 0 : 1);
            OnShareDataChanged();
        }
        public void UpdateBigFont(bool _isBigFont)
        {
            this.isBigFont = _isBigFont;
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["BIG_FONT"] = isBigFont;
            OnShareDataChanged();
        }
        public void UpdateNoImagesMode(bool _isNoImages)
        {
            isNoImage = _isNoImages;
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["NO_IMAGES_MODE"] = isNoImage;
            OnShareDataChanged();
        }

        public delegate void ShareDataChangedEventHandler();

        public event ShareDataChangedEventHandler ShareDataChanged;
    }
}
