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
        public ObservableCollection<Strategy> FocusStrategys { get; set; }

        /// <summary>
        /// 所有攻略
        /// </summary>
        public ObservableCollection<AlphaKeyGroup<Strategy>> AllStrategys { get; set; }

        private ApiService apiService;

        private bool isActive;
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

        public StrategyPageViewModel()
        {
            apiService = new ApiService();

            FocusStrategys = new ObservableCollection<Strategy>();

            AllStrategys = new ObservableCollection<AlphaKeyGroup<Strategy>>();

        }

        /// <summary>
        /// 加载关注攻略
        /// </summary>
        public async Task LoadFocusStrategys()
        {
            IsActive = true;
            List<Strategy> strategys = await apiService.GetStrategys();
            if (strategys != null)
            {
                foreach (var item in strategys)
                {
                    FocusStrategys.Add(item);
                }
            }
            IsActive = false;
        }

        /// <summary>
        /// 加载所有攻略
        /// </summary>
        public async Task LoadAllStrategys()
        {
            IsActive = true;
            List<Strategy> strategys = await apiService.GetAllStrategys();
            if (strategys != null)
            {
                //按拼音分组
                List<AlphaKeyGroup<Strategy>> groupData = AlphaKeyGroup<Strategy>.CreateGroups(
                    strategys, (Strategy s) => s.title, true);

                foreach (var item in groupData)
                {
                    AllStrategys.Add(item);
                }
            }
            IsActive = false;
        }

        /// <summary>
        /// 刷新关注攻略
        /// </summary>
        public async Task RefreshFocusStrategy()
        {
            IsActive = true;
            FocusStrategys.Clear();
            await LoadFocusStrategys();
            IsActive = false;
        }

        /// <summary>
        /// 刷新所有攻略
        /// </summary>
        public async Task RefreshAllStrategy()
        {
            IsActive = true;
            AllStrategys.Clear();
            await LoadAllStrategys();
            IsActive = false;
        }
    }
}
