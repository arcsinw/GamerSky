using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GamerSky.Core.Helper;

namespace GamerSky.Core.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected async void OnPropertyChanged([CallerMemberName]string propertyName="")
        {
            if (PropertyChanged != null)
            {
                if (DispatcherManager.Current.Dispatcher == null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
                else
                {
                    if (DispatcherManager.Current.Dispatcher.HasThreadAccess)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                    }
                    else
                    {
                        await DispatcherManager.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                            delegate ()
                            {
                                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                            });
                    }
                }
            }
        }
        
        
        ///<Summary>
        /// Indicate wether is in design mode
        /// </Summary> 
        public static bool IsDesignMode
        {
            get
            {
                return Windows.ApplicationModel.DesignMode.DesignModeEnabled;
            }
        }

        public bool IsDesktop
        {
            get
            {
                return DeviceInformationHelper.IsDesktop();
            }
        }

    }
}
