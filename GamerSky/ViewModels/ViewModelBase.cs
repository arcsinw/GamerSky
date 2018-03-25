using GamerSky.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml;

namespace GamerSky.ViewModels
{
    public class ViewModelBase : DependencyObject, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(storage, value))
            {
                storage = value;
                OnPropertyChanged(propertyName);
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        public bool IsMobile => ResourceContext.GetForCurrentView().QualifierValues["DeviceFamily"].Equals("Mobile"); //DeviceInformationHelper.IsMobile;

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
        

        public virtual void Refresh() { }
    }
}
