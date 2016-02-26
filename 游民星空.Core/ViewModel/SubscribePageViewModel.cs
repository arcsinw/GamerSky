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
    public class SubscribePageViewModel : ViewModelBase
    {
        public ObservableCollection<Essay> SubscribeTopic { get; set; }

        public ObservableCollection<Essay> SubscribeContent { get; set; }

        private ApiService apiService;

        public SubscribePageViewModel()
        {
            apiService = new ApiService();
            SubscribeTopic = new ObservableCollection<Essay>();

            SubscribeContent = new ObservableCollection<Essay>();
            
        }

  
        /// <summary>
        /// 加载订阅专题
        /// </summary>
        public async Task LoadSubscribeTopic(string nodeIds,int pageIndex)
        {
            var essays = await apiService.GetSubscribeTopic(nodeIds, pageIndex);
        }

        /// <summary>
        /// 加载订阅内容
        /// </summary>
        public async Task LoadSubscribeContent(string sourceId,int pageIndex)
        {
            await apiService.GetSubscribeContent(sourceId, pageIndex);
        }

    }
}
