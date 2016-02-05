using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using 游民星空.Core.Http;
using 游民星空.Core.Model;

namespace 游民星空.Core.IncrementalLoadingCollection
{
    public class EssayIncrementalLoadingCollection : IncrementalLoadingBase<EssayResult>
    {
        private ApiService apiService = new ApiService();

        private int pageIndex = 1;
        protected override bool HasMoreItemsOverride()
        {
            return true;
        }

        protected override async Task<IList<EssayResult>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            int nodeId = 0;
            List<EssayResult> essays = await apiService.GetEssayList(nodeId, pageIndex);
            return essays;
        }
    }
}
