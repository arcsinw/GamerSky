using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Store;
using Windows.Storage;

namespace GamerSky.Core.Helper
{
    /// <summary>
    /// 应用内购买
    /// </summary>
    public class IAPHelper
    {
        /// <summary>
        /// 去除广告的Id
        /// </summary>
        public static string Remove_Ad = "GamerSky.x_RemoveAd";

        public static string Donate = "GamerSky.x_Donate";

#if DEBUG
        public static LicenseInformation licenseInformation = CurrentAppSimulator.LicenseInformation;
#else
        public static LicenseInformation licenseInformation = CurrentAppSimulator.LicenseInformation;
#endif
        public static async Task ConfigureSimulatorAsync(string filename)
        {
            StorageFile proxyFile = await Package.Current.InstalledLocation.GetFileAsync("data\\" + filename);
            await CurrentAppSimulator.ReloadSimulatorAsync(proxyFile);
        }

        /// <summary>
        /// 购买某个应用内产品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static async Task BuyProductAsync(string productId)
        {
            if(!IsProductGot(productId))
            {
                try
                {
                    await CurrentAppSimulator.RequestProductPurchaseAsync(productId);
                    if(licenseInformation.ProductLicenses[productId].IsActive)
                    {
                        Functions.ShowMessage("感谢支持");
                    }
                }
                catch(Exception e)
                {

                }
            }
            else  //产品已购买
            {

            }
        }

        /// <summary>
        /// 是否已购买某产品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public static bool IsProductGot(string productId)
        {
            return licenseInformation.ProductLicenses[productId].IsActive;
        }
    }
}
