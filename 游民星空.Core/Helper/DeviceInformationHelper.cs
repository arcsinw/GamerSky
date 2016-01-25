using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.ExchangeActiveSyncProvisioning;

namespace 游民星空.Core.Helper
{
    public class DeviceInformationHelper
    {
        private static EasClientDeviceInformation easDeviceInfo = new EasClientDeviceInformation();
        /// <summary>
        /// return local device id
        /// </summary>
        /// <returns></returns>
        public static string GetDeviceId()
        {
            return easDeviceInfo.Id.ToString();
        }

        /// <summary>
        /// product name
        /// </summary>
        /// <returns></returns>
        public static string GetProductName()
        {
            return easDeviceInfo.SystemProductName;
        }

        /// <summary>
        /// OS
        /// </summary>
        /// <returns></returns>
        public static string GetOS()
        {
            return easDeviceInfo.OperatingSystem;
        }

        /// <summary>
        /// os version
        /// </summary>
        /// <returns></returns>
        public static string GetOSVer()
        {
            return easDeviceInfo.SystemFirmwareVersion;
        }

        /// <summary>
        /// hardware version
        /// </summary>
        /// <returns></returns>
        public static string GetHardVersion()
        {
            return easDeviceInfo.SystemHardwareVersion;
        }
    }
}
