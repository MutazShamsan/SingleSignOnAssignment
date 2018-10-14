namespace SSO.Service.Logic
{
    /// <summary>
    /// Logic base class to have any common logic or properties shared in all other logic classes
    /// </summary>
    public abstract class LogicBase
    {
        /// <summary>
        /// Property to set true when catching an exception
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Property to set hold the error message
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
