using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class GameStrategysViewModel:ViewModelBase
    {
        /// <summary>
        /// 攻略
        /// </summary>
        public ObservableCollection<EssayResult> Strategys { get; set; }

        private ApiService apiService;

        private StrategyResult strategyResult;
        public GameStrategysViewModel(StrategyResult strategyResult)
        {
            apiService = new ApiService();
            Strategys = new ObservableCollection<EssayResult>();
            this.strategyResult = strategyResult;

            LoadData(strategyResult);
        }

        public async void LoadData(StrategyResult strategyResult)
        {
            List<EssayResult> results = await apiService.GetGameStrategys(strategyResult.specialID);
            foreach(var item in results)
            {
                Strategys.Add(item);
            }
        }


        public void Refresh()
        {
            Strategys.Clear();
            LoadData(strategyResult);
        }

    }
}
