using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamerSky.Controls
{
    /// <summary>
    /// Event args for a SwipeStatus changing event
    /// </summary>
    public class SwipeStatusChangedEventArgs
    {
        /// <summary>
        /// Gets the old value.
        /// </summary>
        public SwipeStatus OldValue { get; internal set; }

        /// <summary>
        /// Gets the new value.
        /// </summary>
        public SwipeStatus NewValue { get; internal set; }
    }
}

