using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace GamerSky.Background
{
    public sealed class ToastBackgroundTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var dererral = taskInstance.GetDeferral();

            dererral.Complete();
        }
    }
}
