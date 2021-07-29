using Google.Cloud.Dialogflow.V2;

namespace KIP_server_TB.Services
{
    /// <summary>
    /// Webhook controller.
    /// </summary>
#pragma warning disable SA1124 // Do not use regions
    public static class TelegramRequestProcessing
    {
        /*
        /// <summary>
        /// Webhook controller.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="jsonParser">The jsonParser.</param>
        /// <returns>ChatId.</returns>
        public static (bool result, string errorMessage, WebhookRequest telegramRequest) RequestIdentification(HttpRequest request, JsonParser jsonParser)
        {
            WebhookRequest telegramRequest;

            using (var reader = new StreamReader(request.Body))
            {
                telegramRequest = jsonParser.Parse<WebhookRequest>(reader);
            }

            var message = telegramRequest.QueryResult.QueryText;
            var intent = telegramRequest.QueryResult.Intent.DisplayName;

            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
            {
                // log
                return this.Ok();
            }

            double userId = 0;

            try
            {
                userId = GetUserIdFromInlineButton(telegramRequest);
            }
            catch
            {
                // log
                userId = GetUserIdFromMessage(telegramRequest);
            }

            if (userId == 0)
            {
                // log
                return this.Ok();
            }
        }
        */

        #region GetUserInfo

        /// <summary>
        /// Webhook controller.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ChatId.</returns>
        public static double GetUserIdFromInlineButton(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["callback_query"].StructValue
                .Fields["from"].StructValue
                .Fields["id"].NumberValue;
        }

        /// <summary>
        /// Webhook controller.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ChatId.</returns>
        public static double GetUserIdFromMessage(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["from"].StructValue
                .Fields["id"].NumberValue;
        }

        /// <summary>
        /// Webhook controller.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ChatId.</returns>
        public static string GetUserNameFromInlineButton(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["callback_query"].StructValue
                .Fields["from"].StructValue
                .Fields["username"].StringValue;
        }

        /// <summary>
        /// Webhook controller.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ChatId.</returns>
        public static string GetChatIdFromInlineButton(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["callback_query"].StructValue
                .Fields["message"].StructValue
                .Fields["chat"].StructValue
                .Fields["id"].StringValue;
        }

        /// <summary>
        /// Webhook controller.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ChatId.</returns>
        public static string GetChatIdFromKeyboard(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["chat"].StructValue
                .Fields["id"].StringValue;
        }

        #endregion
    }
#pragma warning restore SA1124 // Do not use regions
}
