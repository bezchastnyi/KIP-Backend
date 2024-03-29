﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using KIP_server_TB.Constants;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace KIP_server_TB.Services
{
    /// <summary>
    /// TelegramRequestProcessing.
    /// </summary>
#pragma warning disable SA1124 // Do not use regions
    public static class TelegramRequestProcessing
    {
        /// <summary>
        /// Webhook controller.
        /// </summary>
        /// <param name="bot">The request.</param>
        /// <param name="chatId">The request.</param>
        /// <param name="intentFlag">The intentFlag.</param>
        /// <returns>ChatId.</returns>
        public static async Task OutputDaysButtons(ITelegramBotClient bot, string chatId, string intentFlag)
        {
            var inlineButtons = new List<List<InlineKeyboardButton>>();
            foreach (var d in KIPTelegramConstants.DayUkrConstants)
            {
                inlineButtons.Add(new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(d.Value, $"{intentFlag}:{(int)d.Key}"),
                });
            }

            var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

            await bot.SendTextMessageAsync(chatId, "Оберіть день", replyMarkup: inlineKeyboard);
        }

        #region GetUserInfo

        /// <summary>
        /// Webhook controller.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ChatId.</returns>
        public static double? GetUserId(WebhookRequest request)
        {
            double? userId = null;
            try
            {
                userId = GetUserIdFromInlineButton(request);
            }
            catch
            {
                userId = GetUserIdFromMessage(request);
            }

            return userId;
        }

        /// <summary>
        /// Webhook controller.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ChatId.</returns>
        public static string GetChatId(WebhookRequest request)
        {
            string chatId = null;
            try
            {
                chatId = GetChatIdFromInlineButton(request);
            }
            catch
            {
                chatId = GetChatIdFromKeyboard(request);
            }

            return chatId;
        }

        /// <summary>
        /// Webhook controller.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>ChatId.</returns>
        public static string GetUserName(WebhookRequest request)
        {
            string userName = null;
            try
            {
                userName = GetUserNameFromInlineButton(request);
            }
            catch
            {
                return null;
            }

            return userName;
        }

        private static string GetUserNameFromInlineButton(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["callback_query"].StructValue
                .Fields["from"].StructValue
                .Fields["username"].StringValue;
        }

        private static string GetChatIdFromInlineButton(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["callback_query"].StructValue
                .Fields["message"].StructValue
                .Fields["chat"].StructValue
                .Fields["id"].StringValue;
        }

        private static string GetChatIdFromKeyboard(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["chat"].StructValue
                .Fields["id"].StringValue;
        }

        private static double GetUserIdFromInlineButton(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["callback_query"].StructValue
                .Fields["from"].StructValue
                .Fields["id"].NumberValue;
        }

        private static double GetUserIdFromMessage(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["from"].StructValue
                .Fields["id"].NumberValue;
        }

        #endregion
    }
#pragma warning restore SA1124 // Do not use regions
}
