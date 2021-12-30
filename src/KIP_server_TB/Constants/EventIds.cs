using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace KIP_server_TB.Constants
{
    /// <summary>
    /// Event identifiers.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1124:Do not use regions", Justification = "Logging")]
    public static class EventIds
    {
        #region Information
        #endregion

        #region Warning

        /// <summary>
        /// User not found warning identifier.
        /// </summary>
        public static readonly EventId UserNotFoundWarningEventId = new EventId(3300, "USER_NOT_FOUND_WARNING"); // SendMessageController

        #endregion

        #region Error

        /// <summary>
        /// Send message error identifier.
        /// </summary>
        public static readonly EventId SendMessageUnexpectedErrorEventId = new EventId(3400, "SEND_MESSAGE_UNEXPECTED_ERROR"); // SendMessageController

        /// <summary>
        /// Receive webhook error identifier.
        /// </summary>
        public static readonly EventId ReceiveWebhookUnexpectedErrorEventId = new EventId(3401, "RECEIVE_WEBHOOK_UNEXPECTED_ERROR"); // WebhookController

        #endregion
    }
}
