using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace GamerSky.Helper
{
    public class DispatcherManager
    {
        private CoreDispatcher dispatcher;
        public CoreDispatcher Dispatcher
        {
            get
            {
                return dispatcher;
            }
            set
            {
                dispatcher = value;
            }
        }

        private static DispatcherManager current;
        public static DispatcherManager Current
        {
            get
            {
                if(current== null)
                {
                    current = new DispatcherManager();
                }
                return current;
            }
        }
    }
}
