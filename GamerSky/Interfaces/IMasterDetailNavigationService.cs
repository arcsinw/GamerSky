using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Interfaces
{
    public interface IMasterDetailNavigationService : INavigationService
    {
        void MasterNavigateTo(string pageKey);

        void MasterNavigateTo(string pageKey, object parameter);

        void DetailNavigateTo(string pageKey);

        void DetailNavigateTo(string pageKey, object parameter);
    }
}
