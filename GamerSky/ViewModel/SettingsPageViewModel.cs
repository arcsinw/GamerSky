﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Helper;
using GamerSky.Core.Helper;

namespace GamerSky.ViewModel
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public SettingsPageViewModel()
        {
            GetVersion();
        }


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

         
        /// <summary>
        /// 是否显示动态磁贴
        /// </summary>
        public bool IsLiveTileShow
        {
            get
            {
                var obj = LocalSettingsHelper.GetValueByKey(IsLiveTileShow_Key);
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    return (bool)obj;
                }
            }
            set
            {
                if(value)
                {
                    LiveTileHelper.RegisterLiveTileTask();
                    LiveTileHelper.UpdatePrimaryTile();
                }
                else
                {
                    LiveTileHelper.UnRegisterLiveTileTask();
                }
                LocalSettingsHelper.SaveValueByKey(IsLiveTileShow_Key, value);
                OnPropertyChanged();
            }
        }
        private const string IsLiveTileShow_Key = "IsLiveTileShow";


        /// <summary>
        /// 是否开启透明磁贴
        /// </summary>
        public bool IsTransparentTileOn
        {
            get
            {
                return LiveTileHelper.IsTileExists();
            }
            set
            {
                if(value)
                {
                    LiveTileHelper.PinSecondaryTile("X");
                }
                else
                {
                    LiveTileHelper.UnPinSecondaryTile();
                }
            }
        }


        private string version;
        public string Version
        {
            get
            {
                return Functions.GetVersion();
            }
            private set
            {
                version = value;
                OnPropertyChanged();
            }
        }

        public void GetVersion()
        {
            Version = Functions.GetVersion();
        }

        //public bool IsStatusBarShow
        //{
        //   get
        //    {

        //    }
        //    set
        //    {
        //        if(value)
        //        {

        //        }
        //    }
        //} 
    }
}
