using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.StartScreen;
using GamerSky.Core.Http;
using GamerSky.Core.Model;

namespace GamerSky.Core.Helper
{
    /// <summary>
    /// 动态磁贴
    /// </summary>
    public class LiveTileHelper
    {
        private const string TILE_TASK_NAME = "TILETASK";

        private const string ENTRY_NAME = "GamerSky.BackgroundTask.LiveTileTask";
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
            taskBuilder.SetTrigger(new TimeTrigger(60 * 6, false));
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
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.Clear();
            if (SecondaryTile.Exists(TILE_ID))
            {
                var secondaryUpdater = TileUpdateManager.CreateTileUpdaterForSecondaryTile(TILE_ID);
                secondaryUpdater.Clear();
            }
        }

        /// <summary>
        /// 前图后文字的磁贴模板
        /// </summary>
        private const string TileTemplateXml = @"
            <tile>
                <visual version='3' branding='name'>
                    <binding template='TileMedium'>
                        <image src='{0}' placement='peek'/>
                        <text hint-style='captionSubtle' hint-wrap='true'>{1}</text>
                    </binding>
                    <binding template='TileWide'>
                        <image src='{0}' placement='peek'/>
                        <text hint-style='base' hint-wrap='true'>{1}</text>
                    </binding>
                    <binding template='TileLarge'>
                        <image src='{0}' placement='peek'/>
                        <text hint-style='base' hint-wrap='true'>{1}</text>
                        <text hint-style='captionSubtle' hint-wrap='true'>{2}</text>
                    </binding>
                </visual>
            </tile>";
         

        private static TileUpdater secondaryUpdater;
        /// <summary>
        /// 更新动态磁贴
        /// </summary>
        public static async Task UpdatePrimaryTile()
        {
            //获取要闻
            List<Essay> essays = await ApiService.Instance.GetYaowen();
            try
            {
                //更新主磁贴
                var updater = TileUpdateManager.CreateTileUpdaterForApplication();
                updater.EnableNotificationQueue(true);  //启用通知队列 最多支持5个通知
                updater.EnableNotificationQueueForSquare150x150(true);
                updater.EnableNotificationQueueForSquare310x310(true);
                updater.EnableNotificationQueueForWide310x150(true);
                updater.Clear();

                if (SecondaryTile.Exists(TILE_ID))
                {
                    //更新辅助磁贴
                    secondaryUpdater = TileUpdateManager.CreateTileUpdaterForSecondaryTile(TILE_ID);
                    secondaryUpdater.EnableNotificationQueue(true);  //启用通知队列
                    secondaryUpdater.EnableNotificationQueueForSquare150x150(true);
                    secondaryUpdater.EnableNotificationQueueForSquare310x310(true);
                    secondaryUpdater.EnableNotificationQueueForWide310x150(true);
                    secondaryUpdater.Clear();
                }
                if (essays != null)
                {
                    for (int i=0; i<5; i++)
                    {
                        var item = essays[i];
                        var doc = new XmlDocument();
                        var xml = string.Format(TileTemplateXml, item.ThumbnailURLs[0], item.Title, item.AuthorName);
                        doc.LoadXml(WebUtility.HtmlDecode(xml), new XmlLoadSettings
                        {
                            ElementContentWhiteSpace = false,
                            ProhibitDtd = false,
                            ValidateOnParse = false,
                            ResolveExternals = false
                        });
                        updater.Update(new TileNotification(doc));
                        if (SecondaryTile.Exists(TILE_ID))
                        {
                            secondaryUpdater.Update(new TileNotification(doc));
                        }
                    }
              
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("LiveTileHelper UpdatePrimaryTile:" + e.Message);
            }
        }

        private const string TILE_ID = "TransparentTile";

        /// <summary>
        /// pin磁贴到桌面
        /// </summary>
        public static async void PinSecondaryTile(string argument)
        {
            if (!SecondaryTile.Exists(TILE_ID))
            {
                Uri square150x150Logo = new Uri("ms-appx:///Assets/Square150x150Logo_T.png");
                Uri square310x150Logo = new Uri("ms-appx:///Assets/Wide310x150Logo.png");
                Uri square71x71Logo = new Uri("ms-appx:///Assets/Square71x71Logo_T.png");
                Uri square310x310Logo = new Uri("ms-appx:///Assets/Square310x310Logo_T.png");

                SecondaryTile secondaryTile = new SecondaryTile(TILE_ID, " ", argument, square150x150Logo, TileSize.Square150x150);

                //设置磁贴各种格式
                secondaryTile.VisualElements.Square150x150Logo = square150x150Logo;
                secondaryTile.VisualElements.Square71x71Logo = square71x71Logo;
                secondaryTile.VisualElements.Wide310x150Logo = square310x150Logo;
                secondaryTile.VisualElements.Square310x310Logo = square310x310Logo;

                secondaryTile.VisualElements.ShowNameOnSquare150x150Logo = false;
                secondaryTile.VisualElements.ShowNameOnSquare310x310Logo = false;
                secondaryTile.VisualElements.ShowNameOnWide310x150Logo = false;
              

                //把磁贴pin到桌面
                bool result = await secondaryTile.RequestCreateAsync();
            }
        }

        /// <summary>
        /// 删除桌面磁贴
        /// </summary>
        public static async void UnPinSecondaryTile()
        {
            if(SecondaryTile.Exists(TILE_ID))
            {
                var tile = new SecondaryTile(TILE_ID);
                await tile.RequestDeleteAsync();
            }
        }

        /// <summary>
        /// 磁贴是否存在
        /// </summary>
        /// <param name="tileId"></param>
        /// <returns></returns>
        public static bool IsTileExists(string tileId=TILE_ID)
        {
            return SecondaryTile.Exists(tileId);
        }
    }
}
