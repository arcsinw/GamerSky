using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GamerSky.Core.Helper
{
    public class LocalSettingsHelper
    {
        public static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        private const string IsTransparentTileOn_Key = "IsTransparentTileOn";
        private const string IsLiveTileShow_Key = "IsLiveTileShow";

        /// <summary>
        /// 是否打开动态磁贴
        /// </summary>
        public static bool IsLiveTileShow
        {
            get
            {
                return (bool)GetValueByKey(IsLiveTileShow_Key);
            }
            set
            {
                SaveValueByKey(IsLiveTileShow_Key, value);
            }
        }

        /// <summary>
        /// 通过key在LocalSettings中读取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetValueByKey(string key)
        {
            if (localSettings.Values.ContainsKey(key))
            {
                return localSettings.Values[key];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 向LocalSettings中存入值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SaveValueByKey(string key, object value)
        {
            localSettings.Values[key] = value;
        }
    }
}
