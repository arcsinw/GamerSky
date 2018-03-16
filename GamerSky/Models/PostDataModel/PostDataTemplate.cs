using GamerSky.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models.PostDataModel
{
    public class PostDataTemplate<T> 
    {
        public string appVersion = "3.7.1";
        public string deviceId = DeviceInformationHelper.GetDeviceId();
        public string deviceType = "NOKIA N1";
        public string osVersion = "5.0.0";
        public string os = "android";

        public T request;
    }
}
