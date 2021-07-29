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
using KIP_server_TB.Constants;
using KIP_server_TB.DB;
using KIP_server_TB.Models;
using KIP_server_TB.Services;
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
                    userId = TelegramRequestProcessing.GetUserIdFromInlineButton(request);
                }
                catch
                {
                    // log
                    userId = TelegramRequestProcessing.GetUserIdFromMessage(request);
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
                    var faculties = this._postDbContext.Faculty.OrderBy(f => f.FacultyShortName).AsNoTracking().ToList();

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
                        chatId = TelegramRequestProcessing.GetChatIdFromInlineButton(request);
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
                            userName = TelegramRequestProcessing.GetUserNameFromInlineButton(request);
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

                    for (var i = 1; i <= KIPTelegramConstants.MaxCourse; i++)
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
                        chatId = TelegramRequestProcessing.GetChatIdFromInlineButton(request);
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
                    var groups = this._postDbContext.Group.Where(g => g.Course == user.Course && g.Faculty == userFaculty)
                        .OrderBy(g => g.GroupName);

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
                        chatId = TelegramRequestProcessing.GetChatIdFromInlineButton(request);
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

                    var inlineButtons = new List<List<InlineKeyboardButton>>
                    {
                        new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData("Вірно ✅", "ScheduleIntent"),
                            InlineKeyboardButton.WithCallbackData("Невірно ❌", "Гостьовий режим"),
                        },
                    };

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    string chatId = null;
                    try
                    {
                        chatId = TelegramRequestProcessing.GetChatIdFromInlineButton(request);
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

                    await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString(), replyMarkup: inlineKeyboard);

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

                    string chatId = null;
                    try
                    {
                        chatId = TelegramRequestProcessing.GetChatIdFromInlineButton(request);
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

                    var day = ConvertExtensions.StringToInt(message);
                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);

                    var group = this._postDbContext.Group.FirstOrDefault(i => i.GroupName == user.Group);
                    var list = this._postDbContext.StudentSchedule.Where(i => i.GroupID == group.GroupID && i.Day == Enum.Parse<Day>(message)).AsNoTracking().ToList();

                    if (list.Count == 0)
                    {
                        // log
                        await this._telegramBotClient.SendTextMessageAsync(chatId, $"Вибачте, але в базі даних немає розкладу для вашої групи {group.GroupName}");
                        return this.Ok();
                    }

                    var outputSb = new StringBuilder();

                    var week0List = list.Where(i => i.Week == Week.UnPaired).OrderBy(i => i.Number);
                    var week1List = list.Where(i => i.Week == Week.Paired).OrderBy(i => i.Number);

                    outputSb.AppendLine($"{group.GroupName}\n");
                    outputSb.AppendLine("Непарный тиждень:\n");
                    foreach (var l in week0List)
                    {
                        outputSb.AppendLine($"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                        outputSb.AppendLine($"  Викладач: {l.ProfName}");
                        outputSb.AppendLine($"  Аудиторія: {l.AudienceName}\n");
                    }

                    outputSb.AppendLine("\nПарный тиждень:\n");
                    foreach (var l in week1List)
                    {
                        outputSb.AppendLine($"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                        outputSb.AppendLine($"  Викладач: {l.ProfName}");
                        outputSb.AppendLine($"  Аудиторія: {l.AudienceName}\n");
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());

                    return this.Ok();
                }

                #endregion

                #region CathedrasOutput

                if (intent == "Prof Schedule Intent")
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        // log
                        return this.Ok();
                    }

                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);

                    var userFaculty = this._postDbContext.Faculty.FirstOrDefault(f => f.FacultyShortName == user.Faculty);
                    var cathedras = this._postDbContext.Cathedra.Where(c => c.FacultyID == userFaculty.FacultyID).OrderBy(c => c.CathedraName).AsNoTracking().ToList();

                    var inlineButtons = new List<List<InlineKeyboardButton>>();

                    foreach (var c in cathedras)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData(c.CathedraName, c.CathedraID.ToString()),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    string chatId = null;
                    try
                    {
                        chatId = TelegramRequestProcessing.GetChatIdFromInlineButton(request);
                    }
                    catch
                    {
                        // log
                        chatId = TelegramRequestProcessing.GetChatIdFromKeyboard(request);
                    }

                    if (string.IsNullOrEmpty(chatId))
                    {
                        // log
                        return this.Ok();
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть кафедру", replyMarkup: inlineKeyboard);

                    return this.Ok();
                }

                #endregion

                #region ProfsOutput

                if (intent == "Prof Schedule Intent - Cathedra")
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        // log
                        return this.Ok();
                    }

                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);

                    var cathedra = this._postDbContext.Cathedra.FirstOrDefault(c => c.CathedraID == ConvertExtensions.StringToInt(message));
                    var profs = this._postDbContext.Prof.Where(p => p.CathedraID == cathedra.CathedraID).OrderBy(p => p.ProfSurname).AsNoTracking().ToList();

                    var inlineButtons = new List<List<InlineKeyboardButton>>();

                    foreach (var p in profs)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData($"{p.ProfSurname} {p.ProfName} {p.ProfPatronymic}", p.ProfSurname),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    string chatId = null;
                    try
                    {
                        chatId = TelegramRequestProcessing.GetChatIdFromInlineButton(request);
                    }
                    catch
                    {
                        // log
                        chatId = TelegramRequestProcessing.GetChatIdFromKeyboard(request);
                    }

                    if (string.IsNullOrEmpty(chatId))
                    {
                        // log
                        return this.Ok();
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть викладача", replyMarkup: inlineKeyboard);

                    return this.Ok();
                }

                #endregion

                #region DayOutput

                if (intent == "Prof Schedule Intent - Prof")
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        // log
                        return this.Ok();
                    }

                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);
                    var prof = this._postDbContext.Prof.FirstOrDefault(p => p.ProfSurname == message);

                    user.TempProfValue = prof.ProfID;
                    await this._telegramDbContext.SaveChangesAsync();

                    var inlineButtons = new List<List<InlineKeyboardButton>>();

                    foreach (var d in KIPTelegramConstants.DayUkrConstants)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(d.Value, d.Key.ToString()),
                    });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    string chatId = null;
                    try
                    {
                        chatId = TelegramRequestProcessing.GetChatIdFromInlineButton(request);
                    }
                    catch
                    {
                        // log
                        chatId = TelegramRequestProcessing.GetChatIdFromKeyboard(request);
                    }

                    if (string.IsNullOrEmpty(chatId))
                    {
                        // log
                        return this.Ok();
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть день", replyMarkup: inlineKeyboard);

                    return this.Ok();
                }

                #endregion

                #region ProfSchedule

                if (intent == "Prof Schedule Intent - Day")
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        // log
                        return this.Ok();
                    }

                    string chatId = null;
                    try
                    {
                        chatId = TelegramRequestProcessing.GetChatIdFromInlineButton(request);
                    }
                    catch
                    {
                        // log
                        chatId = TelegramRequestProcessing.GetChatIdFromKeyboard(request);
                    }

                    if (string.IsNullOrEmpty(chatId))
                    {
                        // log
                        return this.Ok();
                    }

                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);

                    var prof = this._postDbContext.Prof.FirstOrDefault(i => i.ProfID == user.TempProfValue);
                    var list = this._postDbContext.ProfSchedule.Where(i => i.ProfID == user.TempProfValue && i.Day == Enum.Parse<Day>(message)).AsNoTracking().ToList();

                    if (list.Count == 0)
                    {
                        // log
                        await this._telegramBotClient.SendTextMessageAsync(chatId, $"Вибачте, але в базі даних немає розкладу для {prof.ProfSurname}");
                        return this.Ok();
                    }

                    var outputSb = new StringBuilder();

                    var week0List = list.Where(i => i.Week == Week.UnPaired).OrderBy(i => i.Number);
                    var week1List = list.Where(i => i.Week == Week.Paired).OrderBy(i => i.Number);

                    outputSb.AppendLine($"{prof.ProfSurname} {prof.ProfName} {prof.ProfPatronymic}\n");
                    outputSb.AppendLine("Непарный тиждень:\n");
                    foreach (var l in week0List)
                    {
                        outputSb.AppendLine($"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                        outputSb.AppendLine($"  Група: {string.Join(",", l.GroupNames)}");
                        outputSb.AppendLine($"  Аудиторія: {l.AudienceName}\n");
                    }

                    outputSb.AppendLine("\nПарный тиждень:\n");
                    foreach (var l in week1List)
                    {
                        outputSb.AppendLine($"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                        outputSb.AppendLine($"  Група: {string.Join(",", l.GroupNames)}");
                        outputSb.AppendLine($"  Аудиторія: {l.AudienceName}\n");
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());

                    return this.Ok();
                }

                #endregion

                #region BuildingsOutput

                if (intent == "Audience Schedule Intent")
                {
                    var buildings = this._postDbContext.Building.OrderBy(b => b.BuildingShortName).AsNoTracking().ToList();

                    var inlineButtons = new List<List<InlineKeyboardButton>>();
                    foreach (var b in buildings)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData($"{b.BuildingName} ({b.BuildingShortName})", b.BuildingID.ToString()),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    string chatId = null;
                    try
                    {
                        chatId = TelegramRequestProcessing.GetChatIdFromInlineButton(request);
                    }
                    catch
                    {
                        // log
                        chatId = TelegramRequestProcessing.GetChatIdFromKeyboard(request);
                    }

                    if (string.IsNullOrEmpty(chatId))
                    {
                        // log
                        return this.Ok();
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть навчальний корпус", replyMarkup: inlineKeyboard);

                    return this.Ok();
                }

                #endregion

                #region AudiencesOutput

                if (intent == "Audience Schedule Intent - Building")
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        // log
                        return this.Ok();
                    }

                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);
                    user.TempBuildingValue = ConvertExtensions.StringToInt(message);
                    await this._telegramDbContext.SaveChangesAsync();

                    var audiences = this._postDbContext.Audience.Where(a => a.BuildingID == user.TempBuildingValue).OrderBy(a => a.AudienceName)
                        .AsNoTracking().ToList();

                    var inlineButtons = new List<List<InlineKeyboardButton>>();
                    foreach (var a in audiences)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                        {
                            InlineKeyboardButton.WithCallbackData($"{a.AudienceName}", $"Audience:{a.AudienceName}"),
                        });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    string chatId = null;
                    try
                    {
                        chatId = TelegramRequestProcessing.GetChatIdFromInlineButton(request);
                    }
                    catch
                    {
                        // log
                        chatId = TelegramRequestProcessing.GetChatIdFromKeyboard(request);
                    }

                    if (string.IsNullOrEmpty(chatId))
                    {
                        // log
                        return this.Ok();
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть навчальну аудиторію", replyMarkup: inlineKeyboard);

                    return this.Ok();
                }

                #endregion

                #region DayOutput

                if (intent == "Audience Schedule Intent - Audience")
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        // log
                        return this.Ok();
                    }

                    var arr = message.Split(':');
                    message = arr[1];

                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);

                    var audience = this._postDbContext.Audience.FirstOrDefault(a => a.AudienceName.Contains(message));
                    user.TempAudienceValue = audience.AudienceID;
                    await this._telegramDbContext.SaveChangesAsync();

                    var inlineButtons = new List<List<InlineKeyboardButton>>();

                    foreach (var d in KIPTelegramConstants.DayUkrConstants)
                    {
                        inlineButtons.Add(new List<InlineKeyboardButton>
                    {
                        InlineKeyboardButton.WithCallbackData(d.Value, d.Key.ToString()),
                    });
                    }

                    var inlineKeyboard = new InlineKeyboardMarkup(inlineButtons);

                    string chatId = null;
                    try
                    {
                        chatId = TelegramRequestProcessing.GetChatIdFromInlineButton(request);
                    }
                    catch
                    {
                        // log
                        chatId = TelegramRequestProcessing.GetChatIdFromKeyboard(request);
                    }

                    if (string.IsNullOrEmpty(chatId))
                    {
                        // log
                        return this.Ok();
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, "Оберіть день", replyMarkup: inlineKeyboard);

                    return this.Ok();
                }

                #endregion

                #region AudienceSchedule

                if (intent == "Audience Schedule Intent - Day")
                {
                    if (string.IsNullOrEmpty(message))
                    {
                        // log
                        return this.Ok();
                    }

                    string chatId = null;
                    try
                    {
                        chatId = TelegramRequestProcessing.GetChatIdFromInlineButton(request);
                    }
                    catch
                    {
                        // log
                        chatId = TelegramRequestProcessing.GetChatIdFromKeyboard(request);
                    }

                    if (string.IsNullOrEmpty(chatId))
                    {
                        // log
                        return this.Ok();
                    }

                    var user = this._telegramDbContext.Users.FirstOrDefault(u => u.UserId == userId);

                    var audience = this._postDbContext.Audience.FirstOrDefault(a => a.AudienceID == user.TempAudienceValue);
                    var list = this._postDbContext.AudienceSchedule.Where(i => i.AudienceID == user.TempAudienceValue && i.Day == Enum.Parse<Day>(message)).AsNoTracking().ToList();

                    if (list.Count == 0)
                    {
                        // log
                        await this._telegramBotClient.SendTextMessageAsync(chatId, $"Вибачте, але в базі даних немає розкладу для {audience.AudienceName}");
                        return this.Ok();
                    }

                    var outputSb = new StringBuilder();

                    var week0List = list.Where(i => i.Week == Week.UnPaired).OrderBy(i => i.Number);
                    var week1List = list.Where(i => i.Week == Week.Paired).OrderBy(i => i.Number);

                    outputSb.AppendLine($"{audience.AudienceName}\n");
                    outputSb.AppendLine("Непарный тиждень:\n");
                    foreach (var l in week0List)
                    {
                        outputSb.AppendLine($"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                        outputSb.AppendLine($"  Група: {string.Join(",", l.GroupNames)}");
                        outputSb.AppendLine($"  Викладач: {l.ProfName}\n");
                    }

                    outputSb.AppendLine("\nПарный тиждень:\n");
                    foreach (var l in week1List)
                    {
                        outputSb.AppendLine($"{KIPTelegramConstants.TimeOfLessonConstants.GetValueOrDefault(l.Number)} {l.SubjectName}");
                        outputSb.AppendLine($"  Група: {string.Join(",", l.GroupNames)}");
                        outputSb.AppendLine($"  Викладач: {l.ProfName}\n");
                    }

                    await this._telegramBotClient.SendTextMessageAsync(chatId, outputSb.ToString());

                    return this.Ok();
                }

                #endregion

                // log
                return this.Ok();
            }
            catch (Exception ex)
            {
                // log
                Console.WriteLine(ex.Message);
                return this.Ok();
            }
        }
    }
}
