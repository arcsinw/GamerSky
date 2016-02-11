using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.Helper;

namespace 游民星空.Core.ViewModel
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private bool isToastShow = false;
        /// <summary>
        /// 是否推送要闻
        /// </summary>
        public bool IsToastShow
        {
            get
            {
                return isToastShow;
            }
            set
            {
                isToastShow = value;
                OnPropertyChanged();
            }
        }

        private bool isLiveTileShow = false;
        /// <summary>
        /// 是否显示动态磁贴
        /// </summary>
        public bool IsLiveTileShow
        {
            get
            {
                return isLiveTileShow;
            }
            set
            {
                isLiveTileShow = value;
                if(isLiveTileShow)
                {
                    LiveTileHelper.RegisterLiveTileTask();
                    LiveTileHelper.UpdatePrimaryTile();
                }
                else
                {
                    LiveTileHelper.UnRegisterLiveTileTask();
                }
                OnPropertyChanged();
            }
        }
    }
}
