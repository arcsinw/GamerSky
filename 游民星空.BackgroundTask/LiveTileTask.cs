using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using 游民星空.Core.Helper;
using 游民星空.Core.Model;

namespace 游民星空.BackgroundTask
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
            
            //ToastHelper.ShowToast();
        }
    }
}
