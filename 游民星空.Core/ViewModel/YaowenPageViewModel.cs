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
    public class YaowenPageViewModel : ViewModelBase
    {
        public ObservableCollection<EssayResult> Yaowens { get; set; }

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
            Yaowens = new ObservableCollection<EssayResult>();
        }


        public async Task LoadData()
        {
            IsActive = true;
            List<EssayResult> essays =  await apiService.GetYaowen();
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
