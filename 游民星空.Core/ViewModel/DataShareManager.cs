using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
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


        private List<Essay> favoriteList;
        /// <summary>
        /// 收藏文章列表
        /// </summary>
        public List<Essay> FavoriteList
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
        private const string SettingKey_IsNewVersion = "IsNewVersion";
        private const string SettingKey_BigFont = "BIG_FONT";
        private const string SettingKey_NoImageMode = "NO_IMAGES_MODE";
        private const string RoamingSettingKey_AppTheme = "APP_THEME";
        private const string SettingKey_SubscribeList = "SUBSCRIBE_LIST";

        /// <summary>
        /// 是否第一次启动
        /// </summary>
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

        /// <summary>
        /// 是否是新版本
        /// </summary>
        public bool IsNewVersion
        {
            get
            {
                var value = settings.Values[SettingKey_IsNewVersion];
                string ver = Functions.GetVersion();
                if (value==null)
                {
                    settings.Values[SettingKey_IsNewVersion] = ver;

                    return true;
                }
                settings.Values[SettingKey_IsNewVersion] = ver;
                return (string)value != ver;
            }
            set
            {
                settings.Values[SettingKey_IsNewVersion] = value;
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


        private async void LoadData()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            if(roamingSettings.Values.ContainsKey(RoamingSettingKey_AppTheme))
            {
                appTheme = int.Parse(roamingSettings.Values[RoamingSettingKey_AppTheme].ToString()) == 0 ? ElementTheme.Light : ElementTheme.Dark;
            }
            else
            {
                appTheme = ElementTheme.Light;
            }

            if (roamingSettings.Values.ContainsKey(SettingKey_BigFont))
            {
                isBigFont = bool.Parse(roamingSettings.Values[SettingKey_BigFont].ToString());
            }
            else
            {
                isBigFont = false;
            }

            if (roamingSettings.Values.ContainsKey(SettingKey_NoImageMode))
            {
                isNoImage = bool.Parse(roamingSettings.Values[SettingKey_NoImageMode].ToString());
            }
            else
            {
                isNoImage = false;
            }

            if (localSettings.Values.ContainsKey(SettingKey_SubscribeList))
            {
                subscribeList = Functions.Deserlialize <List<Subscribe>>(localSettings.Values[SettingKey_SubscribeList].ToString());
            }
            else
            {
                subscribeList = new List<Subscribe>();
            }

            //加载收藏列表
            if (IsolatedStorageFile.GetUserStoreForApplication().FileExists(ApplicationData.Current.LocalFolder.Path + FavoriteList_FileName))
            {
                favoriteList = await FileHelper.Current.ReadObjectAsync<List<Essay>>(FavoriteList_FileName, FavoriteList_Folder);
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

        /// <summary>
        /// 更新订阅列表
        /// </summary>
        /// <param name="subscribe"></param>
        public void UpdateSubscribe(Subscribe subscribe)
        {
            bool add = !subscribeList.Any(x => x.sourceId == subscribe.sourceId);
            //bool add = subscribeList.Contains(subscribe) ? false : true;
            if (add)
            {
                subscribeList.Add(subscribe);
            }
            else
            {
                subscribeList.Remove(subscribe);
            }
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[SettingKey_SubscribeList] = Functions.JsonDataSerializer<List<Subscribe>>(subscribeList);
            OnShareDataChanged();
        }
        /// <summary>
        /// 收藏文件夹名
        /// </summary>
        private const string FavoriteList_Folder = "favorite_list";
        /// <summary>
        /// 存储收藏列表的文件名
        /// </summary>
        private const string FavoriteList_FileName = "game_list.json";
        /// <summary>
        /// 更新收藏列表
        /// </summary>
        /// <param name="gameList"></param>
        public async void UpdateFavoriteEssayList(Essay essay)
        {
            bool add = favoriteList.Contains(essay);
            if(add)
            {

            }
            else
            {

            }
            await FileHelper.Current.WriteObjectAsync<List<Essay>>(favoriteList, FavoriteList_FileName, FavoriteList_Folder);
            OnShareDataChanged();
        }

        public delegate void ShareDataChangedEventHandler();

        public event ShareDataChangedEventHandler ShareDataChanged;
    }
}
