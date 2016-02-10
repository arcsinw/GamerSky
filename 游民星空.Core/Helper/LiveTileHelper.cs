using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.Helper
{
    public class LiveTileHelper
    {
        private const string TILE_TASK_NAME = "TILETASK";

        private const string ENTRY_NAME = "游民星空.BackgroundTask.LiveTileTask";
        /// <summary>
        /// 注册动态磁贴后台任务
        /// </summary>
        public static async void RegisterLiveTileTask()
        {



            var status = await BackgroundExecutionManager.RequestAccessAsync();
            if (status == BackgroundAccessStatus.Unspecified || status == BackgroundAccessStatus.Denied)
            {
                return;
            }
            //如果已经注册则先取消注册
            foreach (var t in BackgroundTaskRegistration.AllTasks)
            {
                if (t.Value.Name == TILE_TASK_NAME)
                {
                    t.Value.Unregister(true);
                }
            }
            
            var taskBuilder = new BackgroundTaskBuilder { Name = TILE_TASK_NAME, TaskEntryPoint = ENTRY_NAME };
            taskBuilder.AddCondition(new SystemCondition(SystemConditionType.InternetAvailable));
            taskBuilder.SetTrigger(new TimeTrigger(60, false));
            taskBuilder.Register();

        }

        /// <summary>
        /// 取消后台任务注册
        /// </summary>
        public static void UnRegisterLiveTileTask()
        {
            foreach (var t in BackgroundTaskRegistration.AllTasks)
            {
                if (t.Value.Name == TILE_TASK_NAME)
                {
                    t.Value.Unregister(true);
                }
            }
        }

        /// <summary>
        /// 前图后文字的磁贴模板
        /// </summary>
        private const string TileTemplateXml = @"
            <title branding='name'>
                <visual version='3'>
                    <binding template='TileMedium'>
                        <image src='{0}' placement='peek'/>
                        <text hint-style='base'>{1}</text>
                    </binding>
                    <binding template='TileWide'>
                        <image src='{0}' placement='peek'/>
                        <text hint-style='base'>{1}</text>
                    </binding>
                    <binding template='TileLarge'>
                        <image src='{0}' placement='peek'/>
                        <text hint-style='base'>{1}</text>
                    </binding>
                </visual>
            </title>";

        public static ApiService apiService = new ApiService();
        /// <summary>
        /// 更新动态磁贴
        /// </summary>
        public static async Task UpdatePrimaryTile()
        {
            //获取要闻
            List<EssayResult> essays = await apiService.GetYaowen();

            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);  //启用通知队列
            updater.EnableNotificationQueueForSquare150x150(true);
            updater.EnableNotificationQueueForSquare310x310(true);
            updater.EnableNotificationQueueForWide310x150(true);

            if (essays != null)
            {
                foreach (var item in essays)
                {
                    var doc = new XmlDocument();
                    var xml = string.Format(TileTemplateXml, item.thumbnailURLs[0], item.title);
                    doc.LoadXml(WebUtility.HtmlDecode(xml), new XmlLoadSettings
                    {
                        ElementContentWhiteSpace = false,
                        ProhibitDtd = false,
                        ValidateOnParse = false,
                        ResolveExternals = false
                    });
                    updater.Update(new TileNotification(doc));
                }
            }
        }

    }
}
