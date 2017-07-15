using Arcsinx.Toolkit.Helper;
using GamerSky.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        #region Properties
        private string author;
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
                OnPropertyChanged();
            }
        }

        private string version;
        public string Version
        {
            get
            {
                return version;
            }
            set
            {
                version = value;
                OnPropertyChanged();
            }
        } 
        #endregion

        public AboutViewModel()
        {
            Version = Functions.GetVersion();
            Author = Functions.GetAuthor();
        }
    }
}
