namespace GamerSky.Interfaces
{
    /// <summary>
    /// IBackKeyPressManager
    /// All Root Page Should implement this
    /// </summary>
    public interface IBackKeyPressManager
    {
        /// <summary>
        /// UnRegisterBackKeyPress
        /// </summary>
        void UnRegisterBackKeyPress();


        /// <summary>
        /// RegisterBackKeyPress
        /// </summary>
        void RegisterBackKeyPress();
    }
}
