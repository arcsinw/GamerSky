using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Requests
{
    public class GameReviewRequest
    {
        public int elementsCountPerPage = 20;

        public string extraField1;

        public string extraField2;

        public int pageIndex;

        public string type;
    }
}
