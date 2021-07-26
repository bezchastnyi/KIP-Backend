using System;
using System.Diagnostics.CodeAnalysis;
using KIP_server_Auth.Constants;
using Microsoft.Extensions.Logging;

namespace KIP_server_Auth.Extensions
{
    /// <summary>
    /// Logger extensions.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "Logging")]
    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "Logging")]
    public static class LoggerExtensions
    {
        #region Debug
        #endregion

        #region Information

        private static readonly Action<ILogger, string, string, string, Exception> DataGetSuccess =
            LoggerMessage.Define<string, string, string>(
                LogLevel.Information,
                EventIds.DataGetSuccessEventId,
                "Action: '{ACTION}'. Data has been get successfully. " +
                "Email: '{EMAIL}'. " +
                "Password: '{PASSWORD}'.");

        /// <summary>
        /// Log when an error has occurred while retreiving data from KhPI Db.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="actionName">The action name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        public static void LogDataGetSuccess(
            this ILogger logger, string actionName, string email, string password)
        {
            DataGetSuccess(logger, actionName, email, password, null);
        }

        #endregion

        #region Warning
        #endregion

        #region Error

        private static readonly Action<ILogger, string, string, string, Exception> RetrieveDataFromKhPIDbError =
            LoggerMessage.Define<string, string, string>(
                LogLevel.Error,
                EventIds.RetrieveDataFromKhPIDbErrorEventId,
                "Action: '{ACTION}'. Error while retrieving data from KhPI Db. " +
                "Email: '{EMAIL}'. " +
                "Password: '{PASSWORD}'.");

        /// <summary>
        /// Log when an error has occurred while retreiving data from KhPI Db.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="actionName">The action name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        public static void LogRetrieveDataFromKhPIDbError(
            this ILogger logger, string actionName, string email, string password)
        {
            RetrieveDataFromKhPIDbError(logger, actionName, email, password, null);
        }

        private static readonly Action<ILogger, string, string, string, Exception> MapDataError =
            LoggerMessage.Define<string, string, string>(
                LogLevel.Error,
                EventIds.MapDataErrorEventId,
                "Action: '{ACTION}'. Error while mapping data. " +
                "Email: '{EMAIL}'. " +
                "Password: '{PASSWORD}'.");

        /// <summary>
        /// Log when an error has occurred while mapping data.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="actionName">The action name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        public static void LogMapDataError(
            this ILogger logger, string actionName, string email, string password)
        {
            MapDataError(logger, actionName, email, password, null);
        }

        private static readonly Action<ILogger, string, string, string, Exception> GetDataUnexpectedError =
            LoggerMessage.Define<string, string, string>(
                LogLevel.Error,
                EventIds.GetDataUnexpectedErrorEventId,
                "Action: '{ACTION}'. Unexpected error while getting data. " +
                "Email: '{EMAIL}'. " +
                "Password: '{PASSWORD}'.");

        /// <summary>
        /// Log when an unexpected error has occurred while mapping data.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="actionName">The action name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="exception">The exception.</param>
        public static void LogGetDataUnexpectedError(
            this ILogger logger, string actionName, string email, string password, Exception exception)
        {
            GetDataUnexpectedError(logger, actionName, email, password, exception);
        }

        private static readonly Action<ILogger, string, Exception> JsonDeserializeUnexpectedError =
            LoggerMessage.Define<string>(
                LogLevel.Error,
                EventIds.JsonDeserializeUnexpectedErrorEventId,
                "Unexpected error while deserializing json data. " +
                "Url: '{URL}'.");

        /// <summary>
        /// Log when an unexpected error has occurred while deserializing json data.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="url">The url.</param>
        /// <param name="exception">The exception.</param>
        public static void LogJsonDeserializeUnexpectedError(this ILogger logger, string url, Exception exception)
        {
            JsonDeserializeUnexpectedError(logger, url, exception);
        }

        #endregion
    }
}
