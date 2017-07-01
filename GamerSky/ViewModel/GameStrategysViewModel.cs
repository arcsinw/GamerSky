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

namespace GamerSky.ViewModel
{
    public class GameStrategysViewModel:ViewModelBase
    {
        /// <summary>
        /// 攻略
        /// </summary>
        public ObservableCollection<Essay> Strategys { get; set; }

       //public GameStrategysIncrementalLoadingCollection IncreStrategys { get; set; }
        

        /// <summary>
        /// ProgressRing IsActive
        /// </summary>
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

        private Strategy strategyResult;

        public GameStrategysViewModel()
        { 
            Strategys = new ObservableCollection<Essay>();
            //IncreStrategys = new GameStrategysIncrementalLoadingCollection();
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

        public async Task Refresh()
        {
            IsActive = true;
            Strategys.Clear();
            //IncreStrategys.Clear();
            await LoadData(strategyResult);
            IsActive = false;
        }

    }
}
