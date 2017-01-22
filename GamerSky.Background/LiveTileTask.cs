using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using GamerSky.Core.Helper;
using GamerSky.Core.Model;

namespace GamerSky.Background
{
    public sealed class LiveTileTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            await LiveTileHelper.UpdatePrimaryTile(); 
            deferral.Complete();
        }

        /// <summary>
        /// 更新动态磁贴
        /// </summary>
        private void UpdateLiveTile()
        {
            UpdatePrimaryTile();
        }

        /// <summary>
        /// 更新主磁贴
        /// </summary>
        private void UpdatePrimaryTile()
        {
            
        }
    }
}
