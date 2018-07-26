using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Models.PostDataModel
{
    public class SubjectsListRequest
    {
        public int elementsPerPage = 20;
        public int pageIndex;
        /// <summary>
        /// quanBu | tuiJian
        /// </summary>
        public string type = "quanBu";
    }
}
