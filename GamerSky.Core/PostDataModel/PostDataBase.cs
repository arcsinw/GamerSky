using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSky.Core.Helper;

namespace GamerSky.Core.PostDataModel
{
    public class PostDataBase
    {
        public string appVersion = "2.0.7";
        public string deviceId = DeviceInformationHelper.GetDeviceId();
        public string deviceType = "NOKIA N1";
        public string osVersion = "5.0.0";
        public string os = "android";
    }
}
