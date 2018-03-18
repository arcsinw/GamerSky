using System;
using Windows.UI.Core;

namespace GamerSky.Interfaces
{
    /// <summary>
    /// ICustomTitleBar
    /// All CustomTitlebar Should Impliment this
    /// </summary>
    public interface ICustomTitleBar
    {
        /// <summary>
        /// AppViewBackButtonVisibility
        /// </summary>
        AppViewBackButtonVisibility AppViewBackButtonVisibility { get; set; }

        /// <summary>
        /// BackRequested
        /// </summary>
        event EventHandler<BackKeyPressEventArgs> BackRequested;
    }

    /// <summary>
    /// BackKeyPressEventArgs
    /// </summary>
    public sealed class BackKeyPressEventArgs
    {
        /// <summary>
        /// Handled
        /// </summary>
        public bool Handled { get; set; }
    }
}
