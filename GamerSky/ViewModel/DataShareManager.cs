using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using GamerSky.Helper;
using GamerSky.Model;
using System.Collections.ObjectModel;

namespace GamerSky.ViewModel
{
    /// <summary>
    /// ViewModel之间共享数据
    /// </summary>
    public class DataShareManager
    {
        #region const Settings Keys 
        private const string SettingKey_IsFirstLaunch = "IS_FIRST_LAUNCH";
        private const string SettingKey_IsNewVersion = "IS_NEW_VERSION";
        private const string SettingKey_BigFont = "FONT_SIZE";
        private const string SettingKey_NoImageMode = "NO_IMAGES_MODE";
        private const string RoamingSettingKey_AppTheme = "APP_THEME";
        private const string SettingKey_GameSubscribeList = "GAME_SUBSCRIBE_LIST"; //游戏关注列表
        private const string SettingKey_SubscribeList = "SUBSCRIBE_LIST";                   //订阅列表
        private const string SettingKey_IsStatusBarShow = "IS_STATUSBAR_SHOW";
        private const string SettingKey_User = "USER";
        #endregion

        #region Files and folders'  name
        /// <summary>
        /// 收藏文件夹名
        /// </summary>
        private const string FavoriteList_Folder = "favorite_list";
        /// <summary>
        /// 文章收藏列表的文件名
        /// </summary>
        private const string EssayList_FileName = "favoriteEssay_List.json";
        /// <summary>
        /// 订阅列表的文件名
        /// </summary>
        private const string SubscribeList_FileName = "subscribe_List.json";
        /// <summary>
        /// 攻略列表的文件名
        /// </summary>
        private const string StrategyList_FileName = "strategy_List.json";
        #endregion

        #region Properties
        private User currentUser;
        public User CurrentUser
        {
            get
            {
                return currentUser;
            }
            set
            {
                CurrentUser = value;
            }
        }

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
            set
            {
                appTheme = value;
            }
        }

