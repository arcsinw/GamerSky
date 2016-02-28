using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using 游民星空.Core.Helper;
using 游民星空.Core.Model;

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


        private List<string> favoriteList;
        /// <summary>
        /// 收藏列表
        /// </summary>
        public List<string> FavoriteList
        {
            get
            {
                return favoriteList;
            }          
        }

        private List<Subscribe> subscribeList = new List<Subscribe>();
        /// <summary>
        /// 订阅列表
        /// </summary>
        public List<Subscribe> SubscribeList
        {
            get
            {
                return subscribeList;
            }
        }

        /// <summary>
        /// 本地设置
        /// </summary>
        private static ApplicationDataContainer settings = Windows.Storage.ApplicationData.Current.LocalSettings;

        private const string SettingKey_IsFirstLaunch = "IsFirstLaunch";

        public bool IsFirstLaunch
        {
            get
            {
                var value = settings.Values[SettingKey_IsFirstLaunch];
                return value == null ? true : (bool)value;
            }
            set
            {
                settings.Values[SettingKey_IsFirstLaunch] = value;
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
            var localSettings = ApplicationData.Current.LocalSettings;
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

            if (localSettings.Values.ContainsKey("SUBSCRIBE_LIST"))
            {
                subscribeList = Functions.Deserlialize <List<Subscribe>>(localSettings.Values["SUBSCRIBE_LIST"].ToString());
            }
            else
            {
                subscribeList = new List<Subscribe>();
            }
        }

        private void OnShareDataChanged()
        {
            if(ShareDataChanged!=null)
            {
                ShareDataChanged();
            }
        }
     
        /// <summary>
        /// true 为Dark false 为Light
        /// </summary>
        /// <param name="dark"></param>
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

        public void UpdateSubscribe(Subscribe subscribe)
        {
            bool add = subscribeList.Contains(subscribe) ? false : true;
            if (add)
            {
                subscribeList.Add(subscribe);
            }
            else
            {
                subscribeList.Remove(subscribe);
            }
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values["SUBSCRIBE_LIST"] = Functions.JsonDataSerializer<List<Subscribe>>(subscribeList);
            OnShareDataChanged();
        }

        public delegate void ShareDataChangedEventHandler();

        public event ShareDataChangedEventHandler ShareDataChanged;
    }
}
