using Arcsinx.Toolkit.Helper;
using GamerSky.Helper;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace GamerSky.ViewModel
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

        public bool IsMobile => DeviceInformationHelper.IsMobile;

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
        

        public void GoBack() => NavigationHelper.GoBack();

        public virtual void Refresh() { }
    }
}
