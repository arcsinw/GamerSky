﻿using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using GamerSky.Core.Helper;
using GamerSky.Core.Model;

namespace GamerSky.Core.ViewModel
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


        private List<Essay> favoriteList = new List<Essay>();
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
        /// 订阅收藏列表
        /// </summary>
        public List<Subscribe> SubscribeList
        {
            get
            {
                return subscribeList;
            }
        }

        private List<Strategy> strategyList = new List<Strategy>();
        /// <summary>
        /// 攻略收藏
        /// </summary>
        public List<Strategy> StrategyList
        {
            get
            {
                return strategyList;
            }
            set
            {
                strategyList = value;
            }
        }

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
    
        /// <summary>
        /// 本地设置
        /// </summary>
        private static ApplicationDataContainer settings = Windows.Storage.ApplicationData.Current.LocalSettings;
         
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
                subscribeList = JsonHelper.Deserlialize <List<Subscribe>>(localSettings.Values[SettingKey_SubscribeList].ToString());
            }
            else
            {
                subscribeList = new List<Subscribe>();
            }

            //加载文章收藏列表
            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(FavoriteList_Folder,CreationCollisionOption.OpenIfExists);
            
            if (IsolatedStorageFile.GetUserStoreForApplication().FileExists(folder.Path+"\\"+EssayList_FileName))
            {
                favoriteList = await FileHelper.Current.ReadObjectAsync<List<Essay>>(EssayList_FileName, FavoriteList_Folder);
            }
  
            //加载攻略列表
            if (IsolatedStorageFile.GetUserStoreForApplication().FileExists(folder.Path+"\\" + StrategyList_FileName))
            {
                strategyList = await FileHelper.Current.ReadObjectAsync<List<Strategy>>(StrategyList_FileName, FavoriteList_Folder);
            }
        }

        private void OnShareDataChanged()
        {
            if(ShareDataChanged!=null)
            {
                ShareDataChanged();
            }
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

        public void UpdateBigFont(int _fontSize)
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
            bool add = !subscribeList.Any(x => x.sourceId == subscribe.sourceId);
            
            if (add)
            {
                subscribe.Favorite = true;
                subscribeList.Add(subscribe);
            }
            else
            {
                subscribe.Favorite = false;
                subscribeList.RemoveAll(x => x.sourceId == subscribe.sourceId);
                 
            }
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[SettingKey_SubscribeList] = JsonHelper.Serializer<List<Subscribe>>(subscribeList);
            OnShareDataChanged();
        }
        
        /// <summary>
        /// 更新文章收藏列表 Essay
        /// </summary>
        /// <param name="gameList"></param>
        public async void UpdateFavoriteEssayList(Essay essay)
        {
            bool add = !favoriteList.Any(x => x.contentId == essay.contentId);
            if(add)
            {
                essay.IsFavorite = true;
                favoriteList.Add(essay);
            }
            else
            {
                essay.IsFavorite = false;
                favoriteList.Remove(essay);
            }
            //更新本地文件
            await FileHelper.Current.WriteObjectAsync(favoriteList, EssayList_FileName, FavoriteList_Folder);
            OnShareDataChanged();
        }

        /// <summary>
        /// 更新攻略列表 Strategy
        /// </summary>
        /// <param name="strategy"></param>
        public async void UpdateStrategyList(Strategy strategy)
        {
            bool add = !strategyList.Any(x => x.specialID == strategy.specialID);
            if (add)
            {
                strategyList.Add(strategy);
            }
            else
            {
                strategyList.Remove(strategy);
            }
            //更新本地文件
            await FileHelper.Current.WriteObjectAsync(strategyList, EssayList_FileName, FavoriteList_Folder);
            OnShareDataChanged();
        }
        #endregion

        public delegate void ShareDataChangedEventHandler();

        public event ShareDataChangedEventHandler ShareDataChanged;
    }
}
