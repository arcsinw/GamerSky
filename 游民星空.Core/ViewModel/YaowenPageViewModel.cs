using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.ViewModel
{
    public class YaowenPageViewModel : ViewModelBase
    {
        public ObservableCollection<Essay> Yaowens { get; set; }

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
        public YaowenPageViewModel()
        {
            apiService = new ApiService();
            Yaowens = new ObservableCollection<Essay>();

            AppTheme = DataShareManager.Current.AppTheme;
            DataShareManager.Current.ShareDataChanged += Current_ShareDataChanged;
        }

        private void Current_ShareDataChanged()
        {
            AppTheme = DataShareManager.Current.AppTheme;
        }

        private ElementTheme appTheme;
        public ElementTheme AppTheme
        {
            get
            {
                return appTheme;
            }
            set
            {
                appTheme = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadData(int pageIndex = 1)
        {
            IsActive = true;
            List<Essay> essays =  await apiService.GetYaowen(pageIndex);
            if(essays!=null)
            {
                foreach (var item in essays)
                {
                    Yaowens.Add(item);
                }
            }
            IsActive = false;
        }


        public async Task Refresh()
        {
            Yaowens.Clear();
            await LoadData();
        }
    }
}
