using Microsoft.Services.Store.Engagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Helper
{
    public static class ExperimentHelper
    {

        #region const strings 
        private const string API_KEY = "28b286dc-fc48-488c-ae23-89e48ed7b35b";

        public const string TranslateButtonVisibility = "TranslateButtonVisibility";

        private const string UseTranslateButton = "useTranslateButton";

        private const string ViewEventName = "userViewedTranslateButton";
        #endregion

        private static ExperimentClient experimentClient;

        private static ExperimentVariation variation;

        private static ExperimentVariationResult result;

        static ExperimentHelper()
        {
            experimentClient = new ExperimentClient(API_KEY);
            GetExperimentVariation();
        }
         
        /// <summary>
        /// 获取变量
        /// </summary>
        private static async void GetExperimentVariation()
        {
            result = await experimentClient.GetVariationAsync();
            variation = result.Variation;
        }

        /// <summary>
        /// 检查变量是否需要更新
        /// </summary>
        private static void CheckVariationUpdate()
        {
            if (result == null) return;
            if (result.ErrorCode != EngagementErrorCode.Success || result.Variation.NeedsRefresh)
            {
                UpdateExperimentVariation();
            }
        }

        /// <summary>
        /// 更新变量
        /// </summary>
        private static async void UpdateExperimentVariation()
        {
            result = await experimentClient.RefreshVariationAsync();
            if (result.ErrorCode == EngagementErrorCode.Success)
            {
                variation = result.Variation;
            }
        }

        /// <summary>
        /// 获取int类型变量
        /// </summary>
        /// <param name="name">变量设置名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetInt(string name, int defaultValue)
        {
            CheckVariationUpdate();
            return variation.GetInteger(name, defaultValue);
        }
         
        public static bool GetBool(string name,bool defaultValue)
        {
            CheckVariationUpdate();
            return variation.GetBoolean(name, defaultValue);
        }

        public static double GetDouble(string name,double defaultValue)
        {
            CheckVariationUpdate();
            return variation.GetDouble(name, defaultValue);
        }

        public static string GetString(string name,string defaultValue)
        {
            CheckVariationUpdate();
            return variation.GetString(name, defaultValue);
        }

        /// <summary>
        /// 向Dev Center写日志
        /// </summary>
        public static void LogEventToDevCenter(string eventName,ExperimentVariation variation)
        {
            StoreServicesCustomEvents.Log(eventName, variation);
        }

        /// <summary>
        /// 翻译按钮点击
        /// </summary>
        public static void LogTranslateClick()
        {
            LogEventToDevCenter(UseTranslateButton, variation);
        }

        /// <summary>
        /// 用户看到翻译按钮
        /// </summary>
        public static void LogTranslateViewd()
        {
            LogEventToDevCenter(ViewEventName, variation);
        }
    }
}
