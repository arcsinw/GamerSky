using System;

namespace GamerSky.Controls
{
    /// <summary>
    /// Event args for Refresh Progress changed event
    /// </summary>
    public class RefreshProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets value from 0.0 to 1.0 where 1.0 is active
        /// </summary>
        public double PullProgress { get; set; }
    }
}