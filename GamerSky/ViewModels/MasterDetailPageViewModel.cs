using GalaSoft.MvvmLight;
using GamerSky.Common;
using GamerSky.Interfaces;
using GamerSky.Models;
using GamerSky.Utils;
using GamerSky.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.ViewModels
{
    public class MasterDetailPageViewModel : ViewModelBase
    {
        private readonly IMasterDetailNavigationService _navigationService;

        public MasterDetailPageViewModel(IMasterDetailNavigationService navigationService)
        {
            _navigationService = navigationService;
            ItemSelectedCommand = new RelayCommand(NavigateCommandAction);
        }

        #region Properties
        public List<NavMenuItem> Menus { get; set; } = new List<NavMenuItem>()
        {
            new NavMenuItem() { Icon = "ms-appx:///Assets/Images/icon_xinwen.png", Title = GlobalizationStringLoader.GetString("News"), DestPage = typeof(NewsPage) },
            new NavMenuItem() { Icon = "ms-appx:///Assets/Images/icon_youxi.png", Title = GlobalizationStringLoader.GetString("Game"), DestPage = typeof(GamePage) },
            new NavMenuItem() { Icon = "ms-appx:///Assets/Images/ic_main_quanzi_default.png", Title = GlobalizationStringLoader.GetString("Group"), DestPage = typeof(GroupPage) },
            //new NavMenuItem() { Icon = "ms-appx:///Assets/Images/icon_yuanchuang.png", Title = GlobalizationStringLoader.GetString("Original"), DestPage = typeof(OriginalPage) },
            new NavMenuItem() { Icon = "ms-appx:///Assets/Images/icon_yonghu.png", Title = GlobalizationStringLoader.GetString("Me"), DestPage = typeof(MyPage) }
        };

        public NavMenuItem SelectedMenu { get; set; }

        public RelayCommand ItemSelectedCommand { get; private set; } 
        #endregion

        private void NavigateCommandAction()
        {
            _navigationService.MasterNavigateTo("MainPage", SelectedMenu);
        }
    }
}
