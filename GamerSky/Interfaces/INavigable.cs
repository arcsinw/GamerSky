using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace GamerSky.Interfaces
{
    public interface INavigable
    {
        void OnNavigatedFrom(NavigationEventArgs e);

        void OnNavigatedTo(NavigationEventArgs e);
    }
}
