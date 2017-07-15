using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    public class KeyedList<TKey,TItem>:List<TItem>
    {
        public TKey Key { private set; get; }

        public IEnumerable<TItem> InternalList { private set; get; }

        public KeyedList(TKey key,IEnumerable<TItem> items) : base(items)
        {
            Key = key;
            InternalList = items;
        }

        public KeyedList(IGrouping<TKey,TItem> grouping) : base(grouping)
        {
            Key = grouping.Key;
            InternalList = grouping;
        }

    }
}
