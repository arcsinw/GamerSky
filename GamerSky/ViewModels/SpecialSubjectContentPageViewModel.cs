using GalaSoft.MvvmLight;
using GamerSky.Controls;
using GamerSky.Interfaces;
using GamerSky.Models;
using GamerSky.Services;
using GamerSky.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace GamerSky.ViewModels
{
    /// <summary>
    /// 查看全部特色专题 -> ItemClick 列表页
    /// </summary>
    public class SpecialSubjectContentPageViewModel : ViewModelBase, INavigable
    {
        private IMasterDetailNavigationService navigationService;

        public ObservableCollection<Tuple<string, List<GameDetailV4>>> Games { get; set; } = new ObservableCollection<Tuple<string, List<GameDetailV4>>>();

        public GameSpecial SelectedGameDetail { get; set; }

        public SpecialSubjectContentPageViewModel(IMasterDetailNavigationService _navigationService)
        {
            navigationService = _navigationService;
        }
          
        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            
        }

        public async void OnNavigatedTo(NavigationEventArgs e)
        {
            Games.Clear();

            if (e.Parameter is GameSpecial game)
            {
                SelectedGameDetail = game;

                //Games = new IncrementalLoadingCollection<Tuple<string, List<GameDetailV4>>>(func: LoadData,
                //    onDataLoadedAction: () => { },
                //    onDataLoadingAction: () => { },
                //    onErrorAction: (err) => { ToastService.SendToast(err.Message); });

                await LoadData(game.NodeId);
            }
        }

        private async Task LoadData(string nodeId)
        {
            var result = await ApiService.Instance.GetGameSpecialSubjectContentAsync(nodeId);

            Games = new ObservableCollection<Tuple<string, List<GameDetailV4>>>(result.OrderBy(g => g.Item1).ToList());

            RaisePropertyChanged("Games");
        }
    }
}
