﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace GamerSky.Utils
{
    public class GlobalizationStringLoader
    {
        private static ResourceLoader loader ;
         
        static GlobalizationStringLoader()
        {
            loader = ResourceLoader.GetForCurrentView();
        }

        public static string GetString(string key)
        {
            string value = loader.GetString(key);
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }
    }

}
