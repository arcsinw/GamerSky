using GamerSky.Core.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
namespace GamerSky.Core.ViewModel
{
    public class ViewModelBase : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName]string propertyName="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //if (PropertyChanged != null)
            //{
            //    if (DispatcherManager.Current.Dispatcher == null)
            //    {
            //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            //    }
            //    else
            //    {
            //        if (DispatcherManager.Current.Dispatcher.HasThreadAccess)
            //        {
            //            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            //        }
            //        else
            //        {
            //            await DispatcherManager.Current.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
            //                delegate ()
            //                {
            //                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            //                });
            //        }
            //    }
            //}
        }
        
        public ViewModelBase()
        {
            IsDesktop = DeviceInformationHelper.IsDesktop();
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
            get { return (bool)GetValue(IsDesktopProperty); }
            set { SetValue(IsDesktopProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDesktop.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDesktopProperty =
            DependencyProperty.Register("IsDesktop", typeof(bool), typeof(ViewModelBase), new PropertyMetadata(false));

         
        //public static bool IsDesktop
        //{
        //    get
        //    {
        //        return DeviceInformationHelper.IsDesktop();
        //    }
        //} 
    }
}
