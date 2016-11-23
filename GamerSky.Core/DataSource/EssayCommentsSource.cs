using GamerSky.Core.IncrementalLoadingCollection;
using GamerSky.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using GamerSky.Core.Http;

namespace GamerSky.Core.DataSource
{
    public class EssayCommentsSource : IIncrementalSource<Comment>
    {
        public EssayCommentsSource(string contentId)
        {
            this.contentId = contentId;
        }
        public EssayCommentsSource()
        {

        }
        private string contentId { get; set; }
        private  ApiService apiService = new ApiService();
        public async Task<IEnumerable<Comment>> GetPagedItemsAsync(int pageIndex, int pageSize = 20, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await apiService.GetAllComments(contentId, pageIndex);
        }
    }
}
