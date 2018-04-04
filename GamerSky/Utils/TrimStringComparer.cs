using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Utils
{
    public class TrimStringComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return string.Equals(x.Trim(), y.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(string obj)
        {
            throw new NotImplementedException();
        }
    }
}
