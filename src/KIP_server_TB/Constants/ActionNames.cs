using System.Diagnostics.CodeAnalysis;

namespace KIP_server_TB.Constants
{
    /// <summary>
    /// TODO REBUILD DEPLOYMENT FILES.
    /// Action name constants.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ActionNames
    {
        /// <summary>
        /// SendMessage action.
        /// </summary>
        public const string SendMessage = "Send message";

        /// <summary>
        /// ReceiveTelegramWebhook action.
        /// </summary>
        public const string ReceiveTelegramWebhook = "Receive telegram webhook";
    }
}
