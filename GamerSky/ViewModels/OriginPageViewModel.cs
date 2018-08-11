using GalaSoft.MvvmLight;
using GamerSky.Models.Origin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.ViewModels
{
    public class OriginPageViewModel : ViewModelBase
    {
        public ObservableCollection<Column> AllColumns { get; set; }

        public OriginPageViewModel()
        {
            AllColumns = new ObservableCollection<Column>();
        }

        public void LoadDesignTimeData()
        {

        }

        public void LoadData()
        {

        }
    }
}
