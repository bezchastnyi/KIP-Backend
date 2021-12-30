using System;
using System.Diagnostics.CodeAnalysis;
using KIP_server_TB.Constants;
using Microsoft.Extensions.Logging;

namespace KIP_server_TB.Extensions
{
    /// <summary>
    /// Logger extensions.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "Logging")]
    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:Elements should appear in the correct order", Justification = "Logging")]
    public static class LoggerExtensions
    {
        #region Information
        #endregion

        #region Warning

        // SendMessageController
        private static readonly Action<ILogger, string, string, int, Exception> UserNotFound =
            LoggerMessage.Define<string, string, int>(
                LogLevel.Warning,
                EventIds.UserNotFoundWarningEventId,
                "Action: '{ACTION}'. User not found while sending message. " +
                "Message: '{MESSAGE}'. " +
                "User id: '{USER_ID}'.");

        /// <summary>
        /// Log when a user not found while sending message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="actionName">The action name.</param>
        /// <param name="message">The message.</param>
        /// <param name="userId">The user id.</param>
        public static void LogUserNotFound(this ILogger logger, string actionName, string message, int userId)
        {
            UserNotFound(logger, actionName, message, userId, null);
        }

        #endregion

        #region Error

        // SendMessageController
        private static readonly Action<ILogger, string, string, Exception> SendMessageUnexpectedError =
            LoggerMessage.Define<string, string>(
                LogLevel.Error,
                EventIds.SendMessageUnexpectedErrorEventId,
                "Action: '{ACTION}'. Unexpected error while sending message. " +
                "Message: '{MESSAGE}'.");

        /// <summary>
        /// Log when an unexpected error has occurred while sending message.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="actionName">The action name.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void LogSendMessageUnexpectedError(this ILogger logger, string actionName, string message, Exception exception)
        {
            SendMessageUnexpectedError(logger, actionName, message, exception);
        }

        // WebhookController
        private static readonly Action<ILogger, string, string, Exception> ReceiveWebhookUnexpectedError =
            LoggerMessage.Define<string, string>(
                LogLevel.Error,
                EventIds.ReceiveWebhookUnexpectedErrorEventId,
                "Action: '{ACTION}'. Unexpected error while receiving webhook. " +
                "Request body: '{REQUEST_BODY}'.");

        /// <summary>
        /// Log when an unexpected error has occurred while receiving webhook.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="actionName">The action name.</param>
        /// <param name="requestBody">The request body.</param>
        /// <param name="exception">The exception.</param>
        public static void LogReceiveWebhookUnexpectedError(this ILogger logger, string actionName, string requestBody, Exception exception)
        {
            ReceiveWebhookUnexpectedError(logger, actionName, requestBody, exception);
        }

        #endregion
    }
}
