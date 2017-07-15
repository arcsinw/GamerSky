using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GamerSky.Helper;
using GamerSky.Core.Http;
using GamerSky.Core.Model;
using GamerSky.IncrementalLoadingCollection;
using Arcsinx.Toolkit.IncrementalCollection;
using Arcsinx.Toolkit.Controls;
using GamerSky.Core.Enums;

namespace GamerSky.ViewModel
{
    public class StrategyPageViewModel:ViewModelBase
    {
        #region Properties
        /// <summary>
        /// 关注攻略
        /// </summary>
        public ObservableCollection<Strategy> FocusStrategys { get; set; }

        /// <summary>
        /// 所有攻略
        /// </summary>
        public ObservableCollection<AlphaKeyGroup<Strategy>> AllStrategys { get; set; }

        /// <summary>
        /// 游戏库中游戏列表
        /// </summary>
        //public GameIncrementalCollection Games { get; set; }

        public IncrementalLoadingCollection<Game> Games { get; set; }

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

        #endregion

        public StrategyPageViewModel()
        {

            FocusStrategys = new ObservableCollection<Strategy>();

            AllStrategys = new ObservableCollection<AlphaKeyGroup<Strategy>>();

            Games = new IncrementalLoadingCollection<Game>(LoadGame, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { ToastService.SendToast(((Exception)e).Message); IsActive = false; });

            //Games = new GameIncrementalCollection();
        }

        private async Task<IEnumerable<Game>> LoadGame(uint count, int pageIndex)
        {
            List<Game> games = await ApiService.Instance.GetGameList(pageIndex++);
            return games;
        }

        /// <summary>
        /// 加载关注攻略
        /// </summary>
        public async Task LoadFocusStrategys()
        {
            await ApiService.Instance.GetGameList(1);
            IsActive = true;
            List<Strategy> strategys = await ApiService.Instance.GetStrategys();
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
            List<Strategy> strategys = await ApiService.Instance.GetAllStrategys();
            if (strategys != null)
            {
                //按拼音分组
                List<AlphaKeyGroup<Strategy>> groupData = AlphaKeyGroup<Strategy>.CreateGroups(
                    strategys, (Strategy s) => s.Title, true);

                foreach (var item in groupData)
                {
                    AllStrategys.Add(item);
                }
            }
            IsActive = false;
        }

        /// <summary>
        /// 加载游戏库中游戏列表
        /// </summary>
        /// <returns></returns>
        public async Task LoadGameList(int pageIndex)
        {
            IsActive = true;
            var gameList = await ApiService.Instance.GetGameList(pageIndex);
            if(gameList!= null)
            {
                foreach (var item in gameList)
                {
                    Games.Add(item);
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

        /// <summary>
        /// 刷新游戏库游戏列表
        /// </summary>
        public async void RefreshGameList()
        {
            IsActive = true;
            await Games.ClearAndReloadAsync();
            //Games.Clear();
            //GameIncrementalCollection g = new GameIncrementalCollection();
            //Games = g;
            //g.OnDataLoaded += OnDataLoaded;
            //g.OnDataLoading += OnDataLoading;
            IsActive = false;
        }

        private void OnDataLoading(object sender, EventArgs e)
        {
            IsActive = true;
        }

        private void OnDataLoaded(object sender, EventArgs e)
        {
            IsActive = false;
        }

        public async void Subscribe(Strategy strategy)
        {
            VerificationCode code = await ApiService.Instance.EditSubscribe(SubscribeOperateEnum.add, strategy.SpecialID.ToString());

            //本地存储订阅列表
            //DataShareManager.Current.UpdateSubscribe(strategy);
        }  
    }
}
