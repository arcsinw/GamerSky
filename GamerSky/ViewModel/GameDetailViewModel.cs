using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Arcsinx.Toolkit.IncrementalCollection;
using Arcsinx.Toolkit.Controls;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using GamerSky.View;
using GamerSky.Core.Model;
using GamerSky.Core.Http;

namespace GamerSky.ViewModel
{
    public class GameDetailViewModel : ViewModelBase
    {
        #region Properties
        private GameDetail gameDetail;
        public GameDetail GameDetail
        {
            get
            {
                return gameDetail;
            }
            set
            {
                gameDetail = value;
                OnPropertyChanged();
            }
        }

        public IncrementalLoadingCollection<Essay> News { get; set; }

        public IncrementalLoadingCollection<Essay> Strategys { get; set; }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        #endregion

        public string contentId { get; set; } = "353478";

        public GameDetailViewModel()
        { 
            GameDetail = new GameDetail();

            News = new IncrementalLoadingCollection<Essay>(LoadNews, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { ToastService.SendToast(((Exception)e).Message); });
            Strategys = new IncrementalLoadingCollection<Essay>(LoadNews, () => { IsActive = false; }, () => { IsActive = true; }, (e) => { ToastService.SendToast(((Exception)e).Message); });
            
            if(IsDesignMode)
            { 
                LoadGameDetail(); 
            }
        }

        #region Load and refresh methods
        private async Task<IEnumerable<Essay>> LoadNews(uint count, int pageIndex)
        {
            var result = await ApiService.Instance.GetGameDetailNews(contentId, pageIndex++);
            Debug.WriteLine(pageIndex);
            return result;
        }

        private async Task<IEnumerable<Essay>> LoadStrategys(uint count, int pageIndex)
        {
            var result = await ApiService.Instance.GetGameDetailStrategys(contentId, pageIndex++);
            Debug.WriteLine(pageIndex);
            return result;
        }
        
        public async void LoadGameDetail()
        {
            GameDetail = await ApiService.Instance.GetGameDetail(contentId);
        }

        public async void RefreshGameNews()
        {
            await News.ClearAndReloadAsync();
        }

        public async void RefreshStrategys()
        {
            await Strategys.ClearAndReloadAsync();
        } 
        #endregion

        #region ItemClick' event
        public void strategyListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var essayResult = e.ClickedItem as Essay;

            if (!essayResult.ContentType.Equals("zhuanti"))
            {
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(ReadEssayPage), essayResult);
            }
            else
            {
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(SubscribeContentPage), essayResult.ContentId);
            }
        }

        public void newsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var essayResult = e.ClickedItem as Essay;

            if (!essayResult.ContentType.Equals("zhuanti"))
            {
                //MasterDetailPage.Current.DetailFrame.Navigate(typeof(EssayDetailPage), essayResult);
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(ReadEssayPage), essayResult);
            }
            else
            {
                MasterDetailPage.Current.DetailFrame.Navigate(typeof(SubscribeContentPage), essayResult.ContentId);
            }
        } 
        #endregion
    }
}
