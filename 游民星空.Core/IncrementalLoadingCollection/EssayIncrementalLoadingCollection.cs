using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.IncrementalLoadingCollection
{
    public class EssayIncrementalLoadingCollection : ObservableCollection<Essay>, ISupportIncrementalLoading
    {
        private ApiService apiService = new ApiService();

        private bool isBusy = false;
        private int pageIndex = 1;


        private bool hasMoreItems = true;
        public bool HasMoreItems
        {
            get
            {
                if(isBusy)
                {
                    return false;
                }
                else
                {
                    return hasMoreItems;
                }
            }
            private set
            {
                hasMoreItems = value;
            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            throw new NotImplementedException();
        }
    }
}
