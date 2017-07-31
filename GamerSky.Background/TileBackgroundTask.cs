using GamerSky.Core.Helper;
using GamerSky.Core.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace GamerSky.Background
{
    public sealed class TileBackgroundTask : IBackgroundTask
    {  
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();
            await LiveTileHelper.UpdatePrimaryTile();
            deferral.Complete();
        } 
    }
}