        private int fontSize;
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize
        {
            get
            {
                return fontSize;
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
        /// 收藏文章列表
        /// </summary>
        public ObservableCollection<Essay> FavoriteList { get; set; } = new ObservableCollection<Essay>();

        /// <summary>
        /// 订阅收藏列表
        /// </summary>
        public ObservableCollection<Subscribe> SubscribeList { get; set; } = new ObservableCollection<Subscribe>();

        /// <summary>
        /// 攻略收藏
        /// </summary>
        public ObservableCollection<Strategy> StrategyList { get; set; } = new ObservableCollection<Strategy>();

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
                if (value == null)
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

        #endregion

        #region Singleton
        /// <summary>
        /// 本地设置
        /// </summary>
        private static ApplicationDataContainer settings = Windows.Storage.ApplicationData.Current.LocalSettings;

        private static DataShareManager current;
        public static DataShareManager Current
        {
            get
            {
                if (current == null)
                {
                    current = new DataShareManager();
                }
                return current;
            }
        }

        private DataShareManager()
        {
            LoadData();
        } 
        #endregion

        private async void LoadData()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            if(roamingSettings.Values.ContainsKey(RoamingSettingKey_AppTheme))
            {
                appTheme = roamingSettings.Values[RoamingSettingKey_AppTheme].ToString().Equals("0") ? ElementTheme.Light : ElementTheme.Dark;
            }
            else
            {
                appTheme = ElementTheme.Light;
            }

            if (roamingSettings.Values.ContainsKey(SettingKey_BigFont))
            {
                fontSize = int.Parse(roamingSettings.Values[SettingKey_BigFont].ToString());
            }
            else
            {
                fontSize = 20;
            }

            if (roamingSettings.Values.ContainsKey(SettingKey_NoImageMode))
            {
                isNoImage = bool.Parse(roamingSettings.Values[SettingKey_NoImageMode].ToString());
            }
            else
            {
                isNoImage = false;
            }

            //load user

            //加载订阅列表
            if (localSettings.Values.ContainsKey(SettingKey_SubscribeList))
            {
                var result = JsonHelper.Deserlialize<List<Subscribe>>(localSettings.Values[SettingKey_SubscribeList].ToString());
                if (result != null)
                {
                    result.ForEach(x => SubscribeList.Add(x));
                }
            }

            //加载文章收藏列表
            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(FavoriteList_Folder,CreationCollisionOption.OpenIfExists);
            
            if (IsolatedStorageFile.GetUserStoreForApplication().FileExists(folder.Path+"\\"+EssayList_FileName))
            {
                var result = await FileHelper.Current.ReadObjectAsync<List<Essay>>(EssayList_FileName, FavoriteList_Folder);
                if (result != null)
                {
                    result.ForEach(x => FavoriteList.Add(x));
                }
            }
  
            //加载攻略列表
            if (IsolatedStorageFile.GetUserStoreForApplication().FileExists(folder.Path+"\\" + StrategyList_FileName))
            {
                var result = await FileHelper.Current.ReadObjectAsync<List<Strategy>>(StrategyList_FileName, FavoriteList_Folder);
                if (result != null)
                {
                    result.ForEach(x => StrategyList.Add(x));
                }
            }
        }

        private void OnShareDataChanged()
        {
            ShareDataChanged?.Invoke();
        }

        #region Update properties methods
        public void UpdateUser(User user)
        {
            if(user!=null)
            {
                this.currentUser = user;
                //var localSettings = ApplicationData.Current.LocalSettings;
                //localSettings.Values[SettingKey_User] = JsonHelper.Serializer<User>(user);
                OnShareDataChanged();
            }
        }
        /// <summary>
        /// true 为Dark false 为Light
        /// </summary>
        /// <param name="dark"></param>
        public void UpdateAPPTheme(bool dark)
        {
            appTheme = dark ? ElementTheme.Dark : ElementTheme.Light;
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["APP_THEME"] = ( appTheme == ElementTheme.Light ? 0 : 1);
            OnShareDataChanged();
        }

        public void UpdateFontSize(int _fontSize)
        {
            this.fontSize = _fontSize;
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["BIG_FONT"] = fontSize;
            OnShareDataChanged();
        }
        public void UpdateNoImagesMode(bool _isNoImages)
        {
            isNoImage = _isNoImages;
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["NO_IMAGES_MODE"] = isNoImage;
            OnShareDataChanged();
        }

        /// <summary>
        /// 更新订阅列表 Subscribe
        /// </summary>
        /// <param name="subscribe"></param>
        public void UpdateSubscribe(Subscribe subscribe)
        {
            bool add = !SubscribeList.Any(x => x.SourceId == subscribe.SourceId);
            
            if (add)
            {
                subscribe.IsFavorite = true;
                SubscribeList.Add(subscribe);
            }
            else
            {
                subscribe.IsFavorite = false;
                var result = from x in SubscribeList where x.SourceId.Equals(subscribe.SourceId) select x;
                SubscribeList.Remove(result.First());
                //SubscribeList.Remove(x => (x.SourceId == subscribe.SourceId)); 
            }
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[SettingKey_SubscribeList] = JsonHelper.Serializer(SubscribeList);
            OnShareDataChanged();
        }
        
        /// <summary>
        /// 更新文章收藏列表 Essay
        /// </summary>
        /// <param name="gameList"></param>
        public async void UpdateFavoriteEssayList(Essay essay)
        {
            bool add = !FavoriteList.Any(x => x.ContentId == essay.ContentId);
            if(add)
            {
                essay.IsFavorite = true;
                FavoriteList.Add(essay);
            }
            else
            {
                essay.IsFavorite = false;
                FavoriteList.Remove(essay);
            }
            //更新本地文件
            await FileHelper.Current.WriteObjectAsync(FavoriteList, EssayList_FileName, FavoriteList_Folder);
            OnShareDataChanged();
        }

        /// <summary>
        /// 更新攻略列表 Strategy
        /// </summary>
        /// <param name="strategy"></param>
        public async void UpdateStrategyList(Strategy strategy)
        {
            bool add = !StrategyList.Any(x => x.SpecialID == strategy.SpecialID);
            if (add)
            {
                StrategyList.Add(strategy);
            }
            else
            {
                StrategyList.Remove(strategy);
            }
            //更新本地文件
            await FileHelper.Current.WriteObjectAsync(StrategyList, EssayList_FileName, FavoriteList_Folder);
            OnShareDataChanged();
        }
        #endregion

        public delegate void ShareDataChangedEventHandler();

        public event ShareDataChangedEventHandler ShareDataChanged;
    }
}
