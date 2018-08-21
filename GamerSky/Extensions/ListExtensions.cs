using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Extensions
{
    public static class ListExtensions
    {
        public static string ToParameterString(this List<int> paramater)
        {
            if (paramater.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder(paramater[0]);

            for(int i = 1; i < paramater.Count; i++)
            {
                sb.Append($",{paramater[i]}");
            }

            return sb.ToString();
        }
    }
}
