using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using GamerSky.Collection;
using GamerSky.Common;
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

namespace GamerSky.ViewModels
{
    public class NewsPageViewModel : ViewModelBase
    {
        #region fields
        public ObservableCollection<Tuple<Channel, EssayIncrementalCollection>> Essays { get; set; }

        public Essay SelectedEssay { get; set; }

        private readonly IMasterDetailNavigationService _navigationService;
        #endregion

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                RaisePropertyChanged();
            }
        }




        private async void LoadData()
        {
            List<Channel> channels = await ApiService.Instance.GetChannelList();
            if (channels != null)
            {
                foreach (var item in channels)
                {
                    var essayIncrementalCollection = new EssayIncrementalCollection(item.NodeId);
                    essayIncrementalCollection.OnDataLoading += EssayIncrementalCollection_OnDataLoading;
                    essayIncrementalCollection.OnDataLoaded += EssayIncrementalCollection_OnDataLoaded;

                    Essays.Add(new Tuple<Channel, EssayIncrementalCollection>(item, essayIncrementalCollection));
                }
            }
        }

        private void EssayIncrementalCollection_OnDataLoaded(object sender, EventArgs e)
        {
            IsActive = false;
        }

        private void EssayIncrementalCollection_OnDataLoading(object sender, EventArgs e)
        {
            IsActive = true;
        }

        public NewsPageViewModel(IMasterDetailNavigationService navigationService)
        {
            _navigationService = navigationService;

            Essays = new ObservableCollection<Tuple<Channel, EssayIncrementalCollection>>();

            ItemSelectedCommand = new RelayCommand(NavigateCommandAction);

            LoadData();
        }
         

        public RelayCommand ItemSelectedCommand { get; private set; }

        private void NavigateCommandAction()
        {
            //Messenger.Default.Send(SelectedEssay);
            
            _navigationService.DetailNavigateTo("WebViewPage", SelectedEssay);
        }
    }
}
