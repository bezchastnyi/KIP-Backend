namespace KIP_server_TB.Services
{
    /// <summary>
    /// Webhook controller.
    /// </summary>
#pragma warning disable SA1124 // Do not use regions
    public static class TelegramRequestNoAuthProcessing
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
    }
#pragma warning restore SA1124 // Do not use regions
}
