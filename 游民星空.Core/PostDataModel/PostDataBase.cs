using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.Helper;

namespace 游民星空.Core.PostDataModel
{
    public class PostDataBase
    {
        public static string DeviceId = DeviceInformationHelper.GetDeviceId();
        public string appVersion = "2.0.7";
        public string deviceId = DeviceId;
        public string deviceType = "NOKIA N1";
        public string os = "android";
        public string osVersion = "5.0.0";
    }
}
