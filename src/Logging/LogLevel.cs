namespace Appalachia.Utility.Logging
{
    public enum LogLevel
    {
        /// <summary>
        /// Trace logs are written to local file logs only.  Light and fast for recording extreme detail into process internals.
        /// </summary>
        Trace = 0,
        
        /// <summary>
        /// Use for extra information that helps sort out any issues.
        /// </summary>
        Debug = 100,
        
        /// <summary>
        /// Use for normal process information like process starts and stops.
        /// </summary>
        Info = 200,
        
        /// <summary>
        /// Use whenever potentially costly or dangerous operations are occuring.
        /// </summary>
        Warn = 300,
        
        /// <summary>
        /// Use whenever something is wrong and we detected it before an exception was thrown.
        /// </summary>
        Error = 400,
        
        /// <summary>
        /// Use whenever an unexpected exception is thrown..
        /// </summary>
        Exception = 500,
        
        /// <summary>
        /// Use whenever a serious error has occurred but the application may be recoverable.
        /// </summary>
        Critical = 600,
        
        /// <summary>
        /// Use whenever we are pucked and need to close.
        /// </summary>
        Fatal = 700
    }
}
