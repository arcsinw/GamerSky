using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSky.Helper;

namespace GamerSky.PostDataModel
{
    public class PostDataBase
    {
        public string appVersion = "2.4.1";
        public string deviceId = DeviceInformationHelper.GetDeviceId();
        public string deviceType = "NOKIA N1";
        public string osVersion = "5.0.0";
        public string os = "android";
    }
}
