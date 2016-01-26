using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 游民星空.Core.Model
{
    public class PivotData:ModelBase
    {
        private string key;
        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
                OnPropertyChanged();
            }
        }
        
        public ObservableCollection<EssayResult> Essays { get; set; }
    }
}
