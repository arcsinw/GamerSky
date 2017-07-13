using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.Http;
using GamerSky.IncrementalLoadingCollection;
using GamerSky.Model;
using Arcsinx.Toolkit.IncrementalCollection;
using Arcsinx.Toolkit.Controls;

namespace GamerSky.ViewModel
{
    public class GameStrategysViewModel:ViewModelBase
    {
        /// <summary>
        /// 攻略
        /// </summary>
        //public ObservableCollection<Essay> Strategys { get; set; }

        public IncrementalLoadingCollection<Essay> Strategys { get; set; }

        //public GameStrategysIncrementalLoadingCollection IncreStrategys { get; set; }


        #region Properties
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
        #endregion

        private Strategy strategyResult;

        public GameStrategysViewModel()
        {
            Strategys = new IncrementalLoadingCollection<Essay>(LoadStrategys, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { ToastService.SendToast(((Exception)e).Message); IsActive = false; });

            //Strategys = new ObservableCollection<Essay>();
            //IncreStrategys = new GameStrategysIncrementalLoadingCollection();
        }

        private async Task<IEnumerable<Essay>> LoadStrategys(uint count, int pageIndex)
        {
            List<Essay> results = await ApiService.Instance.GetGameStrategys(strategyResult.SpecialID, pageIndex++);
            return results;
        }

        public async Task LoadData(Strategy strategyResult,int pageIndex=1)
        {
            this.strategyResult = strategyResult;
            IsActive = true;
            List<Essay> results = await ApiService.Instance.GetGameStrategys(strategyResult.SpecialID,pageIndex);
            foreach(var item in results)
            {
                Strategys.Add(item);
                //IncreStrategys.Add(item);
            }
            IsActive = false;
        }

        /// <summary>
        /// 加载更多攻略
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task LoadMoreStrategys(int pageIndex)
        {
            await LoadData(strategyResult, pageIndex);
        }

        public override async void Refresh()
        {
            IsActive = true;

            await Strategys.ClearAndReloadAsync();
             
            IsActive = false;
        }

    }
}
