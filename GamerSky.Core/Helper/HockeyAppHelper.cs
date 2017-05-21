using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Helper
{
    public static class HockeyAppHelper
    {
        public const string APP_ID = "00d089131ae741358edf474dd989d86a";

        //public static void ReportCrash(Exception ex)
        //{
        //    Microsoft.HockeyApp.HockeyClient.Current.Configure(APP_ID,
        //        new Microsoft.HockeyApp.TelemetryConfiguration()
        //        {
        //            DescriptionLoader = (ex)=> { return "HResult= " + ex.HResult.ToString()};
        //        });
        //}
    }
}
