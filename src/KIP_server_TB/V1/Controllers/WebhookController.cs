// <copyright file="WebhookController.cs" company="KIP">
// Copyright (c) KIP. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using KIP_Backend.Attributes;
using KIP_Backend.Extensions;
using KIP_POST_APP.DB;
using KIP_POST_APP.Models.KIP.Helpers;
using KIP_server_TB.DB;
using KIP_server_TB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace KIP_server_TB.V1.Controllers
{
    /// <summary>
    /// Webhook controller.
    /// </summary>
    /// <seealso cref="Controller" />
    [V1]
    [ApiRoute]
    [ApiController]
    public class WebhookController : Controller
    {
        private readonly ILogger<WebhookController> _logger;
        private readonly TelegramDbContext _telegramDbContext;
        private readonly PostDbContext _postDbContext;
        private readonly ITelegramBotClient _telegramBotClient;

        private readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));

        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="telegramDbContext">The telegram db context.</param>
        /// <param name="postDbContext">The POST db context.</param>
        /// <param name="telegramBotClient">The telegramBotClient.</param>
        public WebhookController(
            ILogger<WebhookController> logger,
            TelegramDbContext telegramDbContext,
            PostDbContext postDbContext,
            ITelegramBotClient telegramBotClient)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._telegramDbContext = telegramDbContext ?? throw new ArgumentNullException(nameof(telegramDbContext));
            this._postDbContext = postDbContext ?? throw new ArgumentNullException(nameof(postDbContext));
            this._telegramBotClient = telegramBotClient ?? throw new ArgumentNullException(nameof(telegramBotClient));
        }

        /// <summary>
        /// Current rank of student's group.
        /// </summary>
        /// <returns>Start message.</returns>
        [HttpPost]
        [Route("receive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1123:Do not place regions within elements", Justification = "Webhook receive")]
        public async Task<IActionResult> ReceiveAsync()
        {
            WebhookRequest request;

            try
            {
                #region Identification

                using (var reader = new StreamReader(this.Request.Body))
                {
                    request = this.jsonParser.Parse<WebhookRequest>(reader);
                }

                var message = request.QueryResult.QueryText;
                var intent = request.QueryResult.Intent.DisplayName;

                if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
                {
                    // log
                    return this.Ok();
                }

                double userId = 0;

                try
                {
                    userId = GetUserIdFromButton(request);
                }
                catch
                {
                    // log
                    userId = GetUserIdFromMessage(request);
                }

                if (userId == 0)
                {
                    // log
                    return this.Ok();
                }

                #endregion

                #region FacultyOutput

                if (intent == "No Auth Mode Intent")
                {
                    var faculties = this._postDbContext.Faculty.AsNoTracking().ToList();

                    var inlineButtons = new List<List<InlineKeyboardButton>>();

                    foreach (var f in faculties)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData(f.FacultyShortName, f.FacultyShortName),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    string chatId = null;

                    try
                    {
                        chatId = GetChatIdFromButton(request);
                    }
                    catch
                    {
                        // log
                    }

                    if (string.IsNullOrEmpty(chatId))
                    {
                        // log
                        return this.Ok();
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть свій факультет", replyMarkup: inlineKeyboard);

                    return this.Ok();
                }
                #endregion

                #region CourseOutput

                if (intent == "Faculty Intent")
                {
                    #region FacultyRegistration

                    if (string.IsNullOrEmpty(message))
                    {
                        // log
                        return this.Ok();
                    }

                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);

                    if (user == null)
                    {
                        string userName = null;

                        try
                        {
                            userName = GetUserNameFromButton(request);
                        }
                        catch (Exception ex)
                        {
                            // log
                            Console.WriteLine(ex.Message);
                        }

                        var newUser = new TelegramUser()
                        {
                            UserId = (int)userId,
                            UserName = userName,
                            Faculty = message,
                        };

                        await this._telegramDbContext.Users.AddAsync(newUser);
                        await this._telegramDbContext.SaveChangesAsync();
                    }
                    else
                    {
                        user.Faculty = message;
                        await this._telegramDbContext.SaveChangesAsync();
                    }

                    #endregion

                    var inlineButtons = new List<List<InlineKeyboardButton>>();

                    for (var i = 1; i <= 6; i++)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData(i.ToString(), i.ToString()),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    string chatId = null;

                    try
                    {
                        chatId = GetChatIdFromButton(request);
                    }
                    catch
                    {
                        // log
                    }

                    if (string.IsNullOrEmpty(chatId))
                    {
                        // log
                        return this.Ok();
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть свій курс", replyMarkup: inlineKeyboard);

                    return this.Ok();
                }
                #endregion

                #region GroupOutput

                if (intent == "Course Intent")
                {
                    #region CourseRegistration

                    if (string.IsNullOrEmpty(message))
                    {
                        // log
                        return this.Ok();
                    }

                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);

                    if (user == null)
                    {
                        // log
                        return this.Ok();
                    }
                    else
                    {
                        user.Course = ConvertExtensions.StringToInt(message);
                        await this._telegramDbContext.SaveChangesAsync();
                    }

                    #endregion

                    var userFaculty = this._postDbContext.Faculty.FirstOrDefault(f => f.FacultyShortName == user.Faculty);
                    var groups = this._postDbContext.Group.Where(g => g.Course == user.Course && g.Faculty == userFaculty);

                    var inlineButtons = new List<List<InlineKeyboardButton>>();

                    foreach (var g in groups)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData(g.GroupName, g.GroupName),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    string chatId = null;

                    try
                    {
                        chatId = GetChatIdFromButton(request);
                    }
                    catch
                    {
                        // log
                    }

                    if (string.IsNullOrEmpty(chatId))
                    {
                        // log
                        return this.Ok();
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть свою групу", replyMarkup: inlineKeyboard);

                    return this.Ok();
                }
                #endregion

                #region GroupRegistration

                if (intent == "Course Intent - fallback")
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        // log
                        return this.Ok();
                    }

                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);

                    if (user == null)
                    {
                        // log
                        return this.Ok();
                    }
                    else
                    {
                        user.Group = message;
                        await this._telegramDbContext.SaveChangesAsync();
                    }

                    var outputSb = new StringBuilder();

                    outputSb.AppendLine("Ваш профіль");
                    outputSb.AppendLine($"Факультет: {user.Faculty}");
                    outputSb.AppendLine($"Курс: {user.Course}");
                    outputSb.AppendLine($"Група: {user.Group}");

                    string chatId = null;
                    try
                    {
                        chatId = GetChatIdFromButton(request);
                    }
                    catch
                    {
                        // log
                    }

                    if (string.IsNullOrEmpty(chatId))
                    {
                        // log
                        return this.Ok();
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());

                    return this.Ok();
                }

                #endregion

                #region GroupSchedule

                if (intent == "Group Schedule Intent - fallback")
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        // log
                        return this.Ok();
                    }

                    var day = ConvertExtensions.StringToInt(message);
                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);

                    var groupId = this._postDbContext.Group.FirstOrDefault(i => i.GroupName == user.Group).GroupID;
                    var list = this._postDbContext.StudentSchedule.Where(i => i.GroupID == groupId && i.Day == (Day)day).AsNoTracking().ToList();

                    if (list.Count == 0)
                    {
                        return this.NotFound();
                    }

                    var outputSb = new StringBuilder();

                    var week0List = list.Where(i => i.Week == Week.UnPaired).OrderBy(i => i.Number);
                    var week1List = list.Where(i => i.Week == Week.Paired).OrderBy(i => i.Number);

                    outputSb.AppendLine("Непарный тиждень:");
                    foreach (var l in week0List)
                    {
                        outputSb.AppendLine($"{l.Number + 1}. {l.SubjectName} ({l.ProfName}) [{l.AudienceName}]");
                    }

                    outputSb.AppendLine("\nПарный тиждень:");
                    foreach (var l in week1List)
                    {
                        outputSb.AppendLine($"{l.Number + 1}. {l.SubjectName} ({l.ProfName}) [{l.AudienceName}]");
                    }

                    string chatId = null;
                    try
                    {
                        chatId = GetChatIdFromButton(request);
                    }
                    catch
                    {
                        // log
                    }

                    if (string.IsNullOrEmpty(chatId))
                    {
                        // log
                        return this.Ok();
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());

                    return this.Ok();
                }

                #endregion

                return this.Ok();

                /*

                #region ProfsOutput

                if (existingUser != null && message.Contains("ProfSchedule"))
                {
                    var messageContent = message.Split(":");
                    var day = ConvertExtensions.StringToInt(messageContent[1]);

                    existingUser.TempDayValue = day;
                    await this._telegramDbContext.SaveChangesAsync();

                    var userFaculty = this._postDbContext.Faculty.FirstOrDefault(f => f.FacultyShortName == existingUser.Faculty);
                    var cathedras = this._postDbContext.Cathedra.Where(c => c.FacultyID == userFaculty.FacultyID).AsNoTracking().ToList();

                    var outputSb = new StringBuilder();
                    outputSb.AppendLine("Оберіть викладача\n");

                    foreach (var c in cathedras)
                    {
                        outputSb.AppendLine($"Кафедра [{c.CathedraName}]\n");

                        var profs = this._postDbContext.Prof.Where(p => p.CathedraID == c.CathedraID).AsNoTracking().ToList();

                        var output = string.Join("\n", profs.Select(p => $"{p.ProfSurname} {p.ProfName} {p.ProfPatronymic}"));
                        outputSb.Append(output + "\n\n");
                    }

                    // log
                    var response = new WebhookResponse
                    {
                        FulfillmentText = outputSb.ToString(),
                    };

                    return this.Json(response);
                }

                #endregion

                #region AudiencesOutput

                if (existingUser != null && message.Contains("AudienceSchedule"))
                {
                    var messageContent = message.Split(":");
                    var day = ConvertExtensions.StringToInt(messageContent[1]);

                    existingUser.TempDayValue = day;
                    await this._telegramDbContext.SaveChangesAsync();

                    var buildings = this._postDbContext.Building.AsNoTracking().ToList();

                    var outputSb = new StringBuilder();
                    outputSb.AppendLine("Оберіть аудиторію\n");

                    foreach (var b in buildings)
                    {
                        outputSb.AppendLine($"Корпус [{b.BuildingName} ({b.BuildingShortName})]\n");

                        var audiences = this._postDbContext.Audience.Where(a => a.BuildingID == b.BuildingID).AsNoTracking().ToList();

                        if (audiences.Count != 0)
                        {
                            var output = string.Join("\n", audiences.Select(p => $"{p.AudienceName}"));
                            outputSb.Append(output + "\n\n");
                        }
                    }

                    // log
                    var response = new WebhookResponse
                    {
                        FulfillmentText = outputSb.ToString(),
                    };

                    return this.Json(response);
                }

                #endregion

                #region ProfSchedule

                var userFaculty1 = this._postDbContext.Faculty.FirstOrDefault(f => f.FacultyShortName == existingUser.Faculty);
                var cathedras1 = this._postDbContext.Cathedra.Where(c => c.FacultyID == userFaculty1.FacultyID).AsNoTracking().ToList();

                foreach (var c in cathedras1)
                {
                    var profs = this._postDbContext.Prof.Where(p => p.CathedraID == c.CathedraID).AsNoTracking().ToList();

                    var prof = profs.FirstOrDefault(p => message.Contains(p.ProfSurname));

                    if (prof == null)
                    {
                        continue;
                    }

                    if (existingUser.TempDayValue == null)
                    {
                        // log
                        return this.Ok();
                    }

                    var list = this._postDbContext.ProfSchedule.Where(i => i.ProfID == prof.ProfID && i.Day == (Day)existingUser.TempDayValue).AsNoTracking().ToList();

                    if (list.Count == 0)
                    {
                        return this.NotFound();
                    }

                    var outputSb = new StringBuilder();

                    var week0List = list.Where(i => i.Week == Week.UnPaired).OrderBy(i => i.Number);
                    var week1List = list.Where(i => i.Week == Week.Paired).OrderBy(i => i.Number);

                    outputSb.AppendLine("Непарный тиждень:");
                    foreach (var l in week0List)
                    {
                        outputSb.AppendLine($"{l.Number + 1}. {l.SubjectName} ({string.Join(",", l.GroupID.Select(p => $"{p.Value}"))} [{l.AudienceName}]");
                    }

                    outputSb.AppendLine("\nПарный тиждень:");
                    foreach (var l in week1List)
                    {
                        outputSb.AppendLine($"{l.Number + 1}. {l.SubjectName} ({string.Join(",", l.GroupID.Select(p => $"{p.Value}"))} [{l.AudienceName}]");
                    }

                    existingUser.TempDayValue = null;

                    // log
                    var response = new WebhookResponse()
                    {
                        FulfillmentText = outputSb.ToString(),
                    };

                    return this.Json(response);
                }

                #endregion

                #region AudienceSchedule
                #endregion

                // log
                return this.Ok();*/
            }
            catch (Exception ex)
            {
                // log
                Console.WriteLine(ex.Message);
                return this.Ok();
            }
        }

        private static double GetUserIdFromButton(WebhookRequest request)
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

        private static string GetUserNameFromButton(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["callback_query"].StructValue
                .Fields["from"].StructValue
                .Fields["username"].StringValue;
        }

        private static string GetChatIdFromButton(WebhookRequest request)
        {
            return request.OriginalDetectIntentRequest.Payload
                .Fields["data"].StructValue
                .Fields["callback_query"].StructValue
                .Fields["message"].StructValue
                .Fields["chat"].StructValue
                .Fields["id"].StringValue;
        }
    }
}
