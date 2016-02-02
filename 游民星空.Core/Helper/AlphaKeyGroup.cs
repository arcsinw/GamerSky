using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Helper
{
    public class AlphaKeyGroup<T> : List<T>
    {
        /// <summary>
        /// 用来获取Key的委托
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public delegate string GetKeyDelegate(T item);

        /// <summary>
        /// 
        /// </summary>
        public string key { get; private set; }


        public AlphaKeyGroup(string key)
        {
            this.key = key;
        }

        //private static List<AlphaKeyGroup<T>> CreateGroups(SortedLocaleGrouping slg)
        //{

        //}
    }
}
