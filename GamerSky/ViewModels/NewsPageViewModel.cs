using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GamerSky.Common;
using GamerSky.Interfaces;
using GamerSky.Models;
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
        public ObservableCollection<Tuple<Channel, List<Essay>>> Essays { get; set; }

        public Essay SelectedEssay { get; set; }

        private readonly IMasterDetailNavigationService _navigationService; 
        #endregion

        public NewsPageViewModel(IMasterDetailNavigationService navigationService)
        {
            _navigationService = navigationService;

            Essays = new ObservableCollection<Tuple<Channel, List<Essay>>>();

            ItemSelectedCommand = new RelayCommand(NavigateCommandAction);

            if (IsInDesignModeStatic)
            {
             
            }

            LoadDesignTimeData();
        }

        public void LoadDesignTimeData()
        {
            Essays.Add(new Tuple<Channel, List<Essay>>(new Channel { NodeName = "头条" }, new List<Essay>() { new Essay() { Title = "A" }}));
            Essays.Add(new Tuple<Channel, List<Essay>>(new Channel { NodeName = "新闻" }, new List<Essay>() { new Essay() { Title = "B" } }));
            Essays.Add(new Tuple<Channel, List<Essay>>(new Channel { NodeName = "游戏" }, new List<Essay>() { new Essay() { Title = "C" } }));
            Essays.Add(new Tuple<Channel, List<Essay>>(new Channel { NodeName = "影视" }, new List<Essay>() { new Essay() { Title = "D" } }));
            Essays.Add(new Tuple<Channel, List<Essay>>(new Channel { NodeName = "热点" }, new List<Essay>() { new Essay() { Title = "E" } }));
        }
        
        public RelayCommand ItemSelectedCommand { get; private set; }
        
        private void NavigateCommandAction()
        {
            _navigationService.DetailNavigateTo("WebViewPage", SelectedEssay);
        }
    }
}
