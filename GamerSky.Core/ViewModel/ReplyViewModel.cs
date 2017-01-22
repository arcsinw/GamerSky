using GamerSky.Core.IncrementalLoadingCollection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Core.ViewModel
{
    public class ReplyViewModel : ViewModelBase
    {
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


        public ReplyViewModel()
        {
            Comments = new CommentReplyIncrementalCollection();
        }
    }
}
