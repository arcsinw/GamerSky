using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.Helper;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class StrategyPageViewModel:ViewModelBase
    {
        /// <summary>
        /// 关注攻略
        /// </summary>
        public ObservableCollection<StrategyResult> FocusStrategys { get; set; }

        /// <summary>
        /// 所有攻略
        /// </summary>
        public ObservableCollection<AlphaKeyGroup<StrategyResult>> AllStrategys { get; set; }

        private ApiService apiService;

        public StrategyPageViewModel()
        {
            apiService = new ApiService();

            FocusStrategys = new ObservableCollection<StrategyResult>();

            AllStrategys = new ObservableCollection<AlphaKeyGroup<StrategyResult>>();

        }

        /// <summary>
        /// 加载关注攻略
        /// </summary>
        public async void LoadFocusStrategys()
        {
            List<StrategyResult> strategys = await apiService.GetStrategys();
            if (strategys != null)
            {
                foreach (var item in strategys)
                {
                    FocusStrategys.Add(item);
                }
            }
        }

        /// <summary>
        /// 加载所有攻略
        /// </summary>
        public async void LoadAllStrategys()
        {
            List<StrategyResult> strategys = await apiService.GetAllStrategys();
            if (strategys != null)
            {
                

                //按拼音分组
                List<AlphaKeyGroup<StrategyResult>> groupData = AlphaKeyGroup<StrategyResult>.CreateGroups(
                    strategys, (StrategyResult s) => s.title, true);

                foreach (var item in groupData)
                {
                    AllStrategys.Add(item);
                }
            }
        }

        /// <summary>
        /// 刷新关注攻略
        /// </summary>
        public void RefreshFocusStrategy()
        {
            FocusStrategys.Clear();
            LoadFocusStrategys();
        }

        /// <summary>
        /// 刷新所有攻略
        /// </summary>
        public void RefreshAllStrategy()
        {
            AllStrategys.Clear();
            LoadAllStrategys();
        }
    }
}
