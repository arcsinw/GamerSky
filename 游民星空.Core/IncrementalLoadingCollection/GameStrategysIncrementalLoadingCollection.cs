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
    /// <summary>
    /// 可增量加载的游戏攻略集合
    /// 本质是EssayResult
    /// </summary>
    public class GameStrategysIncrementalLoadingCollection : IncrementalLoadingBase<EssayResult>
    {
        private ApiService apiService = new ApiService();

        private int specialId;
        private int pageIndex = 1;
        protected override bool HasMoreItemsOverride()
        {
            return true;
        }

        protected override async Task<IList<EssayResult>> LoadMoreItemsOverrideAsync(CancellationToken c, uint count)
        {
            List<EssayResult> strategyResults = new List<EssayResult>();
            strategyResults = await apiService.GetGameStrategys(specialId, pageIndex);
            if (strategyResults != null)
            {
                pageIndex++;
            }
            return strategyResults;
        }
    }
}
