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
    public class GameReleaseDataSource : IIncrementalSource<Game>
    { 

        public async Task<IEnumerable<Game>> GetPagedItemsAsync(int pageIndex, int pageSize = 20, CancellationToken cancellationToken = default(CancellationToken))
        {
            List<Game> gameDetailEssays = null;
            gameDetailEssays = await ApiService.Instance.GetGameReleaseList(pageIndex, GameNodeIdEnum.PC);
            return gameDetailEssays;
        }
    }
}
