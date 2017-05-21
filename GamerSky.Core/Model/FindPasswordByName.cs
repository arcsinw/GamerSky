using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamerSky.Core.ResultModel;

namespace GamerSky.Core.Model
{
    public class FindPasswordByNameResult : ResultModelBase
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
