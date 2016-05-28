using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.Model
{
    public class SemanticCollection<T> :ModelBase where T : class 
    {
        private string _Key;
        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                _Key = value;
                OnPropertyChanged();
            }

        }
        private List<T> _RouterStatus;
        public List<T> RouterStatus
        {
            get
            {
                return _RouterStatus;
            }
            set
            {
                _RouterStatus = value;
                OnPropertyChanged();
            }
        }
    }
}
