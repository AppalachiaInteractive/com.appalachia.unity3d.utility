using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Appalachia.Utility.Logging
{
    public static class AppaLog
    {
        /// <summary>
        ///     <para>Clears errors from the developer console.</para>
        /// </summary>
        public static void ClearDeveloperConsole()
        {
            Debug.ClearDeveloperConsole();
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        public static void Error(object message)
        {
            Debug.unityLogger.Log(LogType.Error, message);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void Error(object message, Object context)
        {
            Debug.unityLogger.Log(LogType.Error, message, context);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="logIf">Whether or not to log.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void ErrorIf(bool logIf, object message, Object context)
        {
            if (!logIf)
            {
                return;
            }

            Debug.unityLogger.Log(LogType.Error, message, context);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="logIf">Whether or not to log.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        public static void ErrorIf(bool logIf, object message)
        {
            if (!logIf)
            {
                return;
            }

            Debug.unityLogger.Log(LogType.Error, message);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="exception">Runtime Exception.</param>
        public static void Exception(Exception exception)
        {
            Debug.unityLogger.LogException(exception, null);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="exception">Runtime Exception.</param>
        public static void Exception(Exception exception, Object context)
        {
            Debug.unityLogger.LogException(exception, context);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="logIf">Whether or not to log.</param>
        /// <param name="exception">Runtime Exception.</param>
        public static void ExceptionIf(bool logIf, Exception exception)
        {
            if (!logIf)
            {
                return;
            }

            Debug.unityLogger.LogException(exception, null);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="logIf">Whether or not to log.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="exception">Runtime Exception.</param>
        public static void ExceptionIf(bool logIf, Exception exception, Object context)
        {
            if (!logIf)
            {
                return;
            }

            Debug.unityLogger.LogException(exception, context);
        }

        /// <summary>
        ///     <para>Logs a message to the Unity Console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        public static void Log(object message)
        {
            Debug.unityLogger.Log(LogType.Log, message);
        }

        /// <summary>
        ///     <para>Logs a message to the Unity Console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void Log(object message, Object context)
        {
            Debug.unityLogger.Log(LogType.Log, message, context);
        }

        /// <summary>
        ///     <para>Logs a message to the Unity Console.</para>
        /// </summary>
        /// <param name="logIf">Whether or not to log.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        public static void If(bool logIf, object message)
        {
            if (!logIf)
            {
                return;
            }

            Debug.unityLogger.Log(LogType.Log, message);
        }

        /// <summary>
        ///     <para>Logs a message to the Unity Console.</para>
        /// </summary>
        /// <param name="logIf">Whether or not to log.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void If(bool logIf, object message, Object context)
        {
            if (!logIf)
            {
                return;
            }

            Debug.unityLogger.Log(LogType.Log, message, context);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void LogError(this Object context, object message)
        {
            Debug.unityLogger.Log(LogType.Error, message, context);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="logIf">Whether or not to log.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void LogErrorIf(this Object context, bool logIf, object message)
        {
            if (!logIf)
            {
                return;
            }

            Debug.unityLogger.Log(LogType.Error, message, context);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="exception">Runtime Exception.</param>
        public static void LogException(this Object context, Exception exception)
        {
            Debug.unityLogger.LogException(exception, context);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs an error message to the console.</para>
        /// </summary>
        /// <param name="logIf">Whether or not to log.</param>
        /// <param name="context">Object to which the message applies.</param>
        /// <param name="exception">Runtime Exception.</param>
        public static void LogExceptionIf(this Object context, bool logIf, Exception exception)
        {
            if (!logIf)
            {
                return;
            }

            Debug.unityLogger.LogException(exception, context);
        }

        /// <summary>
        ///     <para>Logs a message to the Unity Console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void Log(this Object context, object message)
        {
            Debug.unityLogger.Log(LogType.Log, message, context);
        }

        /// <summary>
        ///     <para>Logs a message to the Unity Console.</para>
        /// </summary>
        /// <param name="logIf">Whether or not to log.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void LogIf(this Object context, bool logIf, object message)
        {
            if (!logIf)
            {
                return;
            }

            Debug.unityLogger.Log(LogType.Log, message, context);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void LogWarning(this Object context, object message)
        {
            Debug.unityLogger.Log(LogType.Warning, message, context);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs a warning message to the console.</para>
        /// </summary>
        /// <param name="logIf">Whether or not to log.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void LogWarningIf(this Object context, bool logIf, object message)
        {
            if (!logIf)
            {
                return;
            }

            Debug.unityLogger.Log(LogType.Warning, message, context);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        public static void Warning(object message)
        {
            Debug.unityLogger.Log(LogType.Warning, message);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs a warning message to the console.</para>
        /// </summary>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void Warning(object message, Object context)
        {
            Debug.unityLogger.Log(LogType.Warning, message, context);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs a warning message to the console.</para>
        /// </summary>
        /// <param name="logIf">Whether or not to log.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        public static void WarningIf(bool logIf, object message)
        {
            if (!logIf)
            {
                return;
            }

            Debug.unityLogger.Log(LogType.Warning, message);
        }

        /// <summary>
        ///     <para>A variant of Debug.Log that logs a warning message to the console.</para>
        /// </summary>
        /// <param name="logIf">Whether or not to log.</param>
        /// <param name="message">String or object to be converted to string representation for display.</param>
        /// <param name="context">Object to which the message applies.</param>
        public static void WarningIf(bool logIf, object message, Object context)
        {
            if (!logIf)
            {
                return;
            }

            Debug.unityLogger.Log(LogType.Warning, message, context);
        }
    }
}
