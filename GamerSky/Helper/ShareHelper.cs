using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace GamerSky.Helper
{
    public class ShareHelper
    {
        private DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();

        public ShareHelper()
        {
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            try
            {
                DataPackage shareData = args.Request.Data;
                shareData.Properties.Title = "游民星空";
                shareData.Properties.Description = "分享";
                
            }
            catch
            {

            }
        }

        /// <summary>
        /// 系统分享
        /// </summary>
        public static void SystemShare()
        {
            DataTransferManager.ShowShareUI();
        }
    }
}
