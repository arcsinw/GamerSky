using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 游民星空.Core.ResultDataModel;

namespace 游民星空.Core.Model
{
    public class FindPasswordByNameResult : ResultBase
    {
        public FindPasswordByName result { get; set; }
    }
    public class FindPasswordByName
    {
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string question { get; set; }
    }

}
