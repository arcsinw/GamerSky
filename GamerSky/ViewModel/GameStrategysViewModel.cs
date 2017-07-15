using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.IncrementalLoadingCollection;
using GamerSky.Core.Model;
using Arcsinx.Toolkit.IncrementalCollection;
using GamerSky.Core.Http;
using Arcsinx.Toolkit.Controls;

namespace GamerSky.ViewModel
{
    public class GameStrategysViewModel:ViewModelBase
    {
        #region Properties
        public IncrementalLoadingCollection<Essay> Strategys { get; set; }
        
        private bool isActive = true;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
                OnPropertyChanged();
            }
        }
        
        public Strategy CurrentStrategy { get; set; }
        #endregion

        public GameStrategysViewModel()
        {
            Strategys = new IncrementalLoadingCollection<Essay>(LoadStrategys, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { ToastService.SendToast(((Exception)e).Message); IsActive = false; });
        }

        private async Task<IEnumerable<Essay>> LoadStrategys(uint count, int pageIndex)
        {
            List<Essay> results = await ApiService.Instance.GetGameStrategys(CurrentStrategy.SpecialID, pageIndex++);
            return results;
        }
          
        public override async void Refresh()
        {
            IsActive = true;

            await Strategys.ClearAndReloadAsync();
             
            IsActive = false;
        }

    }
}
