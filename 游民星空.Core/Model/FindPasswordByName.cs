using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    public class FindPasswordByName : ResultBase
    {
        public FindPasswordByNameResult result { get; set; }
    }
    public class FindPasswordByNameResult
    {
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string question { get; set; }
    }

}
