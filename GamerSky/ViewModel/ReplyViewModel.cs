using GamerSky.IncrementalLoadingCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.ViewModel
{
    public class ReplyViewModel : ViewModelBase
    {
        #region Properties
        private CommentReplyIncrementalCollection _comments;
        public CommentReplyIncrementalCollection Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
                OnPropertyChanged();
            }
        } 
        #endregion


        public ReplyViewModel()
        {
            Comments = new CommentReplyIncrementalCollection();
        }
    }
}
