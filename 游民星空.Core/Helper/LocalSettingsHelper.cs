using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace 游民星空.Core.Helper
{
    public class LocalSettingsHelper
    {
        static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
    }
}
