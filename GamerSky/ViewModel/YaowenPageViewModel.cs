using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using GamerSky.IncrementalLoadingCollection;
using Arcsinx.Toolkit.IncrementalCollection;
using GamerSky.Core.Model;
using GamerSky.Core.Http;

namespace GamerSky.ViewModel
{
    public class YaowenPageViewModel : ViewModelBase
    {
        #region Properties
        public IncrementalLoadingCollection<Essay> Yaowens { get; set; }
        
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

        private bool isEmpty;

        public bool IsEmpty
        {
            get { return isEmpty; }
            set { isEmpty = value; OnPropertyChanged(); }
        } 
        #endregion

        public YaowenPageViewModel()
        {
            Yaowens = new IncrementalLoadingCollection<Essay>(LoadYaowenAsync, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { IsActive = false; });
        }
        
        private async Task<IEnumerable<Essay>> LoadYaowenAsync(uint count, int pageIndex)
        {
            var result = await ApiService.Instance.GetYaowen(pageIndex++);
            if (result != null && result.Count != 0)
            {
                return result;
            }
            else if (result == null || result.Count == 0)
            {
                Yaowens.NoMore();

                if (Yaowens.Count == 0)
                {
                    IsEmpty = true;
                }
                else
                {
                    IsEmpty = false;
                }
            }

            return null;
        }
        
        public override async void Refresh()
        {
            IsActive = true;

            await Yaowens.ClearAndReloadAsync();
            
            IsActive = false; 
        }
    }
}
